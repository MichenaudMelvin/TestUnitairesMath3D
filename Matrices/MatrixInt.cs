namespace TestUnitaires;

public class MatrixInt
{
    #region ClassDefaults

    int[,] _matrix;

    public int NbLines {get => _matrix.GetLength(0);}
    public int NbColumns {get => _matrix.GetLength(1);}

    public MatrixInt(int[,] newMatrix)
    {
        _matrix = newMatrix;
    }

    public MatrixInt(int lines, int columns)
    {
        _matrix = new int[lines, columns];
    }

    public MatrixInt(MatrixInt copy)
    {
        _matrix = (int[,])copy._matrix.Clone();
    }

    public int[,] ToArray2D()
    {
        return _matrix;
    }

    public int this[int i, int j]
    {
        get { return _matrix[i, j]; }
        set { _matrix[i, j] = value; }
    }

    #endregion

    #region MatrixFunctions

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

    public static MatrixInt Identity(int nbr)
    {
        int[,] matrix = new int[nbr, nbr];
        for (int i = 0; i < nbr; i++)
        {
            for (int j = 0; j < nbr; j++)
            {
                matrix[i, j] = i == j ? 1 : 0;
            }
        }
        
        return new MatrixInt(matrix);
    }

    public MatrixInt Transpose()
    {
        MatrixInt tMatrix = new MatrixInt(NbColumns, NbLines);

        for (int i = 0; i < NbColumns; i++)
        {
            for (int j = 0; j < NbLines; j++)
            {
                tMatrix[i, j] = _matrix[j, i];
            }
        }

        return tMatrix;
    }

    public static MatrixInt Transpose(MatrixInt matrix)
    {
        return matrix.Transpose();
    }

    public static MatrixInt GenerateAugmentedMatrix(MatrixInt matrixA, MatrixInt matrixB)
    {
        MatrixInt augmentedMatrix = new MatrixInt(matrixA.NbLines, matrixA.NbColumns + matrixB.NbColumns);

        for (int i = 0; i < matrixA.NbLines; i++)
        {
            for (int j = 0; j < matrixA.NbColumns; j++)
            {
                augmentedMatrix[i, j] = matrixA[i, j];
            }
        }

        for (int i = 0; i < matrixB.NbLines; i++)
        {
            for (int j = 0; j < matrixB.NbColumns; j++)
            {
                augmentedMatrix[i, matrixA.NbLines + j] = matrixB[i, j];
            }
        }

        return augmentedMatrix;
    }

    public (MatrixInt, MatrixInt) Split(int columnIndex)
    {
        int aSize = columnIndex + 1;
        int bSize = NbColumns - aSize;
        MatrixInt matrixA = new MatrixInt(NbLines, aSize);
        MatrixInt matrixB = new MatrixInt(NbLines, bSize);

        for (int i = 0; i < NbLines; i++)
        {
            for (int j = 0; j < matrixA.NbColumns; j++)
            {
                matrixA[i, j] = _matrix[i, j];
            }
        }

        for (int i = 0; i < NbLines; i++)
        {
            for (int j = 0; j < matrixB.NbColumns; j++) {
                matrixB[i, j] = _matrix[i, aSize + j];
            }
        }

        return (matrixA, matrixB);
    }

    #endregion

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

    public MatrixInt Multiply(MatrixInt matrix)
    {
        if (NbColumns != matrix.NbLines)
        {
            throw new MatrixMultiplyException("A columns number must match B lines number");
        }

        MatrixInt multipliedMatrix = new MatrixInt(NbLines, matrix.NbColumns);
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

    public static MatrixInt Multiply(MatrixInt matrix, int scalar)
    {
        MatrixInt copyMatrix = new MatrixInt(matrix);
        copyMatrix.Multiply(scalar);
        return copyMatrix;
    }

    public static MatrixInt Multiply(MatrixInt a, MatrixInt b)
    {
        return a.Multiply(b);
    }

    public void Add(MatrixInt matrix)
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

    public static MatrixInt Add(MatrixInt a, MatrixInt b)
    {
        MatrixInt newA = new MatrixInt(a);
        newA.Add(b);
        return newA;
    }

    #endregion

    #region OperatorOverload

    public static MatrixInt operator *(MatrixInt matrix, int scalar)
    {
        return Multiply(matrix, scalar);
    }

    public static MatrixInt operator *(int scalar, MatrixInt matrix)
    {
        return matrix * scalar;
    }

    public static MatrixInt operator *(MatrixInt a, MatrixInt b)
    {
        return Multiply(a, b);
    }

    public static MatrixInt operator +(MatrixInt a, MatrixInt b)
    {
        return Add(a, b);
    }

    public static MatrixInt operator -(MatrixInt matrix)
    {
        return matrix * -1;
    }
    
    public static MatrixInt operator -(MatrixInt a, MatrixInt b)
    {
        return a + (-b);
    }

    #endregion
}