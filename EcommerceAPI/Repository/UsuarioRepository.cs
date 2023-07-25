using AutoMapper;
using EcommerceAPI.Repository.Interfaces;
using EcommerceAPI.Models;
using LoginAPI.Dto;
using Microsoft.AspNetCore.Identity;
using Stripe;
using LoginAPI.Models;
using System;
using EcommerceAPI.Token;

namespace EcommerceAPI.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public IMapper _mapper;
        private UserManager<Usuario> _userManager;
        private SignInManager<Usuario> _signInManager;
        private Token.TokenService _tokenService;
        public UsuarioRepository(Token.TokenService tokenService,
                                 SignInManager<Usuario> signInManager,
                                 IMapper mapper,
                                 UserManager<Usuario> userManager)
        {
            //_mapper = mapper;
            //_userManager = userManager;
            //_signInManager = signInManager;
            //_tokenService = tokenService;
        }
        public async Task Cadastro(CreateUsuarioDto dto)
        {
            var usuario = _mapper.Map<Usuario>(dto);

            var result = await _userManager.CreateAsync(usuario);

            if (!result.Succeeded)
                throw new ApplicationException("Falha ao cadastrar usuario");

        }

        public async Task<string> Login(LoginUsuarioDto dto)
        {
            var result = await _signInManager.PasswordSignInAsync(dto.Username, dto.Password, false, false);

            if(!result.Succeeded)
                throw new ApplicationException("Falha ao logar usuario");

            var usuario = _signInManager
              .UserManager
              .Users
              .FirstOrDefault(user => user.NormalizedUserName == dto.Username.ToUpper());
            
            var token = _tokenService.GenerateToken(usuario);

            return token;
        }
    }
}
