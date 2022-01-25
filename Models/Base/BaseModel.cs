using System;

namespace Models.Base
{
    public class BaseModel 
    {
        public BaseModel()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
    }
}