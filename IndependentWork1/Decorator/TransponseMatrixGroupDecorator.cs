using ClientPart.IndependentWork1.Composite;
using ClientPart.IndependentWork1.Interfaces;
using ClientPart.IndependentWork1.Visitor;
using IndependentWork1.Decorator;
using IndependentWork1.Interfaces;
using IndependentWork1.Models;
using IndependentWork1.Realization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndependentWork1.Decorator
{
    class TransponseMatrixGroupDecorator: IMatrix
    {
        IMatrix matrix;
        HorizontalMatrixGroup matrixGroup;
        IIterator groupIterator;

        public double this[int rowIndex, int columnIndex]
        {
            get
            {
               

                if (rowIndex >= RowNumber || columnIndex >= ColumnNumber) return 0;

                IMatrix desired_matrix = getDesiredMatrix(ref rowIndex);
                
                return desired_matrix[rowIndex, columnIndex];
            }
            set
            {
                if (rowIndex < RowNumber && columnIndex < ColumnNumber)
                {

                    IMatrix desired_matrix = getDesiredMatrix(ref rowIndex);

                    desired_matrix[rowIndex, columnIndex] = value;
                }

            }
        }
        public TransponseMatrixGroupDecorator(IMatrix matrix)
        {
            this.matrix = matrix;
            matrixGroup = (HorizontalMatrixGroup) matrix;
            groupIterator = matrixGroup.getCommonIterator();
            RowNumber = getRowNumber();
            ColumnNumber = getColumnNumber();
        }

        private List<IMatrix> getMatrixGroupList()
        {
            List<IMatrix> matrixList = new List<IMatrix>();
            while (groupIterator.hasMore())
            {
                matrixList.Add(groupIterator.getNext());
            }

            groupIterator.Reset();
            return matrixList;
        }
        private void testFunc()
        {
           
           while (groupIterator.hasMore())
            {
                
                IMatrix matr = groupIterator.getNext();
                for (int i = 0; i < matr.RowNumber; i++)
                {
                    for (int j = 0; j < matr.ColumnNumber; j++)
                    {
                        Console.Write("{0,-4:00.00} ", matr[i, j]);
                    }
                    Console.WriteLine();
                }
            }
            groupIterator.Reset();
        }

        public IMatrix getDesiredMatrix(ref int rowIndex)
        {
            int intermediateRowSum = 0;
            int desiredMatrixIndex = 0;

            while (groupIterator.hasMore())
            {
                intermediateRowSum += groupIterator.getNext().RowNumber;
                if (rowIndex < intermediateRowSum)
                {
                    desiredMatrixIndex = groupIterator.CurrentIndex-1;
                    break;
                }
            }

            groupIterator.Reset();
         
            IMatrix desired_matrix = getMatrixGroupList()[desiredMatrixIndex];

            rowIndex = desired_matrix.RowNumber - (intermediateRowSum - rowIndex);

            return desired_matrix;
        }

        private int getRowNumber()
        {
           
                int rowCount = 0;
                while (groupIterator.hasMore())
                {
                    rowCount += groupIterator.getNext().RowNumber;
                }
                groupIterator.Reset();
                return rowCount;
        }

        private int getColumnNumber()
        {
            List<int> columnCountValues = new List<int>();
            while (groupIterator.hasMore())
            {
                columnCountValues.Add(groupIterator.getNext().ColumnNumber);
            }
            groupIterator.Reset();
            return columnCountValues.Max();
        }


        public  int RowNumber
        {
            get;
        }

        public  int ColumnNumber
        {
            get;
        }

        public void Draw(IDrawer drawer, IVisitor visitor)
        {
            for (int i = 0; i < RowNumber; i++)
            {
                for (int j = 0; j < ColumnNumber; j++)
                {
                    drawer.DrawCellBorder(this, i, j);
                }
            }
            drawer.DrawMatrix(this);
        }

     



    }
}
