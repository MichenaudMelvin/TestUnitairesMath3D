namespace Maths_Matrices.Tests;

public struct Vector3
{
    public float x, y, z;

    public static Vector3 One = new Vector3(1f, 1f, 1f);

    public Vector3(float x, float y, float z) { this.x = x; this.y = y; this.z = z; }
}