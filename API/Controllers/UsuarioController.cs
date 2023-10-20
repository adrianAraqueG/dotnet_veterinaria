using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Services;
using API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

public class UsuarioController : ApiBaseController
{
    private readonly IUsuarioService _usuarioService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UsuarioController(IUsuarioService usuarioService, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _usuarioService = usuarioService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    [Authorize(Roles = "Administrador")]
    [HttpPost("register")]
    public async Task<ActionResult> RegisterAsync(RegisterDto model)
    {
        var result = await _usuarioService.RegisterAsync(model);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> GetTokenAsync(LoginDto model)
    {
        var result = await _usuarioService.GetTokenAsync(model);
        SetRefreshTokenInCookie(result.RefreshToken);
        return Ok(result);
    }

    [HttpPost("addrole")]
    public async Task<IActionResult> AddRoleAsync(AddRoleDto model)
    {
        var result = await _usuarioService.AddRoleAsync(model);
        return Ok(result);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken(RefreshTokenCookieDto refreshTokenDto)
    {
        string refreshToken = refreshTokenDto.RefreshToken;
        var response = await _usuarioService.RefreshTokenAsync(refreshToken.ToString());
        if (!string.IsNullOrEmpty(response.RefreshToken))
            SetRefreshTokenInCookie(response.RefreshToken);
        return Ok(response);
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<UserWithRolDto>>> GetUserWithRol()
    {
        var user = await _unitOfWork.Usuarios.GetAllRolesAsync();
        return _mapper.Map<List<UserWithRolDto>>(user);
    }
    [HttpGet("roles")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Rol>>> GetRoles()
    {
        var roles = await _unitOfWork.Roles.GetAllAsync();
        return _mapper.Map<List<Rol>>(roles);
    }

    private void SetRefreshTokenInCookie(string refreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddMinutes(3),
        };
        Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
    }
    
}