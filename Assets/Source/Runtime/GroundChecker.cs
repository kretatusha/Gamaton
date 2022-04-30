using UnityEngine;

namespace Source.Runtime
{
    public class GroundChecker
    {
        public bool IsGrounded { get; private set; }
        private readonly Transform _groundCheckPoint;
        private readonly float _checkRadius;
        private readonly LayerMask _groundLayer;

        public GroundChecker(Transform groundCheckPoint, float checkRadius, LayerMask groundLayer)
        {
            _groundCheckPoint = groundCheckPoint;
            _checkRadius = checkRadius;
            _groundLayer = groundLayer;
        }

        public void Update() =>
            IsGrounded = Physics2D.OverlapCircle(_groundCheckPoint.position, _checkRadius, _groundLayer);
    }
}