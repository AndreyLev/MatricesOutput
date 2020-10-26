using IndependentWork1.Interfaces;
using IndependentWork1.Models;
using IndependentWork1.Realization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ClientPart.IndependentWork1.Composite
{
    class HorizontalMatrixGroup : IMatrix
    {

        List<IMatrix> matrixGroup;
        List<IMatrix> matricesLinksList;
        DenseVector[] matrix;
        IDrawer drawer;

        public List<IMatrix> MatricesList { get { return matricesLinksList; } }

        public List<IMatrix> MatrixGroup { get { return matrixGroup; } }
        public HorizontalMatrixGroup(List<IMatrix> matrixGroup, IDrawer drawer)
        {
            this.matrixGroup = matrixGroup;
            this.drawer = drawer;
            foreach (IMatrix matrix in matrixGroup)
            {
                matrix.Drawer = drawer;
            }
            matricesLinksList = getAllMatrices(this);
            CollectTheMatrix();
            //collectTheMatrix();
        }

        private void FillValuesWithIndex(int columnIndexOne, int columnIndexTwo, IMatrix matrixLink)
        {
            for (int i = 0; i < RowNumber; i++)
            {
                for (int j = columnIndexOne, k = 0; 
                    j < columnIndexTwo && k < matrixLink.ColumnNumber; j++, k++)
                {
                    matrix[i][j] = matrixLink[i, k];
                }
            }
        }
        private void CollectTheMatrix()
        {
            matrix = new DenseVector[RowNumber];
            for (int i = 0; i < matrix.Length; i++)
            {
                matrix[i] = new DenseVector(ColumnNumber);
            }

            int currentIndexOne = 0;
            int currentIndexTwo = 0;
            foreach (IMatrix m in matrixGroup)
            {
                currentIndexTwo += m.ColumnNumber;
                FillValuesWithIndex(currentIndexOne, currentIndexTwo, m);
                currentIndexOne += m.ColumnNumber;
            }
        }
        private void collectTheMatrixByMatricesLinks()
        {
            matrix = new DenseVector[RowNumber];
            for (int i = 0; i < matrix.Length; i++)
            {
                matrix[i] = new DenseVector(ColumnNumber);
            }

            int temp;
            int matrixIndex;
            for (int i = 0; i < RowNumber; i++)
            {
                for (int j = 0; j < ColumnNumber; j++)
                {
                    temp = j;
                    matrixIndex = getDesiredMatrixIndex(i, ref temp);
                    matrix[i][j] = matricesLinksList[matrixIndex][i, temp];
                }
            }

        }

        private int getDesiredMatrixIndex(int rowIndex, ref int columnIndex)
        {
            int intermediateColumnSum = 0;
            int desiredMatrixIndex = 0;
            for (int i = 0; i < matricesLinksList.Count; i++)
            {
                intermediateColumnSum += matricesLinksList[i].ColumnNumber;
                if (columnIndex < intermediateColumnSum)
                {
                    desiredMatrixIndex = i;
                    break;
                }
                desiredMatrixIndex++;
            }

            columnIndex = matricesLinksList[desiredMatrixIndex].ColumnNumber - (intermediateColumnSum - columnIndex);
            return desiredMatrixIndex;
        }

        public List<IMatrix> getAllMatrices(IMatrix matrix)
        {
            List<IMatrix> matrixList = new List<IMatrix>();
            if (matrix == null) return new List<IMatrix>(0); 
            foreach (IMatrix mx in matrixGroup)
            {
                if (mx is SomeMatrix)
                {
                    matrixList.Add(mx);
                }

                if (mx is HorizontalMatrixGroup)
                {
                    HorizontalMatrixGroup mg = (HorizontalMatrixGroup)mx;
                    matrixList.AddRange(mg.getAllMatrices(mg));
                }
            }

            return matrixList;
        }

        public void AddMatrix(IMatrix matrix)
        {
            matrixGroup.Add(matrix);
            matricesLinksList = getAllMatrices(this);
            DenseVector[] newMatrix = new DenseVector[matrix.RowNumber];
        } 

        public IVector this[int rowIndex] {
            get
            {
                if (rowIndex >= RowNumber) return null;

                return matrix[rowIndex];
            }
            set
            {
                if (rowIndex >= RowNumber) throw new InvalidOperationException();

                if (value is IVector)
                {
                    matrix[rowIndex] = (DenseVector)value;
                }
            }
        }

    

        public double this[int rowIndex, int columnIndex] {
            get
            {
                if (rowIndex >= RowNumber) throw new InvalidOperationException();
                if (columnIndex >= ColumnNumber) throw new InvalidOperationException();

                return matrix[rowIndex][columnIndex];
            }
            set
            {
                if (rowIndex >= RowNumber) throw new InvalidOperationException();
                if (columnIndex >= ColumnNumber) throw new InvalidOperationException();

                matrix[rowIndex][columnIndex] = value;
            }
        }

        public int RowNumber
        {
            get
            {
                return matrixGroup.Select(matrix => matrix.RowNumber).Max();
            }
        }

        public int ColumnNumber
        {
            get
            {
                return matrixGroup.Select(matrix => matrix.ColumnNumber).Sum();
            }
        }

        public IDrawer Drawer {
            get { return drawer; }
            set 
            {
                drawer = value;
            }
        }
        public void DoDrawBorder()
        {
            if (matrixGroup.Count > 0)
            {
                drawer.DrawBorder(this);
            }
        }

        public void Draw()
        {
            if (matrixGroup.Count > 0)
            {
                drawer.DrawMatrix(this);
            }
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
