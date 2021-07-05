using Newtonsoft.Json;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Crofana.Config
{
    public class DataContext : DataContextBase
    {
        private const string XLSX_EXTENSION = ".xlsx";

        private Assembly m_domainAssembly;

        public DataContext(Assembly domainAssembly)
        {
            m_domainAssembly = domainAssembly;
        }

        private IDataSet GetOrAddDataSet(Type type)
        {
            if (!m_dataSetMap.ContainsKey(type))
            {
                m_dataSetMap[type] = new DataSet();
            }
            return m_dataSetMap[type];
        }

        private IConfig GetOrAddConfig(Type type, long id)
        {
            var dataSet = GetOrAddDataSet(type);
            if (!dataSet.HasObject(id))
            {
                var config = (IConfig)Activator.CreateInstance(type);
                //config.GetType().GetProperty("Id").SetValue(config, id);
                dataSet.AddObject(id, config);
            }
            return dataSet.GetObject(id);
        }

        private void Read(string path)
        {
            var workbook = WorkbookFactory.Create(path);

            var dataSheet = workbook.GetSheetAt(0);
            var metadataSheet = workbook.GetSheetAt(1);

            var typeName = dataSheet.SheetName;

            var type = m_domainAssembly.GetType(typeName);

            var declareRow = dataSheet.GetRow(0);
            var propList = new List<PropertyInfo>() { type.GetProperty("Id") };
            for (int i = 1; i < declareRow.LastCellNum; i++)
            {
                var cell = declareRow.GetCell(i);
                if (cell == null)
                {
                    // throw
                }
                propList.Add(type.GetProperty(cell.ToString().Split(':')[0]));
            }

            for (int i = 1; i <= dataSheet.LastRowNum; i++)
            {
                var row = dataSheet.GetRow(i);
                if (row != null)
                {
                    var instance = GetOrAddConfig(type, long.Parse(row.GetCell(0).ToString()));
                    for (int j = 0; j < row.LastCellNum; j++)
                    {
                        var cell = row.GetCell(j);
                        if (cell == null)
                        {
                            // throw
                        }
                        propList[j].SetValue(instance, ParseValue(propList[j].PropertyType, cell.ToString()));
                    }
                }
            }

            var dataSet = GetOrAddDataSet(type);

            for (int i = 0; i <= metadataSheet.LastRowNum; i++)
            {
                var row = metadataSheet.GetRow(i);
                if (row != null)
                {
                    var key = row.GetCell(0);
                    var value = row.GetCell(1);
                    if (key != null && value != null)
                    {
                        dataSet.AddMetadata(key.ToString(), value.ToString());
                    }
                }
            }
        }

        private void ReadDataSourcesRecursively(string path)
        {
            foreach (var file in Directory.GetFiles(path))
            {
                if (Path.GetExtension(file) == XLSX_EXTENSION)
                {
                    Read(file);
                }
            }
            foreach (var dir in Directory.GetDirectories(path))
            {
                ReadDataSourcesRecursively(dir);
            }
        }

        private object ParseValue(Type type, string value)
        {
            if (type.IsInterface || type.IsAbstract)
            {
                // throw
            }

            if (type.GetInterfaces().Contains(typeof(IConfig)))
            {
                return GetOrAddConfig(type, long.Parse(value));
            }
            if (m_parserMap.ContainsKey(type))
            {
                return m_parserMap[type](value);
            }
            else if (type.IsEnum)
            {
                return Enum.Parse(type, value);
            }
            else if (type.IsValueType)
            {
                return JsonSerializer.CreateDefault().Deserialize(new StringReader(value), type);
            }
            else if (type.IsArray)
            {
                var values = value.Split(',');
                var elementType = type.GetElementType();
                var array = Array.CreateInstance(elementType, values.Length);
                var setter = type.GetMethod("SetValue", new Type[] { typeof(object), typeof(long) });
                for (int i = 0; i < values.Length; i++)
                {
                    setter.Invoke(array, new object[] { ParseValue(elementType, values[i]), i });
                }
                return array;
            }
            else if (type.IsGenericType)
            {
                if (type.GetGenericTypeDefinition() == typeof(List<>))
                {
                    var values = value.Split(',');
                    var elementType = type.GenericTypeArguments[0];
                    var list = Activator.CreateInstance(type);
                    var adder = type.GetMethod("Add");
                    for (int i = 0; i < values.Length; i++)
                    {
                        adder.Invoke(list, new object[] { ParseValue(elementType, values[i]) });
                    }
                    return list;
                }
                else if (type.GetGenericTypeDefinition() == typeof(Dictionary<,>))
                {
                    var values = value.Split(',');
                    var keyType = type.GenericTypeArguments[0];
                    var valueType = type.GenericTypeArguments[1];
                    var dic = Activator.CreateInstance(type);
                    var adder = type.GetMethod("Add");
                    for (int i = 0; i < values.Length; i++)
                    {
                        var pair = values[i].Split(':');
                        adder.Invoke(dic, new object[] { ParseValue(keyType, pair[0]), ParseValue(valueType, pair[1]) });
                    }
                    return dic;
                }
            }

            return null;
        }


    }
}
