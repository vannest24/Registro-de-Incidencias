using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.Tasks;
using Supabase; 
using System.Collections.Generic; // Para usar List<>
using Microsoft.AspNetCore.Authentication;

namespace Auth
{
    // Aplica la política de Rate Limiting al endpoint POST de inicio de sesión
    [EnableRateLimiting("LoginLimiter")]
    public class IndexModel : PageModel
    {
        private readonly Client _supabaseClient;

        [BindProperty]
        public string? Email { get; set; }

        [BindProperty]
        public string? Password { get; set; }

        [BindProperty]
        public bool RememberMe {get; set; }

        // Inyección de dependencias del cliente de Supabase
        public IndexModel(Client supabaseClient)
        {
            _supabaseClient = supabaseClient;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                Console.WriteLine($"[DEBUG] Intentando iniciar sesión para: {Email}"); //Log de prueba
                // Inicio de sesión con Supabase Auth
                var session = await _supabaseClient.Auth.SignIn(Email, Password);

            if (session != null && session.User != null)
            {
                // 1. Crear los datos del usuario (Claims) a partir de Supabase
                var claims = new List<System.Security.Claims.Claim>
                {
                    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, session.User.Id),
                    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, session.User.Email)
                };

                var identity = new System.Security.Claims.ClaimsIdentity(claims, Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new System.Security.Claims.ClaimsPrincipal(identity);

                // 2. Configurar si la sesión se mantiene iniciada
                var authProperties = new Microsoft.AspNetCore.Authentication.AuthenticationProperties
                {
                    // Si RememberMe es true, la sesión dura semanas. Si es false, se borra al cerrar el navegador.
                    IsPersistent = RememberMe, 
                    
                    // Opcional: Define cuánto tiempo durará la sesión "mantenida" (ej. 30 días)
                    ExpiresUtc = RememberMe ? DateTimeOffset.UtcNow.AddDays(30) : null 
                };

                // 3. Iniciar sesión en ASP.NET Core creando la Cookie
                await HttpContext.SignInAsync(
                    Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme, 
                    principal, 
                    authProperties);

                Console.WriteLine("[DEBUG] Login exitoso. Redirigiendo a Pantalla1...");
                return RedirectToPage("/IncidenciasPages/principal");
            }else
                {
                    Console.WriteLine("[DEBUG] Fallo silencioso: La sesión o el usuario devolvieron null.");
                    ModelState.AddModelError(string.Empty, "Error de autenticación. Verifique su cuenta.");
                }
            }
            catch (Supabase.Gotrue.Exceptions.GotrueException ex)
            {
                // Feedback: Indicar el error. 
                // Por seguridad, es una buena práctica no especificar si falló el correo o la contraseña, 
                // para evitar enumeración de usuarios.
                Console.WriteLine($"[DEBUG ERROR SUPABASE] {ex.Message}");
                ModelState.AddModelError(string.Empty, "Las credenciales proporcionadas son incorrectas.");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"[DEBUG ERROR GENERAL] {ex.Message}");
                ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar iniciar sesión. Por favor, intente más tarde.");
            }

            // Si llegamos aquí, algo falló; volvemos a mostrar la página con los errores
            return Page();
        }
    }
}