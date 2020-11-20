using IndependentWork1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientPart.IndependentWork1.Interfaces
{
    public interface IIterator
    {
        int CurrentIndex { get; }
        IMatrix getNext();

        bool hasMore();

        void Reset();
    }
}
