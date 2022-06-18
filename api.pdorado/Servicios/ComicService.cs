using api.pdorado.Configuration;
using api.pdorado.Data;
using api.pdorado.Data.Models;
using api.pdorado.Servicios.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using pdorado.data.Models;

namespace api.pdorado.Servicios
{
    public class ComicService : IDataService<ComicDTO, Comic>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ComicService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Helper
        private ComicDTO ConvertDTO(Comic db, int idLenguaje)
        {
            ComicDTO dto = _mapper.Map<ComicDTO>(db);

            Comic_Lenguaje lenguaje = db.Lenguajes.FirstOrDefault(x => x.IdLenguaje == idLenguaje);
            Estado_Lenguaje estadoLenguaje = db.Estado.Lenguajes.FirstOrDefault(x => x.IdLenguaje == idLenguaje);
            Genero_Lenguaje generoLenguaje = db.Genero.Lenguajes.FirstOrDefault(x => x.IdLenguaje == idLenguaje);
            if (lenguaje == null || estadoLenguaje == null || generoLenguaje == null)
            {
                return null;
            }

            dto.Descripcion = lenguaje.Descripcion;
            dto.Titulo = lenguaje.Titulo;
            dto.NombreEstado = estadoLenguaje.Descripcion;
            dto.NombreGenero = generoLenguaje.Descripcion;

            return dto;
        }

        private async Task<Comic> ConvertDB(ComicDTO dto, int idLenguaje)
        {
            Comic db = _mapper.Map<Comic>(dto);

            Autor autorComic = await _context.Autor.FindAsync(dto.IdAutor);
            Coleccion coleccionComic = await _context.Coleccion.FindAsync(dto.IdColeccion);
            Genero generoComic = await _context.Genero.FindAsync(dto.IdGenero);
            Estado estadoComic = await _context.Estado.FindAsync(dto.IdEstado);

            if (autorComic != null)
            {
                db.Autor = autorComic;
            }

            if (coleccionComic != null)
            {
                db.Coleccion = coleccionComic;
            }

            if (generoComic != null)
            {
                db.Genero = generoComic;
            }

            if (estadoComic != null)
            {
                db.Estado = estadoComic;
            }

            Comic_Lenguaje? comicLenguaje;

            if ((comicLenguaje = await GetComicLenguaje(db.Id, idLenguaje)) != null)
            {
                comicLenguaje.ActualizadoPor = db.ActualizadoPor;
                comicLenguaje.ActualizadoFecha = db.ActualizadoFecha;
                comicLenguaje.Titulo = dto.Titulo;
                comicLenguaje.Descripcion = dto.Descripcion;
                _context.Entry(comicLenguaje).State = EntityState.Modified;
            }
            else
            {
                comicLenguaje = new Comic_Lenguaje
                {
                    IdComic = db.Id,
                    IdLenguaje = idLenguaje,
                    CreadoPor = db.CreadoPor,
                    CreadoFecha = db.CreadoFecha,
                    Titulo = dto.Titulo,
                    Descripcion = dto.Descripcion
                };
                var lenguajes = await CompletarLenguajes(db.Id, comicLenguaje);
                db.Lenguajes.AddRange(lenguajes);
                await _context.Comic_Lenguaje.AddRangeAsync(lenguajes);
            }

            return db;
        }

        private async Task<Comic_Lenguaje?> GetComicLenguaje(int id, int idLenguaje)
        {
            return await _context.Comic_Lenguaje.FindAsync(id, idLenguaje);
        }

        private async Task<List<Comic_Lenguaje>> CompletarLenguajes(int idComic, Comic_Lenguaje lenguajeExistente)
        {
            List<Comic_Lenguaje> lenguajes = new List<Comic_Lenguaje>
            {
                lenguajeExistente
            };

            foreach (int idioma in Sesion.Instance.Idiomas)
            {
                string idiomaTag = Sesion.GetIdiomaTag(idioma);

                var lenguaje = lenguajes.Where(x => x.IdLenguaje == idioma).FirstOrDefault();
                if (lenguaje == null)
                {
                    lenguajes.Add(new Comic_Lenguaje
                    {
                        IdComic = idComic,
                        IdLenguaje = idioma,
                        CreadoPor = lenguajeExistente.CreadoPor,
                        CreadoFecha = lenguajeExistente.CreadoFecha,
                        Descripcion = $"{idiomaTag} - {lenguajeExistente.Descripcion}",
                        Titulo = $"{idiomaTag} - {lenguajeExistente.Titulo}"
                    });
                }
            }

            return lenguajes;
        }
        #endregion

        public async Task<ComicDTO> Create(int idLenguaje, ComicDTO dto)
        {
            if (_context.Comic == null) 
            {
                return null;
            }

            Comic db = await ConvertDB(dto, idLenguaje);

            await _context.Comic.AddAsync(db);
            await _context.SaveChangesAsync();
            dto.Id = db.Id;

            return dto;
        }

        public async Task<bool> Delete(int id)
        {
            if (_context.Comic == null)
            {
                return false;
            }

            Comic db = await _context.Comic.FindAsync(id);
            if (db == null)
            {
                return false;
            }

            _context.Comic.Remove(db);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ComicDTO> Get(int id, int idLenguaje)
        {
            if (_context.Comic == null) 
            {
                return null;
            }

            Comic db = await _context.Comic.FindAsync(id);

            if (db == null)
            {
                return null;
            }

            return ConvertDTO(db, idLenguaje);
        }

        public async Task<List<ComicDTO>> GetAll(int idLenguaje)
        {
            if (_context.Comic == null)
            {
                return null;
            }

            List<Comic> dbs = await _context.Comic.ToListAsync();
            List<ComicDTO> dtos = new List<ComicDTO>();
            foreach (Comic db in dbs)
            {
                dtos.Add(ConvertDTO(db, idLenguaje));
            }

            return dtos;
        }

        public async Task<ComicDTO> Update(int id, int idLenguaje, ComicDTO dto)
        {
            if (_context.Comic ==  null)
            {
                return null;
            }

            if (await _context.Comic.FindAsync(id) == null)
            {
                return null;
            }

            Comic db = await ConvertDB(dto, idLenguaje);

            await _context.SaveChangesAsync();

            return ConvertDTO(db, idLenguaje);
        }
    }
}
