using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HomeworkTwo.IDal;
using System.Configuration;

namespace HomeworkTwo.Factory
{
    public class FactoryInfo
    {
        private static string[] dalStr = ConfigurationManager.AppSettings["Dal"].Split(',');
        public static ISqlHerper CreateSqlHerperExample()
        {
            Assembly assembly = Assembly.Load(dalStr[0]);
            Type type = assembly.GetType(dalStr[1]);
            object o = Activator.CreateInstance(type);
            ISqlHerper sqlHerper = o as ISqlHerper;
            return sqlHerper;
        }
    }
}
