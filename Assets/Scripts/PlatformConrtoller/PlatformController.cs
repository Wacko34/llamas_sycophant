using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;
using UnityEngine.UIElements;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private float moveSpeed;
    private int pointIndex;

    private void Awake()
    {
        if (points.Length == 0)
        {
            return;
        }
        pointIndex = 0;
        transform.position = points[pointIndex].transform.position;
    }

    private void Update()
    {
        if (points.Length == 0)
        {
            return;
        }

        if (pointIndex <= points.Length - 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[pointIndex].transform.position, moveSpeed * Time.deltaTime);
        }

        if (transform.position == points[pointIndex].transform.position)
        {
            pointIndex++;
        }

        if (pointIndex == points.Length)
        {
            pointIndex = 0;
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            other.gameObject.transform.parent = transform;
        }
    }

    private void OnCollisionExit(Collision other) 
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            other.gameObject.transform.parent = null;
        }
    }
}
