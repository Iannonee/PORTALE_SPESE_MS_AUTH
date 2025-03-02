using Microsoft.IdentityModel.Tokens;
using PORTALE_SPESE_MS_AUTH.Classi;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PORTALE_SPESE_MS_AUTH.Moduli
{
    public class GENERA_TOKEN_JTW
    {
        public static string GENERA(Utente utente)
        {
            string ruolo = OTTIENI_RUOLO(utente);

            // Se le credenziali sono corrette, genera un JWT
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, utente.T001_NomeUtente),
                new Claim(ClaimTypes.Role, ruolo),
                new Claim(ClaimTypes.UserData, utente.T001_IdUtente.ToString())
            };

            string CHIAVE_SEGRETA = $"TESTING_LE_API_CHIAVE_AUTORIZZAZIONE_PRIVATA";

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(CHIAVE_SEGRETA));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "http://localhost:5001", // L'issuer deve essere lo stesso configurato nel server
                audience: "http://localhost:7168", // L'audience deve essere lo stesso configurato nel server
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.WriteToken(token);

            return jwtToken;
        }

        private static string OTTIENI_RUOLO(Utente utente)
        {
            string result = "";

            try
            {
                switch (utente.T001_Livello)
                {
                    case 0:
                        result = "ADMIN";
                        break;
                    case 5:
                        result = "UTENTE_AVANZATO";
                        break;
                    case 10:
                        result = "UTENTE_BASE";
                        break;
                    case 15:
                        result = "UTENTE_DEMO";
                        break;
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }
    }
}
