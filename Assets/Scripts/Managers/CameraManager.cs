using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] CharacterMover characterController;
    [SerializeField] GameObject virtualCamera;
    [SerializeField] float cameraInertiaSpeed = 1.0f;
    [SerializeField] float charYBeforeFollow = 1.0f;

    Vector3 _targetLocalPosition = Vector3.zero;

    // Start is called before the first frame update
    public void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        UpdateTargetPosition();
        UpdatePositionFromTarget();
    }

    private void UpdateTargetPosition()
    {
        Vector3 pos = _targetLocalPosition;
        Vector3 charPos = characterController.transform.localPosition;

        pos.x = -charPos.x;

        if (charPos.y > charYBeforeFollow)
        {
            pos.y = -(charPos.y - charYBeforeFollow);
        }
        else
        {
            pos.y = 0;
        }
        _targetLocalPosition = pos;
    }

    private void UpdatePositionFromTarget()
    {
        // smooth transition with lerp
        virtualCamera.transform.localPosition = Vector3.Lerp(virtualCamera.transform.localPosition,
            _targetLocalPosition,
            Time.deltaTime * cameraInertiaSpeed);
    }
}
