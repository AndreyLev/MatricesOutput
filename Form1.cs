﻿using System;
using System.Windows.Forms;
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
                    mxDecorator.RenumberColumns(0, 1);
                    mxDecorator.RenumberRows(0, 1);
                    DrawMatrixDependingOnCheckbox();
                }
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Строки и/или столбца с таким индексами в матрице нет");
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
    }
}
