using ClientPart.IndependentWork1.Composite;
using IndependentWork1.Interfaces;
using IndependentWork1.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndependentWork1.Decorator
{
    class TransponseMatrixDecorator : BaseDecorator
    {
        IVector[] transposedMatrix;
        public TransponseMatrixDecorator(IMatrix matrix) : base(matrix)
        {
            TransponseMatrix();
        }

        public override double this[int rowIndex, int columnIndex]
        {
            get { return transposedMatrix[rowIndex][columnIndex]; }
            set { transposedMatrix[rowIndex][columnIndex] = value; }
        }

        public override int RowNumber
        {
            get
            {
                return matrix.ColumnNumber;
            }
        }

        public override int ColumnNumber
        {
            get
            {
                return matrix.RowNumber;
            }
        }

        public virtual void TransponseMatrix()
        {
            transposedMatrix = new IVector[RowNumber];

            for (int i = 0; i < RowNumber; i++)
            {
                    transposedMatrix[i] = new DenseVector(ColumnNumber);
            }

            if (matrix is SomeMatrix || matrix is TransponseMatrixDecorator)
            {
                for (int i = 0; i < RowNumber; i++)
                {
                    for (int j = 0; j < ColumnNumber; j++)
                    {
                        transposedMatrix[i][j] = matrix[j, i];
                    }
                }
            }

            if (matrix is HorizontalMatrixGroup)
            {
                HorizontalMatrixGroup mg = (HorizontalMatrixGroup)matrix;
                
            }
        }
    }
}
