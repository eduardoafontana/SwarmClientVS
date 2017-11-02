using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwarmClientVS.Domain.IRepository
{
    public interface IRepository<T> where T : IData
    {
        void Save(T dataModel);
        void SaveLog(string log);

        //https://blog.falafel.com/implement-step-step-generic-repository-pattern-c/
        //IEnumerable<T> List { get; }
        //void Add(T entity);
        //void Delete(T entity);
        //void Update(T entity);
        //T FindById(int Id);
    }
}