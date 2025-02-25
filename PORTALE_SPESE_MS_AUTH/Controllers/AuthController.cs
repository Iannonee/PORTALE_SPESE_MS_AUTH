using LoginService.Models;
using Microsoft.AspNetCore.Mvc;
using PORTALE_SPESE_MS_AUTH.Classi;
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
                return Unauthorized(new { error = "Credenziali non valide" });
            }

            var userData = await response.Content.ReadFromJsonAsync<Utente>();

            // Simula la creazione di un token JWT
            var token = $"fake-jwt-token-for-{userData.T001_IdUtente}";

            return Ok(new { token, userData });
        }
    }
}
