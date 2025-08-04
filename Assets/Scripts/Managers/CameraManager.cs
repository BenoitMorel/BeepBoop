using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] CharacterMover characterController;
    [SerializeField] GameObject virtualCamera;
    [SerializeField] GameObject mainCamera;
    [SerializeField] bool useVirtualCamera = false;
    [SerializeField] float cameraInertiaSpeed = 1.0f;
    [SerializeField] float charYBeforeFollow = 1.0f;

    Vector3 _targetLocalPosition = Vector3.zero;
    Vector3 _mainCameraInitialPosition = Vector3.zero;

    // Start is called before the first frame update
    public void Start()
    {
        _mainCameraInitialPosition = mainCamera.transform.localPosition;
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
        Vector3 position = useVirtualCamera ? _targetLocalPosition : _mainCameraInitialPosition -_targetLocalPosition;
        GameObject go = useVirtualCamera ? virtualCamera : mainCamera;
        // smooth transition with lerp
        go.transform.localPosition = Vector3.Lerp(go.transform.localPosition,
            position,
            Time.deltaTime * cameraInertiaSpeed);
    }
}
