using BitCoinPricesWebAPI.Core.Entities;
using BitCoinPricesWebAPI.Core.Models;
namespace BitCoinPricesWebAPI.Service.Interfaces
{
    public interface ISystemUserService
    {
        Task<string> AuthenticateUser(UserModel user);
    }
}
