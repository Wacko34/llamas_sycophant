using UnityEngine;

public interface IMovable
{
    float MovementSpeed { get; }
    void Move(Vector3 direction);
    void Jump();
}