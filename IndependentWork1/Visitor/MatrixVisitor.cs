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
    class MatrixVisitor : IVisitor
    {
        //IConfigureCellStrategy strategyOne;
        //IConfigureCellStrategy strategyTwo;

        public MatrixVisitor()
        {
            //strategyOne = new ConfigureCommonCellStrategy();
            //strategyTwo = new ConfigureSparceMatrixCellStrategy();
        }
       
       public void DrawMatrix(IDrawer drawer, DenseMatrix matrix)
        {
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
                    drawer.DrawCellBorder(matrix,i,j);
                }
            }
            drawer.DrawMatrix(matrix);
        }

        public void DrawMatrix(IDrawer drawer, HorizontalMatrixGroup matrix)
        {

            for (int i = 0; i < matrix.RowNumber; i++)
            {
                for (int j = 0; j < matrix.ColumnNumber; j++)
                {
                    drawer.DrawCellBorder(matrix, i, j);
                }
            }
            drawer.DrawMatrix(matrix);
        }

        public void DrawMatrix(IDrawer drawer, RenumberingDecorator decorator)
        {
            IMatrix m = decorator.GetSource();
            for (int i = 0; i < decorator.RowNumber; i++)
            {
                for (int j = 0; j < decorator.ColumnNumber; j++)
                {
                    drawer.DrawCellBorder(decorator, i, j);
                }
            }
            drawer.DrawMatrix(decorator);
        }
    }
}
