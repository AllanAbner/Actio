using System.Threading.Tasks;

namespace Actio.Common.Commands
{
    public interface IcommandHandler<in T> where T : ICommand
    {
         Task HandleAsync(T Command);
    }
}