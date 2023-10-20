using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;
namespace Application.Repository;
public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
{
    private readonly APIContext _context;

    public UsuarioRepository(APIContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Usuario> GetByRefreshTokenAsync(string refreshToken)
    {
        return await _context.Usuarios
            .Include(u => u.Roles)
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == refreshToken));
    }

    public async Task<Usuario> GetByUsernameAsync(string username)
    {
        return await _context.Usuarios
            .Include(u => u.Roles)
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
    }

    public async Task<int> GetIDUserAsync(string username)
    {
        var user = await _context.Usuarios
                         .Include(u => u.Id)
                         .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
        return  user.Id;

    
    }
    public async Task<IEnumerable<Usuario>> GetAllRolesAsync(){
        var usuarios = await _context.Usuarios
            .Select(u => new Usuario
            {
                Id = u.Id,
                Username = u.Username,
                Roles = u.Roles.FirstOrDefault() != null ? new List<Rol> { u.Roles.First() } : new List<Rol>()
            })
            .ToListAsync();
        return usuarios;
    }

}
