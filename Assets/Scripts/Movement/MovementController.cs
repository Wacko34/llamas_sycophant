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
        // targetVelocity = new Vector3(direction.x, 0, 0); // Изменено здесь
        _spriteRenderer.flipX = direction.x < 0;

        // Debug.Log(direction);

        if (IsPlayerGrounded())
        {
            _lastDirection = targetVelocity - currentVelocity;
            _rigidbody.AddForce(_lastDirection, ForceMode.VelocityChange);
        }

        // _rigidbody.AddForce(targetVelocity - currentVelocity, ForceMode.VelocityChange);
    }

    public bool IsPlayerGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _height, _groundLayer);
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