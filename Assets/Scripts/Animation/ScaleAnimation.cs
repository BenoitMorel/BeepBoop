using UnityEngine;


public class ScaleAnimation : MonoBehaviour
{
    [SerializeField] Vector3 baseSize = new Vector3(1.0f, 1.0f, 1.0f); // Base size of the object
    [SerializeField] Vector3 amplitude = new Vector3(0.5f, 0.5f, 0.5f); // Amplitude of the scale effect
    [SerializeField] Vector3 frequency = new Vector3(1.0f, 1.0f, 1.0f); // Frequency of the scale effect

    public void Update()
    {
        Vector3 scale = transform.localScale;
        scale.x = baseSize.x + Mathf.Sin(Time.time * frequency.x) * amplitude.x;
        scale.y = baseSize.y + Mathf.Sin(Time.time * frequency.y) * amplitude.y;
        scale.z = baseSize.z + Mathf.Sin(Time.time * frequency.z) * amplitude.z;
        transform.localScale = scale;
    }
}