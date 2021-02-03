using System.Threading.Tasks;

namespace Order.Hangfire
{
    public interface IWorker
    {
        Task Run();
    }
}