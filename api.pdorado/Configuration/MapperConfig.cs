using pdorado.data.Models;
using api.pdorado.Data.Models;
using AutoMapper;

namespace api.pdorado.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            #region Comic
            CreateMap<Comic, ComicDTO>()
                .ForMember(dto => dto.NombreAutor, x => x.MapFrom(bd => $"{bd.Autor.Nombre} {bd.Autor.Apellidos}"))
                .ForMember(dto => dto.NombreColeccion, x => x.MapFrom(bd => bd.Coleccion.Nombre));

            CreateMap<ComicDTO, Comic>()
                .ForPath(bd => bd.Autor.Id, x => x.MapFrom(dto => dto.IdAutor))
                .ForPath(bd => bd.Genero.Id, x => x.MapFrom(dto => dto.IdGenero))
                .ForPath(bd => bd.Coleccion.Id, x => x.MapFrom(dto => dto.IdColeccion))
                .ForPath(bd => bd.Estado.Id, x => x.MapFrom(dto => dto.IdEstado));
            #endregion

            #region Autor
            CreateMap<Autor, AutorDTO>()
                .ForMember(dto => dto.ComicIds, x => x.Ignore());

            CreateMap<AutorDTO, Autor>()
                .ForMember(bd => bd.Comics, x => x.Ignore());
            #endregion

            #region Coleccion
            CreateMap<Coleccion, ColeccionDTO>()
                .ForMember(dto => dto.NombreEditor, x => x.MapFrom(bd => bd.Editor.Nombre));

            CreateMap<ColeccionDTO, Coleccion>()
                .ForPath(bd => bd.Editor.Id, x => x.MapFrom(dto => dto.IdEditor));
            #endregion

            #region Genero
            CreateMap<Genero, GeneroDTO>();
            CreateMap<GeneroDTO, Genero>();
            #endregion

            #region Estado
            CreateMap<Estado, EstadoDTO>();
            CreateMap<EstadoDTO, Estado>();
            #endregion

            #region Editor
            CreateMap<Editor, EditorDTO>();
            CreateMap<EditorDTO, Editor>();
            #endregion

            #region Usuario
            CreateMap<Usuario, UsuarioDTO>();
            CreateMap<UsuarioDTO, Usuario>();
            #endregion
        }
    }
}
