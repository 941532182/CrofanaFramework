using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using Crofana.Config;

namespace Crofana.Config.Test
{
    class Weapon : IConfig
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public int Price { get; private set; }
        public int ATK { get; private set; }
        public bool IsSetupProperly => Id > 0;
    }

    class DataContext : DataContextBase
    {
        public DataContext(Assembly domainAssembly) : base(domainAssembly) { }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DataContext context = new DataContext(typeof(Program).Assembly);
            context.Load("D:\\a");
        }
    }
}
