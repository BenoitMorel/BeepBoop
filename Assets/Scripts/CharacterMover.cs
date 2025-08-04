using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CharacterMover : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float gravity = 30f;

    [Header("Grounding")]
    [SerializeField] LayerMask platformLayer;

    int _jumpCount = 0;
    float _velocityY = 0f;

    public void Update()
    {
        // 1) Horizontal input
        float h = 0f;
        if (Input.GetKey(KeyCode.LeftArrow)) h = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) h = +1f;

        // Build a candidate position...
        Vector3 pos = transform.position;

        pos.x += h * moveSpeed * Time.deltaTime;

        // 2) Apply gravity to vertical velocity
        _velocityY -= gravity * Time.deltaTime;

        // Compute next vertical position
        float verticalDelta = _velocityY * Time.deltaTime;
        float nextY = pos.y + verticalDelta;

        // 3) If we're falling, raycast *from* our CURRENT position *down* to see if we land
        if (_velocityY <= 0f)
        {
            // Ray origin just above where our feet were
            Vector3 rayOrigin = pos;
            float rayLength = Mathf.Abs(verticalDelta);

            if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, rayLength, platformLayer))
            {
                // Snap us to the platform, reset vertical velocity & jumps
                nextY = hit.point.y + 0.005f;
                _velocityY = 0f;
                _jumpCount = 0;
            }
        }
        pos.y = nextY;
        transform.position = pos;

        // 4) Jump input (after we've snapped to ground above)
        if (Input.GetKeyDown(KeyCode.Space) && _jumpCount < 2)
        {
            _velocityY = jumpSpeed;
            _jumpCount++;
            Debug.Log("Jump! Count: " + _jumpCount);
        }
    }
}
