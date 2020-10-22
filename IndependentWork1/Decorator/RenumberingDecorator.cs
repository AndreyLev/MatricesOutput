using IndependentWork1.Interfaces;
using IndependentWork1.Realization;
using System;
using System.Collections;
using System.Collections.Generic;

namespace IndependentWork1.Decorator
{
    class RenumberingDecorator : IMatrix
    {
        IMatrix matrix;
        List<KeyValuePair<int,int>> rowChanges;
        List<KeyValuePair<int, int>> columnChanges;
        bool addFlag = true;

        private void ClearChanges()
        {
            rowChanges.Clear();
            columnChanges.Clear();
        }
        public RenumberingDecorator(IMatrix matrix)
        {
            this.matrix = matrix;
            rowChanges = new List<KeyValuePair<int, int>>();
            columnChanges = new List<KeyValuePair<int, int>>();
        }
        public double this[int rowIndex, int columnIndex]
        {
            get { return matrix[rowIndex, columnIndex]; }
            set { matrix[rowIndex, columnIndex] = value; }
        }

        public int RowNumber
        {
            get { return matrix.RowNumber; }
        }

        public int ColumnNumber
        {
            get { return matrix.ColumnNumber; }
        }

        public IDrawer Drawer 
        { 
            get { return matrix.Drawer; }
            set { matrix.Drawer = value; }
        }

        public IVector this[int rowIndex]
        {
            get { return matrix[rowIndex]; }
            set { matrix[rowIndex] = value; }
        }

        public void DoDrawBorder()
        {
            matrix.DoDrawBorder();
        }

        public void Draw()
        {
            matrix.Draw();
        }

        public IEnumerator GetEnumerator()
        {
            return matrix.GetEnumerator();
        }

        public double getValue(int rowIndex, int columnIndex)
        {
            return matrix.getValue(rowIndex, columnIndex);
        }

        public int setValue(double value, int rowIndex, int columnIndex)
        {
            return matrix.setValue(value, rowIndex, columnIndex);
        }

        public void RenumberRows(int rowOneIndex, int rowTwoIndex)
        {
            if (rowOneIndex >= matrix.RowNumber || rowTwoIndex >= matrix.RowNumber)
                throw new InvalidOperationException();

            double temp;
            for (int i = 0; i < matrix.ColumnNumber; i++)
            {
                temp = matrix[rowOneIndex][i];
                matrix[rowOneIndex,i] = matrix[rowTwoIndex,i];
                matrix[rowTwoIndex, i] = temp;
            }

            if (addFlag)
                rowChanges.Add(new KeyValuePair<int, int>(rowOneIndex, rowTwoIndex));
        }

        public void RenumberColumns(int columnOneIndex, int columnTwoIndex)
        {
            if (columnOneIndex >= matrix.ColumnNumber || columnTwoIndex >= matrix.ColumnNumber)
                throw new InvalidOperationException();

            for (int i = 0; i < matrix.RowNumber; i++)
            {
                double temp = matrix[i, columnOneIndex];
                matrix[i, columnOneIndex] = matrix[i, columnTwoIndex];
                matrix[i, columnTwoIndex] = temp;
            }
            
            if (addFlag)
                columnChanges.Add(new KeyValuePair<int, int>(columnOneIndex, columnTwoIndex));
        }

        public void ResetMatrix()
        {
            addFlag = false;
            for (int i = rowChanges.Count-1; i >= 0; i--)
            {
                RenumberRows(rowChanges[i].Key, rowChanges[i].Value);
            }

            for (int i = columnChanges.Count - 1; i >= 0; i--)
            {
                RenumberColumns(columnChanges[i].Key, columnChanges[i].Value);
            }

            ClearChanges();

            addFlag = true;
        }

        
    }
}
