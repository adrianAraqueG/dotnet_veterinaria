using Domain.Entities;

namespace Domain.Interfaces;

public interface IUsuarioRepository : IGenericRepository<Usuario> 
{ 
    Task<int> GetIDUserAsync(string username);
    Task<Usuario> GetByUsernameAsync(string username);
    Task<Usuario> GetByRefreshTokenAsync(string username);
    Task<IEnumerable<Usuario>> GetAllRolesAsync();
}
