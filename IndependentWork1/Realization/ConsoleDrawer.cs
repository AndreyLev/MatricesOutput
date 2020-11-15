﻿using IndependentWork1.Interfaces;
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

        string border;

        string bufferedElement;

        public ConsoleDrawer()
        {
            data = new List<string>();
            bufferedElement = "";
            border = "";
        }


        public void DrawBorder(IMatrix matrix)
        {

            int borderLength = matrix.ColumnNumber * 10;

           
            while (borderLength-- != 0)
            {
                border += "-";
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


            data.Add(bufferedElement);
        }

        public void DrawCellBorder(IMatrix matrix, int rowIndex, int columnIndex)
        {
            DrawCell(matrix, rowIndex, columnIndex);
            data[data.LastIndexOf(bufferedElement)] = String.Format("| {0} |", bufferedElement);
        }

        public void DrawMatrix()
        {

            if (border.Length > 0) Console.WriteLine(border);

            foreach (string str in data)
            {
                Console.Write(str);
            }

            if (border.Length > 0) Console.WriteLine(border);

            data.Clear();
            border = "";
        }

        public void DrawOnNewLine()
        {
            data.Add("\n");
        }

        public void Reset()
        {
            Console.Clear();
        }
    }
}