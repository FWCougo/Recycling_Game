using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenAnimation : MonoBehaviour
{
    [SerializeField] Vector3 sizeChange;
    [SerializeField] float durationTime;
    [SerializeField] bool tweenSize;


    private void OnEnable()
    {
        if (tweenSize)
        {
            TweenSize();
        }
    }

    public void TweenSize()
    {
        transform.DOScale(sizeChange, durationTime).SetLoops(-1, LoopType.Yoyo);
    }
}
