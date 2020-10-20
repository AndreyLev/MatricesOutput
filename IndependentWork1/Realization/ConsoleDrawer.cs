using IndependentWork1.Interfaces;
using IndependentWork1.Models;
using System;

namespace IndependentWork1.Realization
{
    public class ConsoleDrawer : IDrawer
    {
        delegate void CellHandler(double d);
        delegate void TopBottomCellBorderHandler(double d);
        CellHandler cellHandler;
        TopBottomCellBorderHandler tbHandler;

        public ConsoleDrawer()
        {
            cellHandler = DrawCell;
            tbHandler = null;
        }

        void printLine(double colNumber)
        {
            Console.Write(" ");
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
        public void DrawBorder(IMatrix matrix)
        {

            printLine(matrix.ColumnNumber);

            Console.WriteLine();
            tbHandler = printLine;
            DrawMatrix(matrix);
            tbHandler = null;


            printLine(matrix.ColumnNumber);
            Console.WriteLine();
        }

        public void DrawCell(double el)
        {
            Console.Write("{0,-4:00.00} ", el);
        }

        private void DrawEmptyElement(double el)
        {
            Console.Write("{0,5} ", " ");
        }

        public void DrawCellBorder(double el)
        {
            Console.Write("|");
            cellHandler(el);
            Console.Write("|");
        }

        public void DrawMatrix(IMatrix matrix)
        {
            for (int i = 0; i < matrix.RowNumber; i++)
            {
                for (int j = 0; j < matrix.ColumnNumber; j++)
                {
                    
                    if (matrix[i,j] == 0)
                    {
                        if (matrix is SparseMatrix)
                        {
                            cellHandler = DrawEmptyElement;
                        }
                        
                    }
                    DrawCellBorder(matrix[i, j]);
                    cellHandler = DrawCell;
                   

                }
                Console.WriteLine();
                if (i != matrix.RowNumber - 1 && tbHandler != null)
                {
                    tbHandler(matrix.ColumnNumber);
                    Console.WriteLine();
                }

            }
        }
    }
}
