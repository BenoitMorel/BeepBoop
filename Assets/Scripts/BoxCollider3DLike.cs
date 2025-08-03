using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct LocalAABB3D
{
    public Vector3 Offset;
    public Vector3 Size;

    public AABB3D ToWorldAABB(Transform transform)
    {
        Vector3 scale = transform.lossyScale;
        Vector3 scaledOffset = Vector3.Scale(Offset, scale);
        Vector3 scaledSize = Vector3.Scale(Size, scale);
        Vector3 worldCenter = transform.position + scaledOffset;
        return new AABB3D(worldCenter, scaledSize / 2f);
    }
}

public struct AABB3D
{
    public Vector3 Center;
    public Vector3 HalfSize;

    public AABB3D(Vector3 center, Vector3 halfSize)
    {
        Center = center;
        HalfSize = halfSize;
    }

    public bool Overlaps(AABB3D other)
    {
        return Mathf.Abs(Center.x - other.Center.x) < (HalfSize.x + other.HalfSize.x) &&
               Mathf.Abs(Center.y - other.Center.y) < (HalfSize.y + other.HalfSize.y) &&
               Mathf.Abs(Center.z - other.Center.z) < (HalfSize.z + other.HalfSize.z);
    }
}

public class BoxCollider3DLike : MonoBehaviour
{
    public List<LocalAABB3D> Boxes = new List<LocalAABB3D>()
    {
        new LocalAABB3D { Offset = Vector3.zero, Size = Vector3.one }
    };

    public List<AABB3D> GetAABBs()
    {
        List<AABB3D> result = new List<AABB3D>();
        foreach (var box in Boxes)
        {
            result.Add(box.ToWorldAABB(transform));
        }
        return result;
    }

    public AABB3D ComputeOverallBBox()
    {
        Vector3 min = Vector3.positiveInfinity;
        Vector3 max = Vector3.negativeInfinity;
        foreach (var box in Boxes)
        {
            AABB3D aabb = box.ToWorldAABB(transform);
            min = Vector3.Min(min, aabb.Center - aabb.HalfSize);
            max = Vector3.Max(max, aabb.Center + aabb.HalfSize);
        }
        return new AABB3D((min + max) / 2f, (max - min) / 2f);
    }

    public static bool Intersects(BoxCollider3DLike b1, BoxCollider3DLike b2)
    {
        List<AABB3D> aabbs1 = b1.GetAABBs();
        List<AABB3D> aabbs2 = b2.GetAABBs();
        foreach (var aabb1 in aabbs1)
        {
            foreach (var aabb2 in aabbs2)
            {
                if (aabb1.Overlaps(aabb2))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Vector3 scale = transform.lossyScale;

        foreach (var box in Boxes)
        {
            Vector3 center = transform.position + Vector3.Scale(box.Offset, scale);
            Vector3 size = Vector3.Scale(box.Size, scale);
            Gizmos.DrawWireCube(center, size);
        }
    }
}