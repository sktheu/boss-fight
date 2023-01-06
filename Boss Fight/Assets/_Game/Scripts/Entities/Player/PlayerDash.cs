using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private float _dashForce;
    [SerializeField] private float _dashInterval;
    [SerializeField] private float _isDashingInterval;

    public bool IsDashing = false;

    private PlayerMovement _playerMove;
    private Rigidbody2D _rb;

    [SerializeField] private bool _canDash = true;
    [SerializeField] private bool _dash = false;

    private void Start()
    {
        _playerMove = GetComponent<PlayerMovement>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GetDashInput();
    }

    private void GetDashInput()
    {
        if (Input.GetButton("Dash") && _canDash)
        {
            _canDash = false;
            _dash = true;
            StartCoroutine(SetDashInterval(_dashInterval));
        }
    }

    private void FixedUpdate()
    {
        
        if (_dash)
        {
            _dash = false;
            ApplyDash();
        }
    }

    private void ApplyDash()
    {
        _rb.velocity = Vector2.zero;
        IsDashing = true;
        StartCoroutine(SetIsDashingInterval(_isDashingInterval));

        if (_playerMove.MoveInput == Vector2.zero)
        {
            _rb.AddForce(Vector2.down * _dashForce);
        }
        else
        {
            _rb.AddForce(_playerMove.MoveInput * _dashForce);
        }
    }

    private IEnumerator SetDashInterval(float time)
    {
        yield return new WaitForSeconds(time);
        _canDash = true;
    }

    private IEnumerator SetIsDashingInterval(float time)
    {
        yield return new WaitForSeconds(time);
        IsDashing = false;
    }
}
