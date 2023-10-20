using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Movies.Queries
{
    public  class GetPaginatedMoviesQuery : IRequest<PaginatedList<MovieDTO>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Search {  get; set; } = string.Empty;
    }

    public class GetPaginatedMoviesQueryHandler : IRequestHandler<GetPaginatedMoviesQuery, PaginatedList<MovieDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetPaginatedMoviesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<MovieDTO>> Handle(GetPaginatedMoviesQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Movie> query;
            if (string.IsNullOrEmpty(request.Search))
                query = _context.Movies.AsNoTracking();
            else
                query = _context.Movies.AsNoTracking()
                    .Where(m => m.Title.Contains(request.Search)
                        || m.Description.Contains(request.Search)
                        || m.Genres.Contains(request.Search)
                        || m.Starring.Contains(request.Search)
                        || m.Director.Contains(request.Search));

            return await query
                .OrderBy(m => m.Title)
                .ProjectTo<MovieDTO>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);

        }
    }
}
