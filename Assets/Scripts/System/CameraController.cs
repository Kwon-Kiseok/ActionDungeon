using Cysharp.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _actionFocusCamera;
    [SerializeField] private CinemachineCamera _mainCamera;

    [SerializeField] private CinemachineImpulseSource _impulseSource;

    public bool IsMainCam = true;

    public async void SetHitActionFocusCamera()
    {
        IsMainCam = false;

        _actionFocusCamera.Priority = 5;
        _mainCamera.Priority = 0;

        CameraShake();

        await UniTask.WaitForSeconds(0.5f);
        SetMainCamera();
    }

    public void SetMainCamera()
    {
        IsMainCam = true;

        _mainCamera.Priority = 5;
        _actionFocusCamera.Priority = 0;
    }

    private void CameraShake()
    {
        _impulseSource.GenerateImpulse();
    }
}
