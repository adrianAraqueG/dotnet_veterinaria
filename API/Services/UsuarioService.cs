using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using API.Dtos;
using API.Helpers;
using API.Services;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class UsuarioService : IUsuarioService
{
    private readonly JWT _jwt;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher<Usuario> _passwordHasher;
    public UsuarioService(IUnitOfWork unitOfWork, IOptions<JWT> jwt, IPasswordHasher<Usuario> passwordHasher)
    {
        _jwt = jwt.Value;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }
    public async Task<string> RegisterAsync(RegisterDto registerDto)
    {
        var Usuario = new Usuario
        {
            Email = registerDto.Email,
            Username = registerDto.Username,
            DNI = registerDto.DNI,
        };

        Usuario.Password = _passwordHasher.HashPassword(Usuario, registerDto.Password); //Encrypt password

        Console.WriteLine("Validar existencia Usuario");
        var existingUsuario = _unitOfWork.Usuarios
                                    .Find(u => u.Username.ToLower() == registerDto.Username.ToLower() || u.DNI == registerDto.DNI)
                                    .FirstOrDefault();
        
        if (existingUsuario == null)
        {
            Console.WriteLine("Búsqueda en roles");
            var rolDefault = _unitOfWork.Roles
                                    .Find(u => u.Nombre == Authorization.rol_default.ToString())
                                    .First();
            try
            {
                Console.WriteLine("Búsqueda en roles 2");
                Usuario.Roles.Add(rolDefault);
                _unitOfWork.Usuarios.Add(Usuario);
                await _unitOfWork.SaveAsync();

                return $"Usuario  {registerDto.Username} has been registered successfully";
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return $"Error: {message}";
            }
        }
        else
        {
            return $"Usuario {registerDto.Username} already registered, change the identification or Username.";
        }
    }
    public async Task<DataUserDto> GetTokenAsync(LoginDto model)
    {
        DataUserDto DataUserDto = new DataUserDto();
        var Usuario = await _unitOfWork.Usuarios
                    .GetByUsernameAsync(model.Username);

        if (Usuario == null)
        {
            DataUserDto.IsAuthenticated = false;
            DataUserDto.Message = $"Usuario does not exist with Username {model.Username}.";
            return DataUserDto;
        }

        var result = _passwordHasher.VerifyHashedPassword(Usuario, Usuario.Password, model.Password);

        if (result == PasswordVerificationResult.Success)
        {
            DataUserDto.IsAuthenticated = true;
            JwtSecurityToken jwtSecurityToken = CreateJwtToken(Usuario);
            DataUserDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            DataUserDto.Email = Usuario.Email;
            DataUserDto.Username = Usuario.Username;
            DataUserDto.Roles = Usuario.Roles
                                            .Select(u => u.Nombre)
                                            .ToList();

            if (Usuario.RefreshTokens.Any(a => a.IsActive))
            {
                var activeRefreshToken = Usuario.RefreshTokens.Where(a => a.IsActive == true).FirstOrDefault();
                DataUserDto.RefreshToken = activeRefreshToken.Token;
                DataUserDto.RefreshTokenExpiration = activeRefreshToken.FechaExpiracion;
            }
            else
            {
                var refreshToken = CreateRefreshToken();
                DataUserDto.RefreshToken = refreshToken.Token;
                DataUserDto.RefreshTokenExpiration = refreshToken.FechaExpiracion;
                Usuario.RefreshTokens.Add(refreshToken);
                _unitOfWork.Usuarios.Update(Usuario);
                await _unitOfWork.SaveAsync();
            }

            return DataUserDto;
        }
        DataUserDto.IsAuthenticated = false;
        DataUserDto.Message = $"Credenciales incorrectas para el usuario {Usuario.Username}.";
        return DataUserDto;
    }
    public async Task<string> AddRoleAsync(AddRoleDto model)
    {
        var Usuario = await _unitOfWork.Usuarios
                    .GetByUsernameAsync(model.Username);
        if (Usuario == null)
        {
            return $"Usuario {model.Username} does not exists.";
        }
            var rolExists = _unitOfWork.Roles
                                        .Find(u => u.Nombre.ToLower() == model.Rol.ToLower())
                                        .FirstOrDefault();
            if (rolExists != null)
            {
                var UsuarioHasRole = Usuario.Roles
                                        .Any(u => u.Id == rolExists.Id);

                if (UsuarioHasRole == false)
                {
                if (rolExists.Nombre == Authorization.Roles.Veterinario.ToString())
                    {

                        if (!string.IsNullOrEmpty(model.Especialidad) && !string.IsNullOrEmpty(model.Telefono) && !string.IsNullOrEmpty(model.Nombre))
                        {
                            var Veterinario = new Veterinario
                            {
                                Nombre = model.Nombre,
                                Telefono = model.Telefono,
                                Especialidad = model.Especialidad,
                                IdUsuario = Usuario.Id
                            };
                            _unitOfWork.Veterinarios.Add(Veterinario);
                            await _unitOfWork.SaveAsync();
                        }
                        else
                        {
                            return $"Register Veterinarios needs all data correct (Name, Position)";
                        }

                    }
                    var withoutRole = Usuario.Roles.FirstOrDefault(u => u.Nombre == Authorization.Roles.WithoutRol.ToString());
                    if (withoutRole != null && model.Rol.ToLower() != Authorization.Roles.WithoutRol.ToString().ToLower())
                    {
                        Usuario.Roles.Remove(withoutRole);
                    }
                    Usuario.Roles.Add(rolExists);
                    _unitOfWork.Usuarios.Update(Usuario);
                    await _unitOfWork.SaveAsync();
                    return $"Role {model.Rol} added to Usuario {model.Username} successfully.";
                }
                else
                {
                    return $"Role {model.Rol} ya está asignado al usuario.";
                }
            }
            else
            {
                return $"Role {model.Rol} was not found.";
            }
    }
    public async Task<DataUserDto> RefreshTokenAsync(string refreshToken)
    {
        var DataUserDto = new DataUserDto();

        var usuario = await _unitOfWork.Usuarios
                        .GetByRefreshTokenAsync(refreshToken);

        if (usuario == null)
        {
            DataUserDto.IsAuthenticated = false;
            DataUserDto.Message = $"Token is not assigned to any Usuario.";
            return DataUserDto;
        }

        var refreshTokenBd = usuario.RefreshTokens.Single(x => x.Token == refreshToken);

        if (!refreshTokenBd.IsActive)
        {
            DataUserDto.IsAuthenticated = false;
            DataUserDto.Message = $"Token is not active.";
            return DataUserDto;
        }
        //Revoque the current refresh token and
        refreshTokenBd.Revoked = DateTime.UtcNow;
        //generate a new refresh token and save it in the database
        var newRefreshToken = CreateRefreshToken();
        usuario.RefreshTokens.Add(newRefreshToken);
        _unitOfWork.Usuarios.Update(usuario);
        await _unitOfWork.SaveAsync();
        //Generate a new Json Web Token
        DataUserDto.IsAuthenticated = true;
        JwtSecurityToken jwtSecurityToken = CreateJwtToken(usuario);
        DataUserDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        DataUserDto.Email = usuario.Email;
        DataUserDto.Username = usuario.Username;
        DataUserDto.Roles = usuario.Roles
                                        .Select(u => u.Nombre)
                                        .ToList();
        DataUserDto.RefreshToken = newRefreshToken.Token;
        DataUserDto.RefreshTokenExpiration = newRefreshToken.FechaExpiracion;
        return DataUserDto;
    }
    private RefreshToken CreateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var generator = RandomNumberGenerator.Create())
        {
            generator.GetBytes(randomNumber);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                FechaExpiracion = DateTime.UtcNow.AddMinutes(3),
                Created = DateTime.UtcNow
            };
        }
    }
    private JwtSecurityToken CreateJwtToken(Usuario usuario)
    {
        var roles = usuario.Roles;
        var roleClaims = new List<Claim>();
        foreach (var role in roles)
        {
            roleClaims.Add(new Claim("roles", role.Nombre));
        }
        var claims = new[]
        {
                                new Claim(JwtRegisteredClaimNames.Sub, usuario.Username),
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                                new Claim("uid", usuario.Id.ToString())
                        }
        .Union(roleClaims);
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
            signingCredentials: signingCredentials);
        return jwtSecurityToken;
    }
 
}