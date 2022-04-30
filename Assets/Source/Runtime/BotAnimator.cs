using Source.Runtime;
using UnityEngine;

public class BotAnimator
{
    private readonly int _velocityXHash = Animator.StringToHash("Velocity X");
    private readonly int _groundedHash = Animator.StringToHash("Grounded");
    private readonly int _dieHash = Animator.StringToHash("Die");
    private readonly int _shootHash = Animator.StringToHash("Shoot");
    private readonly Animator _animator;
    private readonly Bot _bot;
    private readonly GroundChecker _groundChecker;

    public BotAnimator(Animator animator, Bot bot, GroundChecker groundChecker,
        CommandReceiver commandReceiver)
    {
        _animator = animator;
        _bot = bot;
        _groundChecker = groundChecker;
        commandReceiver.KillCommanded += OnDied;
        commandReceiver.ShootCommanded += OnShooted;
    }

    public void Update()
    {
        _animator.SetFloat(_velocityXHash, Mathf.Abs(_bot.Velocity.x));
        _animator.SetBool(_groundedHash, _groundChecker.IsGrounded);
    }

    private void OnShooted()
    {
        _animator.SetTrigger(_shootHash);
    }

    private void OnDied()
    {
        _animator.SetTrigger(_dieHash);
    }
}