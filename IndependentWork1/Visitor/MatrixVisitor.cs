using ClientPart.IndependentWork1.Composite;
using ClientPart.IndependentWork1.Interfaces;
using ClientPart.IndependentWork1.Strategy;
using IndependentWork1.Interfaces;
using IndependentWork1.Models;
using IndependentWork1.Realization;
using System;
using System.Collections.Generic;
using static ClientPart.IndependentWork1.Constants.MatrixCellFormatConstants;

namespace ClientPart.IndependentWork1.Visitor
{
    class MatrixVisitor : IVisitor
    {
        IConfigureCellStrategy strategyOne;
        IConfigureCellStrategy strategyTwo;

        public MatrixVisitor()
        {
            strategyOne = new ConfigureCommonCellStrategy();
            strategyTwo = new ConfigureSparceMatrixStrategy();
        }
        private void DrawCell(IDrawer drawer, DenseMatrix matrix, int rowIndex, int columnIndex)
        {
            drawer.ElementTemplate = commonCellTemplate;
            drawer.DrawCellBorder(matrix, rowIndex, columnIndex);
        }

        private void DrawCell(IDrawer drawer, SparseMatrix matrix, int rowIndex, int columnIndex)
        {
            if (matrix[rowIndex, columnIndex] == 0)
            {
                drawer.ElementTemplate = emptyCellTemplate;
            }
            else
            {
                drawer.ElementTemplate = commonCellTemplate;
            }

            drawer.DrawCellBorder(matrix, rowIndex, columnIndex);
        }

        private void DrawCell(IDrawer drawer, HorizontalMatrixGroup matrix, int rowIndex, int columnIndex)
        {
            drawer.ElementTemplate = commonCellTemplate;
            drawer.DrawCellBorder(matrix, rowIndex, columnIndex);
        }
        public void DrawMatrix(IDrawer drawer, DenseMatrix matrix)
        {
            drawer.setStrategy(strategyOne);

            for (int i = 0; i < matrix.RowNumber; i++)
            {
                for (int j = 0; j < matrix.ColumnNumber; j++)
                {
                    DrawCell(drawer, matrix, i, j);
                }
            }
            drawer.DrawMatrix(matrix);
        }

        public void DrawMatrix(IDrawer drawer, SparseMatrix matrix)
        {
            for (int i = 0; i < matrix.RowNumber; i++)
            {
                for (int j = 0; j < matrix.ColumnNumber; j++)
                {
                    if (matrix[i,j] == 0)
                    {
                        drawer.setStrategy(strategyTwo);
                    } else
                    {
                        drawer.setStrategy(strategyOne);
                    }
                    drawer.DrawCellBorder(matrix,i,j);
                }
            }
            drawer.DrawMatrix(matrix);
        }

        public void DrawMatrix(IDrawer drawer, HorizontalMatrixGroup matrix)
        {
            drawer.ElementTemplate = commonCellTemplate;
          
      
            for (int i = 0; i < matrix.RowNumber; i++)
            {
                for (int j = 0; j < matrix.ColumnNumber; j++)
                {
                    int temp = j;
                    switch (matrix.getDesiredMatrixIndex(i,ref temp))
                    {
                        case SparseMatrix matr:
                            if (matrix[i,j] == 0)
                            {
                                drawer.setStrategy(strategyTwo);
                            } else
                            {
                                drawer.setStrategy(strategyOne);
                            }
                            break;
                        default:
                            drawer.setStrategy(strategyOne);
                            break;
                    }
                    drawer.DrawCellBorder(matrix, i, j);
                }
            }
            drawer.DrawMatrix(matrix);
        }
    }
}
