using api.pdorado.Data;
using api.pdorado.Data.Models;
using api.pdorado.Servicios.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using pdorado.data.Models;

namespace api.pdorado.Servicios
{
    public class EditorService : IDataService<EditorDTO, Editor>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public EditorService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private async Task<Editor> ConvertDB(EditorDTO dto)
        {
            Editor db = _mapper.Map<Editor>(dto);

            var colecciones = new List<Coleccion>();

            foreach (int idColeccion in dto.ColeccionIds)
            {
                Coleccion coleccionDB = await _context.Coleccion.FindAsync(idColeccion);
                if (coleccionDB != null)
                {
                    colecciones.Add(coleccionDB);
                }
            }

            return db;
        }

        private EditorDTO ConvertDTO(Editor db)
        {
            EditorDTO dto = _mapper.Map<EditorDTO>(db);

            dto.ColeccionIds = db.Colecciones.Select(x => x.Id).ToList();

            return dto;
        }

        public async Task<EditorDTO> Create(int idLenguaje, EditorDTO dto)
        {
            if (_context.Editor == null)
            {
                return null;
            }

            Editor db = await ConvertDB(dto);

            await _context.Editor.AddAsync(db);
            await _context.SaveChangesAsync();
            dto.Id = db.Id;

            return dto;
        }

        public async Task<bool> Delete(int id)
        {
            if (_context.Editor == null)
            {
                return false;
            }

            Editor db = await _context.Editor.FindAsync(id);
            if (db == null)
            {
                return false;
            }
            _context.Editor.Remove(db);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<EditorDTO> Get(int id, int idLenguaje)
        {
            if (_context.Editor == null)
            {
                return null;
            }
            Editor db = await _context.Editor.Include(x => x.Colecciones).FirstOrDefaultAsync(x => x.Id == id);

            if (db == null)
            {
                return null;
            }

            return ConvertDTO(db);
        }

        public async Task<List<EditorDTO>> GetAll(int idLenguaje)
        {
            if (_context.Editor == null)
            {
                return null;
            }

            List<Editor> dbs = await _context.Editor.Include(x => x.Colecciones).ToListAsync();
            List<EditorDTO> dtos = new List<EditorDTO>();
            foreach (Editor db in dbs)
            {
                dtos.Add(ConvertDTO(db));
            }

            return dtos;
        }

        public async Task<EditorDTO> Update(int id, int idLenguaje, EditorDTO dto)
        {
            if (_context.Editor == null)
            {
                return null;
            }

            if (await _context.Editor.FindAsync(id) == null)
            {
                return null;
            }

            Editor db = await ConvertDB(dto);

            await _context.SaveChangesAsync();

            return ConvertDTO(db);
        }
    }
}
