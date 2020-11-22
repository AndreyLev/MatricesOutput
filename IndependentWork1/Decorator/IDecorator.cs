using IndependentWork1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientPart.IndependentWork1.Decorator
{
    public interface IDecorator : IMatrix
    {
        IMatrix getSource();
    }
}
