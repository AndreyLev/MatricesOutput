using ClientPart.IndependentWork1.Composite;
using IndependentWork1.Decorator;
using IndependentWork1.Interfaces;
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
        void visitDenseMatrix(DenseMatrix matrix);

        void visitSparseMatrix(SparseMatrix matrix);

        void VisitTransponseDecorator(TransponseMatrixGroupDecorator deco);

        void visitMatrixElement(IMatrix matrix, int rowIndex, int columnIndex);
    }
}
