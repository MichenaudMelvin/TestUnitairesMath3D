namespace TestUnitaires;

public class MatrixElementaryOperations
{
    #region SwapFunctions

    public static void SwapLines(MatrixInt matrix, int lineA, int lineB)
    {
        int[] savedLine = new int[matrix.NbColumns];
        for (int i = 0; i < matrix.NbColumns; i++)
        {
            savedLine[i] = matrix[lineA, i];
            matrix[lineA, i] = matrix[lineB, i];
            matrix[lineB, i] = savedLine[i];
        }
    }

    public static void SwapColumns(MatrixInt matrix, int columnA, int columnB)
    {
        int[] savedColumn = new int[matrix.NbLines];
        for (int i = 0; i < matrix.NbLines; i++)
        {
            savedColumn[i] = matrix[i, columnA];
            matrix[i, columnA] = matrix[i, columnB];
            matrix[i, columnB] = savedColumn[i];
        }
    }

    public static void SwapLines(MatrixFloat matrix, int lineA, int lineB)
    {
        float[] savedLine = new float[matrix.NbColumns];
        for (int i = 0; i < matrix.NbColumns; i++)
        {
            savedLine[i] = matrix[lineA, i];
            matrix[lineA, i] = matrix[lineB, i];
            matrix[lineB, i] = savedLine[i];
        }
    }

    public static void SwapColumns(MatrixFloat matrix, int columnA, int columnB)
    {
        float[] savedColumn = new float[matrix.NbLines];
        for (int i = 0; i < matrix.NbLines; i++)
        {
            savedColumn[i] = matrix[i, columnA];
            matrix[i, columnA] = matrix[i, columnB];
            matrix[i, columnB] = savedColumn[i];
        }
    }

    #endregion

    #region MathFunctions

    public static void MultiplyLine(MatrixInt matrix, int line, int factor)
    {
        if (factor == 0)
        {
            throw new MatrixScalarZeroException("Cannot multiply by zero.");
        }

        for (int i = 0; i < matrix.NbColumns; i++)
        {
            matrix[line, i] *= factor;
        }
    }

    public static void MultiplyColumn(MatrixInt matrix, int column, int factor)
    {
        if (factor == 0)
        {
            throw new MatrixScalarZeroException("Cannot multiply by zero.");
        }
        
        for (int i = 0; i < matrix.NbLines; i++)
        {
            matrix[i, column] *= factor;
        }
    }
    public static void MultiplyLine(MatrixFloat matrix, int line, float factor)
    {
        if (factor == 0)
        {
            throw new MatrixScalarZeroException("Cannot multiply by zero.");
        }

        for (int i = 0; i < matrix.NbColumns; i++)
        {
            matrix[line, i] *= factor;
        }
    }

    public static void MultiplyColumn(MatrixFloat matrix, int column, float factor)
    {
        if (factor == 0)
        {
            throw new MatrixScalarZeroException("Cannot multiply by zero.");
        }
        
        for (int i = 0; i < matrix.NbLines; i++)
        {
            matrix[i, column] *= factor;
        }
    }

    public static void AddLineToAnother(MatrixInt matrix, int lineToAdd, int targetLine, int factor)
    {
        for (int i = 0; i < matrix.NbColumns; i++)
        {
            matrix[targetLine, i] += matrix[lineToAdd, i] * factor;
        }
    }

    public static void AddColumnToAnother(MatrixInt matrix, int columnToAdd, int targetColumn, int factor)
    {
        for (int i = 0; i < matrix.NbLines; i++)
        {
            matrix[i, targetColumn] += matrix[i, columnToAdd] * factor;
        }
    }
    
    public static void AddLineToAnother(MatrixFloat matrix, int lineToAdd, int targetLine, float factor)
    {
        for (int i = 0; i < matrix.NbColumns; i++)
        {
            matrix[targetLine, i] += matrix[lineToAdd, i] * factor;
        }
    }

    public static void AddColumnToAnother(MatrixFloat matrix, int columnToAdd, int targetColumn, float factor)
    {
        for (int i = 0; i < matrix.NbLines; i++)
        {
            matrix[i, targetColumn] += matrix[i, columnToAdd] * factor;
        }
    }

    #endregion
}
