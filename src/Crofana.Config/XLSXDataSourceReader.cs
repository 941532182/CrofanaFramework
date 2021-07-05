using System;
using System.Collections.Generic;
using System.Text;

namespace Crofana.Config
{
    public class XLSXDataSourceReader : IDataSourceReader
    {
        public bool HasNext { get; private set; }

        public (Type type, IDataSet data) Read()
        {
            throw new NotImplementedException();
        }
    }
}
