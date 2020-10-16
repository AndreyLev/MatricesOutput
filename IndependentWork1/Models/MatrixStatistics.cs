using IndependentWork1.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IndependentWork1.Models
{
    public class MatrixStatistics
    {
        IMatrix matrix;

        public double ValuesSum
        { 
            get
            {
                double sum = 0;
                foreach (double el in matrix) sum += el;              
                return sum;
            }
        }
        public double Mean
        {
            get
            {
                return ValuesSum / (matrix.RowNumber * matrix.ColumnNumber);
            }
        }
        public double MaxValue
        {
            get
            {
                double max = matrix[0,0];                             
                foreach (double el in matrix) if (el > max) max = el;
                return max;
            }
        }
        public int nonValuesElementsCount
        {
            get
            {
                int count = 0;        
                foreach (double el in matrix) if (el != 0) count++;
                return count;
            }
        }

        public MatrixStatistics(IMatrix matrix)
        {
            this.matrix = matrix;
        }


    }
}
