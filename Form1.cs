using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Security.Policy;
using System.Windows.Forms;
using ClientPart.IndependentWork1.Composite;
using IndependentWork1.Decorator;
using IndependentWork1.Interfaces;
using IndependentWork1.Models;
using IndependentWork1.Realization;

namespace ClientPart
{

    public partial class Form1 : Form
    {
        const int graphicsFormWidth = 800;
        const int graphicsFormHeight = 600;
        Form graphicsForm;
        IMatrix matrix;
        ConsoleDrawer consoleDrawer;
        FormDrawer formDrawer;
        IMatrix matrixDecorator;
        Graphics g;
        bool transponseFlag = true;

        
        public Form1()
        {
            InitializeComponent();
            
            graphicsForm = new Form();
            graphicsForm.Size = new System.Drawing.Size(graphicsFormWidth, graphicsFormHeight);
            graphicsForm.Text = "Графическая визуализация матрицы";
            graphicsForm.MaximizeBox = false;
            graphicsForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            graphicsForm.Show();
            consoleDrawer = new ConsoleDrawer();
            g = graphicsForm.CreateGraphics();
            formDrawer = new FormDrawer(graphicsForm, g);   

        }
        
        private void drawMatrixWithOtherDrawers()
        {
            matrix.Draw(consoleDrawer);
            // matrix.Draw(formDrawer);
        }

        private void drawMatrixBorderWithOtherDrawers()
        {
            matrix.DoDrawBorder(consoleDrawer);
            // matrix.DoDrawBorder(formDrawer);
        }

        private bool IsMatrixBig()
        {
            if (matrix.RowNumber > 8 || matrix.ColumnNumber > 10)
            {
                MessageBox.Show("Матрица большая! Вывод будет только на консоле.");
                return true;
            }
            return false;
        }

        
        //private void DrawMatrixDependingOnCheckbox()
        //{
        //    if (checkBox1.Checked)
        //    {

        //        if (!IsMatrixBig())
        //        {
        //            matrix.DoDrawBorder();
        //            changeDrawers();
        //            matrix.DoDrawBorder();
        //        }
        //        else
        //        {
        //            setConsoleDrawerIfForm();
        //            matrix.DoDrawBorder();
        //        }
        //    }
        //    else
        //    {
        //        if (!IsMatrixBig())
        //        {
        //            matrix.Draw();
        //            changeDrawers();
        //            matrix.Draw();
        //        }
        //        else
        //        {
        //            setConsoleDrawerIfForm();
        //            matrix.Draw();
        //        }
        //    }
        //    Console.WriteLine();
        //}

        void OpenRenumberingButtons()
        {
            button3.Enabled = true;
            button4.Enabled = true;
        }

        private void ClearDrawAreas()
        {
            Console.Clear();
            g.Clear(SystemColors.Control);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearDrawAreas();
            OpenRenumberingButtons();
            int dmRowCount = 7;
            int dmColumnCount = 6; 
            Console.Clear();
            matrix = new DenseMatrix(dmRowCount, dmColumnCount);          
            MatrixInitiator.FillMatrix(matrix, 15, 15);

            drawMatrixWithOtherDrawers();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenRenumberingButtons();
            ClearDrawAreas();
            int smRowCount = 6;
            int smColumnCount = 7;
            matrix = new SparseMatrix(smRowCount, smColumnCount);
            MatrixInitiator.FillMatrix(matrix, 15, 15);

            drawMatrixWithOtherDrawers();

        }

        private void checkBox1_Click(object sender, EventArgs e)
        {

            ClearDrawAreas();
            if (matrix != null)
            {     
                if (checkBox1.Checked)
                {
                    drawMatrixBorderWithOtherDrawers();
                } else
                {
                    drawMatrixWithOtherDrawers();
                }
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
                ClearDrawAreas();

                if (matrix != null)
                {
                    RenumberingDecorator mxDecorator = new RenumberingDecorator(matrix);
                    Random rnd = new Random();
                    int rndRowFirst = rnd.Next(matrix.RowNumber);
                    int rndRowSecond;
                    do
                    {
                        rndRowSecond = rnd.Next(matrix.RowNumber);
                    } while (rndRowFirst == rndRowSecond);
                    
                    
                    mxDecorator.RenumberRows(rndRowFirst, rndRowSecond);

                    int rndColFirst = rnd.Next(matrix.ColumnNumber);
                    int rndColSecond;
                    do
                    {
                        rndColSecond = rnd.Next(matrix.ColumnNumber);
                    } while (rndColSecond == rndColFirst);

                    mxDecorator.RenumberColumns(rndColFirst, rndColSecond);
                    matrix = mxDecorator;
                    

                    drawMatrixWithOtherDrawers();

                    string outputMessage = string.Format("Были поменяны строки с номерами {0} и {1}" +
                           "\nИ стобцы с номерами {2} и {3}", rndRowFirst + 1, rndRowSecond + 1, rndColFirst + 1, rndColSecond + 1);

                    MessageBox.Show(outputMessage);

            }
            
          
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ClearDrawAreas();
            BaseDecorator baseDecorator = matrix as BaseDecorator;
            if (baseDecorator != null)
            {
                matrix = baseDecorator.getMatrixSource();
            }
            drawMatrixWithOtherDrawers();
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
            ClearDrawAreas();

            List<IMatrix> matrixList = new List<IMatrix>();
            
            matrixList.Add(new DenseMatrix(2, 2));
            matrixList.Add(new DenseMatrix(3, 3));
            matrixList.Add(new DenseMatrix(5, 1));
            matrixList.Add(new DenseMatrix(1, 1));

            List<IMatrix> testList = new List<IMatrix>();
            testList.Add(new SparseMatrix(2, 2));
            testList.Add(new SparseMatrix(6, 2));
            for (int i = 1; i <= testList.Count; i++)
            {
                MatrixInitiator.FillMatrixSpecifiedValue(testList[i - 1], i + 4);
            }


            for (int i = 1; i <= matrixList.Count; i++)
            {
                MatrixInitiator.FillMatrixSpecifiedValue(matrixList[i-1], i);
            }

            IMatrix testGroup = new HorizontalMatrixGroup(testList, consoleDrawer);
            matrixList.Add(testGroup);
            matrix = new HorizontalMatrixGroup(matrixList, consoleDrawer);
            
            //DrawMatrixDependingOnCheckbox();

        }

        private void button6_Click(object sender, EventArgs e)
        {

            ClearDrawAreas();
            if (matrix != null)
            {

                matrix = new TransponseMatrixGroupDecorator(matrix, true);


               // DrawMatrixDependingOnCheckbox();
            }
            else
                MessageBox.Show("Сначала нужно сгенерировать матрицу");


        }
    }
}
