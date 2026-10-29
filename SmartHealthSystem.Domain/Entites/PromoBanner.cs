using SmartHealthSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Domain.Entites
{
    public class PromoBanner:BaseEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }        // Reklam başlığı
        public string Description { get; set; }  // Kısa açıklama
        public string ImageUrl { get; set; }     // Resim dosyasının yolu
        public string TargetUrl { get; set; }    // Tıklandığında gideceği link
        public bool IsActive { get; set; }       // Yayında mı?
        public int Order { get; set; }           // Gösterim sırası
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
