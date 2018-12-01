using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using imdbApi.Repositories;
using ImdbDataRefresher.Models;
using Microsoft.AspNetCore.Mvc;

namespace imdbApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IRepository<Movie> movieRepository;

        public MoviesController (IRepository<Movie> movieRepository) {
            this.movieRepository = movieRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Movie>> Get()
        {
            return new JsonResult(movieRepository.GetAll ());
        }

        [HttpGet("{id}")]
        public ActionResult<Movie> Get(string id)
        {
            return new JsonResult (movieRepository.Get (id));
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }


        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
