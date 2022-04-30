using System;
using Source.Runtime;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public class BotCompositionRoot : MonoBehaviour
{
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
    
    private BotAnimator _animator;
    private CommandTransmitter _commandTransmitter;
    private CommandReceiver _commandReceiver;
    private BotCommandable _commandable;

    private void Awake()
    {
        Compose();
    }

    private void Compose()
    {
        _commandTransmitter = FindObjectOfType<CommandTransmitter>();
        _commandReceiver = new CommandReceiver(_commandTransmitter);
        _groundChecker = new GroundChecker(_groundCheckPoint, _groundCheckRadius, _groundLayerMask);
        _commandable = new BotCommandable(_commandReceiver, GetComponent<Rigidbody2D>(), _movementSpeed, _jumpPower);
        _animator = new BotAnimator(GetComponent<Animator>(), _commandable, _groundChecker);

        _commandReceiver.Init();
        _commandable.Init();
    }

    private void FixedUpdate()
    {
        _groundChecker.Update();
        _commandable.Update();
        _animator.Update();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(_groundCheckPoint.position, _groundCheckRadius);
    }
}
