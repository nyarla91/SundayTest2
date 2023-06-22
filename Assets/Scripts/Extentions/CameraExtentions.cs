using UnityEngine;

namespace Extentions
{
    public static class CameraExtentions
    {
        public static Vector3 ScreenToPerspective(this Vector2 screen, Transform camera)
            => camera.forward.WithY(0).normalized * screen.y + camera.right * screen.x;
    }
}