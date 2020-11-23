using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Security.Policy;
using System.Windows.Forms;
using ClientPart.IndependentWork1.Composite;
using ClientPart.IndependentWork1.Decorator;
using ClientPart.IndependentWork1.Strategy;
using ClientPart.IndependentWork1.Visitor;
using IndependentWork1.Decorator;
using IndependentWork1.Interfaces;
using IndependentWork1.Models;
using IndependentWork1.Realization;

namespace ClientPart
{

    public partial class Form1 : Form
    {
        const int graphicsFormWidth = 1500;
        const int graphicsFormHeight = 900;
        Form graphicsForm;
        IMatrix matrix;
        ConsoleDrawer consoleDrawer;
        FormDrawer formDrawer;
        Graphics g;
        IVisitor visitorOne;
        IVisitor visitorTwo;
        IMatrix buffMatrix;

        
        public Form1()
        {
            InitializeComponent();
            
            graphicsForm = new Form();
            graphicsForm.Size = new System.Drawing.Size(graphicsFormWidth, graphicsFormHeight);
            graphicsForm.Text = "Графическая визуализация матрицы";
            graphicsForm.MaximizeBox = false;
            graphicsForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            graphicsForm.Show();
            consoleDrawer = new ConsoleDrawer(new ConfigureCommonCellStrategy());
            g = graphicsForm.CreateGraphics();
            formDrawer = new FormDrawer(graphicsForm, g, new ConfigureCommonCellStrategy());
            visitorOne = new MatrixVisitor(consoleDrawer);
            visitorTwo = new MatrixVisitor(formDrawer);

        }
        
        private void drawMatrixWithOtherDrawers()
        {
            matrix.Draw(consoleDrawer, visitorOne);
            matrix.Draw(formDrawer, visitorTwo);
        }

        private void drawMatrixBorderWithOtherDrawers()
        {
            IMatrix mxBorderDeco = new MatrixBorderDecorator(matrix);
            mxBorderDeco.Draw(consoleDrawer, visitorOne);
            mxBorderDeco.Draw(formDrawer, visitorTwo);
        }

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
           
            OpenRenumberingButtons();
            int dmRowCount = 7;
            int dmColumnCount = 6; 
            matrix = new DenseMatrix(dmRowCount, dmColumnCount);          
            MatrixInitiator.FillMatrix(matrix, 15, 15);

            buffMatrix = ((SomeMatrix)matrix).Clone();
            checkBox1_Click(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            OpenRenumberingButtons();
            ClearDrawAreas();
            int smRowCount = 6;
            int smColumnCount = 7;
            matrix = new SparseMatrix(smRowCount, smColumnCount);
            MatrixInitiator.FillMatrix(matrix, 15, 15);

            buffMatrix = ((SomeMatrix)matrix).Clone();
            checkBox1_Click(sender, e);

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


                    checkBox1_Click(sender, e);

                    string outputMessage = string.Format("Были поменяны строки с номерами {0} и {1}" +
                           "\nИ стобцы с номерами {2} и {3}", rndRowFirst + 1, rndRowSecond + 1, rndColFirst + 1, rndColSecond + 1);

                    MessageBox.Show(outputMessage);

            }
            
          
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ClearDrawAreas();
            //BaseDecorator baseDecorator = matrix as BaseDecorator;
            //if (baseDecorator != null)
            //{
            //    matrix = baseDecorator.getMatrixSource();
            //}
            if (buffMatrix != null)
            {
                matrix = buffMatrix;
            }
            checkBox1_Click(sender, e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
            ClearDrawAreas();

            List<IMatrix> matrixList = new List<IMatrix>();
            
            matrixList.Add(new DenseMatrix(2, 2));
            matrixList.Add(new SparseMatrix(3, 3));
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

            checkBox1_Click(sender, e);

        }

        private void button6_Click(object sender, EventArgs e)
        {

            if (matrix != null)
            {
                //switch (matrix)
                //{
                //    case HorizontalMatrixGroup group:
                //        matrix = new TransponseMatrixGroupDecorator(matrix);
                //        break;
                //    case TransponseMatrixGroupDecorator group_deco:
                //        matrix = group_deco.getPreviousSource();
                //        break;
                //    case TransponseMatrixDecorator matr_deco:
                //        matrix = matr_deco.getPreviousSource();
                //        break;
                //    default:
                //        matrix = new TransponseMatrixDecorator(matrix);
                //        break;
                //}
                matrix = new TransponseMatrixGroupDecorator(matrix);

                checkBox1_Click(sender, e);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
