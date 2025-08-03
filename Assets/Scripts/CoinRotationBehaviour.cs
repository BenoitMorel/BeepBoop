
using UnityEngine;

public class CoinRotationBehaviour: MonoBehaviour
{
    public float rotationSpeed = 100f;
    void Update()
    {
        // Rotate the coin around its Y-axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}