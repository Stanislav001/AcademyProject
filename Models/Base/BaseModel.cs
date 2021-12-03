using Models.Interfaces;
using System;

namespace Models.Base
{
    public class BaseModel : IAuditInfo
    {
        public BaseModel()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
    }
}
