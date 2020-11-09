using IndependentWork1.Interfaces;
using IndependentWork1.Realization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndependentWork1.Decorator
{
    abstract class BaseDecorator : IMatrix
    {
        private IMatrix matrix;

        private IVector[] decorMatrix;
        public BaseDecorator(IMatrix matrix)
        {
            this.matrix = matrix;
            decorMatrix = new IVector[RowNumber];
            for (int i = 0; i < RowNumber; i++)
            {
                decorMatrix[i] = Create(ColumnNumber);
            }
        }

        protected abstract IVector Create(int size);

        public IMatrix getMatrixSource()
        {
            BaseDecorator matrix_link = matrix as BaseDecorator;
            if (matrix_link != null) return matrix_link.getMatrixSource();
            return matrix;
        }

        public virtual double this[int rowIndex, int columnIndex]
        {

            get { return decorMatrix[rowIndex][columnIndex]; }
            set { decorMatrix[rowIndex][columnIndex] = value; }
        }

        public virtual int RowNumber { get { return matrix.RowNumber; } }

        public virtual int ColumnNumber { get { return matrix.ColumnNumber; } }

        public void DoDrawBorder()
        {
           
        }

        public void Draw()
        {
           
        }

        public void Draw(IDrawer drawer)
        {
            throw new NotImplementedException();
        }

        public void DoDrawBorder(IDrawer drawer)
        {
            throw new NotImplementedException();
        }
    }
}
