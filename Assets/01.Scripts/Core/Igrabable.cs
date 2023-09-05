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

        Debug.Log("normal : " + normal);
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove
            (normal * power, .5f)
            .SetEase(Ease.OutExpo))
            .SetDelay(.8f)
            .Join(transform.DOMove
                (normal * power * .5f, .5f)
            .SetEase(Ease.OutExpo))
            .OnComplete(() =>
            {
                Destroy(grabObject);
            });

    }

    private IEnumerator GrabtCoroutine(Vector2 normal, float power)
    {
        rb.AddForce(normal * power, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1f);
        rb.AddForce(normal * power * .5f, ForceMode2D.Impulse);
    }
}
