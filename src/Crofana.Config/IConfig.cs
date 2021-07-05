using System;
using System.Collections.Generic;
using System.Text;

namespace Crofana.Config
{
    public interface IConfig
    {
        public long Id { get; }
        public bool IsSetupProperly { get; }
    }
}
