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

        public static Models.ApiScope ToModel(this Entities.ApiScopeResource resource)
        {
            return resource == null ? null : Mapper.Map<Models.ApiScope>(resource);
        }

        public static Entities.ApiScopeResource ToEntity(this Models.ApiScope resource)
        {
            return resource == null ? null : Mapper.Map<Entities.ApiScopeResource>(resource);
        }
    }
}
