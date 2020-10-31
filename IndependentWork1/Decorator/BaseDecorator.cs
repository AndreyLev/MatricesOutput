using IndependentWork1.Interfaces;
using IndependentWork1.Realization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndependentWork1.Decorator
{
    abstract class BaseDecorator : IMatrix
    {
        protected IMatrix matrix;
        public BaseDecorator(IMatrix matrix)
        {
            this.matrix = matrix;
        }

        public IMatrix MATRIX { get { return matrix; } }
        public IVector this[int rowIndex]
        {
            get { return matrix[rowIndex]; }
            set { matrix[rowIndex] = value; }
        }
        public virtual double this[int rowIndex, int columnIndex]
        {
            get { return matrix[rowIndex, columnIndex]; }
            set { matrix[rowIndex, columnIndex] = value; }
        }

        public virtual int RowNumber { get { return matrix.RowNumber; } }

        public virtual int ColumnNumber { get { return matrix.ColumnNumber; } }

        public IDrawer Drawer
        {
            get { return matrix.Drawer; }
            set { matrix.Drawer = value; }
        }

        public void DoDrawBorder()
        {
            Drawer.DrawBorder(this);
        }

        public void Draw()
        {
            Drawer.DrawMatrix(this);
        }

        public IEnumerator GetEnumerator()
        {
            return matrix.GetEnumerator();
        }

        public double getValue(int rowIndex, int columnIndex)
        {
            return this[rowIndex, columnIndex];
        }

        public int setValue(double value, int rowIndex, int columnIndex)
        {
            this[rowIndex, columnIndex] = value;
            return 1;
        }
    }
}
