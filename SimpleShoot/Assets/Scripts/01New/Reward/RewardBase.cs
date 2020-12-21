using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardBase : MonoBehaviour
{
    protected virtual void Start()
    {
        this.transform.DOLocalMoveY(-4, 1).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            this.gameObject.SetActive(false);
    }

    public virtual void OnDisable()
    {
        Destroy(this.gameObject);
    }
}
