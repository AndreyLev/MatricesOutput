using ClientPart.IndependentWork1.Composite;
using ClientPart.IndependentWork1.Interfaces;
using IndependentWork1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientPart.IndependentWork1.Proxy
{
    public class StartProxyStrategy : IConfigureCellStrategy
    {
        IConfigureCellStrategy nextStrategy;

        public StartProxyStrategy()
        {
            nextStrategy = new ProxyStrategy();
        }
        public string ConfigureCell(IMatrix matrix, int rowIndex, int columnIndex)
        {
            switch (matrix)
            {
                case HorizontalMatrixGroup horizontalGroup:
                    IMatrix m = horizontalGroup.getDesiredMatrixIndex(rowIndex, ref columnIndex);
                    return nextStrategy.ConfigureCell(m, rowIndex, columnIndex);
                default:
                    return nextStrategy.ConfigureCell(matrix, rowIndex, columnIndex);
            }
        }
    }
}
