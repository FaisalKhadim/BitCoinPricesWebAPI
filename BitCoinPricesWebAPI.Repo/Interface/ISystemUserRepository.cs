using BitCoinPricesWebAPI.Core.Entities;
using BitCoinPricesWebAPI.Core.Models;
namespace BitCoinPricesWebAPI.Repo.Interface
{
    public interface ISystemUserRepository 
    {
        Task<dynamic> AuthenticateUser(UserModel user);
    }
}
