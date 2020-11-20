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

        public override void Draw(IDrawer drawer, IVisitor visitor)
        {
            visitor.DrawMatrix(drawer, this);
        }

        protected override IVector create(int size)
        {
            return new DenseVector(size);
        }

  

    }
}
