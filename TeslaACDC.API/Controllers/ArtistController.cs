using Microsoft.AspNetCore.Mvc;
using TeslaACDC.Business.Interfaces;
using TeslaACDC.Data.Models;
namespace TeslaACDC.Controllers;

[Controller]
[Route("api/[controller]")]
public class ArtistController : ControllerBase
{

    private readonly IArtistService _artistService;

    public ArtistController(IArtistService artistService)
    {
        _artistService = artistService;
    }

    [HttpGet]
    [Route("GetAllArtist")]
    public async Task<IActionResult> GetAllArtist()
    {
        var artist = await _artistService.GetAllArtist();
        return Ok(artist);
    }

    [HttpGet]
    [Route("GetById")]
    public async Task<IActionResult> GetArtistById(int id)
    {
        var artist = await _artistService.FindArtistById(id);
        return Ok(artist);
    }

    [HttpGet]
    [Route("GetByName")]
    public async Task<IActionResult> GetArtistByName(string name)
    {
        var artist = await _artistService.FindArtistByName(name);
        return Ok(artist);
    }

    [HttpPost]
    [Route("CreateArtist")]
    public async Task<IActionResult> AddArtist([FromBody] Artist artist)
    {
        var newArtist = await _artistService.AddArtist(artist);
        return Ok(newArtist);
    }

    [HttpPut]
    [Route("Update/{id}")]
    public async Task<IActionResult> UpdateArtist(int id, [FromBody] Artist artist)
    {
        var updatedArtist = await _artistService.UpdateArtist(id, artist);
        return Ok(updatedArtist);
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> DeleteArtist(int id)
    {
        var deleteArtist = await _artistService.DeleteArtist(id);
        return Ok(deleteArtist);
    }
}