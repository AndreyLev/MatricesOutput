using System;
using System.Windows.Forms;
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

        
        public Form1()
        {
            InitializeComponent();
            
            graphicsForm = new Form();
            graphicsForm.Size = new System.Drawing.Size(graphicsFormWidth, graphicsFormHeight);
            graphicsForm.Text = "Графическая визуализация матрицы";
            graphicsForm.MaximizeBox = false;
            graphicsForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            graphicsForm.Show();

        }
        
        private void changeDrawers()
        {
            if (matrix.Drawer is ConsoleDrawer)
                matrix.Drawer = new FormDrawer(graphicsForm);
            else
                matrix.Drawer = new ConsoleDrawer();
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
                matrix.Drawer = new ConsoleDrawer();
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
            matrix = new DenseMatrix(dmRowCount, dmColumnCount, new FormDrawer(graphicsForm));
           
            MatrixInitiator.FillMatrix(matrix, 15, 15);

            DrawMatrixDependingOnCheckbox();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            Console.Clear();
            int smRowCount = 6;
            int smColumnCount = 7;
            matrix = new SparseMatrix(smRowCount, smColumnCount, new ConsoleDrawer());
            MatrixInitiator.FillMatrix(matrix, 15, 15);
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
    }
}
