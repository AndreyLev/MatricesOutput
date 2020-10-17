using IndependentWork1.Models;


namespace IndependentWork1.Realization
{
    public interface IDrawer
    {
        void DrawBorder(SomeMatrix matrix);
        void DrawCellBorder(double el);
        void DrawCell(double el);

        void DrawMatrix(SomeMatrix matrix);
    }
}
