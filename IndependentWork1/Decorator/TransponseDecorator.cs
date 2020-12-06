using ClientPart.IndependentWork1.Composite;
using ClientPart.IndependentWork1.Interfaces;
using ClientPart.IndependentWork1.Visitor;
using IndependentWork1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientPart.IndependentWork1.Decorator
{
    public class TransponseDecorator : BaseDecorator, IMatrix
    {
        private static bool isTransponse = true;
        IMatrix matrix;

        public TransponseDecorator(IMatrix matrix)
        {
            this.matrix = matrix;

            isTransponse = isTransponse ? false : true;
        }

        public double this[int rowIndex, int columnIndex] {
            get
            {
                switch (GetSource(this))
                {
                    case HorizontalMatrixGroup matrixGroup:
                       // BaseDecorator deco = matrix as BaseDecorator;
                        if (isTransponse)
                        {
                            //if (matrix is BaseDecorator)
                            //{
                            //    IMatrix matr = getDesiredMatrix(ref rowIndex);
                            //    return matrix[rowIndex, columnIndex];
                            //}
                            return 1;
                        }
                        else
                        {
                            //if (matrix is TransponseDecorator)
                            //{
                            //    BaseDecorator deco = (BaseDecorator)matrix;
                            //    int columnIndexMapping = GetColumnIndex(rowIndex, columnIndex);
                            //    //IMatrix matr = getDesiredMatrix(ref rowIndex);
                            //    return matrix[rowIndex, rowIndex]; 
                            //}
                            if (matrix is BaseDecorator)
                            { 
                                BaseDecorator transponse = (BaseDecorator)matrix;
                                int columnIndexMapping = GetColumnIndex(ref rowIndex, columnIndex);
                                return matrix[rowIndex, columnIndexMapping];
                            }

                            IMatrix matr = getDesiredMatrix(ref rowIndex);
                            return matr[rowIndex, columnIndex];


                        }
                    default:
                        return matrix[columnIndex, rowIndex];
                }     
            }
            set { } 
        }

        private List<IMatrix> getMatrixGroupList()
        {
            List<IMatrix> matrixList = new List<IMatrix>();
            IIterator iter = ((HorizontalMatrixGroup)(GetSource(matrix))).getCommonIterator();
            while (iter.hasMore())
            {
                matrixList.Add(iter.getNext());
            }

            iter.Reset();
            return matrixList;
        }



        private IMatrix getDesiredMatrix(ref int rowIndex)
        {
            int intermediateRowSum = 0;
            int desiredMatrixIndex = 0;

            IIterator iter = ((HorizontalMatrixGroup)(GetSource(matrix))).getCommonIterator();
            while (iter.hasMore())
            {
                intermediateRowSum += iter.getNext().RowNumber;
                if (rowIndex < intermediateRowSum)
                {
                    desiredMatrixIndex = iter.CurrentIndex - 1;
                    break;
                }
            }

            iter.Reset();

            IMatrix desired_matrix = getMatrixGroupList()[desiredMatrixIndex];

            rowIndex = desired_matrix.RowNumber - (intermediateRowSum - rowIndex);

            return desired_matrix;
        }



        private int getDesiredMatrixIndex(int rowIndex)
        {
            int intermediateRowSum = 0;
            int desiredMatrixIndex = 0;

            IIterator iter = ((HorizontalMatrixGroup)(GetSource(matrix))).getCommonIterator();
            while (iter.hasMore())
            {
                intermediateRowSum += iter.getNext().RowNumber;
                if (rowIndex < intermediateRowSum)
                {
                    desiredMatrixIndex = iter.CurrentIndex - 1;
                    break;
                }
            }

            return desiredMatrixIndex;
        }

        private int CountIntermediateColumnSum(int matrixIndex)
        {
            int intermediateColumnSum = 0;

            IIterator iter = ((HorizontalMatrixGroup)(GetSource(this))).getCommonIterator();
            while (iter.hasMore())
            {
                IMatrix matr = iter.getNext();
                if (iter.CurrentIndex-1 < matrixIndex)
                {
                    intermediateColumnSum += matr.ColumnNumber;
                }
            }
            iter.Reset();

            return intermediateColumnSum;
        }

        private int GetColumnIndex(ref int rowIndex, int columnIndex)
        {
            
            int matrixIndex = getDesiredMatrixIndex(rowIndex);
            getDesiredMatrix(ref rowIndex);
            Console.WriteLine("matrixIndex: " + matrixIndex);
            int colIndex = CountIntermediateColumnSum(matrixIndex); // there is a problem here;   
            int columnIndexMapping = colIndex + columnIndex;
           
            //Console.WriteLine("columnIndexMapping: " + columnIndexMapping);
            return columnIndexMapping; 
        }

        public int RowNumber
        {
            get {
                switch (GetSource(this))
                {
                    case HorizontalMatrixGroup matrixGroup:
                        //BaseDecorator deco = matrix as BaseDecorator;
                        
                        if (isTransponse)
                        {
                             return matrix.RowNumber;
                        } else
                        {
                            //if (matrix is BaseDecorator) return matrix.ColumnNumber;

                            int rowNumber = 0;
                            IIterator iter = matrixGroup.getCommonIterator();
                            while (iter.hasMore())
                            {
                                rowNumber += iter.getNext().RowNumber;
                            }
                            iter.Reset();
                            return rowNumber;
                        }
                        
                    default:
                        return matrix.ColumnNumber;
                }
            }
        }

        public int ColumnNumber
        {
            get
            {
                switch (GetSource(this))
                {
                    case HorizontalMatrixGroup matrixGroup:
                        //BaseDecorator deco = matrix as BaseDecorator;
                        
                        if (isTransponse)
                        {
                            return matrix.ColumnNumber;
                        } else
                        {
                            //if (matrix is BaseDecorator) return matrix.ColumnNumber;

                            List<int> columnCountValues = new List<int>();
                            IIterator iter = matrixGroup.getCommonIterator();
                            while (iter.hasMore())
                            {
                                columnCountValues.Add(iter.getNext().ColumnNumber);
                            }
                            iter.Reset();
                            return columnCountValues.Max();
                        }
                        
                          
                        
                    default:
                        return matrix.RowNumber;
                }
            }
        }

        public void Accept(IVisitor visitor)
        {
            visitor.visitTransponseDecorator(this);
        }

        public override IMatrix getLastSource()
        {
            return matrix;
        }
    }
}
