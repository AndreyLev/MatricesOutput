using ClientPart.IndependentWork1.Visitor;
using IndependentWork1.Interfaces;
using IndependentWork1.Realization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientPart.IndependentWork1.Decorator
{
    public class MatrixBorderDecorator : IMatrix
    {
        IMatrix matrix;

        public MatrixBorderDecorator(IMatrix matrix)
        {
            this.matrix = matrix;
        }

        public double this[int rowIndex, int columnIndex] {
            get => matrix[rowIndex, columnIndex];
            set { } }

        public int RowNumber => matrix.RowNumber;

        public int ColumnNumber => matrix.ColumnNumber;

        public void Draw(IDrawer drawer, IVisitor visitor)
        {
            matrix.Draw(drawer, visitor);   
        }

        public void DrawBorder(IDrawer drawer, IVisitor visitor)
        {
            drawer.DrawBorder(this);
            matrix.Draw(drawer, visitor);
        }
    }
}
