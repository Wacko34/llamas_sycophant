using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour, IMovable
{
    public delegate void DialogueHandler(Vector3 position, string text);
    public static event DialogueHandler onDialogue;
    private SpriteRenderer _spriteRenderer;
    public float MovementSpeed
    {
        get { return _movementSpeed; }
    }
    [SerializeField] private float _movementSpeed;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _height;
    [SerializeField] private float _PlayerWidth;
    [SerializeField] private float _jumpForce;
    private Vector3 _lastDirection;
    private bool _wasGrounded = false;
    private Rigidbody _rigidbody;

    public void Move(Vector3 direction)
    {
        if (float.IsNaN(_movementSpeed))
        {
            return;
        }

        if (_movementSpeed < 0)
        {
            _movementSpeed = 0;
        }

        Vector3 currentVelocity = _rigidbody.velocity;
        Vector3 targetVelocity = new Vector3(direction.x, direction.y, 0);
        targetVelocity *= _movementSpeed;
        _spriteRenderer.flipX = direction.x < 0;

        if (IsPlayerGrounded())
        {
            Debug.Log("isGrounded");
            _lastDirection = targetVelocity - currentVelocity;
            _rigidbody.AddForce(_lastDirection, ForceMode.VelocityChange);
        }
    }

    public bool IsPlayerGrounded()
    {
        bool IsPlayerGrounded = false;

        // КОСТЫЛЬ, который нужен для того, чтобы игрок мог ходить, если вышел за пределы платформы больше, чем на половину
        if (Physics.Raycast(transform.position, Vector3.down, _height, _groundLayer)
        || Physics.Raycast(transform.position + new Vector3(_PlayerWidth, 0, 0), Vector3.down, _height, _groundLayer)
        || Physics.Raycast(transform.position + new Vector3(-_PlayerWidth, 0, 0), Vector3.down, _height, _groundLayer)
        )
        {
            IsPlayerGrounded = true;
        }
        return IsPlayerGrounded;
    }

    public void Jump()
    {
        if (!IsPlayerGrounded())
        {
            return;
        }

        _lastDirection.y = _jumpForce;
        _rigidbody.AddForce(_lastDirection, ForceMode.VelocityChange);
    }

    private void Awake()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // TODO: на выходе скрывать
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Llamas")))
        {
            onDialogue?.Invoke(other.gameObject.transform.position, other.gameObject.GetComponent<Entity>().Description);
        }
    }
}