using AutoMapper;

using Date;

namespace Services
{
    public class BaseService
    {
        public IMapper Mapper { get; set; }
        public ApplicationDbContext DbContext { get; set; }

        public BaseService(ApplicationDbContext dbContext , IMapper mapper)
        {
            this.DbContext = dbContext;
            this.Mapper = mapper;

        }
    }
}