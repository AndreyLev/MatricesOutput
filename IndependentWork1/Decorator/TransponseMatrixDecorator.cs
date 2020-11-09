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
            get { return this[columnIndex, rowIndex]; }
            set {  }
        }

        public override int RowNumber
        {
            get
            {
                return base.ColumnNumber;
            }
        }

        public override int ColumnNumber
        {
            get
            {
                return base.RowNumber;
            }
        }

        protected override IVector Create(int size)
        {
            throw new NotImplementedException();
        }
    }
}