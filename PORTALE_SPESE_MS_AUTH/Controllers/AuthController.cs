using LoginService.Models;
using Microsoft.AspNetCore.Mvc;
using PORTALE_SPESE_MS_AUTH.Classi;
using PORTALE_SPESE_MS_AUTH.Moduli;
using System.Net.Http;

namespace PORTALE_SPESE_MS_AUTH.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly HttpClient _httpClient;

        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // Costruisce la richiesta per il microservizio DB
            var response = await _httpClient.PostAsJsonAsync("https://localhost:6655/api/AUTHQ/CHECK_USER_TO_LOGIN", request);

            if (!response.IsSuccessStatusCode)
            {
                return Unauthorized(new { ERROR = "CREDENZIALI NON VALIDE" });
            }

            var userData = await response.Content.ReadFromJsonAsync<Utente>();

            // Simula la creazione di un token JWT
            var token = GENERA_TOKEN_JTW.GENERA(userData);

            return Ok(new { token, userData });
        }
    }
}
