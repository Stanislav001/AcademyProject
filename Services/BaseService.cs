using AutoMapper;
using Date;

namespace Services
{
    public class BaseService
    {
        public BaseService(ApplicationDbContext dbContext , IMapper mapper)
        {
            this.DbContext = DbContext;
            this.Mapper = mapper;

        }
        public IMapper Mapper { get; set; }
        public ApplicationDbContext DbContext { get; set; }
    }
}
