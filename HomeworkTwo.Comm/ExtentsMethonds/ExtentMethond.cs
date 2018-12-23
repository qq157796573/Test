using HomeworkTwo.Comm.AttributeExtents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTwo.Comm.ExtentsMethonds
{
    /// <summary>
    /// 使用扩展方法类
    /// </summary>
    public static class ExtentMethond
    {
        /// <summary>
        ///      struct, IComparable, IConvertible, IFormattable
        ///      在泛型约束中没有直接约束枚举类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string GetUserStateName(this Enum value)
        {
            Type type = value.GetType();
            string name = value.ToString();
            var fieid = type.GetField(name);
            if (fieid.IsDefined(typeof(NameAttribute), true))
            {
                foreach (var attribute in fieid.GetCustomAttributes(true))
                {
                    if (attribute is NameAttribute)
                    {
                        NameAttribute nameAttribute = (NameAttribute)attribute;
                        name = nameAttribute.Name;
                    }
                }
            }
            return name;
        }

        public static string GetDisplayName<T>(this T t)
        {
            Type type = typeof(T);
            string name = type.Name;
            Type attributeType = typeof(DisplayNameAttribute);

            foreach (var attribute in type.GetCustomAttributes(true))
            {
                if (type.IsDefined(attributeType, true))
                {
                    DisplayNameAttribute displayNameAttribute = attribute as DisplayNameAttribute;
                    if (displayNameAttribute != null)
                    {
                        name = displayNameAttribute.Name;
                        break;
                    }   
                }
            }
            return name;
        }
    }
}
