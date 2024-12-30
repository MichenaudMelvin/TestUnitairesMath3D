namespace Maths_Matrices.Tests;

public struct Vector3
{
    public float x, y, z;

    public static Vector3 One = new Vector3(1f, 1f, 1f);

    public Vector3(float x, float y, float z) { this.x = x; this.y = y; this.z = z; }

    public Vector3(Vector3 copy)
    {
        x = copy.x;
        y = copy.y;
        z = copy.z;
    } 

    public Vector3(Vector4 vector4)
    {
        x = vector4.x;
        y = vector4.y;
        z = vector4.z;
    } 

    public Vector3 Normalize()
    {
        Vector3 copy = new Vector3(x, y, z);
        float squareSum = x * x + y * y + z * z;
        float scale = (float)Math.Pow(squareSum, -0.5);

        return copy * scale;
    }

    public Vector3 Clamp(float Min, float Max)
    {
        Vector3 copy = new Vector3(x, y, z);
        copy.x = Math.Clamp(copy.x, Min, Max);
        copy.y = Math.Clamp(copy.y, Min, Max);
        copy.z = Math.Clamp(copy.z, Min, Max);
        return copy;
    }

    public Vector3 Abs()
    {
        Vector3 copy = new Vector3(x, y, z);
        copy.x = Math.Abs(copy.x);
        copy.y = Math.Abs(copy.y);
        copy.z = Math.Abs(copy.z);
        return copy;
    }

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

    public static Vector3 operator *(float scalar, Vector3 vector)
    {
        return vector * scalar;
    }

    public static Vector3 operator *(Vector3 a, Vector3 b)
    {
        Vector3 result = new Vector3
        {
            x = a.x * b.x,
            y = a.y * b.y,
            z = a.z * b.z
        };
        return result;
    }

    public static Vector3 operator /(Vector3 a, Vector3 b)
    {
        Vector3 result = new Vector3
        {
            x = a.x / b.x,
            y = a.y / b.y,
            z = a.z / b.z
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