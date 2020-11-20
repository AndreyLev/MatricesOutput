using ClientPart.IndependentWork1.Interfaces;
using IndependentWork1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientPart.IndependentWork1.Iterator
{
    class MatrixGroupIterator : IIterator
    {
        List<IMatrix> matrixGroup;

        int currentIndex;
        public int CurrentIndex { get => currentIndex; }

    
        public MatrixGroupIterator(List<IMatrix> matrixGroup)
        {
            this.matrixGroup = matrixGroup;
            currentIndex = 0;
        }

        public IMatrix getNext()
        {
            IMatrix m = matrixGroup[currentIndex];
            currentIndex++;
            return m;
        }

        public bool hasMore()
        {
            return CurrentIndex < matrixGroup.Count;
        }

        public void Reset()
        {
           currentIndex = 0;
        }
    }
}
