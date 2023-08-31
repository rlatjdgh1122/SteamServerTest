using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAiming : MonoBehaviour
{

    [SerializeField] private InputReader _inputReader;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private Transform _pivot;

    private void LateUpdate()
    {
        Vector3 pos = _cameraController.Cam.ScreenToWorldPoint(_inputReader.MousePostion);
        Vector3 dir = (pos - transform.position).normalized;

        Debug.Log("pos : " + pos + "dir :" + dir);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        _pivot.rotation = Quaternion.Euler(0, 0, angle - 90);
    }
}
