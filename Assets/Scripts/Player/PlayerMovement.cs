using System;
using Extentions;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Transform _camera;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _accelerationTime;

        public Vector3 HorizontalVelocty
        {
            get => Rigidbody.velocity.WithY(0);
            private set => Rigidbody.velocity = value.WithY(Rigidbody.velocity.y);
        }

        private Rigidbody _rigidbody;

        private Rigidbody Rigidbody => _rigidbody ??= GetComponent<Rigidbody>();

        public event Func<Vector2> MoveInput; 
        
        private void FixedUpdate()
        {
            Vector3 targetVelocity = (MoveInput?.Invoke() ?? Vector2.zero).ScreenToPerspective(_camera) * _maxSpeed;
            HorizontalVelocty = Vector3.MoveTowards(HorizontalVelocty, targetVelocity, 
                _maxSpeed / _accelerationTime * Time.fixedDeltaTime);
        }
    }
}
