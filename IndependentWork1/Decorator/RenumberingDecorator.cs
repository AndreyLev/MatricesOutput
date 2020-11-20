using ClientPart.IndependentWork1.Visitor;
using IndependentWork1.Interfaces;
using IndependentWork1.Models;
using IndependentWork1.Realization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Odbc;
using System.Linq;
using System.Linq.Expressions;

namespace IndependentWork1.Decorator
{
    class RenumberingDecorator : IMatrix
    {
        IMatrix matrix;
        int[] rows;
        int[] columns;
        public RenumberingDecorator(IMatrix matrix)
        {
            this.matrix = matrix;

            rows = new int[matrix.RowNumber];
            for (int i = 0; i < matrix.RowNumber; i++)
            {
                rows[i] = i;
            }

            columns = new int[matrix.ColumnNumber];
            for (int i = 0; i < matrix.ColumnNumber; i++)
            {
                columns[i] = i;
            }
        }

        public double this[int rowIndex, int columnIndex] {
            get
            {
                return matrix[rows[rowIndex], columns[columnIndex]];
            }
            set { }
        }

        public int RowNumber => matrix.RowNumber;

        public int ColumnNumber => matrix.ColumnNumber;

        public void RenumberRows(int rowIndexOne, int rowIndexTwo)
        {
            rows[rowIndexOne] = rowIndexTwo;
            rows[rowIndexTwo] = rowIndexOne;          
        }
         
        public void RenumberColumns(int columnIndexOne, int columnIndexTwo)
        {
            columns[columnIndexOne] = columnIndexTwo;
            columns[columnIndexTwo] = columnIndexOne;
        }

        public void Draw(IDrawer drawer, IVisitor visitor)
        {
            for (int i = 0; i < RowNumber; i++)
            {
                for (int j = 0; j < ColumnNumber; j++)
                {
                    drawer.DrawCell(this, i, j);
                }
            }
            drawer.DrawMatrix(this);
        }
    }
}
