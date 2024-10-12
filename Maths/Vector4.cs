using TestUnitaires;

namespace Maths_Matrices.Tests;

public struct Vector4
{
    public float x, y, z, w;

    public Vector4(float x, float y, float z, float w) { this.x = x; this.y = y; this.z = z; this.w = w; }

    public static Vector4 operator *(MatrixFloat m, Vector4 v)
    {
        MatrixFloat convertedVector = new MatrixFloat(m)
        {
            [0, 3] = v.x,
            [1, 3] = v.y,
            [2, 3] = v.z,
            [3, 3] = v.w
        };
        MatrixFloat resultMatrix = m * convertedVector;
        return new Vector4(resultMatrix[0, 3], resultMatrix[1, 3], resultMatrix[2, 3], resultMatrix[3, 3]);
    }
}