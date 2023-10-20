
using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class AddRoleDto
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Rol { get; set; }
    public string Nombre { get; set; }
    public string Telefono { get; set; }
    public string Especialidad { get; set; }
}