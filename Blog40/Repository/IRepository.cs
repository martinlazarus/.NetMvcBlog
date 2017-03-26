using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog40.Repository
{
    interface IRepository<T>
    {
        int Add(T model);
        IEnumerable<T> GetAll();
        T Get(int Id);
        int Update(T model);
        int Delete(int Id);
    }
}
