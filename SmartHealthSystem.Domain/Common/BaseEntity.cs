using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Domain.Common
{
    public abstract class BaseEntity
    {
        // Tüm tabloların ortak kimliği
        public int Id { get; set; }

        // Kaydın oluşturulma tarihi
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // İleride güncelleme yapmak istersen
        public DateTime? UpdatedDate { get; set; }

        // Veriyi silmek yerine "pasif" yapmak için (Soft Delete)
        public bool IsActive { get; set; } = true;
    }
}
