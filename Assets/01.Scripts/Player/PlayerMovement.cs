using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f; // 속도 조절을 위해 float로 변경

    private Vector2 targetPosition; // 목적지 위치
    private float startTime; // 이동 시작 시간

    public void OnSetTargetPos(Vector2 movePos)
    {
        targetPosition = movePos;
        startTime = Time.time; // 이동 시작 시간 설정
    }

    private void Update()
    {
        Vector3 targetDir = targetPosition - (Vector2)transform.position;
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-90));

        float distance = Vector2.Distance(transform.position, targetPosition); // 현재 위치와 목적지 간의 거리
        float timeElapsed = Time.time - startTime; // 경과 시간

        if (distance > 0f && timeElapsed > 0f)
        {
            float t = Mathf.Clamp01(timeElapsed / (moveSpeed / distance)); // 보간 시간 비율 계산
            transform.position = Vector3.Lerp(transform.position, targetPosition, t); // 보간된 위치로 이동
        }
    }
}
