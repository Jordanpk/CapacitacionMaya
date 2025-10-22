using ApiProject.Data;
using ApiProject.Methods;
using ApiProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PlayStationGamesController : ControllerBase
    {
        private readonly PlayStationData _db;

        public PlayStationGamesController(PlayStationData db)
        {
            _db = db;
        }

        [HttpGet("games")]
        public async Task<IActionResult> GamesAsync()
        {

            var responseString = await Helpers.ConsultaApiExterna("https://api.sampleapis.com/playstation/games");

            List<Games> objGames = JsonConvert.DeserializeObject<List<Games>>(responseString);
            return Ok(objGames);


        }


        [HttpGet("games/{id:int}")]
        public async Task<IActionResult> GamesByIdAsync(int id)
        {

            var responseString = await Helpers.ConsultaApiExterna($"https://api.sampleapis.com/playstation/games/{id}");

            var objGames = new Games();

            objGames = JsonConvert.DeserializeObject<Games>(responseString.ToString());

            return Ok(objGames);
        }

        [HttpPost("games/{id:int}")]
        public async Task<IActionResult> SaveGame(int id)
        {
            var responseString = await Helpers.ConsultaApiExterna($"https://api.sampleapis.com/playstation/games/{id}");

            var objGames = new Games();

            objGames = JsonConvert.DeserializeObject<Games>(responseString.ToString());

            var (ok, msg) = await Helpers.Save(_db, id, objGames);

            if (!ok)
            {
                return Ok(msg);
            }
            return Conflict(msg);

        }


        [HttpPost("Simpson/{id:int}")]
        public async Task<IActionResult> SaveSimpson(int id)
        {
            var responseString = await Helpers.ConsultaApiExterna($"https://api.sampleapis.com/simpsons/characters/{id}");

            var objSimpson = new Simpson();

            objSimpson = JsonConvert.DeserializeObject<Simpson>(responseString.ToString());

            var (ok, msg) = await Helpers.SaveSimpson(_db, id, objSimpson);

            if (!ok)
                return Ok(msg);
            return Conflict(msg);

        }


        [HttpPut("Simpson/{id:int}")]
        public async Task<IActionResult> UpdateSimp(int id, [FromBody] Simpson simpson)
        {
            var (result, msg) = await Helpers.UpdateCharacter(_db, id, simpson);
            if(result)
                return Ok(msg);
            else 
                return NotFound(msg);
        }



        [HttpPost("Movies/{genre}/{id:int}")]
        public async Task<IActionResult> SaveMovies(MovieGenre genre, int id)
        {
            var slug = genre switch
            {
                MovieGenre.ActionAdventure => "action-adventure",
                MovieGenre.SciFiFantasy => "scifi-fantasy",
                _ => genre.ToString().ToLower()
            };

            var url = $"https://api.sampleapis.com/movies/{slug}/{id}";
            var responseString = await Helpers.ConsultaApiExterna(url);

            var objMovies = JsonConvert.DeserializeObject<Movies>(responseString);

            objMovies.genero = genre;

            var (ok, msg) = await Helpers.SaveMovies(_db, id, objMovies);



            if (!ok)
                return Conflict(msg);

            return Ok(msg);
        }

    }

}
