using api.pdorado.data.Models;
using api.pdorado.Data.Models;
using AutoMapper;

namespace api.pdorado.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Comic, ComicDTO>()
                .ForMember(dto => dto.NombreAutor, x => x.MapFrom(bd => bd.Autor.Nombre + bd.Autor.Apellidos))
                .ForMember(dto => dto.NombreColeccion, x => x.MapFrom(bd => bd.Coleccion.Nombre))
                .ForMember(dto => dto.IdAutor, x => x.MapFrom(bd => bd.Autor.Id))
                .ForMember(dto => dto.IdColeccion, x => x.MapFrom(bd => bd.Coleccion.Id))
                .ForMember(dto => dto.IdGenero, x => x.MapFrom(bd => bd.Genero.Id))
                .ForMember(dto => dto.IdEstado, x => x.MapFrom(bd => bd.Estado.Id));

            CreateMap<ComicDTO, Comic>()
                .ForMember(bd => bd.Autor.Id, x => x.MapFrom(dto => dto.IdAutor))
                .ForMember(bd => bd.Genero.Id, x => x.MapFrom(dto => dto.IdGenero))
                .ForMember(bd => bd.Coleccion.Id, x => x.MapFrom(dto => dto.IdColeccion))
                .ForMember(bd => bd.Estado.Id, x => x.MapFrom(dto => dto.IdEstado));
        }
    }
}
