using Application.Common.Models;
using Application.Movies.Queries;
using Microsoft.AspNetCore.Mvc;

namespace PublicAPI.Controllers;

public class MoviesController : ApiControllerBase
{
    private readonly ILogger<MoviesController> _logger;

    public MoviesController(ILogger<MoviesController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<MovieDTO>>> GetPaginatedMovies([FromQuery] GetPaginatedMoviesQuery query)
    {
        return await Mediator.Send(query);
    }

}

