using AutoMapper;

namespace IdentityServer4.Contrib.Postgresql.Mappers
{
    public static class ApiScopeResourceMappers
    {
        static ApiScopeResourceMappers()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<ApiResourceMapperProfile>())
                .CreateMapper();
        }
        public static IMapper Mapper { get; }

        public static Models.Scope ToModel(this Entities.ApiScopeResource resource)
        {
            return resource == null ? null : Mapper.Map<Models.Scope>(resource);
        }

        public static Entities.ApiScopeResource ToEntity(this Models.Scope resource)
        {
            return resource == null ? null : Mapper.Map<Entities.ApiScopeResource>(resource);
        }
    }
}
