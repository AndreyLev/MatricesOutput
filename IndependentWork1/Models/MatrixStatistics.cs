using IndependentWork1.Interfaces;
using System;

namespace IndependentWork1.Models
{
    class MatrixStatistics
    {
        IMatrix matrix;

        public double ValuesSum
        {
            get
            {
                double sum = 0;
                for (int i = 0; i < matrix.RowNumber; i++)
                {
                    for (int j = 0; j < matrix.ColumnNumber; j++)
                    {
                        sum += matrix[i, j];
                    }
                }
                return sum;
            }
        }
        public double Mean
        {
            get
            {
                return ValuesSum / nonValuesElementsCount;
            }
        }
        public double MaxValue
        {
            get
            {
                double max = matrix[0, 0];
                for (int i = 0; i < matrix.RowNumber; i++)
                {
                    for (int j = 0; j < matrix.ColumnNumber; j++)
                    {
                        if (matrix[i, j] > max) max = matrix[i, j];
                    }
                }
                return max;
            }
        }
        public int nonValuesElementsCount
        {
            get
            {
                int count = 0;
                for (int i = 0; i < matrix.RowNumber; i++)
                {
                    for (int j = 0; j < matrix.ColumnNumber; j++)
                    {
                        if (matrix[i, j] > 0) count++;
                    }
                }
                return count;
            }
        }

        public MatrixStatistics(IMatrix matrix)
        {
            this.matrix = matrix;
        }


    }
}