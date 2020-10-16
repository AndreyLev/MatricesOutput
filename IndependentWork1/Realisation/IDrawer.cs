using IndependentWork1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndependentWork1.Realisation
{
    public interface IDrawer
    {
        void DrawBorder(SomeMatrix matrix);
        void DrawCellBorder(double el);
        void DrawCell(double el);

        void DrawMatrix(SomeMatrix matrix);
    }
}
