using IndependentWork1.Interfaces;
using IndependentWork1.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;

namespace IndependentWork1.Realization
{
    public class ConsoleDrawer : IDrawer
    {
        static string emptyElementTemplate = "{0,-5:00.00} ";
        static string commonElementTemplate = "{0,-4:00.00} ";

        List<string> data;

        string bufferedElement;

        int buffElNumber;

        public ConsoleDrawer()
        {
            data = new List<string>();
            bufferedElement = "";
            buffElNumber = 0;
        }


        public void DrawBorder(IMatrix matrix)
        {
            int borderLength = matrix.ColumnNumber * 10;
            string border = "";
            while (borderLength-- != 0)
            {
                border += "-";
                if (borderLength == 0) border += "\n";
            }

            data.Insert(0, border);
            data.Insert(data.Count, border);

            int insertIndex = 1;
            for (int i = 0; i < matrix.RowNumber-1; i++)
            {
                insertIndex += matrix.ColumnNumber;
                data.Insert(insertIndex, border);
                insertIndex++;
            }
            
        }

        public void DrawCell(IMatrix matrix, int rowIndex, int columnIndex)
        {
            bufferedElement = "";
            switch (matrix)
            {
                case SparseMatrix matr:
                    if (matr[rowIndex, columnIndex] == 0)
                    {
                        bufferedElement += String.Format(emptyElementTemplate, "");
                        break;
                    }
                    else goto default;       
                default:
                    bufferedElement += String.Format(commonElementTemplate, matrix[rowIndex, columnIndex]);
                    break;
            }
            if (columnIndex == matrix.ColumnNumber - 1) bufferedElement += "\n";
            data.Add(bufferedElement);
            buffElNumber++;
        }

        public void DrawCellBorder(IMatrix matrix, int rowIndex, int columnIndex)
        {
            DrawCell(matrix, rowIndex, columnIndex);
            if (data[buffElNumber - 1].Contains('\n'))
            {
                data[buffElNumber-1] = 
                    data[buffElNumber - 1].Remove(data[buffElNumber-1].Length-1);
            }

            data[buffElNumber - 1] = String.Format("| {0} |", data[buffElNumber - 1]);
            if (columnIndex == matrix.ColumnNumber - 1) data[buffElNumber - 1] += "\n";
        }

        public void DrawMatrix()
        {
            foreach (string str in data)
            {
                Console.Write(str);
            }

            data.Clear();
            buffElNumber = 0;
        }

    }
}
