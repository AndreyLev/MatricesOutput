using IndependentWork1.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IndependentWork1.Models
{
    public class SparseVector : IVector
    {
        public int DIM { get; set; }

        SortedList<int, double> vector;

        public SortedList<int, double> Vector
        {
            get { return vector; }
        }
        
        public double this[int index]
        {
            get
            {
                return vector[index];
            }
            set
            {
                vector[index] = value;
            }
        }

        public SparseVector(int dim)
        {
            vector = new SortedList<int, double>(dim);
            for (int i = 0; i <= dim; i++)
            {
                vector.Add(i, 0);
            }
            DIM = dim;
        }

        public bool removeElement(int key)
        {
            return vector.Remove(key); 
        }
        public double getValue(int index)
        {
            return vector[index];
        }

        public SortedList<int, double> getVector()
        {
            return vector;
        }

        public int setValue(int index, double value)
        {
             this[index] = value;
            return 1;
        }

        public void getKeys()
        {
            Console.WriteLine("\n" + vector.Keys);
        }
    }
}
