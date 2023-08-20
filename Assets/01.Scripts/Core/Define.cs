using UnityEngine;

namespace Core
{
    public enum WayType
    {
        Front,
        Back,
        Left,
        Right,
    }
    class Define
    {

    }
    class Calculate
    {
        public static Vector3 Get_in_plane_Vector(Vector3 value) //∆Ú∏ÈªÛ πÈ≈Õ¿« ª¨º¿
        {
            return new Vector3(value.x, 0, value.z);
        }
        public static Vector3 Get_in_plane_Subtraction(Vector3 a, Vector3 b) //∆Ú∏ÈªÛ πÈ≈Õ¿« ª¨º¿
        {
            return new Vector3(a.x - b.x, 0, a.z - b.z);
        }
        public static Vector3 Get_in_plane_Direction(Vector3 a, Vector3 b) //∆Ú∏ÈªÛ πÈ≈Õ¿« ª¨º¿
        {
            return new Vector3(a.x - b.x, 0, a.z - b.z).normalized;
        }
    }
}

