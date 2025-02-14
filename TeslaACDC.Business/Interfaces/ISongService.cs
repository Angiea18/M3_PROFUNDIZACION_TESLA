using System;
using TeslaACDC.Data.Models;

namespace TeslaACDC.Business.Interfaces;

public interface ISongService
{
    public Task<BaseMessage<Song>> GetAllSongs();
    public Task<BaseMessage<Song>> FindSongById(int id);
    public Task<BaseMessage<Song>> FindSongByName(string name);
    public Task<BaseMessage<Song>> AddSong(Song song);
    public Task<BaseMessage<Song>> UpdateSong(int id, Song song);
    public Task<BaseMessage<Song>> DeleteSong(int id);

}