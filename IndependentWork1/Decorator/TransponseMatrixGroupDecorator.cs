using ClientPart.IndependentWork1.Composite;
using IndependentWork1.Decorator;
using IndependentWork1.Interfaces;
using IndependentWork1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndependentWork1.Decorator
{
    class TransponseMatrixGroupDecorator: BaseDecorator
    { 

        HorizontalMatrixGroup matrixGroup;

        bool transponseFlag;

        public TransponseMatrixGroupDecorator(IMatrix matrix, bool transponseFlag) : base(matrix)
        {

            matrixGroup = (HorizontalMatrixGroup)matrix;

            this.transponseFlag = transponseFlag;

            if (transponseFlag)
                CollectTheMatrix();
            else
                matrixGroup.doHorizontalGroup();
        }

        public override int RowNumber
        {
            get
            {
                if (transponseFlag)
                    return RowNumberSum();
                else
                    return base.RowNumber;
            }
        }

        public override int ColumnNumber
        {
            get
            {
                if (transponseFlag)
                    return ColumnNumberMax();
                else
                    return base.ColumnNumber;
            }
        }

        private int RowNumberSum()
        {
            return matrixGroup.MatrixGroup.Select(matrix => matrix.RowNumber).Sum();
        }

        private int ColumnNumberMax()
        {
            return matrixGroup.MatrixGroup.Select(matrix => matrix.ColumnNumber).Max();
        }

        private void FillValuesWithIndex(int rowIndexOne, int rowIndexTwo, 
            IMatrix matrixLink, DenseMatrix dm)
        {
            for (int i = 0, k = rowIndexOne; i < matrixLink.RowNumber && k < rowIndexTwo; i++, k++)
            {
                for (int j = 0; j < matrixLink.ColumnNumber; j++)
                {
                    dm[k,j] = matrixLink[i, j];
                }
            }
        }
        private void CollectTheMatrix()
        {
            DenseMatrix matrixValues;
            matrixValues = new DenseMatrix(RowNumber, ColumnNumber);

            int currentIndexOne = 0;
            int currentIndexTwo = 0;
            foreach (IMatrix m in matrixGroup.MatrixGroup)
            {
                currentIndexTwo += m.RowNumber;
                FillValuesWithIndex(currentIndexOne, currentIndexTwo, m, matrixValues);
                currentIndexOne += m.RowNumber;
            }
            matrixGroup.MATRIX = matrixValues;
        }

        protected override IVector Create(int size)
        {
            throw new NotImplementedException();
        }
    }
}
