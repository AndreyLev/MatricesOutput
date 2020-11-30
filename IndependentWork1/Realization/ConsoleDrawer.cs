using ClientPart.IndependentWork1.Interfaces;
using ClientPart.IndependentWork1.Strategy;
using ClientPart.IndependentWork1.Visitor;
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
        IConfigureCellStrategy strategy;

        string border;

        int bufferRow;

        List<List<string>> matrixData;
        int rowData;
        int columnData;

        List<string> data;

        string bufferedElement;

        public ConsoleDrawer(IConfigureCellStrategy strategy)
        {
          
            bufferedElement = "";
            border = "";
            
            matrixData = new List<List<string>>();
            rowData = 0;
            columnData = 0;
            bufferRow = 0;
            this.strategy = new ConfigureCommonCellStrategy();
            data = new List<string>();
        }

        public void setStrategy(IConfigureCellStrategy strategy)
        {
            this.strategy = strategy;
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
            if (rowData == 0 && columnData == 0) matrixData.Add(new List<string>());
            bufferedElement = "";


            bufferedElement += strategy.ConfigureCell(matrix, rowIndex, columnIndex);
            data.Add(bufferedElement);
            //bufferRow = rowData;
            //matrixData[rowData].Add(bufferedElement);
            //columnData++;
            //if (columnData == matrix.ColumnNumber)
            //{
            //    rowData++;
            //    matrixData.Add(new List<string>());
            //    columnData = 0;
            //}
            
        }

        public void DrawCellBorder(IMatrix matrix, int rowIndex, int columnIndex)
        {
            DrawCell(matrix, rowIndex, columnIndex);
            //matrixData[bufferRow][matrixData[bufferRow].Count-1] = 
            //    String.Format("| {0} |", matrixData[bufferRow][matrixData[bufferRow].Count - 1]);
            data[data.IndexOf(data.Last())] =
    String.Format("| {0} |", data[data.IndexOf(data.Last())]);
        }

        public void DrawMatrix(IMatrix matrix)
        {
            
            
            if (border.Length > 0) Console.WriteLine(border);

            //for (int i = 0; i < rowData; i++)
            //{
            //    for (int j = 0; j < matrixData[i].Count; j++)
            //    {
            //        Console.Write(matrixData[i][j]);
            //    }
            //    Console.WriteLine();
            //}
            for (int i = 1; i <= data.Count; i++)
            {
                Console.Write(data[i-1]);
                if (i >= matrix.ColumnNumber &&  (i % matrix.ColumnNumber == 0))
                {
                    Console.WriteLine();
                }
            }

            if (border.Length > 0) Console.WriteLine(border);


            data.Clear();
            matrixData.Clear();
            rowData = 0;
            columnData = 0;
            border = "";
        }

    }
}