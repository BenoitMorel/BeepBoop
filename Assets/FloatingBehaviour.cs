

using UnityEngine;

public class FloatingBehaviour: MonoBehaviour
{
    [SerializeField] private float amplitude = 0.5f; // Amplitude of the floating effect
    [SerializeField] private float frequency = 1.0f; // Frequency of the floating effect
    public void Update()
    {
        Vector3 pos = transform.localPosition;
        pos.y = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.localPosition = pos;
    }
}