using ClientPart.IndependentWork1.Composite;
using ClientPart.IndependentWork1.Interfaces;
using ClientPart.IndependentWork1.Strategy;
using IndependentWork1.Interfaces;
using IndependentWork1.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace IndependentWork1.Realization
{
    class FormDrawer : IDrawer
    {
        IConfigureCellStrategy strategy;

        private bool isBorder = false;


        Form graphicsForm;
        Graphics g;
        Pen myPen;
        Font drawFont;
        SolidBrush myBrush;
        int currentX = 30;
        int currentY = 30;
        int width = 80;
        int height = 40;
        int xStep = 80;
        int yStep = 40;
        StringFormat strFormat;
        RectangleF drawRect;
        Dictionary<string, RectangleF> data;
        List<Rectangle> rectBorder;
        Rectangle borderRectangle;
        Pen borderPen;
        List<string> strData;
        List<RectangleF> rectData;
        RectangleF rectForDraw;
        List<int> isCellBorder;

        int counter;

        public Graphics GraphicsObj { get { return g; } }

        public string ElementTemplate { get; set; }

        public FormDrawer(Form graphicsForm, Graphics g, IConfigureCellStrategy strategy)
        {
            this.graphicsForm = graphicsForm;
            this.g = g;
            myPen = new Pen(Color.Red, 2);
            borderPen = new Pen(Color.Black, 4);
            drawFont = new Font("Arial", 16);
            myBrush = new SolidBrush(Color.Red);
            strFormat = new StringFormat();
            strFormat.Alignment = StringAlignment.Center;
            strFormat.LineAlignment = StringAlignment.Center;
            drawRect = new RectangleF(currentX, currentY, width, height);
            data = new Dictionary<string, RectangleF>();
            strData = new List<string>();
            rectData = new List<RectangleF>();
            rectBorder = new List<Rectangle>();
            counter = 0;
            this.strategy = new ConfigureCommonCellStrategy();
            isCellBorder = new List<int>();
        }

        public void setStrategy(IConfigureCellStrategy strategy)
        {
            this.strategy = strategy;
        }

        public void DrawBorder(IMatrix matrix)
        {
            int x, y;

            x = 30;
            y = 30;
            int borderWidth = xStep * matrix.ColumnNumber;
            int borderHeight = yStep * matrix.RowNumber;
            
            borderRectangle = new Rectangle(x, y, borderWidth, borderHeight);
            isBorder = true;
        }

        public void DrawCellBorder(IMatrix matrix, int rowIndex, int columnIndex)
        {
            DrawCell(matrix, rowIndex, columnIndex);
            isCellBorder.Add(counter);
        }

        public void DrawCell(IMatrix matrix, int rowIndex, int columnIndex)
        {
            string outputString;

            outputString = strategy.ConfigureCell(matrix, rowIndex, columnIndex);

            strData.Add(outputString);
            counter++;
        }

        public void DrawMatrix(IMatrix matrix)
        {


            for (int i = 1; i <= strData.Count; i++)
            {
                rectForDraw = new RectangleF(currentX, currentY, width, height);
                g.DrawString(strData[i - 1], drawFont, myBrush, rectForDraw, strFormat);

                if (isCellBorder.Contains(i))
                {
                    g.DrawRectangle(myPen, currentX, currentY, width, height);
                }

                currentX += xStep;
                if (i >= matrix.ColumnNumber && (i % matrix.ColumnNumber == 0))
                {
                    currentY += yStep;
                    currentX = 30;
                }
            }


            foreach (var elBorder in rectBorder)
            {
                g.DrawRectangle(myPen, elBorder);
            }

            if (isBorder)
            {
                g.DrawRectangle(borderPen, borderRectangle);
            }


            currentX = 30;
            currentY = 30;
            isBorder = false;
            data.Clear();
            rectBorder.Clear();
            strData.Clear();
        }


    }
}
