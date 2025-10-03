using UnityEngine;
using System;

public class OverlapSettings : MonoBehaviour
{
    public Collider[] Colliders = new Collider[10];
    public int Size;
    public float SphereRadius;

    public LayerMask SearchLayer;
    public Transform OverlapPoint;
    public Vector3 BoxSize;
    public Vector3 PositionOffset;

    public void SetOverlapPoint(Transform newOverlapPoint)
    {
        OverlapPoint = newOverlapPoint;
    }

    public void SetSearchMask(LayerMask newMask)
    {
        SearchLayer = newMask;
    }

    public void SetOffset(Vector3 offset)
    {
        PositionOffset = offset;
    }

    public void SetBoxSize(Vector3 size)
    {
        BoxSize = size;
    }

    public void SetSphereRadius(float radius)
    {
        if (radius < 0f)
            throw new ArgumentOutOfRangeException(nameof(radius));

        SphereRadius = radius;
    }



}
