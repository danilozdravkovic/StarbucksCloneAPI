using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StarbuckClone.Domain;
using StarbuckClone.Implementation.Extensions;
using StarbucksClone.Application.DTO;
using StarbucksClone.Application.UseCases.Queries.Users;
using StarbucksClone.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarbuckClone.Implementation.UseCases.Queries.Users
{
    public class EFSearchUsersQuery : ISearchUsersQuery
    {
        public readonly SCContext _context;
        public readonly IMapper _mapper;

        public EFSearchUsersQuery(SCContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public int Id => 6;

        public string Name => "Search users";

        public PagedResponse<UserDto> Execute(UserSearchDto search)
        {
            IQueryable<User> query = _context.Users.Include(u => u.UseCases).AsQueryable();

            if (!string.IsNullOrEmpty(search.Username))
            {
                query = query.Where(x => x.Username.ToLower().Contains(search.Username.ToLower()));
            }

            if (!string.IsNullOrEmpty(search.FirstName))
            {
                query = query.Where(x => x.FirstName.ToLower().Contains(search.FirstName.ToLower()));
            }

            if (!string.IsNullOrEmpty(search.LastName))
            {
                query = query.Where(x => x.LastName.ToLower().Contains(search.LastName.ToLower()));
            }

            if (!string.IsNullOrEmpty(search.Email))
            {
                query = query.Where(x => x.Email.ToLower().Contains(search.Email.ToLower()));
            }

            if (!string.IsNullOrEmpty(search.Role))
            {
                query = query.Where(x => _context.Roles.Where(r=>r.Name==search.Role).FirstOrDefault().Id == x.Id);
            }

            if (search.RegistratedDateFrom.HasValue)
            {
                query = query.Where(x => x.CreatedAt > search.RegistratedDateFrom);
            }

            if (search.RegistratedDateTo.HasValue)
            {
                query = query.Where(x => x.CreatedAt < search.RegistratedDateTo);
            }
            if (search.IsActive.HasValue)
            {
                query = query.Where(x => x.IsActive==search.IsActive);
            }

            return query.AddPagination<User,UserDto>(search.Page, search.PerPage, _mapper);

           
        }
    }
}
