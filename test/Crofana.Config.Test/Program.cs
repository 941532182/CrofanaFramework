using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;

namespace Crofana.Config.Test
{
    using Abstractions;
    using Newtonsoft.Json;
    using System.IO;

    public enum EGender
    {
        Male,
        Female,
    }

    class Program
    {
        static void Main(string[] args)
        {
            var dataContext = new CrofanaDataContext(typeof(Program).Assembly);
            dataContext.Load("D:\\Projects\\C#\\CrofanaFramework\\test\\Crofana.Config.Test\\Configs");

            var wzh = dataContext.GetObject<Character>(1);
            var vgy = dataContext.GetObject<Character>(2);

            Func<EGender, string> gender2str = gender => gender == EGender.Male ? "男" : "女";

            Console.WriteLine($"{wzh.Name}-{gender2str(wzh.Gender)}-{wzh.Soul.Name}");
            Console.WriteLine($"{vgy.Name}-{gender2str(vgy.Gender)}-{vgy.Soul.Name}");
        }
    }
}
