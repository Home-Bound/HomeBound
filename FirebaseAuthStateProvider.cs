using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace InmoCierres // <-- Asegúrate de que este sea tu namespace
{
    public class FirebaseAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _js;

        public FirebaseAuthStateProvider(IJSRuntime js)
        {
            _js = js;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                // Aquí Blazor le pregunta a Firebase si hay una sesión guardada
                var user = await _js.InvokeAsync<UsuarioInfo>("obtenerUsuarioActual");

                if (user != null && !string.IsNullOrEmpty(user.uid))
                {
                    // ¡Encontramos al usuario! Le avisamos a Blazor quién es.
                    var identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.uid),
                        new Claim(ClaimTypes.Name, user.nombre ?? ""),
                        new Claim(ClaimTypes.Email, user.email ?? "")
                    }, "Firebase");

                    var claimsPrincipal = new ClaimsPrincipal(identity);
                    return new AuthenticationState(claimsPrincipal);
                }
            }
            catch
            {
                // Si algo falla, lo dejamos pasar como no logueado
            }

            // Nadie está logueado, devolvemos un usuario en blanco
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public void NotificarLogin(UsuarioInfo user)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.uid),
                new Claim(ClaimTypes.Name, user.nombre ?? ""),
                new Claim(ClaimTypes.Email, user.email ?? "")
            }, "Firebase");

            var claimsPrincipal = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public void NotificarLogout()
        {
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
    }

    public class UsuarioInfo
    {
        public string uid { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string nombre { get; set; } = string.Empty;
        public string fotoUrl { get; set; } = string.Empty;
    }
}