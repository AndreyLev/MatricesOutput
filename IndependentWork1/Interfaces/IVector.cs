﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IndependentWork1.Interfaces
{
    public interface IVector
    {
        int DIM { get; }

        double this[int index] { get; set; }
        
    }
}
