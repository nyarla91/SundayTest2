using System;
using UnityEngine;

namespace Extentions
{
    public class Transformable : MonoBehaviour
    {
        private Transform _transform;
        
        [Obsolete("Use Transform property instead")] public new Transform transform => Transform;
        
        public Transform Transform => _transform ??= gameObject.transform;
    }
}