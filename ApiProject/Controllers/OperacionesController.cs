using ApiProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace ApiProject.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class OperacionesController : Controller
    {
        [HttpGet]
        public int Result()
        {
            int R = 250 - 380 + 7900 / 3;
            return R;
        }

        [HttpPost]
        public IActionResult SumarValores([FromBody] Suma Sum)
        {

            if (Sum.Num1 >= 0 && Sum.Num2 > 0)
            {
                int resultado1 = Sum.Num1 + Sum.Num2;
                int resultado2 = Sum.Num1 - Sum.Num2;
                int resultado3 = Sum.Num1 * Sum.Num2;
                int resultado4 = Sum.Num1 / Sum.Num2;

                string respuesta =
                $"Suma = {resultado1}\r\n" +
                $"Resta = {resultado2}\r\n" +
                $"Multiplicación = {resultado3}\r\n" +
                $"División = {resultado4}";

                return Ok(respuesta);
            }

            if (Sum.Num1 >= 0 && Sum.Num2 >= 0)
            {
                int resultado11 = Sum.Num1 + Sum.Num2;
                int resultado22 = Sum.Num1 - Sum.Num2;
                int resultado33 = Sum.Num1 * Sum.Num2;
                string respuesta2 =
                   $"Suma = {resultado11}\r\n" +
                   $"Resta = {resultado22}\r\n" +
                   $"Multiplicación = {resultado33}\r\n";

                return Ok(respuesta2);
            }
            return BadRequest("Operación no permitida");


        }

    }
}
