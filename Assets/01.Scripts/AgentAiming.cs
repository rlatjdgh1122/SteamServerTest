using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAiming : MonoBehaviour
{

    [SerializeField] private InputReader _inputReader;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private Transform _pivot;

    private void Update()
    {
        Vector3 screentToPlayerPos = _cameraController.Cam.WorldToScreenPoint(transform.position);
        Vector3 dir = (
                new Vector3(
                _inputReader.MousePostion.x,
                _inputReader.MousePostion.y,
                0)
                - screentToPlayerPos).normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        _pivot.rotation = Quaternion.Euler(0, 0, angle - 90);
    }
}
