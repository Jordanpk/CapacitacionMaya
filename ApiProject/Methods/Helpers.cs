using ApiProject.Data;
using ApiProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using static System.Net.WebRequestMethods;

namespace ApiProject.Methods
{
    public class Helpers
    {
        private static readonly HttpClient _http = new HttpClient();
        public static int metodo2(int num1, int num2)
        {
            return num1*num2;
        }

  

        public static async Task<string> ConsultaApiExterna(string url)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();



            return responseString;
        }

        public static async Task<(bool ok, string msg)> Save(PlayStationData dbContext, int id, Games objGames)
        {
            bool exist = await dbContext.Games.AnyAsync(g => g.id == id);

            if (exist)
            {
                return (false, "Ya existe este juego.");
            }

            objGames.releaseDatesString = objGames.releaseDates.Japan
                ?? objGames.releaseDates.Australia
                ?? objGames.releaseDates.NorthAmerica
                ?? objGames.releaseDates.Europe; 

            await dbContext.Games.AddAsync(objGames);
            await dbContext.SaveChangesAsync();

            return (true, "Guardado con éxito.");

        }

        public static async Task<(bool ok, string msg)> SaveSimpson(PlayStationData dbContext, int id, Simpson objSimpson)
        {
            bool exist = await dbContext.Simpson.AnyAsync(s => s.id == id);

            if (exist)
            {
                return (false, "Ya existe este personaje.");
            }
            
            await dbContext.Simpson.AddAsync(objSimpson);
            await dbContext.SaveChangesAsync();

            return (true, "Guardado con éxito.");

        }

        public static async Task<(bool ok, string msg)> UpdateCharacter(PlayStationData _db, int id, Simpson simpson) 
        {
            var simp = await _db.Simpson.FindAsync(id);

            if (simp != null)
            {
                simp.normalized_name = simpson.normalized_name;
                simp.gender = simpson.gender;

                await _db.SaveChangesAsync();

                return(true, "Cambios guardados correctamente.");
            }

            return(false, "No se encontró ningún registro con este id.");
        }


        public static async Task<(bool ok, string msg)> SaveMovies(PlayStationData dbContext, int id, Movies objMovies)
        {
            bool exist = await dbContext.Movies.AnyAsync(m => m.id == id);

            if (exist)
            {
                return (false, "Ya existe esta movie.");
            }

            await dbContext.Movies.AddAsync(objMovies);
            await dbContext.SaveChangesAsync();

            return (true, "Guardado con éxito.");

        }

        public static async Task<(bool ok, string msg)> SaveBeer(PlayStationData dbContext, int id, Beers objBeer)
        {
            bool exist = await dbContext.Beers.AnyAsync(b => b.id == id && b.type_beer == objBeer.type_beer);

            if (exist)
            {
                return (false, "Ya existe esta cerveza");
            }

            await dbContext.Beers.AddAsync(objBeer);
            await dbContext.SaveChangesAsync();

            return (true, "Guardada con éxito.");
        }


        public static async Task<T> ConsultaApiExterna<T>(string url, string jsonPath = null)
        {
            var json = await _http.GetStringAsync(url);

            // 2) Si no hay jsonPath, deserializa todo el JSON al tipo T
            if (string.IsNullOrWhiteSpace(jsonPath))
                return JsonConvert.DeserializeObject<T>(json);

            // 3) Si hay jsonPath, navega al nodo y deserializa solo ese fragmento
            var root = JObject.Parse(json);
            var token = root.SelectToken(jsonPath); // Soporta "items", "data.items", etc.

            if (token == null)
                throw new JsonException($"No se encontró el nodo '{jsonPath}' en la respuesta.");

            return token.ToObject<T>(); // Usa el T que pasaste en <T>
        }


    }
}
