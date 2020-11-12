using IndependentWork1.Interfaces;
using IndependentWork1.Models;
using IndependentWork1.Realization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Odbc;
using System.Linq;
using System.Linq.Expressions;

namespace IndependentWork1.Decorator
{
    class RenumberingDecorator : BaseDecorator
    {

        List<KeyValuePair<int, int>> rowChanges;
        List<KeyValuePair<int, int>> columnChanges;

        public override double this[int rowIndex, int columnIndex]
        {
            get
            {
                if (MapRow(rowIndex) > -1 && MapColumn(columnIndex) > -1)
                {
                    return base[MapRow(rowIndex), MapColumn(columnIndex)];
                }
                else if (MapRow(rowIndex) > -1)
                {
                    return base[MapRow(rowIndex), columnIndex];
                }
                else if (MapColumn(columnIndex) > -1)
                {
                    return base[rowIndex, MapColumn(columnIndex)];
                }


                return base[rowIndex, columnIndex];
            }
            set { }
        }

        public RenumberingDecorator(IMatrix matrix) : base(matrix)
        {
            rowChanges = new List<KeyValuePair<int, int>>();
            columnChanges = new List<KeyValuePair<int, int>>();
        }

        private int MapRow(int rowIndex)
        {
            IEnumerable<KeyValuePair<int, int>> collection = rowChanges.Where(rowPair => rowPair.Key == rowIndex);
            if (collection.Count() > 0) return collection.Last().Value;
            return -1;
        }

        private int MapColumn(int columnIndex)
        {
            IEnumerable<KeyValuePair<int, int>> collection = columnChanges.Where(columnPair => columnPair.Key == columnIndex);
            if (collection.Count() > 0) return collection.Last().Value;
            return -1;
        }

        public void RenumberRows(int rowOneIndex, int rowTwoIndex)
        {
            rowChanges.Add(new KeyValuePair<int, int>(rowOneIndex, rowTwoIndex));
            rowChanges.Add(new KeyValuePair<int, int>(rowTwoIndex, rowOneIndex));
        }

        public void RenumberColumns(int columnOneIndex, int columnTwoIndex)
        {
            columnChanges.Add(new KeyValuePair<int, int>(columnOneIndex, columnTwoIndex));
            columnChanges.Add(new KeyValuePair<int, int>(columnTwoIndex, columnOneIndex));
        }


    }
}
