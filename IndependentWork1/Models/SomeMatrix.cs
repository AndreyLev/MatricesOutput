using IndependentWork1.Interfaces;
using IndependentWork1.Realization;
using System.Collections;
using System.Drawing;

namespace IndependentWork1.Models
{
    public abstract class SomeMatrix : IMatrix
    {
        private IVector[] matrix;

        protected SomeMatrix(int rowCount, int columnNumber)
        {
            matrix = new IVector[rowCount];
            for (int i = 0; i < rowCount; i++)
            {
                matrix[i] = create(columnNumber);
            }
            RowNumber = rowCount;
            ColumnNumber = columnNumber;
        }

        protected abstract IVector create(int size);
        public virtual double this[int rowIndex, int columnIndex]
        { 
            get {
                if (rowIndex >= RowNumber || columnIndex >= ColumnNumber) return 0;
                return matrix[rowIndex][columnIndex]; 
            }
            set {
                if (!(rowIndex >= RowNumber || columnIndex >= ColumnNumber))
                    matrix[rowIndex][columnIndex] = value; 
            }
        }

        public int RowNumber { get; protected set;  }

        public int ColumnNumber { get; protected set;  }


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
