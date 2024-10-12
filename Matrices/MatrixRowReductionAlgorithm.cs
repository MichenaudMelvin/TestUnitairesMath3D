namespace TestUnitaires;

public class MatrixRowReductionAlgorithm
{
    public static (MatrixFloat, MatrixFloat) Apply(MatrixFloat matrixA, MatrixFloat matrixB)
    {
        MatrixFloat augmentedMatrix = MatrixFloat.GenerateAugmentedMatrix(matrixA, matrixB);

        bool bTryToBeInverted = matrixB.NbColumns != 1;

        for (int j = 0; j < matrixA.NbColumns;)
        {
            for (int i = 0; i < matrixA.NbColumns; i++)
            {
                float maxValue = float.MinValue;
                for (int l = i; l < augmentedMatrix.NbLines; l++)
                {
                    if (augmentedMatrix[l, j] > maxValue)
                    {
                        maxValue = augmentedMatrix[l, j];
                    }
                }

                bool bCannotBeInverted = true;
                for (int k = i; k < augmentedMatrix.NbLines; k++)
                {
                    if (augmentedMatrix[k, j] == 0)
                    {
                        continue;
                    }

                    bCannotBeInverted = false;

                    if (k >= i && augmentedMatrix[k, j] >= maxValue)
                    {
                        if (k != i)
                        {
                            MatrixElementaryOperations.SwapLines(augmentedMatrix, k, i);
                        }

                        MatrixElementaryOperations.MultiplyLine(augmentedMatrix, i, 1/augmentedMatrix[i, j]);

                        for (int r = 0; r < augmentedMatrix.NbLines; r++)
                        {
                            if (i != r)
                            {
                                MatrixElementaryOperations.AddLineToAnother(augmentedMatrix, i, r, -augmentedMatrix[r, j]);
                            }
                        }

                        break;
                    }
                }

                if (bCannotBeInverted && bTryToBeInverted)
                {
                    throw new MatrixInvertException("This matrix cannot be inverted.");
                }
                j++;
            }
        }

        return augmentedMatrix.Split(matrixA.NbColumns - 1);
    }
}