
namespace Domain.Entities;

public class RefreshToken : BaseEntity
{
    public int IdUsuario { get; set; }
    public Usuario Usuario { get; set; }
    public string Token { get; set; }
    public DateTime FechaExpiracion { get; set; }
    public bool EsExpirado => DateTime.UtcNow >= FechaExpiracion;
    public DateTime Created { get; set; }
    public DateTime? Revoked { get; set; }
    public bool IsActive => Revoked == null && !EsExpirado;
}
