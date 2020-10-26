using IndependentWork1.Interfaces;
using IndependentWork1.Realization;
using System.Collections;
using System.Drawing;

namespace IndependentWork1.Models
{
    public abstract class SomeMatrix : IMatrix
    {
        public IVector[] matrix;
        protected IDrawer drawer;

        public IVector[] Matrix
        {
            get { return matrix; }
        }

        public IDrawer Drawer { 
            get { return drawer; }
            set { drawer = value; } 
        }

        protected SomeMatrix()
        {
            this.drawer = new ConsoleDrawer();
        }
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

        public IVector this[int rowIndex]
        {
            get { return matrix[rowIndex]; }
            set
            {
                if (value is IVector)
                {
                    matrix[rowIndex] = value;
                }
            }
        }

        public abstract void Draw();

        public abstract void DoDrawBorder();
     
        
        public void ClearDrawerWindowIfGrapics()
        {
            if (drawer is FormDrawer)
            {
                FormDrawer formDrawer = (FormDrawer) drawer;
                formDrawer.GraphicsObj.Clear(SystemColors.Control);
            }
        } 

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
