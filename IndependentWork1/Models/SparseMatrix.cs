using IndependentWork1.Realization;
using System;
using System.Collections;

namespace IndependentWork1.Models
{
    public class SparseMatrix : SomeMatrix
    {


        public override double this[int rowIndex, int columnIndex]
        {
            get
            {
                if (rowIndex >= RowNumber) return 0;
                if (columnIndex >= ColumnNumber) return 0;
                SparseVector temp = (SparseVector)matrix[rowIndex];
                if (temp.Vector.ContainsKey(columnIndex))
                {
                    return matrix[rowIndex][columnIndex];
                }

                return 0;
            }
            set
            {
                if (rowIndex < RowNumber && columnIndex < ColumnNumber)
                    matrix[rowIndex][columnIndex] = value;
            }
        }

        public SparseMatrix(int rowCount, int columnCount, IDrawer drawer) : base(drawer)
        {
            matrix = new SparseVector[rowCount];
            for (int i = 0; i < matrix.Length; i++)
            {
                matrix[i] = new SparseVector(columnCount);
            }
            RowNumber = rowCount;
            ColumnNumber = columnCount;
        }
        public override IEnumerator GetEnumerator()
        {
            return new SparceMatrixEnumerator((SparseVector[])matrix);
        }

        public void collapse()
        {
            SparseVector[] c_matrix = (SparseVector[])matrix;
            for (int i = 0; i < RowNumber; i++)
            {
                for (int j = 0; j < ColumnNumber; j++)
                {
                    if (this[i, j] == 0)
                    {
                        c_matrix[i].removeElement(j);

                    }
                }
            }
        }

        public override void Draw()
        {
            ClearDrawerWindowIfGrapics();
            drawer.DrawMatrix(this);
        }

        public override void DoDrawBorder()
        {
            ClearDrawerWindowIfGrapics();
            drawer.DrawBorder(this);
        }
    }
}
