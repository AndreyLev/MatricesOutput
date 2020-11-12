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
using System.Windows.Forms;

namespace IndependentWork1.Decorator
{
    abstract class BaseDecorator : IMatrix
    {
        private IMatrix matrix;

        public BaseDecorator(IMatrix matrix)
        {
            this.matrix = matrix;
        }

        public IMatrix getMatrixSource()
        {
            BaseDecorator matrix_link = matrix as BaseDecorator;
            if (matrix_link != null) return matrix_link.getMatrixSource();
            return matrix;
        }

        public virtual double this[int rowIndex, int columnIndex]
        {

            get { return matrix[rowIndex,columnIndex]; }
            set {  }
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
            for (int i = 0; i < RowNumber; i++)
            {
                for (int j = 0; j < ColumnNumber; j++)
                {
                    drawer.DrawCellBorder(this, i, j);
                }
                drawer.DrawOnNewLine();
            }
            drawer.DrawMatrix();
        }

        public void DoDrawBorder(IDrawer drawer)
        {
            for (int i = 0; i < RowNumber; i++)
            {
                for (int j = 0; j < ColumnNumber; j++)
                {
                    drawer.DrawCellBorder(this, i, j);
                }
                drawer.DrawOnNewLine();
            }
            drawer.DrawBorder(this);
            drawer.DrawMatrix();
        }
    }
}
