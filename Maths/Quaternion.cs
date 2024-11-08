using TestUnitaires;

namespace Maths_Matrices.Tests;

public struct Quaternion
{
    public float x, y, z, w;

    public static Quaternion Identity => new Quaternion(0, 0, 0, 1);

    public MatrixFloat Matrix
    {
        get
        {
            MatrixFloat matrix = MatrixFloat.Identity(4);
            matrix[0, 0] = 1 - (2 * y * y) - (2 * z * z);
            matrix[0, 1] = (2 * x * y) - (2 * w * z);
            matrix[0, 2] = (2 * x * z) + (2 * w * y);
            matrix[1, 0] = (2 * x * y) + (2 * w * z);
            matrix[1, 1] = 1 - (2 * x * x) - (2 * z * z);
            matrix[1, 2] = (2 * y * z) - (2 * w * x);
            matrix[2, 0] = (2 * x * z) - (2 * w * y);
            matrix[2, 1] = (2 * y * z) + (2 * w * x);
            matrix[2, 2] = 1 - (2 * x * x) - (2 * y * y);
            return matrix;
        }
    }

    public Vector3 EulerAngles
    {
        get
        {
            MatrixFloat m = Matrix;
            Vector3 result;
            result.x = float.RadiansToDegrees((float)Math.Asin(-m[1, 2]));

            bool bValue = Math.Cos(result.x) != 0;

            double yResult = bValue ? Math.Atan2(m[0, 2], m[2, 2]) : Math.Atan2(-m[2, 0], m[0, 0]);
            result.y = float.RadiansToDegrees((float)yResult);

            double zResult = bValue ? (float)Math.Atan2(m[1, 0], m[1, 1]) : 0;

            result.z = float.RadiansToDegrees((float)zResult);
            return result;
        }
    }

    public Quaternion(float x, float y, float z, float w) { this.x = x; this.y = y; this.z = z; this.w = w; }

    public Quaternion Invert()
    {
        return new Quaternion(-x, -y, -z, w);
    }

    public static Quaternion Invert(Quaternion q)
    {
        return q.Invert();
    }

    public static Quaternion AngleAxis(float angle, Vector3 axis)
    {
        float halfAngle = float.DegreesToRadians(angle/2);

        float sin = (float)Math.Sin(halfAngle);
        float w = (float)Math.Cos(halfAngle);
        Vector3 vector = axis.Normalize() * sin;

        return new Quaternion(vector.x, vector.y, vector.z, w);
    }

    public static Quaternion Euler(float x, float y, float z)
    {
        return AngleAxis(y, new Vector3(0, 1, 0)) * AngleAxis(x, new Vector3(1, 0, 0)) * AngleAxis(z, new Vector3(0, 0, 1));
    } 

    public static Quaternion operator *(Quaternion a, Quaternion b)
    {
        Quaternion result = new Quaternion
        {
            w = a.w * b.w - a.x * b.x - a.y * b.y - a.z * b.z,
            x = a.w * b.x + a.x * b.w + a.y * b.z - a.z * b.y,
            y = a.w * b.y - a.x * b.z + a.y * b.w + a.z * b.x,
            z = a.w * b.z + a.x * b.y - a.y * b.x + a.z * b.w
        };
        return result;
    }

    public static Vector3 operator *(Quaternion rotation, Vector3 point)
    {
        Quaternion convertedPoint = new Quaternion(point.x, point.y, point.z, 0);

        Quaternion result =  rotation * convertedPoint * rotation.Invert();

        return new Vector3(result.x, result.y, result.z);
    }
}