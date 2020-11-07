using IndependentWork1.Interfaces;
using IndependentWork1.Realization;
using System;
using System.Collections;

namespace IndependentWork1.Models
{
    public class SparseMatrix : SomeMatrix
    {


        protected override IVector create(int size)
        {
            return new SparseVector(size);
        }

        public SparseMatrix(int rowCount, int columnCount) : base(rowCount, columnCount)
        {
        }

        public override void Draw()
        {
            drawer.DrawMatrix(this);
        }

        public override void DoDrawBorder()
        {
            drawer.DrawBorder(this);
        }
    }
}
