using EdecanesV2.Data;
using EdecanesV2.Models;

namespace EdecanesV2.Repositories.Impl
{
    public class DisponibilidadesRepository
    {
        private readonly ApplicationDbContext _context;

        public DisponibilidadesRepository(ApplicationDbContext context)
        {
            _context = context;
        }


    }
}
