using IndependentWork1.Interfaces;
using IndependentWork1.Models;


namespace IndependentWork1.Realization
{
    public interface IDrawer
    {  
        void DrawBorder(IMatrix matrix);
        void DrawCellBorder(IMatrix matrix, int rowIndex, int columnIndex);
        void DrawCell(IMatrix matrix, int rowIndex, int columnIndex);
        void DrawMatrix();
        void DrawOnNewLine();
        void Reset();

    }
}
