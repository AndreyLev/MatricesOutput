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
        delegate int RowCountHandler();
        delegate int ColumnNumberHandler();

        RowCountHandler rowNumberHandler;
        ColumnNumberHandler columnNumberHandler;

        HorizontalMatrixGroup matrixGroup;

        DenseVector[] matrixValues;

        public override double this[int rowIndex, int columnIndex]
        {
            get { return matrixValues[rowIndex][columnIndex]; }
            set { matrixValues[rowIndex][columnIndex] = value; }
        }

        public TransponseMatrixGroupDecorator(IMatrix matrix) : base(matrix)
        {
            if (!(matrix is HorizontalMatrixGroup))
                throw new NotSupportedException();

            matrixGroup = (HorizontalMatrixGroup)matrix;
            rowNumberHandler = RowNumberSum;
            columnNumberHandler = ColumnNumberMax;
            CollectTheMatrix();
        }

        public override int RowNumber
        {
            get
            {
                return rowNumberHandler();
            }
        }

        public override int ColumnNumber
        {
            get
            {
                return columnNumberHandler();
            }
        }

        private int RowNumberSum()
        {
            return matrixGroup.MatrixGroup.Select(matrix => matrix.RowNumber).Sum();
        }

        private int RowNumberMax()
        {
            return matrixGroup.MatrixGroup.Select(matrix => matrix.RowNumber).Max();
        }

        private int ColumnNumberSum()
        {
            return matrixGroup.MatrixGroup.Select(matrix => matrix.ColumnNumber).Sum();
        }

        private int ColumnNumberMax()
        {
            return matrixGroup.MatrixGroup.Select(matrix => matrix.ColumnNumber).Max();
        }

        private void FillValuesWithIndex(int rowIndexOne, int rowIndexTwo, IMatrix matrixLink)
        {
            for (int i = 0, k = rowIndexOne; i < matrixLink.RowNumber && k < rowIndexTwo; i++, k++)
            {
                for (int j = 0; j < matrixLink.ColumnNumber; j++)
                {
                    matrixValues[k][j] = matrixLink[i, j];
                }
            }
        }
        private void CollectTheMatrix()
        {
            matrixValues = new DenseVector[RowNumber];
            for (int i = 0; i < matrixValues.Length; i++)
            {
                matrixValues[i] = new DenseVector(ColumnNumber);
            }

            int currentIndexOne = 0;
            int currentIndexTwo = 0;
            foreach (IMatrix m in matrixGroup.MatrixGroup)
            {
                currentIndexTwo += m.RowNumber;
                FillValuesWithIndex(currentIndexOne, currentIndexTwo, m);
                currentIndexOne += m.RowNumber;
            }
        }

        public void Transponse()
        {

        }


    }
}
