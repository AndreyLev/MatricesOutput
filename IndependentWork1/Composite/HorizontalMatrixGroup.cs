using ClientPart.IndependentWork1.Interfaces;
using ClientPart.IndependentWork1.Iterator;
using ClientPart.IndependentWork1.Visitor;
using IndependentWork1.Interfaces;
using IndependentWork1.Models;
using IndependentWork1.Realization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClientPart.IndependentWork1.Composite
{
    public class HorizontalMatrixGroup : IMatrix
    {


        IDrawer drawer;
      
        List<IMatrix> matrixGroup;

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

        public HorizontalMatrixGroup(List<IMatrix> matrixGroup, IDrawer drawer)
        {
            this.matrixGroup = matrixGroup;
            this.drawer = drawer;
        }

        public IIterator getCommonIterator()
        {
            return new MatrixGroupIterator(matrixGroup);
        }

        public IIterator getAllMatricesIterator()
        {
            return new AllMatricesIterator(getAllMatrices());
        }
       

        public double this[int rowIndex, int columnIndex]
        {
            get
            {
                if (rowIndex >= RowNumber || columnIndex >= ColumnNumber) return 0;

                IMatrix desired_matrix = getDesiredMatrixIndex(rowIndex, ref columnIndex);

                return desired_matrix[rowIndex, columnIndex];
            }
            set
            {
                if (rowIndex < RowNumber && columnIndex < ColumnNumber)
                {

                    IMatrix desired_matrix = getDesiredMatrixIndex(rowIndex, ref columnIndex);

                    desired_matrix[rowIndex, columnIndex] = value;
                }

            }
        }

        private int MapColumnIndex(int columnIndex)
        {
            int IndexMapping = -1;
            
            return IndexMapping;
        }
        public IMatrix getDesiredMatrixIndex(int rowIndex, ref int columnIndex)
        {
            List<IMatrix> matricesLinksList = getAllMatrices();
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

            IMatrix desired_matrix = matricesLinksList[desiredMatrixIndex];

            columnIndex = desired_matrix.ColumnNumber - (intermediateColumnSum - columnIndex);
            return desired_matrix;
        }

        private List<IMatrix> getAllMatrices()
        {
            List<IMatrix> matrixList = new List<IMatrix>();
            if (matrixGroup == null) return new List<IMatrix>(0);
            foreach (IMatrix mx in matrixGroup)
            {
                if (mx is SomeMatrix)
                {
                    matrixList.Add(mx);
                }

                if (mx is HorizontalMatrixGroup)
                {
                    HorizontalMatrixGroup mg = (HorizontalMatrixGroup)mx;
                    matrixList.AddRange(mg.getAllMatrices());
                }
            }

            return matrixList;
        }

        public void AddMatrix(IMatrix matrix)
        {
            matrixGroup.Add(matrix);
        } 


        public void Draw(IDrawer drawer)
        {

            for (int i = 0; i < RowNumber; i++)
            {
                foreach (IMatrix matrix in matrixGroup)
                {
                        for (int j = 0; j < matrix.ColumnNumber; j++)
                        {
                            drawer.DrawCellBorder(matrix, i, j);
                        }      
                }
            }
            drawer.DrawMatrix(this);
        }

        public void DoDrawBorder(IDrawer drawer)
        {
            
            for (int i = 0; i < RowNumber; i++)
            {  
                foreach (IMatrix matrix in matrixGroup)
                {   
                    for (int j = 0; j < matrix.ColumnNumber; j++)
                    {
                        drawer.DrawCellBorder(matrix, i, j);
                    }
                }
            }
            drawer.DrawBorder(this);
            drawer.DrawMatrix(this);
        }

        public void Accept(IVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public void Draw(IDrawer drawer, IVisitor visitor)
        {
          for (int i = 0; i < RowNumber; i++)
            {
                for (int e = 0; e < matrixGroup.Count; e++)
                {
                    for (int j = 0; j < matrixGroup[e].ColumnNumber; j++)
                    {
                        visitor.visitMatrixElement(matrixGroup[e], i, j);
                    }
                }
            }
            drawer.DrawMatrix(this);

        }

        public void DrawCell(IMatrix matrix, int rowIndex, int columnIndex, IVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public void DrawCellBorder(IMatrix matrix, int rowIndex, int columnIndex, IVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
