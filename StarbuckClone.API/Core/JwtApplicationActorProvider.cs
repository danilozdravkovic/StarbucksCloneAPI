﻿using Newtonsoft.Json;
using StarbuckClone.Implementation;
using StarbucksClone.Application;
using System.IdentityModel.Tokens.Jwt;

namespace StarbuckClone.API.Core
{
    public class JwtApplicationActorProvider : IApplicationActorProvider
    {
        private string authorizationHeader;

        public JwtApplicationActorProvider(string authorizationHeader)
        {
            this.authorizationHeader = authorizationHeader;
        }

        public IApplicationActor GetActor()
        {
            if (authorizationHeader.Split("Bearer ").Length != 2)
            {
                return new UnauthorizedActor();
            }

            string token = authorizationHeader.Split("Bearer ")[1];

            var handler = new JwtSecurityTokenHandler();

            var tokenObj = handler.ReadJwtToken(token);

            var claims = tokenObj.Claims;

            var claim = claims.First(x => x.Type == "jti").Value;

            var actor = new Actor
            {
                Email = claims.First(x => x.Type == "Email").Value,
                Username = claims.First(x => x.Type == "Username").Value,
                FirstName = claims.First(x => x.Type == "FirstName").Value,
                LastName = claims.First(x => x.Type == "LastName").Value,
                Id = int.Parse(claims.First(x => x.Type == "Id").Value),
                RoleId = int.TryParse(claims.FirstOrDefault(x => x.Type == "RoleId")?.Value, out var roleId) ? roleId : (int?)null,
                AllowedUseCases = JsonConvert.DeserializeObject<List<int>>(claims.First(x => x.Type == "UseCaseIds").Value)
            };

            return actor;
        }
    }
}
