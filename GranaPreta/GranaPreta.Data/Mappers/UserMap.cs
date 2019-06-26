using System;
using Dapper.FluentMap.Dommel.Mapping;
using GranaPreta.Domain;

namespace GranaPreta.Data.Mappers
{
    public class UserMap: DommelEntityMap<User>
    {
        public UserMap()
        {
            ToTable("USER");
            Map(x => x.UserId).ToColumn("ID").IsKey();
            Map(x => x.Name).ToColumn("NAME");
            Map(x => x.Email).ToColumn("EMAIL");
            Map(x => x.AccessKey).ToColumn("ACCESS_KEY");
        }
    }
}
