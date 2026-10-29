using Microsoft.EntityFrameworkCore;
using SmartHealthSystem.Application.Contracts;
using SmartHealthSystem.Domain.Entites;
using SmartHealthSystem.Persistence.AppDbContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Persistence.Repoistories
{
    public class PromoBannerRepository : GenericRepository<PromoBanner>, IPromoBannerRepository
    {
        private readonly AppDbContexts _context;

        public PromoBannerRepository(AppDbContexts context) : base(context)
        {
            _context = context;
        }

        public async Task<List<PromoBanner>> GetActiveBannersAsync()
        {
            return await _context.PromoBanners
                .AsNoTracking() // Performans için: Takip etme (Read-only işlemler için ideal)
                .Where(x => x.IsActive)
                .OrderBy(x => x.Order)
                .ToListAsync();
        }

        public async Task<PromoBanner> GetBannerByIdAsync(Guid id)
        {
            return await _context.PromoBanners.FindAsync(id);
        }
    }
}
