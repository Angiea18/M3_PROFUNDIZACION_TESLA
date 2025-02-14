using System;

namespace TeslaACDC.Business.Interfaces;
using TeslaACDC.Data.Models;


public interface IArtistService
{
    public Task<BaseMessage<Artist>> GetAllArtist();
    public Task<BaseMessage<Artist>> FindArtistById(int id);
    public Task<BaseMessage<Artist>> FindArtistByName(string name);
    public Task<BaseMessage<Artist>> AddArtist(Artist artist);
    public Task<BaseMessage<Artist>> UpdateArtist(int id, Artist artist);
    public Task<BaseMessage<Artist>> DeleteArtist(int id);
}