using TeslaACDC.Data.Models;

namespace TeslaACDC.Business.Interfaces;

public interface IAlbumService
{
    public Task<BaseMessage<Album>> GetAllAlbums();
    public Task<BaseMessage<Album>> FindAlbumById(int id);
    public Task<BaseMessage<Album>> FindAlbumByName(string name);
    public Task<BaseMessage<Album>> FindAlbumByRange(int year1, int year2);
    public Task<BaseMessage<Album>> AddAlbum(Album album);
    public Task<BaseMessage<Album>> UpdateAlbum(int id, Album album);
    public Task<BaseMessage<Album>> DeleteAlbum(int id);

}