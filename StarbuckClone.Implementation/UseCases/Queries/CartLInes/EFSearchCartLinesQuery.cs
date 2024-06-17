using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StarbuckClone.Domain;
using StarbuckClone.Implementation.Extensions;
using StarbucksClone.Application;
using StarbucksClone.Application.DTO;
using StarbucksClone.Application.UseCases.Queries.CartLines;
using StarbucksClone.DataAccess;

namespace StarbuckClone.Implementation.UseCases.Queries.CartLInes
{
    public class EFSearchCartLinesQuery : ISearchCartLinesQuery
    {
        public readonly IApplicationActor _actor;
        public readonly SCContext _context;
        public readonly IMapper _mapper;


        public EFSearchCartLinesQuery(IApplicationActor actor, SCContext context, IMapper mapper)
        {
            _actor = actor;
            _context = context;
            _mapper = mapper;
        }
        public int Id =>10;

        public string Name => "Search users in-cart products";

        public PagedResponse<CartLineDto> Execute(PagedSearchDto search)
        {
            var userId = _actor.Id;
            var userCartLines = _context.CartLines.Include(c => c.Product).Include(c => c.CartLinesAddIns).Where(c=>c.UserId==userId);

            return userCartLines.AddPagination<CartLine,CartLineDto>(search.Page, search.PerPage, _mapper) ;
        }
    }
}



