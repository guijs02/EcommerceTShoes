using AutoMapper;
using EcommerceAPI.Repository.Interfaces;
using EcommerceAPI.Models;
using LoginAPI.Dto;
using Microsoft.AspNetCore.Identity;
using Stripe;
using LoginAPI.Models;
using System;
using EcommerceAPI.Token;
using EcommerceTShoes.Auth;
using System.Security.Claims;

namespace EcommerceAPI.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public IMapper _mapper;
        private UserManager<Usuario> _userManager;
        private SignInManager<Usuario> _signInManager;
        private Token.TokenService _tokenService;
        private IConfiguration _configuration;
        public UsuarioRepository(Token.TokenService tokenService,
                                 SignInManager<Usuario> signInManager,
                                 IMapper mapper,
                                 UserManager<Usuario> userManager,
                                 IConfiguration configuration)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        public async Task<bool> Cadastro(CreateUsuarioDto dto)
        {
            try
            {

                var usuario = _mapper.Map<Usuario>(dto);

                var result = await _userManager.CreateAsync(usuario, dto.Password);

                if (!result.Succeeded)
                    throw new ApplicationException("Falha ao cadastrar usuario");

                return result.Succeeded;
            }
            catch (Exception ex) 
            {
                throw;
            }
        }

        public async Task<string> Login(LoginUsuarioDto dto)
        {
            try
            {

                var result = await _signInManager.PasswordSignInAsync(dto.Username, dto.Password, false, false);

                if (!result.Succeeded)
                    throw new ApplicationException("Falha ao logar usuario");

                var usuario = _signInManager
                             .UserManager
                             .Users
                             .FirstOrDefault(user => user.NormalizedUserName == dto.Username.ToUpper());


                var token = _tokenService.GenerateToken(usuario, _configuration);

                return token;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
