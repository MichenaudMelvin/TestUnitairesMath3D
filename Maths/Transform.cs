using TestUnitaires;

namespace Maths_Matrices.Tests;

public struct Transform
{
    public Vector3 LocalPosition;
    public Vector3 LocalRotation;
    public Vector3 LocalScale;

    private MatrixFloat _LocalTranslationMatrix;
    public MatrixFloat LocalTranslationMatrix
    {
        get
        {
            _LocalTranslationMatrix[0, 3] = LocalPosition.x;
            _LocalTranslationMatrix[1, 3] = LocalPosition.y;
            _LocalTranslationMatrix[2, 3] = LocalPosition.z;
            return _LocalTranslationMatrix;
        }
    }

    private MatrixFloat _LocalRotationXMatrix;
    public MatrixFloat LocalRotationXMatrix
    {
        get
        {
            _LocalRotationXMatrix[1, 1] = (float)Math.Cos(LocalRotation.x * Math.PI / 180);
            _LocalRotationXMatrix[1, 2] = -(float)Math.Sin(LocalRotation.x * Math.PI / 180);
            _LocalRotationXMatrix[2, 1] = (float)Math.Sin(LocalRotation.x * Math.PI / 180);
            _LocalRotationXMatrix[2, 2] = (float)Math.Cos(LocalRotation.x * Math.PI / 180);
            return _LocalRotationXMatrix;
        }
    }

    private MatrixFloat _LocalRotationYMatrix;
    public MatrixFloat LocalRotationYMatrix
    {
        get
        {
            _LocalRotationYMatrix[0, 0] = (float)Math.Cos(LocalRotation.y * Math.PI / 180);
            _LocalRotationYMatrix[0, 2] = (float)Math.Sin(LocalRotation.y * Math.PI / 180);
            _LocalRotationYMatrix[2, 0] = -(float)Math.Sin(LocalRotation.y * Math.PI / 180);
            _LocalRotationYMatrix[2, 2] = (float)Math.Cos(LocalRotation.y * Math.PI / 180);
            return _LocalRotationYMatrix;
        }
    }

    private MatrixFloat _LocalRotationZMatrix;
    public MatrixFloat LocalRotationZMatrix
    {
        get
        {
            _LocalRotationZMatrix[0, 0] = (float)Math.Cos(LocalRotation.z * Math.PI / 180);
            _LocalRotationZMatrix[0, 1] = -(float)Math.Sin(LocalRotation.z * Math.PI / 180);
            _LocalRotationZMatrix[1, 0] = (float)Math.Sin(LocalRotation.z * Math.PI / 180);
            _LocalRotationZMatrix[1, 1] = (float)Math.Cos(LocalRotation.z * Math.PI / 180);
            return _LocalRotationZMatrix;
        }
    }

    public MatrixFloat LocalRotationMatrix => LocalRotationYMatrix * LocalRotationXMatrix * LocalRotationZMatrix;

    private MatrixFloat _LocalScaleMatrix;
    public MatrixFloat LocalScaleMatrix
    {
        get
        {
            _LocalScaleMatrix[0, 0] = LocalScale.x;
            _LocalScaleMatrix[1, 1] = LocalScale.y;
            _LocalScaleMatrix[2, 2] = LocalScale.z;
            return _LocalScaleMatrix;
        }
    }

    public MatrixFloat LocalToWorldMatrix => LocalTranslationMatrix * LocalRotationMatrix * LocalScaleMatrix;
    public MatrixFloat WorldToLocalMatrix => LocalToWorldMatrix.InvertByRowReduction();

    private Vector3 _WorldPosition;
    public Vector3 WorldPosition
    {
        get
        {
            _WorldPosition.x = LocalToWorldMatrix[0, 3];
            _WorldPosition.y = LocalToWorldMatrix[1, 3];
            _WorldPosition.z = LocalToWorldMatrix[2, 3];
            return _WorldPosition;
        }
        set => _WorldPosition = value;
    }

    public Transform()
    {
        _LocalTranslationMatrix = MatrixFloat.Identity(4);

        _LocalRotationXMatrix = MatrixFloat.Identity(4);
        _LocalRotationYMatrix = MatrixFloat.Identity(4);
        _LocalRotationZMatrix = MatrixFloat.Identity(4);

        LocalScale = Vector3.One;
        _LocalScaleMatrix = MatrixFloat.Identity(4);
    }

    public void SetParent(Transform parent)
    {
        _LocalTranslationMatrix = parent.LocalTranslationMatrix;
        _LocalRotationXMatrix = parent.LocalRotationXMatrix;
        _LocalRotationYMatrix = parent.LocalRotationYMatrix;
        _LocalRotationZMatrix = parent.LocalRotationZMatrix;
        _LocalScaleMatrix = parent.LocalScaleMatrix;
    }
}