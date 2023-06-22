using System;
using Extentions;
using UnityEngine;

namespace Player
{
    public class PlayerAnimation : Transformable
    {
        [SerializeField] private GameObject _weaponModel;
        [SerializeField] [Tooltip("Degrees per second")] private float _rotationSpeed;
        [SerializeField] private Animator _animator;
        
        public event Func<Vector3> MoveVelocity;
        public event Func<Vector3> AimDirection;
        public event Func<float> VerticalSpeed;
        
        private void Update()
        {
            Vector3 aimDirection = AimDirection?.Invoke() ?? Vector3.zero;
            bool aiming = ! aimDirection.magnitude.ApproximatelyEqual(0);
            Vector3 moveVelocity = MoveVelocity?.Invoke() ?? Vector3.zero;
            float speed = moveVelocity.magnitude;
            
            UpdateAnimationFalling( ! (VerticalSpeed?.Invoke() ?? 0).ApproximatelyEqual(0));
            UpdateAnimationSpeed(speed);

            UpdateAnimationAiming( aiming);
            _weaponModel.SetActive(aiming);
            UpdateAnimationAimRunDirection(aimDirection, moveVelocity);
            
            RotateTowardsLook(moveVelocity, aimDirection);
        }

        private void UpdateAnimationAimRunDirection(Vector3 aimDirection, Vector3 moveVelocity)
        {
            int aimRunDirection = 0;
            if ( ! aimDirection.magnitude.ApproximatelyEqual(0) && ! moveVelocity.magnitude.ApproximatelyEqual(0))
            {
                float moveDegree = moveVelocity.normalized.XZtoXY().ToDegrees();
                float aimDegree = Transform.forward.XZtoXY().ToDegrees();
                float difference = Mathf.Repeat(aimDegree - moveDegree, 360);

                aimRunDirection = Mathf.RoundToInt(difference / 90) + 1;
                if (aimRunDirection == 5)
                    aimRunDirection = 1;
            }

            print(aimRunDirection);
            _animator.SetInteger("AimRunDirection", aimRunDirection);
        }

        private void RotateTowardsLook(Vector3 moveVelocity, Vector3 aimDirection)
        {
            if ( ! aimDirection.magnitude.ApproximatelyEqual(0))
                RotateTowards(aimDirection);
            else if ( ! moveVelocity.magnitude.ApproximatelyEqual(0))
                RotateTowards(moveVelocity);
        }

        private void RotateTowards(Vector3 direction)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            Transform.rotation = Quaternion.RotateTowards(Transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }

        private void UpdateAnimationFalling(bool falling) => _animator.SetBool("Falling", falling);

        private void UpdateAnimationSpeed(float speed) => _animator.SetFloat("Speed", speed);

        private void UpdateAnimationAiming(bool aiming) => _animator.SetBool("Aiming", aiming);
    }
}