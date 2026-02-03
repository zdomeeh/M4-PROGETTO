using UnityEngine;
using UnityEngine.Events;

public class RigidbodyCharacter : MonoBehaviour
{
    [SerializeField] private float Speed = 5f;
    [SerializeField] private float JumpHeight = 2f;
    [SerializeField] private float GroundDistance = 0.2f;
    [SerializeField] private LayerMask Ground;
    [SerializeField] private Transform _groundChecker;

    private Rigidbody _body;
    private Vector3 _inputs = Vector3.zero;
    private bool _isGrounded = true;

    public UnityEvent<float> OnUpdateHorizontalSpeed;
    public UnityEvent<bool> OnIsGrounded;
    public UnityEvent OnJump;

    void Start()
    {
        // Prendo il Rigidbody una volta sola
        _body = GetComponent<Rigidbody>();

        // Se non è stato assegnato, uso il primo figlio come GroundChecker
        if (_groundChecker == null)
            _groundChecker = transform.GetChild(0);
    }

    void Update()
    {
        // Memorizzo lo stato precedente per verificare se cambia
        bool wasGrounded = _isGrounded;

        // Controllo se il player è a terra usando una sfera sotto i piedi
        _isGrounded = Physics.CheckSphere(
            _groundChecker.position,
            GroundDistance,
            Ground,
            QueryTriggerInteraction.Ignore
        );

        // Se il grounding è cambiato, invoco l'evento corrispondente
        if (wasGrounded != _isGrounded)
        {
            OnIsGrounded.Invoke(_isGrounded);
        }

        // Leggo input WASD / frecce
        Vector2 moveInput = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        );

        // Normalizzo l'input se supera 1 (es. diagonale)
        float sqrtLength = moveInput.sqrMagnitude;
        if (sqrtLength > 1)
            moveInput /= Mathf.Sqrt(sqrtLength);

        // Salvo input in _inputs (usato nel FixedUpdate)
        _inputs = Vector3.zero;
        _inputs.x = moveInput.x;
        _inputs.z = moveInput.y;

        // Ruoto il player verso la direzione di camminata
        if (_inputs != Vector3.zero)
            transform.forward = _inputs;

        // Aggiorno evento della velocità orizzontale (utile per animazioni)
        OnUpdateHorizontalSpeed.Invoke(_inputs.sqrMagnitude);

        // Gestione salto
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            // Evento per collegare animazioni o suoni
            OnJump.Invoke();

            // Calcolo la forza necessaria per il salto e la applico al Rigidbody
            _body.AddForce(
                Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y),
                ForceMode.VelocityChange
            );
        }
    }

    void FixedUpdate()
    {
        // Movimento fisico usando Rigidbody.MovePosition
        _body.MovePosition(
            _body.position + _inputs * Speed * Time.fixedDeltaTime
        );
    }

    private void OnDrawGizmos()
    {
        // Disegna una sfera blu sotto i piedi per vedere il ground check
        if (_groundChecker != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(_groundChecker.position, GroundDistance);
        }
    }
}