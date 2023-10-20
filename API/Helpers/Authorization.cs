namespace API.Helpers;

public class Authorization
{
    public enum Roles
    {
        Administrator,
        Veterinario,
        WithoutRol
    }
    public const Roles rol_default = Roles.WithoutRol;
}