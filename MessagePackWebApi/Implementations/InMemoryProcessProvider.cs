using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Models.Processes;

namespace MessagePackWebApi.Implementations
{
    public class InMemoryProcessProvider : IProcessProvider
    {
        Dictionary<int, Process> processes = new Dictionary<int, Process>();

        public Task<bool> Create(Process process)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

           var result = processes.TryAdd(process.Id, process);

            tcs.SetResult(result);

            return tcs.Task;
        }

        public Task<bool> Delete(int id)
        {

            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            var result = processes.Remove(id);

            tcs.SetResult(result);

            return tcs.Task;
        }

        public Task<Process> Get(int id)
        {
            TaskCompletionSource<Process> tcs = new TaskCompletionSource<Process>();
            Process process;

            var result = processes.TryGetValue(id, out process);

            tcs.SetResult(process);

            return tcs.Task;
        }

        public Task<IEnumerable<Process>> Get(Func<Process, bool> predicate, int take, int skip)
        {
            TaskCompletionSource<IEnumerable<Process>> tcs = new TaskCompletionSource<IEnumerable<Process>>();

           var result = this.processes.Values.Where(predicate).Skip(skip).Take(take);

            tcs.SetResult(result);

            return tcs.Task;
        }
    }
}
