using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerCharacterAnimationController : MonoBehaviour
{
    private Animator _animator;

    private int _xDirection = 1;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetCollisionsCount(int collisionsCount)
    {
        _animator.SetInteger("Collisions", collisionsCount);
    }

    public void SetMovingSpeed(float movingSpeed)
    {
        _animator.SetFloat("MovingSpeed", Mathf.Abs(movingSpeed));

        var newDirection = 0;

        if (movingSpeed > 0.01f)
        {
            newDirection = 1;
        }
        else if (movingSpeed < -0.01f)
        {
            newDirection = -1;
        }

        if (newDirection != 0 && newDirection != _xDirection)
        {
            transform.localScale = new Vector3(newDirection, 1, 1);
            _xDirection = newDirection;
        }
    }
}
