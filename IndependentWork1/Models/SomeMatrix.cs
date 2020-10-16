using IndependentWork1.Interfaces;
using IndependentWork1.Realisation;
using System;
using System.Collections;
using System.Text;

namespace IndependentWork1.Models
{
    public abstract class SomeMatrix : IMatrix
    {
        public IVector[] matrix;
        protected IDrawer drawer;

        public IVector[] VectorArr
        {
            get { return matrix; }
        }

        protected SomeMatrix(IDrawer drawer)
        {
            this.drawer = drawer;
        }
        public virtual double this[int rowIndex, int columnIndex]
        { 
            get { return matrix[rowIndex][columnIndex];  }
            set { matrix[rowIndex][columnIndex] = value; }
        }

        public int RowNumber { get; protected set;  }

        public int ColumnNumber { get; protected set;  }

        public abstract void Draw();

        public abstract void DoDrawBorder();
        

        public abstract IEnumerator GetEnumerator();
        

        public double getValue(int rowIndex, int columnIndex)
        {
            return matrix[rowIndex][columnIndex];
        }

        public int setValue(double value, int rowIndex, int columnIndex)
        {
            matrix[rowIndex][columnIndex] = value;
            return 1;
        }
    }
}
