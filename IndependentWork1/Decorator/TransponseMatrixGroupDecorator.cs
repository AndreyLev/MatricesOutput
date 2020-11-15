using ClientPart.IndependentWork1.Composite;
using IndependentWork1.Decorator;
using IndependentWork1.Interfaces;
using IndependentWork1.Models;
using IndependentWork1.Realization;
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

        public override double this[int rowIndex, int columnIndex]
        {
            get
            {
                if (rowIndex >= RowNumber || columnIndex >= ColumnNumber) return 0;

                int desiredMatrixIndex = getDesiredMatrixIndex(ref rowIndex, columnIndex);
                Console.WriteLine("{0} : {1}", desiredMatrixIndex, rowIndex);

                return matrixGroup.MATRIX_GROUP[desiredMatrixIndex][rowIndex, columnIndex];
            }
            set
            {
                if (rowIndex < RowNumber && columnIndex < ColumnNumber)
                {

                    int desiredMatrixIndex = getDesiredMatrixIndex(ref rowIndex, columnIndex);

                    matrixGroup.MATRIX_GROUP[desiredMatrixIndex][rowIndex, columnIndex] = value;
                }

            }
        }
        public TransponseMatrixGroupDecorator(IMatrix matrix) : base(matrix)
        {
            matrixGroup = (HorizontalMatrixGroup) matrix;
        }

        private int getDesiredMatrixIndex(ref int rowIndex, int columnIndex)
        {
            int intermediateRowSum = 0;
            int desiredMatrixIndex = 0;
            for (int i = 0; i < matrixGroup.MATRIX_GROUP.Count; i++)
            {
                intermediateRowSum += matrixGroup.MATRIX_GROUP[i].RowNumber;
                if (rowIndex < intermediateRowSum)
                {
                    desiredMatrixIndex = i;
                    break;
                }
                desiredMatrixIndex++;
            }


            rowIndex = matrixGroup.MATRIX_GROUP[desiredMatrixIndex].RowNumber - (intermediateRowSum - rowIndex);
            return desiredMatrixIndex;
        }

        public override int RowNumber
        {
            get
            {
                return matrixGroup.MATRIX_GROUP.Select(matrix => matrix.RowNumber).Sum();
            }
        }

        public override int ColumnNumber
        {
            get
            {
                return matrixGroup.MATRIX_GROUP.Select(matrix => matrix.ColumnNumber).Max();
            }
        }

        public override void Draw(IDrawer drawer)
        {
            foreach (IMatrix matrix in matrixGroup.MATRIX_GROUP)
            {
                for (int i = 0; i < matrix.RowNumber; i++)
                {
                    for (int j = 0; j < matrix.ColumnNumber; j++)
                    {
                        drawer.DrawCellBorder(matrix, i, j);
                    }
                    drawer.DrawOnNewLine();
                }
                drawer.DrawMatrix();
            }
        }

        public override void DoDrawBorder(IDrawer drawer)
        {
            foreach (IMatrix matrix in matrixGroup.MATRIX_GROUP)
            {
                for (int i = 0; i < matrix.RowNumber; i++)
                {
                    for (int j = 0; j < matrix.ColumnNumber; j++)
                    {
                        drawer.DrawCellBorder(matrix, i, j);
                    }
                    drawer.DrawOnNewLine();
                }
                drawer.DrawBorder(matrix);
                drawer.DrawMatrix();
            }
        }



    }
}
