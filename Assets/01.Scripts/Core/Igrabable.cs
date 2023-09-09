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
    public void SetGrabt(Vector2 normal, float power, GameObject grabObject)
    {
        if (!IsGrab) return;

        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOMove
            ((-normal * power) + (Vector2)transform.position, .5f)
            .SetEase(Ease.OutExpo))
            .SetDelay(.8f)
            .Join(transform.DOMove
            ((-normal * (power / 5f)) + (Vector2)transform.position, .3f)
            .SetEase(Ease.OutExpo))
            .OnComplete(() =>
            {
                Destroy(grabObject);
            });
    }
}
