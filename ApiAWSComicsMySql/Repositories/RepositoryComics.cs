using ApiAWSComicsMySql.Data;
using ApiAWSComicsMySql.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiAWSComicsMySql.Repositories
{
    public class RepositoryComics
    {

        private ComicsContext context;

        public RepositoryComics(ComicsContext context)
        {
            this.context = context;
        }

        public async Task<List<Comic>> GetComicAsync()
        {
            return await this.context.Comics.ToListAsync();
        }

        public async Task<Comic> FindComicAsync(int id)
        {
            return await this.context.Comics
                .FirstOrDefaultAsync(x => x.IdComic == id);
        }

        private async Task<int> GetMaxIdComicAsync()
        {
            return await this.context.Comics.MaxAsync(x => x.IdComic) + 1;
        }

        public async Task CreateComic(string nombre, string imagen)
        {
            Comic comic = new Comic
            {
                IdComic = await this.GetMaxIdComicAsync(),
                Nombre = nombre,
                Imagen = imagen
            };
            this.context.Comics.Add(comic);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateComic(int id, string nombre, string imagen)
        {
            Comic comic = await this.FindComicAsync(id);
            comic.Nombre = nombre;
            comic.Imagen = imagen;
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteComic(int id)
        {
            Comic comic = await this.FindComicAsync(id);
            this.context.Comics.Remove(comic);
            await this.context.SaveChangesAsync();
        }

    }
}
