using IndependentWork1.Interfaces;
using IndependentWork1.Models;
using IndependentWork1.Realization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace ClientPart.IndependentWork1.Composite
{
    class HorizontalMatrixGroup : IMatrix
    {


        DenseMatrix matrix;
        IDrawer drawer;
        List<IMatrix> matrixGroup;

        public List<IMatrix> MATRIX_GROUP { get => matrixGroup; }

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
            get => drawer;
            set => drawer = value;
        }

        public HorizontalMatrixGroup(List<IMatrix> matrixGroup, IDrawer drawer)
        {
            this.matrixGroup = matrixGroup;
            this.drawer = drawer;
            foreach (IMatrix matrix in matrixGroup)
            {
                // matrix.Drawer = drawer;
            }
            CollectTheMatrix();
        }


        private void FillValuesWithIndex(int columnIndexOne, int columnIndexTwo, IMatrix matrixLink)
        {
            for (int i = 0; i < RowNumber; i++)
            {
                for (int j = columnIndexOne, k = 0; 
                    j < columnIndexTwo && k < matrixLink.ColumnNumber; j++, k++)
                {
                    matrix[i,j] = matrixLink[i, k];
                }
            }
        }
        private void CollectTheMatrix()
        {
            matrix = new DenseMatrix(RowNumber, ColumnNumber);


            int currentIndexOne = 0;
            int currentIndexTwo = 0;
            foreach (IMatrix m in matrixGroup)
            {
                currentIndexTwo += m.ColumnNumber;
                FillValuesWithIndex(currentIndexOne, currentIndexTwo, m);
                currentIndexOne += m.ColumnNumber;
            }
        }
        

        //private int getDesiredMatrixIndex(int rowIndex, ref int columnIndex)
        //{
        //    int intermediateColumnSum = 0;
        //    int desiredMatrixIndex = 0;
        //    for (int i = 0; i < matricesLinksList.Count; i++)
        //    {
        //        intermediateColumnSum += matricesLinksList[i].ColumnNumber;
        //        if (columnIndex < intermediateColumnSum)
        //        {
        //            desiredMatrixIndex = i;
        //            break;
        //        }
        //        desiredMatrixIndex++;
        //    }

        //    columnIndex = matricesLinksList[desiredMatrixIndex].ColumnNumber - (intermediateColumnSum - columnIndex);
        //    return desiredMatrixIndex;
        //}

        //public List<IMatrix> getAllMatrices(IMatrix matrix)
        //{
        //    List<IMatrix> matrixList = new List<IMatrix>();
        //    if (matrix == null) return new List<IMatrix>(0); 
        //    foreach (IMatrix mx in matrixGroup)
        //    {
        //        if (mx is SomeMatrix)
        //        {
        //            matrixList.Add(mx);
        //        }

        //        if (mx is HorizontalMatrixGroup)
        //        {
        //            HorizontalMatrixGroup mg = (HorizontalMatrixGroup)mx;
        //            matrixList.AddRange(mg.getAllMatrices(mg));
        //        }
        //    }

        //    return matrixList;
        //}

        public void AddMatrix(IMatrix matrix)
        {
            matrixGroup.Add(matrix);
            CollectTheMatrix();
        } 

        public double this[int rowIndex, int columnIndex] {
            get
            {
                if (rowIndex >= RowNumber || columnIndex >= ColumnNumber)
                    return 0;

                return matrix[rowIndex,columnIndex];
            }
            set
            {
                if (rowIndex < RowNumber & columnIndex < ColumnNumber)
                    matrix[rowIndex,columnIndex] = value;
            }
        }

        public void DoDrawBorder()
        {
            if (matrixGroup.Count > 0)
            {
                
            }
        }

        public void Draw()
        {
            if (matrixGroup.Count > 0)
            {
                
            }
        }

        public void Draw(IDrawer drawer)
        {
            throw new NotImplementedException();
        }

        public void DoDrawBorder(IDrawer drawer)
        {
            throw new NotImplementedException();
        }
    }
}
