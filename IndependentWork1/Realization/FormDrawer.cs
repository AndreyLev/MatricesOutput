using ClientPart.IndependentWork1.Composite;
using IndependentWork1.Interfaces;
using IndependentWork1.Models;
using System.Drawing;
using System.Windows.Forms;

namespace IndependentWork1.Realization
{
    class FormDrawer : IDrawer
    {
        delegate void CellHandler(double el);

        Form graphicsForm;
        Graphics g;
        Pen myPen;
        Font drawFont;
        SolidBrush myBrush;
        int currentX = 30;
        int currentY = 30;
        int width = 80;
        int height = 40;
        int xStep = 90;
        int yStep = 50;
        StringFormat strFormat;
        RectangleF drawRect;
        CellHandler cellHandler;

        public Graphics GraphicsObj { get { return g; } }

        public FormDrawer(Form graphicsForm, Graphics g)
        {
            this.graphicsForm = graphicsForm;
            this.g = g;
            myPen = new Pen(Color.Red, 2);
            drawFont = new Font("Arial", 16);
            myBrush = new SolidBrush(Color.Red);
            strFormat = new StringFormat();
            strFormat.Alignment = StringAlignment.Center;
            strFormat.LineAlignment = StringAlignment.Center;
            drawRect = new RectangleF(currentX, currentY, width, height);
            cellHandler = DrawCell;
        }

        void ResetCurrentValues()
        {
            currentX = 30;
            currentY = 30;
        }
        public void DrawBorder(IMatrix matrix)
        {
            ResetCurrentValues();
            int x = currentX - 10;
            int y = currentY - 10;
            int borderWidth = xStep * matrix.ColumnNumber + 10;
            int borderHeight = yStep * matrix.RowNumber + 10;
            g.DrawRectangle(myPen, x, y, borderWidth, borderHeight);
            DrawMatrix(matrix);
        }

        public void DrawCell(double el)
        {
            string outputString;
            outputString = string.Format("{0,4:00.00} ", el);
            g.DrawString(outputString, drawFont, myBrush, drawRect, strFormat);
        }

        public void DrawCellBorder(double el)
        {
            g.DrawRectangle(myPen, currentX, currentY, width, height);
            cellHandler?.Invoke(el);
        }

        public void DrawMatrix(IMatrix matrix)
        {
            ResetCurrentValues();
 
            for (int i = 0; i < matrix.RowNumber; i++)
            {
                for (int j = 0; j < matrix.ColumnNumber; j++)
                {
                    drawRect = new RectangleF(currentX, currentY, width, height);
                    if (matrix[i,j] == 0)
                    {
                        if (matrix is SparseMatrix)
                        {
                            cellHandler = null;
                        }
                    }
                    DrawCellBorder(matrix[i, j]);
                    cellHandler = DrawCell;
                    currentX += xStep;
                }
                currentX = 30;
                currentY += yStep;
            }
        }

        /* Code below relates on fourth lab work */

        public void DrawMatrixGroup(IMatrix matrix)
        {
            // throw new System.NotImplementedException();

            HorizontalMatrixGroup matrixGroup = (HorizontalMatrixGroup)matrix;

        }
    }
}
