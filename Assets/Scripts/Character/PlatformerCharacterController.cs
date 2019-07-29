using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformerCharacterController : MonoBehaviour
{
    /*[SerializeField] private*/ public float _movingSpeed = 0.5f;
    [SerializeField] private float _JumpForce = 1f;
    [SerializeField] private float _BoostFactor = 1.5f;
    [SerializeField] private float _MovingRaycastDistance = 0.5f;
    [SerializeField] private LayerMask _raycastLayerMask;

    [SerializeField] private AudioClip walkingSound;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip landingSound;

    public float CharacterMovingSpeed
    {
        get { return _movingSpeed; }
        set { _movingSpeed = value; }
    }

    private PlatformerCharacterAnimationController _animationController;
    private Rigidbody2D _rigidbody2D;
    private AudioSource _audioSource;
    private int _collisionsCount = 0;
    private bool _doubleJumpUsed = false;
    private bool _isMovingX = false;
    private Timer m_timer;
    private float charSpeedBeforeFreeze;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animationController = GetComponentInChildren<PlatformerCharacterAnimationController>();
        _audioSource = GetComponent<AudioSource>();
        
    }

    void OnEnable()
    {
        PlatformerInputManager.OnMovingLeft += PlatformerInputManagerOnmovingLeft;
        PlatformerInputManager.OnMovingRight += PlatformerInputManagerOnmovingRight;
        PlatformerInputManager.OnJump += PlatformerInputManagerOnJump;
    }

    private void Start()
    {
        FreezerScript.ChangeSpeedByFactor += ChangeSpeed;
    }

    void UpdateIsMovingState()
    {
        if (_isMovingX && ( _collisionsCount == 0 || _rigidbody2D.velocity.x <= 0))
        {
            _isMovingX = false;
            _audioSource.Stop();
        }else if(!_isMovingX && _collisionsCount > 0 && _rigidbody2D.velocity.x <= 0)
        {
            _isMovingX = false;
            _audioSource.Play();
        }
    }

    private void PlatformerInputManagerOnmovingRight(bool isBoosted)
    {
        if (CanMoveToDirection(Vector2.right))
        {
            float speed = isBoosted ? _BoostFactor * _movingSpeed : _movingSpeed;
             _rigidbody2D.velocity = new Vector2(speed, _rigidbody2D.velocity.y);
        }
    }

    private bool CanMoveToDirection(Vector2 direction )
    {
        if (_collisionsCount <= 0)
        {
            return false;
        }

        //int layerMask = ~LayerMask.GetMask("Character");
        var hitInfo = Physics2D.Raycast(transform.position, direction, _MovingRaycastDistance, _raycastLayerMask);

        return hitInfo.collider == null || hitInfo.collider.isTrigger;
    }

    private void PlatformerInputManagerOnmovingLeft(bool isBoosted)
    {
        if (CanMoveToDirection(Vector2.left))
        {
            float speed = isBoosted ? _BoostFactor * _movingSpeed : _movingSpeed;
            _rigidbody2D.velocity = new Vector2(-speed, _rigidbody2D.velocity.y);
        }
    }

    private void PlatformerInputManagerOnJump()
    {
        if (_collisionsCount > 0 || !_doubleJumpUsed)
        {
            if (_collisionsCount <= 0)
            {
                _doubleJumpUsed = true;
            }

            GetComponent<Rigidbody2D>().AddForce(Vector2.up * _JumpForce, ForceMode2D.Impulse);

            _audioSource.PlayOneShot(jumpSound);
        }
    }

    private void Update()
    {
        _animationController.SetMovingSpeed(_rigidbody2D.velocity.x);
        UpdateIsMovingState();
        if (m_timer != null && m_timer.Finished)
        {
            CharacterMovingSpeed = charSpeedBeforeFreeze;
            Destroy(m_timer);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _collisionsCount++;

        Vector2 normal = collision.contacts.First().normal;

        bool isBottomCollision = Mathf.Abs(normal.x) < 0.1f && normal.y > 0.9f;
        if (isBottomCollision)
        {
            _doubleJumpUsed = false;
        }

        _animationController.SetCollisionsCount(_collisionsCount);

        if (transform.parent == null && isBottomCollision)
        {
            transform.parent = collision.transform;
            _audioSource.PlayOneShot(landingSound);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _collisionsCount--;

        _animationController.SetCollisionsCount(_collisionsCount);

        if (transform.parent == collision.transform)
        {
            transform.parent = null;
        }
    }

    private void OnDisable()
    {
        PlatformerInputManager.OnMovingLeft -= PlatformerInputManagerOnmovingLeft;
        PlatformerInputManager.OnMovingRight -= PlatformerInputManagerOnmovingRight;
        PlatformerInputManager.OnJump -= PlatformerInputManagerOnJump;
    }

    private void ChangeSpeed(float factor, float sec)
    {
        charSpeedBeforeFreeze = CharacterMovingSpeed;
        CharacterMovingSpeed *= factor;
        m_timer = gameObject.AddComponent<Timer>();
        m_timer.Duration = sec;
        m_timer.Run();

    }

    private void OnDestroy()
    {
        FreezerScript.ChangeSpeedByFactor -= ChangeSpeed;
    }
}