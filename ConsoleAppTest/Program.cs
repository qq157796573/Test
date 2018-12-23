using HomeworkTwo.Comm;
using HomeworkTwo.Model;
using System;
using System.Collections.Generic; 
using System.Linq;
using System.Reflection;
using HomeworkTwo.Comm.AttributeExtents;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Company company = new Company()
            {

                Id = 1,
                Name = "软谋教育",
                CreateTime = DateTime.Now,
                CreatorId = 1,
                LastModifierId = 1,
                LastModifyTime = DateTime.Now
            };

            Show(company);                   
            Console.ReadKey();
        }

        private static void Show<T>(T t)
        {
            Type type = typeof(T);
            foreach (PropertyInfo prop in type.GetProperties())
            {
                string name = prop.Name;//默认值
                if (prop.IsDefined(typeof(DisplayNameAttribute),true))
                {
                    DisplayNameAttribute nameAttribute = prop.GetCustomAttribute(typeof(DisplayNameAttribute), true) as DisplayNameAttribute;
                    if (nameAttribute != null)
                    {
                        name = nameAttribute.Name;
                    }
                }
                Console.WriteLine($"{name}:{prop.GetValue(t)}");
            }
        }
    }
}
