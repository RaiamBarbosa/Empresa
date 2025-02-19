using Empresa.Models;
using Empresa.Services;
using Hangfire.States;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace Empresa.Controllers
{
    [ApiController]
    [Route("api/atenticacao")]
    public class AuthController : ControllerBase
    {
        private readonly TokenGenerator _tokenGenerator;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManeger;

        public AuthController(TokenGenerator tokenGenerator, RoleManager<IdentityRole> roleManeger,
            UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _tokenGenerator = tokenGenerator;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManeger = roleManeger;
        }


        /// <summary>
        /// Registrar Usuário
        /// </summary>

        [HttpPost("registrar-usuario")]
        public async Task<IActionResult> RegistrarUsuario([FromForm] RegisterUsuario register_usuario)
        {
            if (!ModelState.IsValid) return NotFound();

            var usuario = new IdentityUser()
            {
                UserName = register_usuario.Usuario,
                Email = register_usuario.Email,
                PasswordHash = register_usuario.Password,
                EmailConfirmed = true
            };
            
            var result = await _userManager.CreateAsync(usuario, register_usuario.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(usuario, false);
       
                return Ok("Registrado Com Sucesso!!!");
            }
            return BadRequest(result);
        }


        /// <summary>
        /// Logar  Usuário
        /// </summary>

        [HttpPost("logar-usuario")]
        public async Task<IActionResult> Login(LoginUsuario login_Usuario)
        {
            if (!ModelState.IsValid) return NotFound();

            var result = await _signInManager.PasswordSignInAsync(login_Usuario.Usuario, login_Usuario.Password, false, true);

            if (result.Succeeded)
            {
                var token = _tokenGenerator.GerarToken(login_Usuario);
                var tokenRefresh = _tokenGenerator.GenerateRefreshToken();

                return Ok(
                    new
                    {
                        Usuario = login_Usuario + "logado com Sucesso",
                        Token = token,
                        TokenRefresh = tokenRefresh,
                    }
                );
            }
            if (result.IsLockedOut) return NotFound("Número de tentativas esgotada");

            return NotFound("Usuario ou senha invalidos");
        }

        [HttpGet("role")]
        public async Task<IActionResult> RoleSql ()
        {
            var role = "Admin";
            await _roleManeger.CreateAsync(new IdentityRole(role));
            return Ok();
        }
    }
}
