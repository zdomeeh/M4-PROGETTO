using UnityEngine;
using UnityEngine.Events;

public class RigidbodyCharacter : MonoBehaviour
{
    [SerializeField] private float Speed = 5f;
    [SerializeField] private float JumpHeight = 2f;
    [SerializeField] private float GroundDistance = 0.2f;
    [SerializeField] private LayerMask Ground;

    [SerializeField] private float RotationSpeed = 12f;

    [SerializeField] private float InputSmoothTime = 0.1f;

    [SerializeField] private Transform _groundChecker;
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private PlayerAudio _playerAudio;
    [SerializeField] private Animator _animator;

    private Rigidbody _body;
    private Vector3 _smoothedMoveInput;
    private Vector3 _moveVelocity;
    private bool _isGrounded;

    public UnityEvent<float> OnUpdateHorizontalSpeed;
    public UnityEvent<bool> OnIsGrounded;
    public UnityEvent OnJump;

    void Start()
    {
        // Inizializzazione del Rigidbody
        _body = GetComponent<Rigidbody>();
        _body.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        _body.interpolation = RigidbodyInterpolation.Interpolate;

        // Disabilita root motion per l'Animator
        _animator.applyRootMotion = false;

        // Se non è assegnato il ground checker, usa il primo figlio
        if (_groundChecker == null)
            _groundChecker = transform.GetChild(0);
    }

    void Update()
    {
        // Controllo se il personaggio è a terra
        bool wasGrounded = _isGrounded;
        _isGrounded = Physics.CheckSphere(
            _groundChecker.position,
            GroundDistance,
            Ground,
            QueryTriggerInteraction.Ignore
        );

        // Se lo stato cambia, invia l'evento
        if (wasGrounded != _isGrounded)
            OnIsGrounded.Invoke(_isGrounded);

        // Lettura input raw della tastiera per rotazione
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Calcolo direzione rispetto alla telecamera
        Vector3 camForward = _cameraPivot.forward;
        Vector3 camRight = _cameraPivot.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 rawInput = Vector3.zero;
        if (Mathf.Abs(v) > 0.1f)
            rawInput += camForward * Mathf.Sign(v);
        if (Mathf.Abs(h) > 0.1f)
            rawInput += camRight * Mathf.Sign(h);
        rawInput = rawInput.normalized;

        // Smoothing dell'input
        // Se eravamo fermi e c'è input, forziamo subito l'input per uscire da Idle
        if (_smoothedMoveInput.magnitude < 0.01f && rawInput.magnitude > 0.01f)
        {
            _smoothedMoveInput = rawInput; // forziamo l'input immediatamente
        }
        else
        {
            _smoothedMoveInput = Vector3.SmoothDamp(
                _smoothedMoveInput,
                rawInput,
                ref _moveVelocity,
                InputSmoothTime
            );
        }

        // Imposta a zero input molto piccoli per evitare problemi con il Blend Tree
        if (_smoothedMoveInput.magnitude < 0.01f)
            _smoothedMoveInput = Vector3.zero;

        // Rotazione del personaggio verso la direzione dell'input
        if (rawInput.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(rawInput);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                RotationSpeed * Time.deltaTime
            );
        }

        // Aggiornamento parametri dell'Animator
        Vector3 localMove = transform.InverseTransformDirection(_smoothedMoveInput);
        _animator.SetFloat("MoveX", Mathf.Abs(localMove.x) < 0.01f ? 0f : localMove.x, 0.1f, Time.deltaTime);
        _animator.SetFloat("MoveZ", Mathf.Abs(localMove.z) < 0.01f ? 0f : localMove.z, 0.1f, Time.deltaTime);
        _animator.SetBool("IsGrounded", _isGrounded);

        // Gestione salto
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            // Imposta la velocità verticale per saltare
            _body.velocity = new Vector3(
                _body.velocity.x,
                Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y),
                _body.velocity.z
            );

            // Trigger animazione salto
            _animator.SetTrigger("Jump");

            // Riproduce audio salto se presente
            if (_playerAudio != null)
                _playerAudio.PlayJump();

            // Invoca evento di salto
            OnJump.Invoke();
        }

        // Aggiorna evento con velocità orizzontale
        OnUpdateHorizontalSpeed.Invoke(_smoothedMoveInput.sqrMagnitude);
    }

    void FixedUpdate()
    {
        // Applica la velocità calcolata al Rigidbody
        Vector3 velocity = _smoothedMoveInput * Speed;
        velocity.y = _body.velocity.y; // mantiene la componente verticale

        _body.velocity = velocity;

        // Blocca rotazioni indesiderate generate dalla fisica
        _body.angularVelocity = Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        // Disegna una sfera per visualizzare il ground checker
        if (_groundChecker == null) return;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_groundChecker.position, GroundDistance);
    }
}