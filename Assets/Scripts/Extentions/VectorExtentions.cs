using UnityEngine;

namespace Extentions
{
    public static class VectorExtentions
    {
        public static Vector2 WithX(this Vector2 vector, float newX) => new Vector2(newX, vector.y);

        public static Vector2 WithY(this Vector2 vector, float newY) => new Vector2(vector.x, newY);
        
        public static Vector3 WithZ(this Vector2 vector, float newZ) => new Vector3(vector.x, vector.y, newZ);
        public static Vector3 WithX(this Vector3 vector, float newX) => new Vector3(newX, vector.y, vector.z);
 
        public static Vector3 WithY(this Vector3 vector, float newY) => new Vector3(vector.x, newY, vector.z);

        public static Vector3 WithZ(this Vector3 vector, float newZ) => new Vector3(vector.x, vector.y, newZ);

        public static Vector2 XZtoXY(this Vector3 vector)
        {
            return new Vector2(vector.x, vector.z);
        }

        public static Vector3 XYtoXZ(this Vector2 vector)
        {
            return new Vector3(vector.x, 0, vector.y);
        }
        
        public static float ToDegrees(this Vector2 vector)
        {
            vector.Normalize();
            return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
        }
    }
}