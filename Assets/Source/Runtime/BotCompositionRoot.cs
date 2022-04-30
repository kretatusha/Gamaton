using System;
using Source.Runtime;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public class BotCompositionRoot : MonoBehaviour
{
    [SerializeField]
    private Transform _botBody;
    [Header("Movement")]
    [SerializeField]
    private float _movementSpeed;
    [SerializeField]
    private float _jumpPower;

    [Header("Grounded")]
    [SerializeField]
    private Transform _groundCheckPoint;
    [SerializeField]
    private float _groundCheckRadius;
    [SerializeField]
    private LayerMask _groundLayerMask;
    private GroundChecker _groundChecker;

    private BotAnimator _botAnimator;
    private CommandTransmitter _commandTransmitter;
    private CommandReceiver _commandReceiver;
    private BotCommandable _commandable;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _animator.enabled = true;
        _rigidbody2D.simulated = true;
    }

    private void OnDisable()
    {
        _animator.enabled = false;
        _rigidbody2D.simulated = false;
    }

    private void Start()
    {
        Compose();
    }

    private void Compose()
    {
        _commandTransmitter = FindObjectOfType<CommandTransmitter>();
        _commandReceiver = new CommandReceiver(_commandTransmitter);
        _groundChecker = new GroundChecker(_groundCheckPoint, _groundCheckRadius, _groundLayerMask);
        _commandable = new BotCommandable(_commandReceiver, _rigidbody2D, _botBody, _movementSpeed,
            _jumpPower);
        _botAnimator = new BotAnimator(GetComponent<Animator>(), _commandable, _groundChecker);

        _commandReceiver.Init();
        _commandable.Init();
    }

    private void FixedUpdate()
    {
        _groundChecker.Update();
        _commandable.Update();
    }

    private void Update()
    {
        _botAnimator.Update();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(_groundCheckPoint.position, _groundCheckRadius);
    }
}