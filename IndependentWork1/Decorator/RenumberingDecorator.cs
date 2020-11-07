using IndependentWork1.Interfaces;
using IndependentWork1.Models;
using IndependentWork1.Realization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IndependentWork1.Decorator
{
    class RenumberingDecorator : BaseDecorator
    {
    
  
        public RenumberingDecorator(IMatrix matrix) : base(matrix)
        {
            CopyMatrix(matrix);
        }

        private void CopyMatrix(IMatrix matrix)
        {
            for (int i = 0; i < RowNumber; i++)
            {
                for (int j = 0; j < ColumnNumber; j++)
                {
                    this[i, j] = matrix[i, j];
                }
            }
        }
     
        public void RenumberRows(int rowOneIndex, int rowTwoIndex)
        {

            double temp;
            for (int i = 0; i < ColumnNumber; i++)
            {
                temp = this[rowOneIndex, i];
                this[rowOneIndex, i] = this[rowTwoIndex, i];
                this[rowTwoIndex, i] = temp;
            }

        }

        public void RenumberColumns(int columnOneIndex, int columnTwoIndex)
        {
            double temp;
            for (int i = 0; i < RowNumber; i++)
            {
                temp = this[i, columnOneIndex];
                this[i, columnOneIndex] = this[i, columnTwoIndex];
                this[i, columnTwoIndex] = temp;
            }
        }

        protected override IVector Create(int size)
        {
            return new DenseVector(ColumnNumber);
        }
    }
}
