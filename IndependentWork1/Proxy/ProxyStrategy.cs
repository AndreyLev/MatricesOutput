using ClientPart.IndependentWork1.Composite;
using ClientPart.IndependentWork1.Interfaces;
using ClientPart.IndependentWork1.Strategy;
using IndependentWork1.Interfaces;
using IndependentWork1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientPart.IndependentWork1.Proxy
{
    public class ProxyStrategy : IConfigureCellStrategy
    {
        Dictionary<string, IConfigureCellStrategy> strategies;

        public ProxyStrategy()
        {
            strategies = new Dictionary<string, IConfigureCellStrategy>();
            strategies.Add("CommonCellStrategy", new ConfigureCommonCellStrategy());
            strategies.Add("SparseCellStrategy", new ConfigureSparceMatrixCellStrategy());
        }
        public string ConfigureCell(IMatrix matrix, int rowIndex, int columnIndex)
        {
            switch (matrix)
            {
                case SparseMatrix matr:
                    if (matr[rowIndex, columnIndex] == 0)
                    {
                        return strategies["SparseCellStrategy"].ConfigureCell(matrix, rowIndex, columnIndex);
                    }       
                    return strategies["CommonCellStrategy"].ConfigureCell(matrix, rowIndex, columnIndex);
                default:
                    return strategies["CommonCellStrategy"].ConfigureCell(matrix, rowIndex, columnIndex);
            }
        }
    }
}
