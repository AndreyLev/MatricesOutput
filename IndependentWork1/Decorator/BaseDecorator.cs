using IndependentWork1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientPart.IndependentWork1.Decorator
{
    public abstract class BaseDecorator
    {
        public abstract IMatrix getLastSource();
        public IMatrix GetSource(IMatrix matrix)
        {
            BaseDecorator baseDeco = matrix as BaseDecorator;
            if (baseDeco != null) return baseDeco.GetSource(getLastSource());
            return matrix;
        }
    }
}
