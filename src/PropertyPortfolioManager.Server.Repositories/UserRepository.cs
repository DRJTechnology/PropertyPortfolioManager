using Dapper;
using Microsoft.Data.SqlClient;
using PropertyPortfolioManager.Models.Dto.Profile;
using PropertyPortfolioManager.Server.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyPortfolioManager.Server.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection dbConnection;

        public UserRepository(IDbConnection dbConnection) {
            this.dbConnection = dbConnection;
        }

        public async Task<int> Create(UserDto user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            var parameters = new DynamicParameters();
            parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@ObjectIdentifier", user.ObjectIdentifier);
            parameters.Add("@Name", user.Name);

            await this.dbConnection.ExecuteAsync("profile.User_Create", parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<int>("@Id");
        }

        public async Task<UserDto> GetByObjectIdentifier(string userObjectIdentifierString)
        {
            var userObjectIdentifier = new Guid(userObjectIdentifierString);
            return await GetByObjectIdentifier(userObjectIdentifier);
        }

        public async Task<UserDto> GetByObjectIdentifier(Guid userObjectIdentifier)
        {
            var user = await dbConnection.QueryAsync<UserDto>("profile.User_GetByObjectIdentifier", 
                                                            new { @ObjectIdentifier = userObjectIdentifier },
                                                            commandType: CommandType.StoredProcedure);

            return user.SingleOrDefault()!;
        }
    }
}
