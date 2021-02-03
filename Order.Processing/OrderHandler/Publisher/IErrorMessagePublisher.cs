using System.Threading.Tasks;

namespace OrderHandler
{
    public interface IErrorMessagePublisher
    {
        Task Publish(string error);
    }
}