using Microsoft.AspNetCore.Mvc;
using TeslaACDC.Data.Models;
using TeslaACDC.Business.Interfaces;


namespace TeslaACDC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SongController : ControllerBase
    {
        private readonly ISongService _songService;

        public SongController(ISongService songService)
        {
            _songService = songService;
        }


        [HttpGet]
        [Route("GetAllSong")]
        public async Task<IActionResult> GetAllSongs()
        {
            var songs = await _songService.GetAllSongs();
            return Ok(songs);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetSongById(int id)
        {
            var song = await _songService.FindSongById(id);
            return Ok(song);
        }

        [HttpGet]
        [Route("GetByName")]
        public async Task<IActionResult> GetSongByName(string name)
        {
            var song = await _songService.FindSongByName(name);
            return Ok(song);
        }


        [HttpPost]
        [Route("CreateSong")]
        public async Task<IActionResult> AddSong(Song song)
        {
            var newSong = await _songService.AddSong(song);
            return Ok(newSong);
        }

        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateSong(int id, Song song)
        {
            var updatedSong = await _songService.UpdateSong(id, song);
            return Ok(updatedSong);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
           var deleteSong = await _songService.DeleteSong(id);
            return Ok(deleteSong);
        }

    }
}