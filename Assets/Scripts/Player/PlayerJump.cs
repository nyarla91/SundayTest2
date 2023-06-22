using System.Linq;
using Extentions;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerJump : Transformable
    {
        [SerializeField] private float _accelerationOfGravity;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _coyoteTime;
        [SerializeField] [Range(0, 90)] private float _maxSlope;

        private float _groundedTime;
        
        public bool IsGrounded => _groundedTime < _coyoteTime;
        
        private Rigidbody _rigidbody;

        private Rigidbody Rigidbody => _rigidbody ??= GetComponent<Rigidbody>();

        public float VerticalSpeed
        {
            get => Rigidbody.velocity.y;
            private set => Rigidbody.velocity = Rigidbody.velocity.WithY(value);
        }

        public void TryJump()
        {
            if ( ! IsGrounded)
                return;
            VerticalSpeed = _jumpForce;
            Rigidbody.MovePosition(Rigidbody.position + Vector3.up * VerticalSpeed * Time.fixedDeltaTime);
        }

        private void FixedUpdate()
        {
            _groundedTime += Time.fixedDeltaTime;
            if ( ! IsGrounded)
                VerticalSpeed -= _accelerationOfGravity * Time.fixedDeltaTime;
        }

        private void OnCollisionStay(Collision other)
        {
            if (!IsCollisionVertical(other))
                return;
            VerticalSpeed = 0;
            _groundedTime = 0;
        }

        private bool IsCollisionVertical(Collision collision)
            => collision.contacts.Any(contact => Vector3.Angle(contact.normal, Vector3.up) <= _maxSlope);
    }
}