using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _gravityForce = 9.8f;
    [SerializeField] private float _jumpPower = 2f;
    [SerializeField] private Collider2D _targetCollider;
    [SerializeField] private CircleCollider2D _collider;
    [SerializeField] private float _gravityAcceleration = 9.8f;
    [SerializeField] private UIInputHolder _inputHolder;

    private int _dir = 0;
    private Vector2 _currentVelocityDirection;
    private bool _inGround;

    private float _currentGravityAcceleration = 0f;

    private void OnEnable()
    {
        _inputHolder.Left.onHold.AddListener(() => SetMoveDirection(-1));
        _inputHolder.Right.onHold.AddListener(() => SetMoveDirection(1));
        _inputHolder.Left.onRelease.AddListener(() => SetMoveDirection(0));
        _inputHolder.Right.onRelease.AddListener(() => SetMoveDirection(0));
        _inputHolder.Jump.onClick.AddListener(() => TryJump());
    }

    private void OnDisable()
    {
        _inputHolder.Left.onHold.RemoveListener(() => SetMoveDirection(-1));
        _inputHolder.Right.onHold.RemoveListener(() => SetMoveDirection(-1));
        _inputHolder.Left.onRelease.RemoveListener(() => SetMoveDirection(0));
        _inputHolder.Right.onRelease.RemoveListener(() => SetMoveDirection(0));
        _inputHolder.Jump.onClick.RemoveListener(() => TryJump());
    }

    private void SetMoveDirection(int dir)
    {
        _dir = dir;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D)) SetMoveDirection(1);
        if (Input.GetKey(KeyCode.A)) SetMoveDirection(-1);
        if (Input.GetKeyUp(KeyCode.D)) SetMoveDirection(0);
        if (Input.GetKeyUp(KeyCode.A)) SetMoveDirection(0);

        if (Input.GetKeyDown(KeyCode.Space)) TryJump();

        float worldRadius = _collider.radius * Mathf.Max(transform.localScale.x, transform.localScale.y);
        var collider = Physics2D.OverlapCircle(transform.position, worldRadius);
        _inGround = collider != null && collider.transform == _target;
        _currentGravityAcceleration = _inGround ? 1 : _gravityAcceleration;

        Vector2 contactPoint = _targetCollider.ClosestPoint(transform.position);
        _currentVelocityDirection = contactPoint - (Vector2)transform.position;

        transform.up = -_currentVelocityDirection;

    }

    private void TryJump()
    {
        if (_inGround)
            _rb.AddForce(transform.up * _jumpPower);
    }

    private void FixedUpdate()
    {
        _rb.velocity = _currentVelocityDirection * _gravityForce * _currentGravityAcceleration + (Vector2)transform.right * _dir * _speed * Time.fixedDeltaTime;
    }

}
