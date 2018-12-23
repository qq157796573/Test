using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTwo.IDal
{
    public interface ISqlHerper
    {
        List<T> QueryAll<T>() where T : new();
        T QueryOne<T>(int id) where T : new();
        bool Insert<T>(T t);
        bool Update<T>(T t);
        bool Delete<T>(int id);
    }
}
