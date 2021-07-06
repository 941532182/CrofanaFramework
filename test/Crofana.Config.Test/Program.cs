using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;

namespace Crofana.Config.Test
{
    using Abstractions;
    using Newtonsoft.Json;
    using System.IO;

    class Character : IConfig
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public Weapon Weapon { get; private set; }
        public bool IsSetupProperly => Id > 0;
    }

    class Weapon : IConfig
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public int Price { get; private set; }
        public int ATK { get; private set; }
        public Character Owner { get; private set; }
        public bool IsSetupProperly => Id > 0;
    }

    struct Vector3
    {
        public float x { get; set; }
        float y { get; set; }
        float z { get; set; }
    }

    struct Test : IConfig
    {
        public long Id { get; private set; }
        public bool IsSetupProperly => Id > 0;
        public Vector3[] Arr { get; private set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var v = (Vector3)JsonSerializer.CreateDefault().Deserialize(new StringReader("{\"x\":2}"), typeof(Vector3));
            DataContext context = new DataContext(typeof(Program).Assembly);
            context.Load("D:\\a");
        }
    }
}
