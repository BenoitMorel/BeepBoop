

using UnityEngine;

public class Collidable : MonoBehaviour
{
    [SerializeField] private BoxCollider3DLike boxCollider;

    public BoxCollider3DLike BoxCollider
    {
        get => boxCollider;
        set => boxCollider = value;
    }
}
