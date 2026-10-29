using SmartHealthSystem.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Contracts
{
    public interface IPromoBannerRepository : IGenericRepository<PromoBanner>
    {
        // IGenericRepository dışındaki özel metodları buraya yazabilirsin.
        // Örneğin: Sadece aktif olanları getiren bir metod:
        Task<List<PromoBanner>> GetActiveBannersAsync();
        Task<PromoBanner> GetBannerByIdAsync(Guid id);
    }
}
