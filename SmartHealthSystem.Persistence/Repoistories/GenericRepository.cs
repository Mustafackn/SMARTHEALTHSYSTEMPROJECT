using Microsoft.EntityFrameworkCore;
using SmartHealthSystem.Application.Contracts;
using SmartHealthSystem.Persistence.AppDbContext; // Kendi DbContext yolun
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartHealthSystem.Persistence.Repoistories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContexts _context;

        public GenericRepository(AppDbContexts context)
        {
            _context = context;
        }

        // ASIL EKLEME FONKSİYONU: Kayıt yapıp eklenen nesneyi geri döner
        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity; // ID oluştuktan sonra nesneyi geri dönüyoruz
        }

        // TÜMÜNÜ GETİR
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        // ID İLE GETİR
        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        // GÜNCELLE
        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // SİL
        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        // VAR MI KONTROLÜ
        public async Task<bool> Exists(int id)
        {
            var entity = await GetByIdAsync(id);
            return entity != null;
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate)
        {
            // AsNoTracking() ifadesini kaldırdık. 
            // Böylece EF bu nesneyi takip eder ve UpdateAsync dendiğinde değişiklikleri SQL'e yansıtır.
            return _context.Set<T>().Where(predicate);
        }
    }
}