using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ErrorHandler
{
    public interface IConsumer
    {
        Task ConsumeAsync();
    }
}
