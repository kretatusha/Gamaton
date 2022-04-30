﻿using UnityEngine;

namespace Source.Runtime
{
    public class BotCommandable
    {
        private readonly CommandReceiver _commandReceiver;
        private readonly Rigidbody2D _rigidbody2D;
        private readonly float _movementSpeed;
        private readonly float _jumpPower;
        private bool _isMove;
        private Vector3 _velocity;
        
        public Vector3 Velocity => _velocity;

        public BotCommandable(CommandReceiver commandReceiver, Rigidbody2D rigidbody2D, float movementSpeed, float jumpPower)
        {
            _commandReceiver = commandReceiver;
            _rigidbody2D = rigidbody2D;
            _movementSpeed = movementSpeed;
            _jumpPower = jumpPower;
        }

        public void Init()
        {
            _commandReceiver.MoveCommanded += Move;
            _commandReceiver.JumpCommanded += Jump;
            _commandReceiver.StopCommanded += Stop;
            _commandReceiver.FlipCommanded += Flip;
        }

        private void Move() => _isMove = true;

        private void Jump()
        {
            _rigidbody2D.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
        }

        private void Flip()
        {
            _rigidbody2D.transform.Rotate(Vector3.up, 180);
            Move();
        }

        private void Stop() => _isMove = false;

        public void Update()
        {
            _velocity.x = _isMove ? _rigidbody2D.transform.right.x * _movementSpeed : 0;
            _velocity.y = _rigidbody2D.velocity.y;
            _rigidbody2D.velocity = Velocity; 
        }
    }
}