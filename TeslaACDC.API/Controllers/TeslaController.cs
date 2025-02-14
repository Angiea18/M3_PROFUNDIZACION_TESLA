// namespace TeslaACDC.Controllers;

// using Microsoft.AspNetCore.Mvc;
// using TeslaACDC.Business.Interfaces;
// using TeslaACDC.Business.Services;
// using TeslaACDC.Data.Models;

// [ApiController]
// [Route("api/[controller]")]
// public class TeslaController : ControllerBase
// {
//     private readonly IAlbumService _albumService;
//     private readonly IMatematica _matematica;

//     public TeslaController(IAlbumService albumService, IMatematica matematica)
//     {
//         _albumService = albumService;
//         _matematica = matematica;
//     }

//     [HttpGet]
//     [Route("GetAlbum")]
//     public async Task<IActionResult> GetAlbum()
//     {
//         var lista = await _albumService.GetList();
//         return Ok(lista);           
//     }
    
//     [HttpPost]
//     [Route("AddAlbum")]
//     public async Task<IActionResult> AddAlbum([FromBody] Album album)
//     {
//         var updatedList = await _albumService.AddAlbum(album);
//         return Ok(updatedList);
//     }
    

//         // 2. Sumar dos valores
//     [HttpPost]
//     [Route("Sum")]
//     public async Task<IActionResult> Sum([FromBody] NumbersToSum numbers)
//     {
//         var result = await _matematica.Sum(numbers.Value1, numbers.Value2);
//         return Ok(new { Resultado = result });
//     }

//     // 3. Calcular el área de un cuadrado
//     [HttpPost]
//     [Route("SquareArea")]
//     public async Task<IActionResult> SquareArea([FromBody] Square square)
//     {
//         var area = await _matematica.SquareArea(square.SideLength);
//         return Ok(new { Area = area });
//     }

//     // 4. Calcular el área de un triángulo
//     [HttpPost]
//     [Route("TriangleArea")]
//     public async Task<IActionResult> TriangleArea([FromBody] Triangle triangle)
//     {
//         var area = await Task.Run(() => (triangle.Base * triangle.Height) / 2);
//         return Ok(new { Area = area });
//     }

//     // 5. Calcular el área de un cuadrado recibiendo todos los lados
//     [HttpPost]
//     [Route("SquareAreaDetailed")]
//     public async Task<IActionResult> SquareAreaDetailed([FromBody] Square square)
//     {
//     var area = await _matematica.SquareArea(square.SideLength);
//     return Ok(new { Area = area });
//     }
// }
    
    