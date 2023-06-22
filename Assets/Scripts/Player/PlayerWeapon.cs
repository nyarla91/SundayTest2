using System;
using Extentions;
using UnityEngine;

namespace Player
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private Transform _camera;
        
        public Vector3 AimDirection => (AimDirectionInput?.Invoke() ?? Vector2.zero).ScreenToPerspective(_camera).normalized;

        public bool IsAiming => AimDirection.Equals(Vector3.zero);
        
        public event Func<Vector2> AimDirectionInput;
    }
}