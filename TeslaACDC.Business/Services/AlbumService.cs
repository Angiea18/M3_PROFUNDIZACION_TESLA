using System.Net;
using TeslaACDC.Business.Interfaces;
using TeslaACDC.Data;
using TeslaACDC.Data.Models;

namespace TeslaACDC.Business.Services;

public class AlbumService : IAlbumService
{
    private readonly IUnitOfWork _unitOfWork;

    public AlbumService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseMessage<Album>> GetAllAlbums()
    {
        var albums = await _unitOfWork.AlbumRepository.GetAllAsync();
        return BuildResponse(albums.ToList(), "", albums.Any() ? HttpStatusCode.OK : HttpStatusCode.NotFound, albums.Count());
    }

    public async Task<BaseMessage<Album>> AddAlbum(Album album)
    {
        var validationMessage = ValidateAlbum(album);
        if (!string.IsNullOrEmpty(validationMessage))
        {
            return BuildResponse(null, validationMessage, HttpStatusCode.BadRequest, 0);
        }

        try
        {
            await _unitOfWork.AlbumRepository.AddAsync(album);
            await _unitOfWork.SaveAsync();
            return BuildResponse(new List<Album> { album }, "Álbum agregado exitosamente.", HttpStatusCode.Created, 1);
        }
        catch (Exception ex)
        {
            return BuildResponse(null, $"Error interno: {ex.Message}", HttpStatusCode.InternalServerError, 0);
        }
    }

    public async Task<BaseMessage<Album>> FindAlbumById(int id)
    {
        var album = await _unitOfWork.AlbumRepository.FindAsync(id);
        return album != null ?
            BuildResponse(new List<Album> { album }, "", HttpStatusCode.OK, 1) :
            BuildResponse(null, "Álbum no encontrado.", HttpStatusCode.NotFound, 0);
    }

    public async Task<BaseMessage<Album>> FindAlbumByName(string name)
    {
        var albums = await _unitOfWork.AlbumRepository.GetAllAsync(x => x.Name.ToLower().Contains(name.ToLower()));
        return BuildResponse(albums.ToList(), "", albums.Any() ? HttpStatusCode.OK : HttpStatusCode.NotFound, albums.Count());
    }

    public async Task<BaseMessage<Album>> FindAlbumByRange(int year1, int year2)
    {
        if (year1 > year2 || year1 < 1901 || year2 > DateTime.Now.Year)
        {
            return BuildResponse(null, "Rango de años inválido (1901 - año actual).", HttpStatusCode.BadRequest, 0);
        }

        var albums = await _unitOfWork.AlbumRepository.GetAllAsync(album => album.Year >= year1 && album.Year <= year2);
        return BuildResponse(albums.ToList(), "", albums.Any() ? HttpStatusCode.OK : HttpStatusCode.NotFound, albums.Count());
    }

    public async Task<BaseMessage<Album>> UpdateAlbum(int id, Album album)
    {
        var albumEntity = await _unitOfWork.AlbumRepository.FindAsync(id);
        if (albumEntity == null)
        {
            return BuildResponse(null, "Álbum no encontrado.", HttpStatusCode.NotFound, 0);
        }

        albumEntity.Name = album.Name;
        albumEntity.Year = album.Year;
        albumEntity.ArtistId = album.ArtistId;
        albumEntity.Artist = album.Artist;
        albumEntity.Genre = album.Genre;

        try
        {
            _unitOfWork.AlbumRepository.Update(albumEntity);
            await _unitOfWork.SaveAsync();
            return BuildResponse(new List<Album> { albumEntity }, "Álbum actualizado exitosamente.", HttpStatusCode.OK, 1);
        }
        catch (Exception ex)
        {
            return BuildResponse(null, $"Error al actualizar: {ex.Message}", HttpStatusCode.InternalServerError, 0);
        }
    }

    public async Task<BaseMessage<Album>> DeleteAlbum(int id)
    {
        var album = await _unitOfWork.AlbumRepository.FindAsync(id);
        if (album == null)
        {
            return BuildResponse(null, "Álbum no encontrado.", HttpStatusCode.NotFound, 0);
        }

        try
        {
            _unitOfWork.AlbumRepository.Delete(album);
            await _unitOfWork.SaveAsync();
            return BuildResponse(new List<Album> { album }, "Álbum eliminado exitosamente.", HttpStatusCode.OK, 1);
        }
        catch (Exception ex)
        {
            return BuildResponse(null, $"Error al eliminar: {ex.Message}", HttpStatusCode.InternalServerError, 0);
        }
    }

    private string ValidateAlbum(Album album)
    {
        if (string.IsNullOrWhiteSpace(album.Name))
        {
            return "El nombre del álbum es requerido.";
        }
        if (album.Year < 1901 || album.Year > DateTime.Now.Year)
        {
            return "El año del álbum debe estar entre 1901 y el año actual.";
        }
        return string.Empty;
    }

    private BaseMessage<Album> BuildResponse(List<Album>? albums, string message, HttpStatusCode status, int totalElements)
    {
        return new BaseMessage<Album>
        {
            Message = message,
            StatusCode = status,
            TotalElements = totalElements,
            ResponseElements = albums ?? new List<Album>()
        };
    }
}