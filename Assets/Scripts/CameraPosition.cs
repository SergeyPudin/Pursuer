using UnityEngine;

[RequireComponent (typeof(Camera))]
public class CameraPosition : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private int _cameraDistanse;
    [SerializeField] private int _cameraHeight;

    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        Vector3 playerDirection = _player.transform.forward;
        Vector3 cameraPosition = _player.transform.position - playerDirection * _cameraDistanse + Vector3.up * _cameraHeight;

        _camera.transform.position = cameraPosition;
        _camera.transform.LookAt(_player.transform.position);
    }
}