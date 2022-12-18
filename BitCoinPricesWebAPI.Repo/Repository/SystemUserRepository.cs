using Dapper;
using BitCoinPricesWebAPI.Core.Entities;
using BitCoinPricesWebAPI.Core.Models;
using BitCoinPricesWebAPI.Repo.Data;
using BitCoinPricesWebAPI.Repo.Interface;
using BitCoinPricesWebAPI.Repo.Repository.Base;
using System.Data;
namespace BitCoinPricesWebAPI.Repo.Repository
{
    public class SystemUserRepository : RepositoryBase<SystemUser>, ISystemUserRepository
    {
        public SystemUserRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
        public async Task<dynamic> AuthenticateUser(UserModel user)
        {
            var procedureName = "GetUser";
            var parameters = new DynamicParameters();
            parameters.Add("@UserName", user.UserName, DbType.String, ParameterDirection.Input);
            parameters.Add("@Password", user.Password, DbType.String, ParameterDirection.Input);
            var result = await QueryFirstOrDefaultAsync<dynamic>(procedureName, parameters);
            return result;
        }

    }
}
