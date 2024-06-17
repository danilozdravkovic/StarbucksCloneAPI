using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using StarbuckClone.Domain;
using StarbucksClone.Application.DTO;
using StarbucksClone.Application.Exceptions;
using StarbucksClone.Application.UseCases.Queries.Users;
using StarbucksClone.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StarbuckClone.Implementation.UseCases.Queries.Users
{
    public class EFGetUserQuery : IGetUserQuery
    {
        public readonly SCContext _context;
        public readonly IMapper _mapper;
     
        public EFGetUserQuery(SCContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public int Id => 21;
        public string Name => "Get user";

        public UserDto Execute(int search)
        {
            var user = _context.Users.Where(x=>x.Id==search && x.IsActive);
            if (user.IsNullOrEmpty())
            {
                throw new NotFoundException(typeof(User).ToString(),search);
            }

            return _mapper.ProjectTo<UserDto>(user).FirstOrDefault();
        }
    }
}
