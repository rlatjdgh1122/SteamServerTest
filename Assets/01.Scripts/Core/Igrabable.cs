using DG.Tweening;
using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Igrabable : NetworkBehaviour
{
    [SerializeField] protected bool IsGrab = true;
    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetGrabt(Vector2 normal, float power)
    {
        if (!IsGrab) return;
        Vector2 dir = normal - (Vector2)transform.position;

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(dir.normalized * power, .5f).SetEase(Ease.OutExpo)).
            SetDelay(.8f).
            Join(transform.DOMove(dir.normalized * power * .5f, .5f).SetEase(Ease.OutExpo));

    }

    private IEnumerator GrabtCoroutine(Vector2 normal, float power)
    {
        rb.AddForce(normal * power, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1f);
        rb.AddForce(normal * power * .5f, ForceMode2D.Impulse);
    }
}
