using ClientPart.IndependentWork1.Visitor;
using IndependentWork1.Interfaces;
using IndependentWork1.Realization;
using System;
using System.Collections;


namespace IndependentWork1.Models
{
    public class DenseMatrix : SomeMatrix
    {


        public DenseMatrix(int rowCount, int columnCount) : base(rowCount,columnCount)
        {
            
        }

        public override IMatrix Clone()
        {
            IMatrix matr = new DenseMatrix(RowNumber, ColumnNumber);
            for (int i = 0; i < RowNumber; i++)
            {
                for (int j = 0; j < ColumnNumber; j++)
                {
                    matr[i, j] = this[i, j];
                }
            }
            return matr;
        }

        public override void Draw(IDrawer drawer, IVisitor visitor)
        {
            for (int i = 0; i < RowNumber; i++)
            {
                for (int j = 0; j < ColumnNumber; j++)
                {
                    visitor.visitMatrixElement(this, i, j);
                }
            }
            drawer.DrawMatrix(this);
        }

        protected override IVector create(int size)
        {
            return new DenseVector(size);
        }

  

    }
}
