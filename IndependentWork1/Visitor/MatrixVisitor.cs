using ClientPart.IndependentWork1.Composite;
using IndependentWork1.Models;
using IndependentWork1.Realization;
using System;
using System.Collections.Generic;
using static ClientPart.IndependentWork1.Constants.MatrixCellFormatConstants;

namespace ClientPart.IndependentWork1.Visitor
{
    class MatrixVisitor : IVisitor
    {
       

        public void DrawMatrix(IDrawer drawer, DenseMatrix matrix)
        {
            drawer.ElementTemplate = commonCellTemplate;

            for (int i = 0; i < matrix.RowNumber; i++)
            {
                for (int j = 0; j < matrix.ColumnNumber; j++)
                {
                    drawer.DrawCellBorder(matrix, i, j);
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
                        drawer.ElementTemplate = emptyCellTemplate;
                    } else
                    {
                        drawer.ElementTemplate = commonCellTemplate;
                    }

                    drawer.DrawCellBorder(matrix, i, j);
                }
            }
            drawer.DrawMatrix(matrix);
        }

        public void DrawMatrix(IDrawer drawer, HorizontalMatrixGroup matrix)
        {
            throw new NotImplementedException();
        }
    }
}
