using Source.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

[RequireComponent(
    typeof(Animator), 
    typeof(Rigidbody2D), 
    typeof(BotShooter))]
public class BotCompositionRoot : MonoBehaviour
{
    [SerializeField]
    private Transform _botBody;
    [Header("Movement")]
    [SerializeField]
    private float _movementSpeedMax;
    [SerializeField]
    private float _movementSpeedMin;
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

    [Header("Shooing")]
    [SerializeField]
    private Rigidbody2D _bulletPrefab;
    [SerializeField]
    private float _bulletSpeed;
    [SerializeField]
    private Transform _shootPoint;
    private BotShooter _botShooter;

    private BotAnimator _botAnimator;
    private CommandTransmitter _commandTransmitter;
    private CommandReceiver _commandReceiver;
    private Bot _commandable;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _botShooter = GetComponent<BotShooter>();
        _commandable = GetComponent<Bot>();
        _commandable.enabled = false;
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
        
        var movementSpeed = Random.Range(_movementSpeedMin, _movementSpeedMax);

        _botAnimator = new BotAnimator(GetComponent<Animator>(), _commandable, _groundChecker, _commandReceiver);
        _commandable.Init(_commandReceiver, _rigidbody2D, _botBody, movementSpeed, _jumpPower, _botAnimator);

        _botShooter.Init(_shootPoint, _bulletPrefab, _bulletSpeed, _botBody);
        _commandReceiver.Init();
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