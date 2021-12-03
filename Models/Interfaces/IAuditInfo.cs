using System;

namespace Models.Interfaces
{
    public interface IAuditInfo
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
