using ClientPart.IndependentWork1.Interfaces;
using IndependentWork1.Interfaces;
using IndependentWork1.Realization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientPart.IndependentWork1.Decorator
{
    public class DrawerDecorator : IDrawer
    {
        IDrawer drawer;

        public DrawerDecorator(IDrawer drawer)
        {
            this.drawer = drawer;
        }

        public IDrawer Drawer { get => drawer; set => drawer = value; }
        public string ElementTemplate 
        { 
            get => drawer.ElementTemplate; 
            set { }
        }

        public void DrawBorder(IMatrix matrix)
        {
            drawer.DrawBorder(matrix);
        }

        public void DrawCell(IMatrix matrix, int rowIndex, int columnIndex)
        {
            drawer.DrawCell(matrix, rowIndex, columnIndex);
        }

        public void DrawCellBorder(IMatrix matrix, int rowIndex, int columnIndex)
        {
            drawer.DrawCellBorder(matrix, rowIndex, columnIndex);
        }

        public void DrawMatrix(IMatrix matrix)
        {
            drawer.DrawMatrix(matrix);
        }

        public void DrawMatrixWithOneBorder(IMatrix matrix)
        {
            drawer.DrawBorder(matrix);
            drawer.DrawMatrix(matrix);
        }

        public void setStrategy(IConfigureCellStrategy strategy)
        {
            drawer.setStrategy(strategy);
        }
    }
}
