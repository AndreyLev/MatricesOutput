using ClientPart.IndependentWork1.Composite;
using IndependentWork1.Decorator;
using IndependentWork1.Interfaces;
using IndependentWork1.Models;
using IndependentWork1.Realization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndependentWork1.Decorator
{
    class TransponseMatrixGroupDecorator: BaseDecorator
    {

        DenseMatrix matrixValues;
        HorizontalMatrixGroup matrixGroup;

        bool transponseFlag;

        public TransponseMatrixGroupDecorator(IMatrix matrix, bool transponseFlag) : base(matrix)
        {

            //matrixGroup = (HorizontalMatrixGroup)matrix;

            //this.transponseFlag = transponseFlag;

            //if (transponseFlag)
            //    CollectTheMatrix();
            //else
            //    matrixGroup.doHorizontalGroup();
            matrixGroup = matrix as HorizontalMatrixGroup;
            if (matrixGroup == null)
            {
                if (matrix is TransponseMatrixGroupDecorator)
                {
                    TransponseMatrixGroupDecorator transponseDecorator =
                                            (TransponseMatrixGroupDecorator)matrix;
                    matrixGroup = (HorizontalMatrixGroup)
                                        transponseDecorator.getMatrixSource();
                } else
                {
                    List<IMatrix> matrix_group = new List<IMatrix>() { matrix };
                    matrixGroup = new HorizontalMatrixGroup(matrix_group, new ConsoleDrawer());
                    
                }

            }
            CollectTheMatrix();
            
        }

        public override int RowNumber
        {
            get
            {
                return matrixGroup.MATRIX_GROUP.Select(matrix => matrix.RowNumber).Sum();
            }
        }

        public override int ColumnNumber
        {
            get
            {
                return matrixGroup.MATRIX_GROUP.Select(matrix => matrix.ColumnNumber).Max();
            }
        }

        private void FillValuesWithIndex(int rowIndexOne, int rowIndexTwo, 
            IMatrix matrixLink, DenseMatrix dm)
        {
            for (int i = 0, k = rowIndexOne; i < matrixLink.RowNumber && k < rowIndexTwo; i++, k++)
            {
                for (int j = 0; j < matrixLink.ColumnNumber; j++)
                {
                    dm[k,j] = matrixLink[i, j];
                }
            }
        }
        private void CollectTheMatrix()
        {
            matrixValues = new DenseMatrix(RowNumber, ColumnNumber);

            int currentIndexOne = 0;
            int currentIndexTwo = 0;
            foreach (IMatrix m in matrixGroup.MATRIX_GROUP)
            {
                currentIndexTwo += m.RowNumber;
                FillValuesWithIndex(currentIndexOne, currentIndexTwo, m, matrixValues);
                currentIndexOne += m.RowNumber;
            }
        }

    }
}
