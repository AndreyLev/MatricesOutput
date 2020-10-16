using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using IndependentWork1.Models;
using ClientPart.IndependentWork1.Realisation;

namespace ClientPart
{

    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();    

        Form graphicsForm;
        SomeMatrix matrix;

        
        public Form1()
        {
            InitializeComponent();
            
            graphicsForm = new Form();
            graphicsForm.Text = "Графическая визуализация матрицы";
            graphicsForm.Show();
            
           
            //FreeConsole();
            //Console.WriteLine("example");
            Console.Out.WriteLine("test");
            Console.WriteLine("343434");
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            matrix = new DenseMatrix(6,10, new ConsoleDrawer());
            MatrixInitiator.FillMatrix(matrix, 15, 15);
            if (checkBox1.Checked) matrix.DoDrawBorder();
            else matrix.Draw();
            Console.WriteLine();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            matrix = new SparseMatrix(4, 5, new ConsoleDrawer());
            MatrixInitiator.FillMatrix(matrix, 15, 15);
            if (checkBox1.Checked) matrix.DoDrawBorder();
            else matrix.Draw();
            Console.WriteLine();
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked) matrix.DoDrawBorder();
            else matrix.Draw();
            Console.WriteLine();
        }
    }
}
