using IndependentWork1.Interfaces;
using IndependentWork1.Models;
using IndependentWork1.Realization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientPart.IndependentWork1.Strategy
{
    interface IStrategy
    {
        void DrawCell(IDrawer drawer,DenseMatrix matrix, int rowIndex, int columnIndex);

        void DrawCell(IDrawer drawer, SparseMatrix matrix, int rowIndex, int columnIndex);
    }
}
