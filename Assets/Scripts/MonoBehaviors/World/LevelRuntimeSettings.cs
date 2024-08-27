using Cinemachine;
using UnityEngine;

public class LevelRuntimeSettings : MonoBehaviour
{
    [SerializeField] public float cameraSize;

    CinemachineVirtualCamera virtualCamera;

    void Start()
    {
        virtualCamera = GameObject.Find("Vcam").GetComponent<CinemachineVirtualCamera>();
        virtualCamera.m_Lens.OrthographicSize = cameraSize;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
