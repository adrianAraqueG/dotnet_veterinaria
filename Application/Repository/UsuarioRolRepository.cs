using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
    public class UsuarioRolRepository: GenericRepository<UsuarioRol>, IUsuarioRolRepository
{
    private readonly APIContext _context;

    public UsuarioRolRepository(APIContext context) : base(context)
    {
        _context = context;
    }
}