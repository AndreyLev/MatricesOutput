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
        IDrawer drawer;

        public MatrixVisitor(IDrawer drawer)
        {
            this.drawer = drawer;
        }

        public void visitDenseMatrix(DenseMatrix matrix)
        {
            drawer.DrawBorder(matrix);
        }

        public void visitMatrixElement(IMatrix matrix, int rowIndex, int columnIndex)
        {
            drawer.DrawCellBorder(matrix, rowIndex, columnIndex);
        }

        public void visitSparseMatrix(SparseMatrix matrix)
        {
            drawer.DrawBorder(matrix);
        }

        public void VisitTransponseDecorator(TransponseMatrixGroupDecorator deco)
        {
            drawer.DrawMatrix(deco);
        }
    }
}
