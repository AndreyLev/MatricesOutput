using IndependentWork1.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IndependentWork1.Models
{
    public class DenseVector : IVector
    {
        double[] coords;

        public int DIM { get; }

        public double this[int index]
        {
            get { return coords[index];  }
            set { coords[index] = value; }
        }

        public DenseVector(int coordsCount)
        {
            coords = new double[coordsCount];
            DIM = coordsCount;
        }
        public double getValue(int index)
        {
            return coords[index];
        }

        public double[] getVector()
        {
            return coords;
        }

        public int setValue(int index, double value)
        {
            coords[index] = value;
            return 1;
        }

        public void printValue()
        {
            for (int i = 0; i < coords.Length; i++)
            {
                Console.Write(coords[i] +  "\t");
            }
        }
    }
}
