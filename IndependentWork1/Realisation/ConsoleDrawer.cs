using IndependentWork1.Models;
using IndependentWork1.Realisation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientPart.IndependentWork1.Realisation
{
    public class ConsoleDrawer : IDrawer
    {
        delegate void CellHandler(double d);
        CellHandler cellHandler;

        public ConsoleDrawer()
        {
            cellHandler = DrawCell;
        }
        
        void printLine(double colNumber)
        {
            for (int i = 0; i < colNumber; i++)
            {
                if (i == colNumber - 1)
                {
                    Console.Write("------");
                    break;
                }
                Console.Write("--------");
            }
        }
        public void DrawBorder(SomeMatrix matrix)
        {
            Console.Write(" ");
            printLine(matrix.ColumnNumber);

            Console.WriteLine();
            cellHandler = DrawCellBorder;
            DrawMatrix(matrix);
            cellHandler = DrawCell;

            Console.Write(" ");
            printLine(matrix.ColumnNumber);
            Console.WriteLine();
        }

        public void DrawCell(double el)
        {
            Console.Write("{0,-4:00.00} ", el);
        }

        public void DrawCellBorder(double el)
        {
            Console.Write("|");
            DrawCell(el);
            Console.Write("|");
        }

        public void DrawMatrix(SomeMatrix matrix)
        {
                for (int i = 0; i < matrix.RowNumber; i++)
                {
                    for (int j = 0; j < matrix.ColumnNumber; j++)
                    {
                        cellHandler(matrix[i, j]);
                    }
                    Console.WriteLine();
                }
            
        }
    }
}
