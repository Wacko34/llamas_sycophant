using UnityEngine;

public class CameraController : MonoBehaviour 
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _smoothTime = 0.3f;
    private Vector3 _currentVelocity = Vector3.zero;

    public void FollowPlayer()
    {
        transform.position = Vector3.SmoothDamp(
            transform.position,
            new Vector3(_playerTransform.position.x, _playerTransform.position.y, transform.position.z),
            ref _currentVelocity,
            _smoothTime,
            Mathf.Infinity,
            Time.fixedDeltaTime
        );
    }

    private void LateUpdate() 
    {    
        FollowPlayer();
    }
}