using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTwo.Comm.AttributeExtents
{
    public class DisplayNameAttribute : Attribute
    {
        public string Name { get; set; }
        public DisplayNameAttribute(string name)
        {
            this.Name = name;
        }
    }
}
