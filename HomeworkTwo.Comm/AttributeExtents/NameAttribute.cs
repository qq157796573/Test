using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTwo.Comm
{
    [AttributeUsage(AttributeTargets.Class| AttributeTargets.Property | AttributeTargets.Field, AllowMultiple =false,Inherited =true)]
    public class NameAttribute : Attribute
    {
        public string Name { get; set; }
        public NameAttribute(string name)
        {
            this.Name = name;
        }
    }
}
