using HomeworkTwo.Comm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTwo.IDal
{
    public interface ISqlHerper
    {
        IEnumerable<T> QueryAll<T>() where T : BaseModel, new();
        T QueryOne<T>(int id) where T : BaseModel, new();
        bool Insert<T>(T t);
        bool Update<T>(T t);
        bool Delete<T>(int id);
    }
}
