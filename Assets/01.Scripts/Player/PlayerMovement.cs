using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f; // �ӵ� ������ ���� float�� ����

    private Vector2 targetPosition; // ������ ��ġ
    private float startTime; // �̵� ���� �ð�

    public void OnSetTargetPos(Vector2 movePos)
    {
        targetPosition = movePos;
        startTime = Time.time; // �̵� ���� �ð� ����
    }

    private void Update()
    {
        Vector3 targetDir = targetPosition - (Vector2)transform.position;
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-90));

        float distance = Vector2.Distance(transform.position, targetPosition); // ���� ��ġ�� ������ ���� �Ÿ�
        float timeElapsed = Time.time - startTime; // ��� �ð�

        if (distance > 0f && timeElapsed > 0f)
        {
            float t = Mathf.Clamp01(timeElapsed / (moveSpeed / distance)); // ���� �ð� ���� ���
            transform.position = Vector3.Lerp(transform.position, targetPosition, t); // ������ ��ġ�� �̵�
        }
    }
}
