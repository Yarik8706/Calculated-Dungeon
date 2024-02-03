using System;
using System.Collections;
using System.Collections.Generic;
using Inputs;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float forseX = 50f;
    [SerializeField] private float maxSpeedX = 10f;
    [SerializeField] private float jumpForse = 20f;
    [SerializeField] private bool faceRight = true;
    [SerializeField] private bool isGrounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float chackRadius = 0.1f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float dashForse = 20f;
    [SerializeField] private float deshTime = 0.1f;
    [SerializeField] private float jumpCooldown = 0.2f;
    [SerializeField] private float runEffectCooldown = 0.2f;
    [SerializeField] private ParticleSystem runEffect;
    [SerializeField] private ParticleSystem jumpEffect;
    
    
    private Rigidbody2D _rigidbody2D;
    
    private bool _iAmDeshing;
    private bool _canDesh = true;
    private float _deshTimeNow;
    private float _currentJumpCooldwon;
    private float _currentRunEffectCooldwon;
    private float scaleMovementInAir = 0.3f;

    internal float NowXVelocity { get; set; }
    public float NowYVelocity { get; set; }
    public float MoveX { get; set; }
    public bool LestJump { get; set; }
    public bool DoubleJump { get; set; } = true;
    private bool LestDesh { get; set; }
    public bool Death { get; set; }
    public bool CanIDeath { get; set; } = true;

    public static PlayerControl Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        var boxCollider2D = GetComponent<BoxCollider2D>();
        groundCheck.localPosition += new Vector3(boxCollider2D.offset.x, 0, 0);
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _currentRunEffectCooldwon = runEffectCooldown;
        _currentJumpCooldwon = jumpCooldown;
        chackRadius = boxCollider2D.size.x * 0.9f * 0.5f;
    }

    private void Update()
    {
        if (Death)
        {
            MoveX = 0;
            return;
        }
        if(_currentJumpCooldwon > 0) _currentJumpCooldwon -= Time.deltaTime;
        MoveX = InputControl.Instance.moveX;
        
        if (InputControl.Instance.jump && _currentJumpCooldwon <= 0)
        {
            if (isGrounded)
            {
                LestJump = true;
                _currentJumpCooldwon = jumpCooldown;
            }
            else if (DoubleJump)
            {
                LestJump = true;
                DoubleJump = false;
            }
        }
        
        if (InputControl.Instance.shift && _canDesh)
        {
            LestDesh = true;
        }
        var runEffectStatus = isGrounded && _rigidbody2D.velocity.x != 0 && _currentRunEffectCooldwon <= 0;
        _currentRunEffectCooldwon -= Time.deltaTime;
        if (runEffectStatus)
        {
            _currentRunEffectCooldwon = runEffectCooldown;
            Instantiate(runEffect, groundCheck.transform.position, Quaternion.identity);
        }

        NowXVelocity = _rigidbody2D.velocity.x;
        NowYVelocity = _rigidbody2D.velocity.y;
    }

    void FixedUpdate()
    {
        // Деш в сторону
        if (LestDesh)
        {
            LestDesh = false;
            _canDesh = false;
            _iAmDeshing = true;
            _deshTimeNow = deshTime;
        }

        if (_iAmDeshing)
        {
            _deshTimeNow -= Time.fixedDeltaTime;
            if (_deshTimeNow >= 0)
            {
                float dashVecX = faceRight ? 1 : -1;
                _rigidbody2D.velocity = new Vector2(dashVecX * dashForse, 0);
                return;
            }
            _iAmDeshing = false;
            StopMoveAtMoment();
        }

        var contactCollider = Physics2D.OverlapCircle(groundCheck.position, chackRadius, whatIsGround);

        isGrounded = contactCollider != null;
        
        float fly_range = 1f;
        if (isGrounded)
        {
            DoubleJump = true;
            _canDesh = true;
            if (_rigidbody2D.velocity.y < -8f)
            {
                jumpEffect.transform.position = groundCheck.transform.position;
                jumpEffect.Play();
                CameraShake.Instance.Shake(0.1f, 0.3f);
            }
        }
        else //коэффицент движения в полёте
        {
            fly_range = scaleMovementInAir;
        }
            
        _rigidbody2D.AddForce(new Vector2(MoveX, 0).normalized * forseX * fly_range * Time.fixedDeltaTime, ForceMode2D.Impulse);

        if (_rigidbody2D.velocity.x < -maxSpeedX)
            _rigidbody2D.velocity = new Vector2(-maxSpeedX, _rigidbody2D.velocity.y);
        if (_rigidbody2D.velocity.x > maxSpeedX)
            _rigidbody2D.velocity = new Vector2(maxSpeedX, _rigidbody2D.velocity.y);

        // первый-второй прыжок
        if (LestJump)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpForse);
            LestJump = false;
        }

        switch (MoveX)
        {
            case > 0 when !faceRight:
            case < 0 when faceRight:
                Flip();
                break;
        }
    }

    public void Die()
    {
        if (!CanIDeath) return;
        if (Death) return;
        Death = true;
    }

    private void Flip()
    {
        faceRight = !faceRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void StopMoveAtMoment(float timer = 0f)
    {
        StartCoroutine(StopMoveAtMomentCor(timer));
    }

    private IEnumerator StopMoveAtMomentCor(float timer)
    {
        while (timer >= 0)
        {
            timer -= Time.fixedDeltaTime;
            _rigidbody2D.velocity = new Vector2(0, 0);
            yield return new WaitForFixedUpdate();
        }
    }
}
