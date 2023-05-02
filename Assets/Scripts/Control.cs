
using UnityEngine;
using UnityEngine.Events;

public class Control : MonoBehaviour
{
    [SerializeField] bool isAndroid;

    [SerializeField] string finishTag;
    [SerializeField] string enemyTag;

    [SerializeField] Transform groundCheckTransform;
    [SerializeField] LayerMask groundLayer;

    [Header("Sounds")]
    [SerializeField] AudioClip gameOverSound;
    [SerializeField] AudioClip jumpSound;

    [Header("Values")]
    public float Speed;
    public float JumpForce;

    private AudioSource _audio;
    private Rigidbody2D _rb;
    private VariableJoystick _js;
    private CapsuleCollider2D _collider;
    private SpriteRenderer _spriteRenderer;
    private float _radiusGrCheck = 0.3f;

    public enum State
    {
        Stay,
        Move,
        inJump
    }
    public State _state
    {
        get {
            if (_isGrounded)
                return _movementVector != Vector2.zero ? State.Move : State.Stay;
            else
                return State.inJump;
        }
    }
    private bool _isGrounded
    {
        get{
            return Physics2D.OverlapCircle(groundCheckTransform.position, _radiusGrCheck, groundLayer);
        }
    }
    private Vector2 _movementVector
    {
        get {
            float Horizontal = 0;
            if (!isAndroid)
                Horizontal = Input.GetAxis("Horizontal");
            else
                Horizontal = _js.Horizontal;

            return new Vector2(Horizontal, 0);
        }
    }

    public UnityEvent OnFinishEvents;
    public UnityEvent OnEnemyEvents;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CapsuleCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audio = GetComponent<AudioSource>();
        _js = FindObjectOfType<VariableJoystick>();

        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    void Update()
    {
        JumpLogic();
    }

    private void FixedUpdate()
    {
        MoveLogic();
    }
    private void MoveLogic() {
        _rb.AddForce(_movementVector*Speed, ForceMode2D.Impulse);
    }
    private void JumpLogic() {
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }
    public void Jump() {
        if (_isGrounded)
        {
            _rb.AddForce(Vector3.up * JumpForce, ForceMode2D.Impulse);
            playSound(jumpSound);
        }
    }
    public bool isGrounded()
    {
        return _isGrounded;
    }
    public float getMoveDirection() {
        return _movementVector.x;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == finishTag)
            OnFinishEvents.Invoke();
        if (collision.gameObject.tag == enemyTag)
        {
            OnEnemyEvents.Invoke();
            playSound(gameOverSound);
        }
        Key key = collision.GetComponent<Key>();
        if (key != null)
            key.Picked();
    }

    private void playSound(AudioClip ac) 
    {
        _audio.clip = ac;
        _audio.Play();
    }
}
