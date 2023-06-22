using Extentions;
using UnityEngine;

namespace Player.View
{
    public class FootprintDrawer : Transformable
    {
        [SerializeField] private bool _rightFoot;
        [SerializeField] private GameObject _footprintPrefab;
        [SerializeField] private float _minDistanceToGround;
        [SerializeField] private PlayerJump _playerJump;

        private float _previousFrameHeight;
        
        private void Update()
        {
            float height = Transform.position.y - _playerJump.Transform.position.y;
            
            if (_previousFrameHeight > _minDistanceToGround && height <= _minDistanceToGround && _playerJump.IsGrounded)
                InstantiateFootprint();
            
            _previousFrameHeight = height;
        }

        private void InstantiateFootprint()
        {
            Vector3 position = Transform.position.WithY(_playerJump.Transform.position.y + _minDistanceToGround);
            Quaternion rotation = Quaternion.LookRotation(Transform.up.WithY(0).normalized, Vector3.up);
            Transform footprint = Instantiate(_footprintPrefab, position, rotation).transform;
            /*if (_rightFoot)
                footprint.localScale = footprint.localPosition.WithX( - footprint.localPosition.x);*/
        }
    }
}