namespace TestUnitaires;

public class MatrixFloat
{
    float[,] _matrix;

    public int NbLines {get => _matrix.GetLength(0);}
    public int NbColumns {get => _matrix.GetLength(1);}

    public MatrixFloat(float[,] newMatrix)
    {
        _matrix = newMatrix;
    }
    public MatrixFloat(int lines, int columns)
    {
        _matrix = new float[lines, columns];
    }

    public MatrixFloat(MatrixFloat copy)
    {
        _matrix = (float[,])copy._matrix.Clone();
    }

    public float[,] ToArray2D()
    {
        return _matrix;
    }

    public float this[int i, int j]
    {
        get { return _matrix[i, j]; }
        set { _matrix[i, j] = value; }
    }

    public bool IsIdentity()
    {
        if (NbColumns != NbLines)
        {
            return false;
        }

        for (int i = 0; i < NbLines; i++)
        {
            for (int j = 0; j < NbColumns; j++)
            {
                int targetInt = i == j ? 1 : 0;
                if (_matrix[i, j] != targetInt)
                {
                    return false;
                }
            }
        }

        return true; 
    }

    public static MatrixFloat Identity(int nbr)
    {
        float[,] matrix = new float[nbr, nbr];
        for (int i = 0; i < nbr; i++)
        {
            for (int j = 0; j < nbr; j++)
            {
                matrix[i, j] = i == j ? 1 : 0;
            }
        }

        return new MatrixFloat(matrix);
    }

    public MatrixFloat Transpose()
    {
        MatrixFloat tMatrix = new MatrixFloat(NbColumns, NbLines);

        for (int i = 0; i < NbColumns; i++)
        {
            for (int j = 0; j < NbLines; j++)
            {
                tMatrix[i, j] = _matrix[j, i];
            }
        }

        return tMatrix;
    }

    public static MatrixFloat Transpose(MatrixFloat matrix)
    {
        return matrix.Transpose();
    }

    public static MatrixFloat GenerateAugmentedMatrix(MatrixFloat matrixA, MatrixFloat matrixB)
    {
        MatrixFloat augmentedMatrix = new MatrixFloat(matrixA.NbLines, matrixA.NbColumns + matrixB.NbColumns);

        for (int i = 0; i < matrixA.NbLines; i++)
        {
            for (int j = 0; j < matrixA.NbColumns; j++)
            {
                augmentedMatrix[i, j] = matrixA[i, j];
            }
        }

        for (int i = 0; i < matrixB.NbLines; i++)
        {
            augmentedMatrix[i, matrixA.NbLines] = matrixB[i, 0];
        }

        return augmentedMatrix;
    }

    public (MatrixFloat, MatrixFloat) Split(int columnIndex)
    {
        MatrixFloat matrixA = new MatrixFloat(NbLines, columnIndex + 1);
        MatrixFloat matrixB = new MatrixFloat(NbLines, columnIndex - 1);

        for (int i = 0; i < NbLines; i++)
        {
            for (int j = 0; j < matrixA.NbColumns; j++)
            {
                matrixA[i, j] = _matrix[i, j];
            }
        }

        for (int i = 0; i < NbLines; i++)
        {
            matrixB[i, 0] = _matrix[i, columnIndex + 1];
        }

        return (matrixA, matrixB);
    }

    #region MathFunctions

    public void Multiply(int scalar)
    {
        for (int i = 0; i < NbLines; i++)
        {
            for (int j = 0; j < NbColumns; j++)
            {
                _matrix[i, j] *= scalar;
            }
        }
    }

    public MatrixFloat Multiply(MatrixFloat matrix)
    {
        if (NbColumns != matrix.NbLines)
        {
            throw new MatrixMultiplyException("A columns number must match B lines number");
        }

        MatrixFloat multipliedMatrix = new MatrixFloat(NbLines, matrix.NbColumns);
        for (int i = 0; i < NbLines; i++)
        {
            for (int j = 0; j < matrix.NbColumns; j++)
            {
                for (int k = 0; k < NbColumns; k++)
                {
                    multipliedMatrix[i, j] += _matrix[i, k] * matrix[k, j];
                }
            }
        }

        return multipliedMatrix;
    }

    public static MatrixFloat Multiply(MatrixFloat matrix, int scalar)
    {
        MatrixFloat copyMatrix = new MatrixFloat(matrix);
        copyMatrix.Multiply(scalar);
        return copyMatrix;
    }

    public static MatrixFloat Multiply(MatrixFloat a, MatrixFloat b)
    {
        return a.Multiply(b);
    }

    public void Add(MatrixFloat matrix)
    {
        if (NbLines * NbColumns != matrix.NbLines * matrix.NbColumns)
        {
            throw new MatrixSumException("Matrices does not have the same size");
        }

        for (int i = 0; i < NbLines; i++)
        {
            for (int j = 0; j < NbColumns; j++)
            {
                _matrix[i, j] += matrix[i, j];
            }
        }
    }

    public static MatrixFloat Add(MatrixFloat a, MatrixFloat b)
    {
        MatrixFloat newA = new MatrixFloat(a);
        newA.Add(b);
        return newA;
    }

    #endregion

    #region OperatorOverload

    public static MatrixFloat operator *(MatrixFloat matrix, int scalar)
    {
        return Multiply(matrix, scalar);
    }

    public static MatrixFloat operator *(int scalar, MatrixFloat matrix)
    {
        return matrix * scalar;
    }

    public static MatrixFloat operator *(MatrixFloat a, MatrixFloat b)
    {
        return Multiply(a, b);
    }

    public static MatrixFloat operator +(MatrixFloat a, MatrixFloat b)
    {
        return Add(a, b);
    }

    public static MatrixFloat operator -(MatrixFloat matrix)
    {
        return matrix * -1;
    }
    
    public static MatrixFloat operator -(MatrixFloat a, MatrixFloat b)
    {
        return a + (-b);
    }

    #endregion
}