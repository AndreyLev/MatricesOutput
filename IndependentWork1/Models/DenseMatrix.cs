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

        protected override IVector create(int size)
        {
            return new DenseVector(size);
        }

        public override void DoDrawBorder()
        {
            drawer.DrawBorder(this);
        }

        public override void Draw()
        {
            drawer.DrawMatrix(this);
        }

    }
}
