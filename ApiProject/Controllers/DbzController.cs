using ApiProject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ApiProject.Methods;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DbzController : ControllerBase
    {
        [HttpGet("characters")]
        public async Task<IActionResult> PersonajesAsync()
        {

            //var responseString = await Helpers.ConsultaApiExterna("https://dragonball-api.com/api/characters"); 

            var responseString = await Helpers.ConsultaApiExterna("https://dragonball-api.com/api/characters");

            JObject root = JObject.Parse(responseString.ToString());
            JToken nodoEspecifico = root["items"];
            var Pj = nodoEspecifico.ToObject<List<Characters>>();
            return Ok(Pj);
                

            //var personajes = await Helpers.ConsultaApiExterna<List<Characters>>("https://dragonball-api.com/api/characters", "items");
            //return Ok(personajes);

           
        }

        [HttpGet("characters/{id:int}")]
        public async Task<IActionResult> PersonajesByIdAsync(int id)
        {

            var responseString = await Helpers.ConsultaApiExterna($"https://dragonball-api.com/api/characters/{id}");

          

            var objCharacter = new CharacterById();

           objCharacter = JsonConvert.DeserializeObject<CharacterById>(responseString.ToString());

            return Ok(objCharacter.name);
        }

        [HttpGet("planets")]
        public async Task<IActionResult> PlanetasAsync()
        {

            var responseString = await Helpers.ConsultaApiExterna("https://dragonball-api.com/api/planets");

            JObject root = JObject.Parse(responseString.ToString());
            JToken nodoEspecifico = root["items"];
            var Plan = nodoEspecifico.ToObject<List<Planets>>();


            return Ok(Plan);
        }

        [HttpGet("planets/{id:int}")]
        public async Task<IActionResult> PlanetasByIdAsync(int id)
        {

            var responseString = await Helpers.ConsultaApiExterna($"https://dragonball-api.com/api/planets/{id}");


            var objPlanet = new Planets();

            objPlanet = JsonConvert.DeserializeObject<Planets>(responseString.ToString());


            return Ok(objPlanet);
        }
    }
}
