namespace TestUnitaires;

public class MatrixRowReductionAlgorithm
{
    public static (MatrixFloat, MatrixFloat) Apply(MatrixFloat matrixA, MatrixFloat matrixB)
    {
        MatrixFloat augmentedMatrix = MatrixFloat.GenerateAugmentedMatrix(matrixA, matrixB);
    
        for (int j = 0; j < augmentedMatrix.NbColumns;)
        {
            for (int i = 0; i < augmentedMatrix.NbColumns; i++)
            {
                float maxValue = float.MinValue;
                for (int l = i; l < augmentedMatrix.NbLines; l++)
                {
                    if (augmentedMatrix[l, j] > maxValue)
                    {
                        maxValue = augmentedMatrix[l, j];
                    }
                }

                for (int k = 0; k < augmentedMatrix.NbLines; k++)
                {
                    if (k >= i && augmentedMatrix[k, j] != 0 && augmentedMatrix[k, j] >= maxValue)
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
                j++;
            }
        }

        return augmentedMatrix.Split(matrixA.NbLines - 1);
    }
}