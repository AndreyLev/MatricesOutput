using ClientPart.IndependentWork1.Composite;
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

        static string emptyElementTemplate = "{0,-5:00.00} ";
        static string cellStringFormat = "{0,4:00.00} ";

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
        Dictionary<RectangleF, string> data;
        List<Rectangle> rectBorder;
        Rectangle borderRectangle;
        Pen borderPen;

        public Graphics GraphicsObj { get { return g; } }

        public FormDrawer(Form graphicsForm, Graphics g)
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
            data = new Dictionary<RectangleF, string>();
            rectBorder = new List<Rectangle>();
        }
      
        public void DrawBorder(IMatrix matrix)
        {
            int x, y;
           
            x = (int)data.First().Key.X;
            y = (int)data.First().Key.Y;
            int borderWidth = xStep * matrix.ColumnNumber;
            int borderHeight = yStep * matrix.RowNumber;
            
            borderRectangle = new Rectangle(x, y, borderWidth, borderHeight);
            isBorder = true;
        }

        public void DrawCellBorder(IMatrix matrix, int rowIndex, int columnIndex)
        {
            DrawCell(matrix, rowIndex, columnIndex);
            RectangleF lastRect= data.Last().Key;
            rectBorder.Add(new Rectangle((int)lastRect.X, (int)lastRect.Y, 
                (int)lastRect.Width, (int)lastRect.Height));
        }

        public void DrawCell(IMatrix matrix, int rowIndex, int columnIndex)
        {
            string outputString;
            switch (matrix)
            {
                case SparseMatrix matr:
                    if (matr[rowIndex, columnIndex] == 0)
                    {
                        outputString = string.Format(emptyElementTemplate, "");
                        break;
                    }
                    else goto default;
                default:
                    outputString = string.Format(cellStringFormat, matrix[rowIndex, columnIndex]);
                    break;
            }
    

            drawRect = new RectangleF(currentX, currentY, width, height);

            data.Add(drawRect, outputString);

            currentX += xStep;
        }

        public void DrawMatrix()
        {

          
            foreach (var buffEl in data)
            {
                g.DrawString(buffEl.Value, drawFont, myBrush, buffEl.Key, strFormat);
            }

        
            foreach (var elBorder in rectBorder)
            {
                g.DrawRectangle(myPen, elBorder);
            }

            if (isBorder)
            {
                g.DrawRectangle(borderPen, borderRectangle);
            }

            

            isBorder = false;
            data.Clear();
            rectBorder.Clear();
        }

        public void DrawOnNewLine()
        {
            currentX = 30;
            currentY += yStep;
        }

        public void Reset()
        {
            currentX = 30;
            currentY = 30;
        }
    }
}
