using Unity.VisualScripting;
using UnityEngine;

namespace Player.View
{
    public class Footprint : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private float _fadeDuration;

        private float _alpha = 1;

        private void Update()
        {
            _alpha -= Time.fixedDeltaTime / _fadeDuration;
            if (_alpha <= 0)
            {
                Destroy(gameObject);
                return;
            }

            Color color = _renderer.material.color;
            color.a = _alpha;
            _renderer.material.color = color;
        }
    }
}