using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.Tasks;
// Asegúrate de tener instalado el paquete NuGet 'supabase-csharp'
using Supabase; 

namespace TuProyecto.Pages
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
                // Inicio de sesión con Supabase Auth
                var session = await _supabaseClient.Auth.SignIn(Email, Password);

                if (session != null && session.User != null)
                {
                    // Lógica exitosa (ej. establecer cookies de autenticación de ASP.NET, redireccionar)
                    return RedirectToPage("/IncidenciasPages/principal");
                }
            }
            catch (Supabase.Gotrue.Exceptions.GotrueException)
            {
                // Feedback: Indicar el error. 
                // Por seguridad, es una buena práctica no especificar si falló el correo o la contraseña, 
                // para evitar enumeración de usuarios.
                ModelState.AddModelError(string.Empty, "Las credenciales proporcionadas son incorrectas.");
            }
            catch (System.Exception)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar iniciar sesión. Por favor, intente más tarde.");
            }

            // Si llegamos aquí, algo falló; volvemos a mostrar la página con los errores
            return Page();
        }
    }
}