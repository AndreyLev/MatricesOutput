using ClientPart.IndependentWork1.Composite;
using ClientPart.IndependentWork1.Visitor;
using IndependentWork1.Interfaces;
using IndependentWork1.Models;
using IndependentWork1.Realization;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndependentWork1.Decorator
{
    class TransponseMatrixDecorator : IMatrix
    {
        IMatrix matrix;
        public TransponseMatrixDecorator(IMatrix matrix)
        {
            this.matrix = matrix;
        }

        public double this[int rowIndex, int columnIndex]
        {
            get { return matrix[columnIndex, rowIndex]; }
            set {  }
        }

        public int RowNumber
        {
            get
            {
                return matrix.ColumnNumber;
            }
        }

        public int ColumnNumber
        {
            get
            {
                return matrix.RowNumber;
            }
        }

        public void Draw(IDrawer drawer, IVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
