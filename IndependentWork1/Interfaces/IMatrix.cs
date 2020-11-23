using ClientPart.IndependentWork1.Visitor;
using IndependentWork1.Realization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace IndependentWork1.Interfaces
{
    public interface IMatrix { 

        int RowNumber { get; }
        int ColumnNumber { get; }

        double this[int rowIndex, int columnIndex] { get; set; }

        void Draw(IDrawer drawer, IVisitor visitor);

        void DrawCell(IMatrix matrix, int rowIndex, int columnIndex, IVisitor visitor);

        void DrawCellBorder(IMatrix matrix, int rowIndex, int columnIndex, IVisitor visitor);
    }
}
