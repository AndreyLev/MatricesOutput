using IndependentWork1.Interfaces;
using IndependentWork1.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientPart.IndependentWork1.Composite
{
    class HorizontalMatrixGroup : IMatrix
    {

        List<IMatrix> matrixGroup;

        public HorizontalMatrixGroup(List<IMatrix> matrixGroup)
        {
            this.matrixGroup = matrixGroup;
        }

        public void AddMatrix(IMatrix matrix)
        {
            matrixGroup.Add(matrix);
        }
        public IVector this[int rowIndex] {
            get
            {
                if (rowIndex >= RowNumber) return null;

                IVector vector = new DenseVector(RowNumber); ;
                
                List<double> values = new List<double>();
                for (int matrixNumber = 0; matrixNumber < matrixGroup.Count; matrixNumber++)
                {
                    for (int column = 0; column < matrixGroup[matrixNumber].ColumnNumber; column++)
                    {
                        values.Add(matrixGroup[matrixNumber][rowIndex, column]);
                    }
                }
                for (int i = 0; i < values.Count; i++)
                {
                    vector[i] = values[i];
                }
                
                return vector;
            }
            set
            {
                if (rowIndex >= RowNumber) throw new InvalidOperationException();

                if (value is IVector)
                {
                    int index = value.DIM;
                    for (int matrixNumber = 0; matrixNumber < matrixGroup.Count; matrixNumber++)
                    {
                        for (int column = 0; column < matrixGroup[matrixNumber].ColumnNumber; column++)
                        {
                            matrixGroup[matrixNumber][rowIndex, column] = value[index];
                            index++;
                        }
                    }
                }
            }
        }

        private int getDesiredMatrixIndex(int columnIndex)
        {
            int intermediateColumnSum = 0;
            int desiredMatrixIndex = 0;
            for (int i = 0; i < matrixGroup.Count; i++)
            {
                intermediateColumnSum += matrixGroup[i].ColumnNumber;
                if (columnIndex < intermediateColumnSum)
                {
                    desiredMatrixIndex = i;
                    break;
                }
                i++;
            }
            return desiredMatrixIndex;
        }

        public double this[int rowIndex, int columnIndex] {
            get
            {
                if (rowIndex >= RowNumber) throw new InvalidOperationException();
                if (columnIndex >= ColumnNumber) throw new InvalidOperationException();

                int matrixIndex = getDesiredMatrixIndex(columnIndex);
                return matrixGroup[matrixIndex][rowIndex, columnIndex];
            }
            set
            {
                if (rowIndex >= RowNumber) throw new InvalidOperationException();
                if (columnIndex >= ColumnNumber) throw new InvalidOperationException();

                int matrixIndex = getDesiredMatrixIndex(columnIndex);
                matrixGroup[matrixIndex][rowIndex, columnIndex] = value;
            }
        }

        public int RowNumber
        {
            get
            {
                return matrixGroup.Select(matrix => matrix.RowNumber).Max();
            }
        }

        public int ColumnNumber
        {
            get
            {
                return matrixGroup.Select(matrix => matrix.ColumnNumber).Sum();
            }
        }

        public void DoDrawBorder()
        {
            if (matrixGroup.Count > 0)
            {
                SomeMatrix mx = (SomeMatrix)matrixGroup[0];
                mx.Drawer.DrawBorder(this);

            }
        }

        public void Draw()
        {
            
            if (matrixGroup.Count > 0)
            {
                SomeMatrix mx = (SomeMatrix)matrixGroup[0];
                mx.Drawer.DrawMatrix(this);
                
            }
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public double getValue(int rowIndex, int columnIndex)
        {
            return this[rowIndex, columnIndex];
        }

        public int setValue(double value, int rowIndex, int columnIndex)
        {
            this[rowIndex, columnIndex] = value;
            return 1;
        }
    }
}
