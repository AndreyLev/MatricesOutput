using System;
using System.Collections.Generic;
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
        SomeMatrix matrix;
        ConsoleDrawer consoleDrawer;
        FormDrawer formDrawer;
        IMatrix matrixDecorator;

        
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
            formDrawer = new FormDrawer(graphicsForm);

        }
        
        private void changeDrawers()
        {
            if (matrix.Drawer is ConsoleDrawer)
                matrix.Drawer = formDrawer;
            else
                matrix.Drawer = consoleDrawer;
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

        private void setConsoleDrawerIfForm()
        {
            if (matrix.Drawer is FormDrawer)
            {
                matrix.Drawer = consoleDrawer;
            }
                
        }
        private void DrawMatrixDependingOnCheckbox()
        {
            if (checkBox1.Checked)
            {
                
                if (!IsMatrixBig())
                {
                    matrix.DoDrawBorder();
                    changeDrawers();
                    matrix.DoDrawBorder();
                } else
                {
                    setConsoleDrawerIfForm();
                    matrix.DoDrawBorder();
                }
            }
            else
            {
               
                if (!IsMatrixBig())
                {
                    matrix.Draw();
                    changeDrawers();
                    matrix.Draw();
                }
                else
                {
                    setConsoleDrawerIfForm();
                    matrix.Draw();
                }
            }
            Console.WriteLine();
        }

        private void button1_Click(object sender, EventArgs e)
        {
  
            int dmRowCount = 7;
            int dmColumnCount = 6; 
            Console.Clear();
            matrix = new DenseMatrix(dmRowCount, dmColumnCount, formDrawer);
            matrixDecorator = new RenumberingDecorator(matrix);
           
            MatrixInitiator.FillMatrix(matrixDecorator, 15, 15);

            DrawMatrixDependingOnCheckbox();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            Console.Clear();
            int smRowCount = 6;
            int smColumnCount = 7;
            matrix = new SparseMatrix(smRowCount, smColumnCount, consoleDrawer);
            matrixDecorator = new RenumberingDecorator(matrix);
            MatrixInitiator.FillMatrix(matrixDecorator, 15, 15);
             DrawMatrixDependingOnCheckbox();

        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
             
            Console.Clear();
            if (matrix != null)
            {     
                DrawMatrixDependingOnCheckbox();
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Console.Clear();
                if (matrixDecorator != null)
                {
                    RenumberingDecorator mxDecorator = (RenumberingDecorator)matrixDecorator;
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
                    DrawMatrixDependingOnCheckbox();

                    string outputMessage = string.Format("Были поменяны строки с номерами {0} и {1}" +
                        "\nИ стобцы с номерами {2} и {3}", rndRowFirst+1, rndRowSecond+1, rndColFirst+1, rndColSecond+1);

                    MessageBox.Show(outputMessage);
                }
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Строки и/или столбца с такими/таким индексами/индексом в матрице нет");
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Console.Clear();
            if (matrixDecorator != null)
            {
                RenumberingDecorator mxDecorator = (RenumberingDecorator)matrixDecorator;
                mxDecorator.ResetMatrix();
                DrawMatrixDependingOnCheckbox();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<IMatrix> matrixList = new List<IMatrix>();
            matrixList.Add(new DenseMatrix(2, 2, consoleDrawer));
            matrixList.Add(new DenseMatrix(3, 3, consoleDrawer));
            matrixList.Add(new DenseMatrix(5, 1, consoleDrawer));
            matrixList.Add(new DenseMatrix(1, 1, consoleDrawer));
            
            for (int i = 1; i <= matrixList.Count; i++)
            {
                MatrixInitiator.FillMatrixSpecifiedValue(matrixList[i - 1], i);
            }

            HorizontalMatrixGroup matrixGroup = new HorizontalMatrixGroup(matrixList);
            matrixGroup.Draw();
        }
    }
}
