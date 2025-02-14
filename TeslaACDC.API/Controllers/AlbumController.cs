namespace TeslaACDC.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using TeslaACDC.Business.Interfaces;
using TeslaACDC.Data.Models;

[ApiController]
[Route("api/[controller]")]
public class AlbumController : ControllerBase
{
    private readonly IAlbumService _albumService;

    public AlbumController(IAlbumService albumService)
    {
        _albumService = albumService;
    }

    
    [HttpGet]
    [Route("GetAllAlbum")]
    public async Task<IActionResult> GetAllAlbums()
    {
        var albums = await _albumService.GetAllAlbums();
        return Ok(albums);
    }

    
    [HttpGet]
    [Route("GetById")]
    public async Task<IActionResult> GetAlbumById(int id)
    {
        var album = await _albumService.FindAlbumById(id);
        if (album is null)
            return NotFound($"No se encontró un álbum con el ID {id}.");
        
        return Ok(album);
    }

    
    [HttpGet]
    [Route("GetByName")]
    public async Task<IActionResult> GetAlbumByName(string name)
    {
        var album = await _albumService.FindAlbumByName(name);
        if (album is null)
            return NotFound($"No se encontró un álbum con el nombre '{name}'.");
        
        return Ok(album);
    }

    
    [HttpGet]
    [Route("GetByRange/{year1}/{year2}")]
    public async Task<IActionResult> GetAlbumsByRange(int year1, int year2)
    {
        var albums = await _albumService.FindAlbumByRange(year1, year2);
        return Ok(albums);
    }

    
    [HttpPost]
    [Route("CreateAlbum")]
      public async Task<IActionResult> AddAlbum(Album album)
    {
        var newAlbum = await _albumService.AddAlbum(album);
        return Ok(newAlbum);
    }

   
    [HttpPut]
     [Route("Update/{id}")]
     public async Task<IActionResult> UpdateAlbum(int id, Album album)
    {
       var updatedAlbum = await _albumService.UpdateAlbum(id, album);
        return Ok(updatedAlbum);
    }

    
     [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> DeleteAlbum(int id)
    {
        var deleteAlbum = await _albumService.DeleteAlbum(id);
        return Ok(deleteAlbum);
    }
}
