using ClientPart.IndependentWork1.Visitor;
using IndependentWork1.Interfaces;
using IndependentWork1.Realization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientPart.IndependentWork1.Decorator
{
    public class MatrixBorderDecorator : IMatrix
    {
        IMatrix matrix;

        public MatrixBorderDecorator(IMatrix matrix)
        {
            this.matrix = matrix;
        }

        public double this[int rowIndex, int columnIndex] {
            get => matrix[rowIndex, columnIndex];
            set { } }

        public int RowNumber => matrix.RowNumber;

        public int ColumnNumber => matrix.ColumnNumber;

        public void Accept(IVisitor visitor)
        {
            //visitor.visitMatrixBorder(matrix);
            //matrix.Draw(visitor);
            //for (int i = 0; i < matrix.RowNumber; i++)
            //{
            //    for (int j = 0; j < matrix.ColumnNumber; j++)
            //    {
            //        DrawCell(matrix, i, j, visitor);
            //    }
            //}
            //visitor.visitMatrix(this);
        }

    }
}
