namespace TeslaACDC.Business.Services;

using System.Net;
using System.Threading.Tasks;
using TeslaACDC.Business.Interfaces;
using TeslaACDC.Data.Models;
using TeslaACDC.Data;
using System.Collections.Generic;
using System.Linq;

public class SongService : ISongService
{
    private readonly IUnitOfWork _unitOfWork;

    public SongService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseMessage<Song>> GetAllSongs()
    {
        var songs = await _unitOfWork.SongRepository.GetAllAsync();
        return songs.Any()
            ? BuildMessage(songs.ToList(), "", HttpStatusCode.OK, songs.Count())
            : BuildMessage(new List<Song>(), "No se encontraron canciones.", HttpStatusCode.NotFound, 0);
    }

    public async Task<BaseMessage<Song>> AddSong(Song song)
    {
        if (song == null || string.IsNullOrWhiteSpace(song.Name))
        {
            return BuildMessage(null, "El nombre de la canción no puede estar vacío.", HttpStatusCode.BadRequest, 0);
        }

        var existingSongs = await _unitOfWork.SongRepository.GetAllAsync();
        if (existingSongs.Any(s => s.Name.ToLower() == song.Name.ToLower()))
        {
            return BuildMessage(null, "El nombre de la canción ya existe.", HttpStatusCode.BadRequest, 0);
        }

        await _unitOfWork.SongRepository.AddAsync(song);
        await _unitOfWork.SaveAsync();

        return BuildMessage(new List<Song> { song }, "Canción agregada exitosamente.", HttpStatusCode.Created, 1);
    }

    public async Task<BaseMessage<Song>> FindSongById(int id)
    {
        var song = await _unitOfWork.SongRepository.FindAsync(id);
        return song != null
            ? BuildMessage(new List<Song> { song }, "", HttpStatusCode.OK, 1)
            : BuildMessage(new List<Song>(), "Canción no encontrada.", HttpStatusCode.NotFound, 0);
    }

    public async Task<BaseMessage<Song>> FindSongByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return BuildMessage(new List<Song>(), "El nombre no puede estar vacío.", HttpStatusCode.BadRequest, 0);
        }

        var songs = await _unitOfWork.SongRepository.GetAllAsync(x => x.Name.ToLower().Contains(name.ToLower()));
        return songs.Any()
            ? BuildMessage(songs.ToList(), "", HttpStatusCode.OK, songs.Count())
            : BuildMessage(new List<Song>(), "No se encontraron canciones con ese nombre.", HttpStatusCode.NotFound, 0);
    }

    public async Task<BaseMessage<Song>> UpdateSong(int id, Song song)
    {
        var songEntity = await _unitOfWork.SongRepository.FindAsync(id);
        if (songEntity == null)
        {
            return BuildMessage(new List<Song>(), "Canción no encontrada.", HttpStatusCode.NotFound, 0);
        }

        songEntity.Name = song.Name;
        songEntity.DurationSeg = song.DurationSeg;

        _ = _unitOfWork.SongRepository.Update(songEntity);
        await _unitOfWork.SaveAsync();
        return BuildMessage(new List<Song> { songEntity }, "Canción actualizada correctamente.", HttpStatusCode.OK, 1);
    }

    public async Task<BaseMessage<Song>> DeleteSong(int id)
    {
        var song = await _unitOfWork.SongRepository.FindAsync(id);
        if (song == null)
        {
            return BuildMessage(new List<Song>(), "Canción no encontrada.", HttpStatusCode.NotFound, 0);
        }

        _ = _unitOfWork.SongRepository.Delete(song);
        await _unitOfWork.SaveAsync();
        return BuildMessage(new List<Song> { song }, "Canción eliminada correctamente.", HttpStatusCode.OK, 1);
    }

    private BaseMessage<Song> BuildMessage(List<Song>? responseElements, string message, HttpStatusCode statusCode, int totalElements)
    {
        return new BaseMessage<Song>
        {
            Message = message,
            StatusCode = statusCode,
            TotalElements = totalElements,
            ResponseElements = responseElements ?? new List<Song>()
        };
    }
}