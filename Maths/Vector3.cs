namespace Maths_Matrices.Tests;

public struct Vector3
{
    public float x, y, z;

    public static Vector3 One = new Vector3(1f, 1f, 1f);

    public Vector3(float x, float y, float z) { this.x = x; this.y = y; this.z = z; }

    public static Vector3 operator +(Vector3 a, Vector3 b)
    {
        Vector3 result = new Vector3
        {
            x = a.x + b.x,
            y = a.y + b.y,
            z = a.z + b.z
        };
        return result;
    }

    public static Vector3 operator *(Vector3 vector, float scalar)
    {
        Vector3 result = new Vector3
        {
            x = vector.x * scalar,
            y = vector.y * scalar,
            z = vector.z * scalar
        };
        return result;
    }

    public static Vector3 operator -(Vector3 vector)
    {
        return vector * -1;
    }

    public static Vector3 operator -(Vector3 a, Vector3 b)
    {
        return a + (-b);
    }
}