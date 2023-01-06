using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    [HideInInspector] public Vector2 MoveInput;

    private PlayerDash _playerDash;
    private Rigidbody2D _rb;
    private SpriteRenderer _spr;

    private void Start()
    {
        _playerDash = GetComponent<PlayerDash>();
        _rb = GetComponent<Rigidbody2D>();
        _spr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        GetMovementInput();
        FlipX();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void GetMovementInput()
    {
        if (!_playerDash.IsDashing) MoveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    private void ApplyMovement()
    {
        if (!_playerDash.IsDashing) _rb.velocity = MoveInput * _speed;
    }

    private void FlipX()
    {
        if (MoveInput == Vector2.zero) return;

        _spr.flipX = (MoveInput.x == -1) ? true : false;
    }
}
