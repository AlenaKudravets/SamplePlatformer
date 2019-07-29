using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TweenObjectMover : MonoBehaviour
{

    [SerializeField] private float MovingTime;
    [SerializeField] private Vector3 EndPosition;
    [SerializeField] private bool PingPong;

    private Vector3 _startPosition;

    void Awake()
    {
        _startPosition = transform.localPosition;
    }

    void Start ()
    {
        var tweener = transform.DOLocalMove(EndPosition, MovingTime);

        tweener.onComplete += OnForwardComplete;
    }

    private void OnForwardComplete()
    {
        if (PingPong)
        {
            var tweener = transform.DOLocalMove(_startPosition, MovingTime);

            tweener.onComplete += OnBackwardComplete;
        }
    }

    private void OnBackwardComplete()
    {
        if (PingPong)
        {
            var tweener = transform.DOLocalMove(EndPosition, MovingTime);

            tweener.onComplete += OnForwardComplete;
        }
    }
}
