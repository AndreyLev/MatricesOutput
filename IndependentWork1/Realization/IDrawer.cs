using IndependentWork1.Interfaces;
using IndependentWork1.Models;


namespace IndependentWork1.Realization
{
    public interface IDrawer
    {
        void DrawBorder(IMatrix matrix);
        void DrawCellBorder(double el);
        void DrawCell(double el);

        void DrawMatrix(IMatrix matrix);

    }
}
