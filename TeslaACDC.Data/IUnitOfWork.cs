using System;
using TeslaACDC.Data.Models;

namespace TeslaACDC.Data;

public interface IUnitOfWork
{
    IRepository<int, Artist> ArtistRepository { get; }
    IRepository<int, Album> AlbumRepository { get; }
    IRepository<int, Song> SongRepository { get; }

    Task SaveAsync();
}