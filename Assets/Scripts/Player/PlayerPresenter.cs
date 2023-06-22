using System;
using Extentions;
using Joystick;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerPresenter : Transformable
    {
        [SerializeField] private Button _jumpButton;
        [SerializeField] private PlayerAnimation _animation;
        [SerializeField] private OnScreenJoystick _moveJoystick;
        [SerializeField] private OnScreenJoystick _aimJoystick;

        private PlayerMovement _movement;
        private PlayerJump _jump;
        private PlayerWeapon _weapon;

        private PlayerMovement Movement => _movement ??= GetComponent<PlayerMovement>();
        private PlayerJump Jump => _jump ??= GetComponent<PlayerJump>();
        private PlayerWeapon Weapon => _weapon ??= GetComponent<PlayerWeapon>();
        
        private void Awake()
        {
            _jumpButton.onClick.AddListener(Jump.TryJump);
            Movement.MoveInput += () => _moveJoystick.Offset;
            Weapon.AimDirectionInput += () => _aimJoystick.Offset;
            _animation.AimDirection += () => Weapon.AimDirection;
            _animation.MoveVelocity += () => Movement.HorizontalVelocty;
            _animation.VerticalSpeed += () => Jump.VerticalSpeed;
        }
    }
}