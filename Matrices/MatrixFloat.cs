namespace TestUnitaires;

public class MatrixFloat
{
    #region ClassDefaults

    float[,] _matrix;

    public int NbLines {get => _matrix.GetLength(0);}
    public int NbColumns {get => _matrix.GetLength(1);}
    public int MatrixSize {get => NbLines * NbColumns;}

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

    public override string ToString()
    {
        string output = "";
        for (int i = 0; i < NbColumns; i++)
        {
            for (int j = 0; j < NbLines; j++)
            {
                output += _matrix[i, j].ToString() + ", ";
            }

            output += "\n";
        }

        return output;
    }

    public float this[int i, int j]
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
            for (int j = 0; j < matrixB.NbColumns; j++)
            {
                augmentedMatrix[i, matrixA.NbLines + j] = matrixB[i, j];
            }
        }

        return augmentedMatrix;
    }

    public (MatrixFloat, MatrixFloat) Split(int columnIndex)
    {
        int aSize = columnIndex + 1;
        int bSize = NbColumns - aSize;
        MatrixFloat matrixA = new MatrixFloat(NbLines, aSize);
        MatrixFloat matrixB = new MatrixFloat(NbLines, bSize);

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

    public MatrixFloat InvertByRowReduction()
    {
        (MatrixFloat matrixA, MatrixFloat matrixB) = MatrixRowReductionAlgorithm.Apply(this, Identity(NbLines));
        return matrixB;
    }

    public static MatrixFloat InvertByRowReduction(MatrixFloat matrix)
    {
        return matrix.InvertByRowReduction();
    }

    public MatrixFloat SubMatrix(int lineToRemove, int columnToRemove)
    {
        MatrixFloat subMatrix = new MatrixFloat(NbLines - 1, NbColumns - 1);

        bool bReachLine = false;
        for (int i = 0; i < NbLines; i++)
        {
            if (i == lineToRemove)
            {
                bReachLine = true;
                continue;
            }

            bool bReachColumn = false;
            for (int j = 0; j < NbColumns; j++)
            {
                if (j == columnToRemove)
                {
                    bReachColumn = true;
                    continue;
                }

                subMatrix[i + (bReachLine ? -1 : 0), j + (bReachColumn ? -1 : 0)] = _matrix[i, j];
            }
        }

        return subMatrix;
    }

    public static MatrixFloat SubMatrix(MatrixFloat matrix, int lineToRemove, int columnToRemove)
    {
        return matrix.SubMatrix(lineToRemove, columnToRemove);
    }

    public static float Determinant(MatrixFloat matrix)
    {
        if (matrix.MatrixSize == 1*1)
        {
            return matrix[0, 0];
        }

        if (matrix.MatrixSize == 2*2)
        {
            return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
        }

        float det = 0;
        if (matrix.MatrixSize == 4*4)
        {
            for (int i = 0; i < matrix.NbColumns; i++)
            {
                MatrixFloat subMatrix = matrix.SubMatrix(0, i);
                int factor = (i % 2 == 0 ? 1 : -1);
                det += matrix[0, i] * ((subMatrix[0, 0] * subMatrix[1, 1] * subMatrix[2, 2]) -
                                       (subMatrix[0, 0] * subMatrix[1, 2] * subMatrix[2, 1]) -
                                       (subMatrix[0, 1] * subMatrix[1, 0] * subMatrix[2, 2]) +
                                       (subMatrix[0, 1] * subMatrix[1, 2] * subMatrix[2, 0]) +
                                       (subMatrix[0, 2] * subMatrix[1, 0] * subMatrix[2, 1]) -
                                       (subMatrix[0, 2] * subMatrix[1, 1] * subMatrix[2, 0])) * factor;
            }

            return det;
        }

        for (int i = 0; i < matrix.NbColumns; i++)
        {
            MatrixFloat subMatrix = matrix.SubMatrix(0, i);
            int factor = (i % 2 == 0 ? 1 : -1);
            det += matrix[0, i] * (subMatrix[0, 0] * subMatrix[1, 1] - subMatrix[0, 1] * subMatrix[1, 0]) * factor;
        }

        return det;
    }

    public MatrixFloat Adjugate()
    {
        MatrixFloat cofactorMatrix = new MatrixFloat(this);

        for (int i = 0; i < NbColumns; i++)
        {
            bool bInvert = i % 2 != 0;
            for (int j = 0; j < NbLines; j++)
            {
                MatrixFloat subMatrix = SubMatrix(i, j);

                int factor = bInvert ? 
                    j%2 != 0 ? 1 : -1 :
                    j%2 == 0 ? 1 : -1;

                cofactorMatrix[i, j] = Determinant(subMatrix) * factor;
            }
        }

        return cofactorMatrix.Transpose();
    }

    public static MatrixFloat Adjugate(MatrixFloat matrix)
    {
        return matrix.Adjugate();
    }

    public MatrixFloat InvertByDeterminant()
    {
        float det = Determinant(this);
        if (det == 0)
        {
            throw new MatrixInvertException("Cannot invert matrix with a null determinant");
        }

        MatrixFloat invertedMatrix = new MatrixFloat(this);
        return (1/det) * invertedMatrix.Adjugate();
    }

    public static MatrixFloat InvertByDeterminant(MatrixFloat matrix)
    {
        return matrix.InvertByDeterminant();
    }

    #endregion

    #region MathFunctions

    public void Multiply(float scalar)
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

    public static MatrixFloat Multiply(MatrixFloat matrix, float scalar)
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
        if (MatrixSize != matrix.MatrixSize)
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

    public static MatrixFloat operator *(MatrixFloat matrix, float scalar)
    {
        return Multiply(matrix, scalar);
    }

    public static MatrixFloat operator *(float scalar, MatrixFloat matrix)
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