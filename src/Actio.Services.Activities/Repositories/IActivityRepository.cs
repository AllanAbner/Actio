using Actio.Services.Activities.Domain.Models;
using System.Threading.Tasks;

namespace Actio.Services.Activities.Repositories
{
    public interface IActivityRepository
    {
        Task<Activity> GetAsync();

        Task AddAsync(Activity activity);
    }
}