﻿using ClientPart.IndependentWork1.Composite;
using IndependentWork1.Interfaces;
using IndependentWork1.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndependentWork1.Decorator
{
    class TransponseMatrixDecorator : BaseDecorator
    {
        public TransponseMatrixDecorator(IMatrix matrix) : base(matrix)
        {
        }

        public override double this[int rowIndex, int columnIndex]
        {
            get { return matrix[columnIndex, rowIndex]; }
            set { matrix[columnIndex,rowIndex] = value; }
        }

        public override int RowNumber
        {
            get
            {
                return matrix.ColumnNumber;
            }
        }

        public override int ColumnNumber
        {
            get
            {
                return matrix.RowNumber;
            }
        }

    }
}
