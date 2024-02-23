using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour, IMovable
{
    public float MovementSpeed
    {
        get { return _movementSpeed; }
    }
    [SerializeField] private float _movementSpeed;
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

        _rigidbody.AddForce(targetVelocity - currentVelocity, ForceMode.VelocityChange);
    }

    public void Jump()
    {
        throw new System.NotImplementedException();
    }

    private void Awake()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Llamas")))
        {
            Debug.Log(other.gameObject.GetComponent<Entity>().Description);
        }
    }
}