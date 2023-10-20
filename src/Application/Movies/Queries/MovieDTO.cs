using Application.Common.Mappings;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Movies.Queries
{
    public class MovieDTO : IMapFrom<Movie>
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Genres { get; set; }

        public int Year { get; set; }

        public string? Country { get; set; }

        public string? Director { get; set; }

        public string? Starring { get; set; }

        public string? Original_Language { get; set; }

    }
}
