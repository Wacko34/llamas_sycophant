using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour, IMovable
{
    public delegate void DialogueHandler(Vector3 position, string text);
    public delegate void DialogueExitHandler();
    public static event DialogueHandler onDialogue;
    public static event DialogueExitHandler onDialogueExit;
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
    private Rigidbody _rigidbody;
    private BoxCollider _boxCollider;
    private Vector3 _boxSize;
    private RaycastHit _hit;
    private Vector3 _initialPosition;

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

        if (direction.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if (direction.x > 0)
        {
            _spriteRenderer.flipX = false;
        }

        _lastDirection = targetVelocity - currentVelocity;

        if (!IsPlayerGrounded())
        {
            _lastDirection.y = Physics2D.gravity.y * Time.deltaTime;
        }

        _rigidbody.AddForce(_lastDirection, ForceMode.VelocityChange);
    }

    public bool IsPlayerGrounded()
    {        
        return Physics.BoxCast(_boxCollider.bounds.center, _boxSize * 0.5f, Vector3.down, out _hit, transform.rotation, _height, _groundLayer) ? true : false;
    }

    public void Jump()
    {
        if (!IsPlayerGrounded())
        {
            return;
        }

        _lastDirection.y = Mathf.Sqrt(2 * _jumpForce * Mathf.Abs(Physics2D.gravity.y));
        _rigidbody.AddForce(_lastDirection, ForceMode.VelocityChange);
    }

    private void Awake()
    {
        _initialPosition = gameObject.transform.position;
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider>();
        _boxSize = _boxCollider.size;
        onDialogueExit?.Invoke();
    }

    private void FixedUpdate() 
    {
        CalculateMovement();
    }

    private void CalculateMovement()
    {
        if (!IsPlayerGrounded())
        {
            _lastDirection.y = Physics2D.gravity.y * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log(other.name);
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Spikes")))
        {
            onDialogueExit?.Invoke();
            gameObject.transform.position = _initialPosition;
        }

        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Llamas")))
        {
            onDialogue?.Invoke(other.gameObject.transform.position, other.gameObject.GetComponent<Entity>().Description);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Llamas")))
        {
            onDialogueExit?.Invoke();
        }
    }
}