using ClientPart.IndependentWork1.Composite;
using IndependentWork1.Decorator;
using IndependentWork1.Models;
using IndependentWork1.Realization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientPart.IndependentWork1.Visitor
{
    public interface IVisitor
    {
        void DrawMatrix(IDrawer drawer, DenseMatrix matrix);

        void DrawMatrix(IDrawer drawer, SparseMatrix matrix);

        void DrawMatrix(IDrawer drawer, HorizontalMatrixGroup matrix);

        void DrawMatrix(IDrawer drawer, RenumberingDecorator decorator);
    }
}
