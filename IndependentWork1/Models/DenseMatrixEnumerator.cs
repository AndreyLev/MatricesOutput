using IndependentWork1.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace IndependentWork1.Models
{
    public class DenseMatrixEnumerator : IEnumerator
    {
        IVector[] matrix;
        int rowPosition = -1;
        int columnPosition = -1;

        public DenseMatrixEnumerator(IVector[] matrix)
        {
            this.matrix = matrix;
        }

        public object Current
        {
            get
            {
                if (rowPosition == -1 || columnPosition == -1 
                    || rowPosition >= matrix.Length 
                    || columnPosition >= matrix[0].DIM)
                    throw new InvalidOperationException();
                return matrix[rowPosition][columnPosition];

            }
        }

        public bool MoveNext()
        {
            if (rowPosition < matrix.Length)
            {
                if (columnPosition < matrix[0].DIM - 1)
                {
                    if (rowPosition == -1) rowPosition++;
                    columnPosition++;
                    return true;
                }
                else if (columnPosition == matrix[0].DIM - 1)
                {
                    rowPosition++;
                    columnPosition = 0;
                    if (rowPosition == matrix.Length) return false;
                    return true;
                }
                else
                    return false;
                
            }
            else
                return false;
        }

        public void Reset()
        {
            rowPosition = -1;
            columnPosition = -1;
        }
    }
}
