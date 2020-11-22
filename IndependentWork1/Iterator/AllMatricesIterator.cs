using ClientPart.IndependentWork1.Interfaces;
using IndependentWork1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientPart.IndependentWork1.Iterator
{
    public class AllMatricesIterator : IIterator
    {
        List<IMatrix> matrixList;

        int currentIndex;
        public int CurrentIndex { get => currentIndex; }
        public AllMatricesIterator(List<IMatrix> matrixList)
        {
            this.matrixList = matrixList;
            currentIndex = 0;
        }

        public IMatrix getNext()
        {
            IMatrix m = matrixList[currentIndex];
            currentIndex++;
            return m;
        }

        public bool hasMore()
        {
            return currentIndex < matrixList.Count;
        }

        public void Reset()
        {
            currentIndex = 0;
        }
    }
}
