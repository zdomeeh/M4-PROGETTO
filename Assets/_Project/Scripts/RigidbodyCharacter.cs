using UnityEngine;
using UnityEngine.Events;

public class RigidbodyCharacter : MonoBehaviour
{
    [SerializeField] private float Speed = 5f;
    [SerializeField] private float JumpHeight = 2f;
    [SerializeField] private float GroundDistance = 0.2f;
    [SerializeField] private LayerMask Ground;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private Transform _cameraTransform;

    private Rigidbody _body;
    private Vector3 _inputs = Vector3.zero;
    private bool _isGrounded = true;

    public UnityEvent<float> OnUpdateHorizontalSpeed;
    public UnityEvent<bool> OnIsGrounded;
    public UnityEvent OnJump;

    void Start()
    {
        // Recupera il Rigidbody una sola volta
        _body = GetComponent<Rigidbody>();

        // Se non assegnato, usa il primo figlio come GroundChecker
        if (_groundChecker == null)
            _groundChecker = transform.GetChild(0);
    }

    void Update()
    {
        // Salva lo stato precedente del grounding
        bool wasGrounded = _isGrounded;

        // Controlla se il player è a terra con una sfera sotto i piedi
        _isGrounded = Physics.CheckSphere(
            _groundChecker.position,
            GroundDistance,
            Ground,
            QueryTriggerInteraction.Ignore
        );

        // Se cambia lo stato di grounding, notifica l’evento
        if (wasGrounded != _isGrounded)
            OnIsGrounded.Invoke(_isGrounded);

        // Legge input da tastiera
        Vector2 moveInput = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        );

        // Normalizza l’input per evitare velocità maggiore in diagonale
        float sqrtLength = moveInput.sqrMagnitude;
        if (sqrtLength > 1)
            moveInput /= Mathf.Sqrt(sqrtLength);

        // Direzioni della camera sul piano orizzontale
        Vector3 cameraForward = _cameraTransform.forward;
        Vector3 cameraRight = _cameraTransform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward.Normalize();
        cameraRight.Normalize();

        // Movimento relativo alla camera
        _inputs = cameraForward * moveInput.y + cameraRight * moveInput.x;

        // Ruota il player verso la direzione di movimento
        if (_inputs != Vector3.zero)
            transform.forward = _inputs;

        // Aggiorna evento per animazioni
        OnUpdateHorizontalSpeed.Invoke(_inputs.sqrMagnitude);

        // Gestione salto
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            // Evento per animazioni / suoni
            OnJump.Invoke();

            // Applica una forza istantanea verso l’alto
            _body.AddForce(
                Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y),
                ForceMode.VelocityChange
            );
        }
    }

    void FixedUpdate()
    {
        // Movimento fisico del player
        _body.MovePosition(
            _body.position + _inputs * Speed * Time.fixedDeltaTime
        );
    }

    private void OnDrawGizmos()
    {
        // Disegna la sfera del ground check per debug
        if (_groundChecker != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(_groundChecker.position, GroundDistance);
        }
    }
}