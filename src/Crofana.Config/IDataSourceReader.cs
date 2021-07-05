using System;
using System.Collections.Generic;
using System.Text;

namespace Crofana.Config
{
    public interface IDataSourceReader
    {
        bool HasNext { get; }
        (Type type, IDataSet data) Read();
    }
}
