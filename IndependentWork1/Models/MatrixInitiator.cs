using IndependentWork1.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IndependentWork1.Models
{
    public class MatrixInitiator
    {
        private static void ShuffleMatrix(IMatrix matrix)
        {
            Random rnd = new Random();
            int tempArrSize = matrix.RowNumber * matrix.ColumnNumber;
            double[] tempArr = new double[tempArrSize];
            int currentRow = 0;
            int currentColumn = 0;
            for (int i = 0; i < tempArrSize; i++)
            {
               
                if (i>0 && i % matrix.ColumnNumber == 0)
                {
                    
                        currentColumn = 0;
                        currentRow++;
                    
                }
              
                tempArr[i] = matrix[currentRow, currentColumn];
                currentColumn++;
                
            }
           
            // Реализация метода тасования массива Ричарда Дурштенфельдома
            int j;
            for (int i = tempArrSize-1; i >= 1; i--)
            {
                j = rnd.Next(i + 1);
               
                double temp = tempArr[j];
                tempArr[j] = tempArr[i];
                tempArr[i] = temp;
            }

            
            int tempArrCurrentPosition = 0; 
            for (int i = 0; i < matrix.RowNumber; i++)
            {
                for (int k = 0; k < matrix.ColumnNumber; k++)
                {
                    matrix[i,k] = tempArr[tempArrCurrentPosition];
                    tempArrCurrentPosition++;
                }
            }

            if (matrix is SparseMatrix)
            {
                SparseMatrix sparceMatrix = (SparseMatrix) matrix;
                sparceMatrix.collapse();
            }
        }
        public static void FillMatrix(IMatrix matrix, int nonValueElementsNumber, double maxValue)
        {
            int matrixSize = matrix.RowNumber * matrix.ColumnNumber;
            if (nonValueElementsNumber > matrixSize) nonValueElementsNumber = matrixSize;

            Random rnd = new Random();

            int counter = nonValueElementsNumber;
            int currentRow = 0;
            int currentColumn = 0;
            
            while (counter-- != 0)
            {
               
                if (currentColumn < matrix.ColumnNumber)
                {
 
                    matrix[currentRow, currentColumn] = rnd.NextDouble() * maxValue;
                    currentColumn++;
 
                } else if (currentColumn == matrix.ColumnNumber)
                {
                    
                    currentRow++;
                    currentColumn = 0;
                    matrix[currentRow,currentColumn] = rnd.NextDouble() * maxValue;
                    currentColumn++;
                }

            }
            ShuffleMatrix(matrix);
        }
    }
}
