using ClientPart.IndependentWork1.Composite;
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

        public void visitDenseMatrix(DenseMatrix matrix)
        {
            for (int i = 0; i < matrix.RowNumber; i++)
            {
                for (int j = 0; j < matrix.ColumnNumber; j++)
                {
                   cellStrategy = new ConfigureCommonCellStrategy();
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
                    if (matrix[i, j] == 0)
                    {
                        cellStrategy = new ConfigureSparceMatrixCellStrategy();
                    }
                    else
                        cellStrategy = new ConfigureCommonCellStrategy();

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

        public void visitHorizontalMatrixGroup(HorizontalMatrixGroup matrix)
        {
            throw new NotImplementedException();
        }
    }
}
