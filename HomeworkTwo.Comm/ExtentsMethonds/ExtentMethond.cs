using HomeworkTwo.Comm.AttributeExtents;
using HomeworkTwo.Comm.AttributeExtents.Validata;
using HomeworkTwo.Comm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTwo.Comm.ExtentsMethonds
{
    /// <summary>
    /// 使用扩展方法类
    /// </summary>
    public static class ExtentMethond
    {
        #region 枚举中文名称
        /// <summary>
        /// 获取枚举特性的名称，没有就放回当前属性名称     
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string GetEnumName(this Enum value)
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
        #endregion

        #region 测试数据
        public static string MyLabelFor<TModel, TValue>(Expression<Func<TModel, TValue>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }
            MemberExpression memberExpression = (MemberExpression)expression.Body;
            PropertyInfo propertyInfo = memberExpression.Member as PropertyInfo;
            return propertyInfo.GetDisplayName();
        }
            #endregion

            #region 获取属性中文名称
            /// <summary>
            /// 获取中文名称
            /// </summary>
            /// <param name="property">当前中文名称</param>
            /// <returns></returns>
            public static string GetDisplayName<T>(Func<T> func) where T : class,new()
        {
            T t = new T();
            return "";

            //Type type
            //return func.Invoke();
            //string name;
            //if (MyCache.GetCache($"property{property.Name}") == null)
            //{
            //    Type attributeType = typeof(DisplayNameAttribute);
            //    name = property.Name;
            //    foreach (var attribute in property.GetCustomAttributes(true))
            //    {
            //        DisplayNameAttribute displayNameAttribute = attribute as DisplayNameAttribute;
            //        if (displayNameAttribute != null)
            //        {
            //            name = displayNameAttribute.Name;
            //            break;
            //        }
            //    }
            //    MyCache.SetCache($"property{property.Name}", name);
            //}
            //else
            //{
            //    name = MyCache.GetCache($"property{property.Name}").ToString();
            //}
            //return name;

        }
        #endregion

        #region 获取属性中文名称
        /// <summary>
        /// 获取中文名称
        /// </summary>
        /// <param name="property">当前中文名称</param>
        /// <returns></returns>
        public static string GetDisplayName(this PropertyInfo property)
        {
            Type attributeType = typeof(DisplayNameAttribute);
            string name = property.Name;
            foreach (var attribute in property.GetCustomAttributes(true))
            {
                DisplayNameAttribute displayNameAttribute = attribute as DisplayNameAttribute;
                if (displayNameAttribute != null)
                {
                    name = displayNameAttribute.Name;
                    break;
                }
            }
            return name;
        }
        #endregion
        #region 获取数据库对应字段名称
        public static string GetPropName(this PropertyInfo propertyInfo)
        {
            string name;
            if (MyCache.GetCache($"propertyInfo{propertyInfo.Name}") == null)
            {
                name = propertyInfo.Name;
                Type attributeType = typeof(NameAttribute);
                NameAttribute nameAttribute = propertyInfo.GetCustomAttribute(attributeType, true) as NameAttribute;
                if (nameAttribute != null)
                    name = nameAttribute.Name;
                MyCache.SetCache($"propertyInfo{propertyInfo.Name}", name);
            }
            else
            {
                name = MyCache.GetCache($"propertyInfo{propertyInfo.Name}").ToString();
            }

            return name;
        }
        #endregion

        #region 获取类类名
        public static string GetClassName<T>(this T t) where T : class
        {
            string name;
            if (MyCache.GetCache($"Class{typeof(T).Name}") == null)
            {
                Type type = typeof(T);
                name = type.Name;
                NameAttribute nameAttribute = type.GetCustomAttribute(typeof(NameAttribute), true) as NameAttribute;
                if (nameAttribute != null)
                    name = nameAttribute.Name;
                MyCache.SetCache($"Class{typeof(T).Name}", name);
            }
            else
            {
                name = MyCache.GetCache($"Class{typeof(T).Name}").ToString();
            }
            return name;
        }

        public static IEnumerable<ValidataErrorModel> Validata<T>(this T t) where T : BaseModel
        {
            Type type = typeof(T);
            List<ValidataErrorModel> validataErrorModels = validataErrorModels = new List<ValidataErrorModel>();
            foreach (PropertyInfo prop in type.GetProperties())
            {
                if (prop.IsDefined(typeof(AbstractValidataAttribute), true))
                {
                    IEnumerable<AbstractValidataAttribute> abstractValidataAttribute = prop.GetCustomAttributes<AbstractValidataAttribute>(true) as IEnumerable<AbstractValidataAttribute>;
                    foreach (var validataAttribute in abstractValidataAttribute)
                    {
                        //validataErrorModels.Add(validataAttribute.Validata(prop.GetValue(t)));
                        ValidataErrorModel validataErrorModel = validataAttribute.Validata(prop.GetValue(t));
                        if (validataErrorModel.IsError) //只返回错误信息
                        {
                            validataErrorModel.ErrorMsg = prop.GetDisplayName() + validataErrorModel.ErrorMsg;
                            validataErrorModels.Add(validataErrorModel);
                            //yield return validataErrorModel;
                        }
                    }
                }
            }
            return validataErrorModels;
        }
        #endregion
    }
}
