namespace TeslaACDC.Business.Services;

using System.Net;
using System.Threading.Tasks;
using TeslaACDC.Business.Interfaces;
using TeslaACDC.Data;
using TeslaACDC.Data.Models;
using System.Collections.Generic;
using System.Linq;

public class ArtistService : IArtistService
{
    private readonly IUnitOfWork _unitOfWork;

    public ArtistService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseMessage<Artist>> GetAllArtist()
    {
        var lista = await _unitOfWork.ArtistRepository.GetAllAsync();
        return lista.Any()
            ? BuildMessage(lista.ToList(), "Lista de artistas obtenida.", HttpStatusCode.OK, lista.Count())
            : BuildMessage(new List<Artist>(), "No se encontraron artistas.", HttpStatusCode.NotFound, 0);
    }

    public async Task<BaseMessage<Artist>> AddArtist(Artist artist)
    {
        if (artist == null || string.IsNullOrWhiteSpace(artist.Name))
        {
            return BuildMessage(null, "El nombre del artista no puede estar vacío.", HttpStatusCode.BadRequest, 0);
        }

        var existingArtists = await _unitOfWork.ArtistRepository.GetAllAsync();
        if (existingArtists.Any(a => a.Name.ToLower() == artist.Name.ToLower()))
        {
            return BuildMessage(null, "El nombre del artista ya existe.", HttpStatusCode.BadRequest, 0);
        }

        await _unitOfWork.ArtistRepository.AddAsync(artist);
        await _unitOfWork.SaveAsync();
        return BuildMessage(new List<Artist> { artist }, "Artista agregado exitosamente.", HttpStatusCode.Created, 1);
    }

    public async Task<BaseMessage<Artist>> FindArtistById(int id)
    {
        var artist = await _unitOfWork.ArtistRepository.FindAsync(id);
        return artist != null
            ? BuildMessage(new List<Artist> { artist }, "Artista encontrado.", HttpStatusCode.OK, 1)
            : BuildMessage(new List<Artist>(), "Artista no encontrado.", HttpStatusCode.NotFound, 0);
    }

    public async Task<BaseMessage<Artist>> FindArtistByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return BuildMessage(new List<Artist>(), "El nombre no puede estar vacío.", HttpStatusCode.BadRequest, 0);
        }

        var artists = await _unitOfWork.ArtistRepository.GetAllAsync(x => x.Name.ToLower().Contains(name.ToLower()));
        return artists.Any()
            ? BuildMessage(artists.ToList(), "Artistas encontrados.", HttpStatusCode.OK, artists.Count())
            : BuildMessage(new List<Artist>(), "No se encontraron artistas con ese nombre.", HttpStatusCode.NotFound, 0);
    }

    public async Task<BaseMessage<Artist>> UpdateArtist(int id, Artist artist)
    {
        var artistEntity = await _unitOfWork.ArtistRepository.FindAsync(id);
        if (artistEntity == null)
        {
            return BuildMessage(new List<Artist>(), "Artista no encontrado.", HttpStatusCode.NotFound, 0);
        }

        artistEntity.Name = artist.Name;
        artistEntity.Label = artist.Label;
        artistEntity.IsOnTour = artist.IsOnTour;

        _ = _unitOfWork.ArtistRepository.Update(artistEntity);
        await _unitOfWork.SaveAsync();
        return BuildMessage(new List<Artist> { artistEntity }, "Artista actualizado correctamente.", HttpStatusCode.OK, 1);
    }

    public async Task<BaseMessage<Artist>> DeleteArtist(int id)
    {
        var artist = await _unitOfWork.ArtistRepository.FindAsync(id);
        if (artist == null)
        {
            return BuildMessage(new List<Artist>(), "Artista no encontrado.", HttpStatusCode.NotFound, 0);
        }

        _ = _unitOfWork.ArtistRepository.Delete(artist);
        await _unitOfWork.SaveAsync();
        return BuildMessage(new List<Artist> { artist }, "Artista eliminado correctamente.", HttpStatusCode.OK, 1);
    }

    private BaseMessage<Artist> BuildMessage(List<Artist>? responseElements, string message, HttpStatusCode statusCode, int totalElements)
    {
        return new BaseMessage<Artist>
        {
            Message = message,
            StatusCode = statusCode,
            TotalElements = totalElements,
            ResponseElements = responseElements ?? new List<Artist>()
        };
    }
}
