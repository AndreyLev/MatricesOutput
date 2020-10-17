using IndependentWork1.Interfaces;
using IndependentWork1.Realisation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace IndependentWork1.Models
{
    public class SparseMatrix : SomeMatrix
    {
        new SparseVector[] matrix;

        public SparseVector[] Matrix { 
            
            get
            {
                return matrix;
            } 
        }

        public override double this[int rowIndex, int columnIndex]
        {
            get
            {
                if (rowIndex >= RowNumber) return 0;
                if (columnIndex >= ColumnNumber) return 0;

                if (matrix[rowIndex].Vector.Keys.Contains(columnIndex))
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
            return new SparceMatrixEnumerator(matrix);
        }

        public void printMatrix()
        {
           
            for (int i = 0; i < matrix.Length; i++)
            {
                
                for (int k = 0; k < ColumnNumber; k++)
                {
                    if (matrix[i].Vector.ContainsKey(k))
                    {
                        if (matrix[i][k] == 0) continue;
                        Console.Write("{0,5:##.#0}\t ", matrix[i][k]);
                    }
                    
                    
                    
                    
                }
                Console.WriteLine();
            }
        }

        public void collapse()
        {
            for (int i = 0; i < RowNumber; i++)
            {
                for (int j = 0; j < ColumnNumber; j++)
                {
                    if (matrix[i][j] == 0)
                    {
                       matrix[i].removeElement(j);
                     
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
