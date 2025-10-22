using ApiProject.Data;
using ApiProject.Dtos;
using ApiProject.Methods;
using ApiProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;

namespace ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeersController : ControllerBase
    {
        private readonly PlayStationData _db;

        public BeersController(PlayStationData db)
        {
            _db = db;
        }

        [HttpPost("Beers/{id:int}")]
        public async Task<IActionResult> SaveBeer(TypeBeer type, int id)
        {

            var url = $"https://api.sampleapis.com/beers/{type}/{id}";
            var responseString = await Helpers.ConsultaApiExterna(url);

            var dtoBeer = JsonConvert.DeserializeObject<BeerDto>(responseString);

            var objBeer = new Beers()
            {
                price = Convert.ToDouble(dtoBeer.price.Replace("$", ""), CultureInfo.InvariantCulture),
                name = dtoBeer.name,
                image = dtoBeer.image,
                id = dtoBeer.id,
                type_beer = type
            };

            var (ok, msg) = await Helpers.SaveBeer(_db, id, objBeer);


            if (!ok)
                return Conflict(msg);

            return Ok(msg);
        }
    }
}
