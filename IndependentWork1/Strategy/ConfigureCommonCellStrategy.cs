using ClientPart.IndependentWork1.Interfaces;
using IndependentWork1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ClientPart.IndependentWork1.Constants.MatrixCellFormatConstants;

namespace ClientPart.IndependentWork1.Strategy
{
    public class ConfigureCommonCellStrategy : IConfigureCellStrategy
    {
        public string ConfigureCell(IMatrix matrix, int rowIndex, int columnIndex)
        {
            return String.Format(commonCellTemplate + " ", matrix[rowIndex, columnIndex]);
        }
    }
}
