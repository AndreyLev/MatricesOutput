using ClientPart.IndependentWork1.Composite;
using ClientPart.IndependentWork1.Decorator;
using ClientPart.IndependentWork1.Interfaces;
using ClientPart.IndependentWork1.Strategy;
using IndependentWork1.Decorator;
using IndependentWork1.Interfaces;
using IndependentWork1.Models;
using IndependentWork1.Realization;
using System;
using System.Collections.Generic;
using static ClientPart.IndependentWork1.Constants.MatrixCellFormatConstants;

namespace ClientPart.IndependentWork1.Visitor
{
    class DrawMatrixVisitor : IVisitor
    {
        IDrawer drawer;
        IConfigureCellStrategy cellStrategy;

        public DrawMatrixVisitor(IDrawer drawer)
        {
            this.drawer = drawer;
            cellStrategy = new ConfigureCommonCellStrategy();
        }

        private void setStrategyIfMatrixIs(IMatrix matrix, int rowIndex, int columnIndex, int addIndex, IMatrix deco)
        {
            switch (matrix)
            {
                case SparseMatrix sparseMatrix:
                    if (deco[rowIndex, columnIndex] == 0)
                    {
                        cellStrategy = new ConfigureSparceMatrixCellStrategy();
                    }
                    else
                        cellStrategy = new ConfigureCommonCellStrategy();
                    break;
                case HorizontalMatrixGroup matrixGroup:
                    //int j_copy = addIndex;
                    //IMatrix matr = matrixGroup.getDesiredMatrixIndex(rowIndex, ref j_copy);
                    //setStrategyIfMatrixIs(matr, rowIndex, columnIndex,j_copy,deco);
                    if (deco[rowIndex, columnIndex] == 0)
                    {
                        cellStrategy = new ConfigureSparceMatrixCellStrategy();
                    }
                    else
                        cellStrategy = new ConfigureCommonCellStrategy();
                    break;
                default:
                    cellStrategy = new ConfigureCommonCellStrategy();
                    break;
            }
        }
        public void visitDenseMatrix(DenseMatrix matrix)
        {
            for (int i = 0; i < matrix.RowNumber; i++)
            {
                for (int j = 0; j < matrix.ColumnNumber; j++)
                {
                    //cellStrategy = new ConfigureCommonCellStrategy();
                    setStrategyIfMatrixIs(matrix, i, j, j, matrix);
                    visitMatrixElement(matrix, i, j);
                }
            }
            visitMatrix(matrix);
        }

        public void visitSparseMatrix(SparseMatrix matrix)
        {
            for (int i = 0; i < matrix.RowNumber; i++)
            {
                for (int j = 0; j < matrix.ColumnNumber; j++)
                {
                    //if (matrix[i,j] == 0)
                    //{
                    //    cellStrategy = new ConfigureSparceMatrixCellStrategy();
                    //}
                    //else
                    //    cellStrategy = new ConfigureCommonCellStrategy();
                    setStrategyIfMatrixIs(matrix, i, j, j, matrix);
                    visitMatrixElement(matrix, i, j);
                }
            }
            visitMatrix(matrix);
        }

        public void visitMatrixElement(IMatrix matrix, int rowIndex, int columnIndex)
        {
            drawer.setStrategy(cellStrategy);
            drawer.DrawCellBorder(matrix, rowIndex, columnIndex);
        }

        public void visitMatrix(IMatrix matrix)
        {
            drawer.DrawMatrix(matrix);
        }

        public void visitMatrixBorder(IMatrix matrix)
        {
            drawer.DrawBorder(matrix);
        }

        //private void fillHorizontalMatrixRowBuff(int rowNumber, HorizontalMatrixGroup matrix)
        //{
        //    IIterator iter = matrix.getCommonIterator();

        //        while (iter.hasMore())
        //        {
        //            IMatrix matr = iter.getNext();
        //            switch (matr)
        //            {
        //                case HorizontalMatrixGroup mGroup:
        //                    fillHorizontalMatrixRowBuff(rowNumber, mGroup);
        //                    break;
        //                default:
        //                    for (int col = 0; col < matr.ColumnNumber; col++)
        //                    {
        //                        switch (matr)
        //                        {
        //                            case SparseMatrix sparseMatrix:
        //                                if (sparseMatrix[rowNumber, col] == 0)
        //                                {
        //                                    cellStrategy = new ConfigureSparceMatrixCellStrategy();
        //                                }
        //                                else
        //                                    cellStrategy = new ConfigureCommonCellStrategy();
        //                                break;
        //                            default:
        //                                cellStrategy = new ConfigureCommonCellStrategy();
        //                                break;
        //                        }
        //                    visitMatrixElement(matr, rowNumber, col);
        //                    }
        //                    break;
        //            }
        //        }
        //        iter.Reset();
        //}
        public void visitHorizontalMatrixGroup(HorizontalMatrixGroup matrix)
        {
            //for (int i = 0; i < matrix.RowNumber; i++)
            //{
            //    fillHorizontalMatrixRowBuff(i, matrix);
            //}
            for (int i = 0; i < matrix.RowNumber; i++)
            {
                for (int j = 0; j < matrix.ColumnNumber; j++)
                {
                    setStrategyIfMatrixIs(matrix, i, j, j, matrix);
                    visitMatrixElement(matrix, i, j);
                }
            }
            visitMatrix(matrix);
        }

        public void visitRenumberingDecorator(RenumberingDecorator decorator)
        {
            for (int i = 0; i < decorator.RowNumber; i++)
            {
                for (int j = 0; j < decorator.ColumnNumber; j++)
                {
                    setStrategyIfMatrixIs(decorator.GetSource(decorator),i,j,j,decorator);
                    visitMatrixElement(decorator, i, j);
                }
            }
            visitMatrix(decorator);
        }

        public void visitTransponseDecorator(TransponseDecorator decorator)
        {
            //switch (decorator.GetSource(decorator))
            //{
            //    case HorizontalMatrixGroup group:
            //        IIterator iter = group.getCommonIterator();
            //        int rowCounter = 0;
            //        while (iter.hasMore())
            //        {
            //            IMatrix m = iter.getNext();
            //            for (int i = 0; i < m.RowNumber; i++)
            //            {
            //                for (int j = 0; j < decorator.ColumnNumber; j++)
            //                {
            //                    setStrategyIfMatrixIs(m, rowCounter, j, j, decorator);
            //                    visitMatrixElement(decorator, rowCounter, j);
            //                }
            //                rowCounter++;
            //            }
            //        }
            //        iter.Reset();
            //        break;
            //    default:
            //        for (int i = 0; i < decorator.RowNumber; i++)
            //        {
            //            for (int j = 0; j < decorator.ColumnNumber; j++)
            //            {
            //                setStrategyIfMatrixIs(decorator.GetSource(decorator), i, j, j, decorator);
            //                visitMatrixElement(decorator, i, j);
            //            }
            //        }
            //        break;
            //}
            for (int i = 0; i < decorator.RowNumber; i++)
            {
                for (int j = 0; j < decorator.ColumnNumber; j++)
                {
                    setStrategyIfMatrixIs(decorator.GetSource(decorator), i, j, j, decorator);
                    visitMatrixElement(decorator, i, j);
                }
            }
            visitMatrix(decorator);
        }
    }
}
