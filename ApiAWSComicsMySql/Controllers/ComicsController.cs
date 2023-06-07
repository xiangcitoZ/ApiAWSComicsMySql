using ApiAWSComicsMySql.Models;
using ApiAWSComicsMySql.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAWSComicsMySql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComicsController : ControllerBase
    {
        private RepositoryComics repo;



        public ComicsController(RepositoryComics repo)
        {
            this.repo = repo;
        }



        [HttpGet]
        public async Task<ActionResult<List<Comic>>> GetComicList()
        {
            return await this.repo.GetComicsAsync();
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Comic>> Find(int id)
        {
            return await this.repo.FindComicAsync(id);
        }




        [HttpPost]
        public async Task<ActionResult> Create(Comic comic)
        {
            await this.repo.CreateComic(comic.Nombre, comic.Imagen);
            return Ok();
        }



        [HttpPut]
        public async Task<ActionResult> Update(Comic comic)
        {
            await this.repo.UpdateComic(comic.IdComic, comic.Nombre, comic.Imagen);
            return Ok();
        }



        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            await this.repo.DeleteComicAsync(id);
            return Ok();
        }



    }

   
}
