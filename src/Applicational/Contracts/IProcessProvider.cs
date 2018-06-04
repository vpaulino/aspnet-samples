using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Processes;

namespace Contracts
{
    public interface IProcessProvider
    {
        Task<Process> Get(int id);
        Task<bool> Create(Process process);
        Task<bool> Delete(int id);

        Task<IEnumerable<Process>> Get(Func<Process,bool> predicate, int top, int skip);
    }
}
