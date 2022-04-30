using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.Runtime
{
    public class Bot : MonoBehaviour, IDamageable
    {
        
        private CommandReceiver _commandReceiver;
        private Rigidbody2D _rigidbody2D;
        private Transform _body;
        private float _movementSpeed;
        private float _jumpPower;
        private bool _isMove;
        private Vector3 _velocity;
        private BotAnimator _botAnimator;

        public Vector3 Velocity => _rigidbody2D.velocity;

        public void Init(CommandReceiver commandReceiver, Rigidbody2D rigidbody2D, Transform body,
            float movementSpeed, float jumpPower, BotAnimator botAnimator)
        {
            _commandReceiver = commandReceiver;
            _rigidbody2D = rigidbody2D;
            _body = body;
            _movementSpeed = movementSpeed;
            _jumpPower = jumpPower;
            _botAnimator = botAnimator;

            _commandReceiver.MoveCommanded += Move;
            _commandReceiver.JumpCommanded += Jump;
            _commandReceiver.StopCommanded += Stop;
            _commandReceiver.FlipCommanded += Flip;

            enabled = true;
        }

        private void Move() => _isMove = true;

        private void Jump()
        {
            _rigidbody2D.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
        }

        private void Flip()
        {
            _body.Rotate(Vector3.up, 180);
        }

        private void Stop() => _isMove = false;

        public void Update()
        {
            _velocity.x = _isMove ? _body.right.x * _movementSpeed : 0;
            _velocity.y = _rigidbody2D.velocity.y;
            _rigidbody2D.velocity = _velocity;
        }
        
        public void Reload()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }

        public void TakeDamage()
        {
            Stop();
            _botAnimator.OnDied();
        }
    }
}