using IdentityServer4.Contrib.Postgresql.Mappers;
using Xunit;
using Client = IdentityServer4.Models.Client;

namespace IdentityServer4.Contrib.Postgresql.UnitTests
{
    public class ClientMapperTests
    {
        [Fact]
        public void ClientAutomapperConfigurationIsValid()
        {
            var model = new Client();
            var mappedEntity = model.ToEntity();
            var mappedModel = mappedEntity.ToModel();

            Assert.NotNull(mappedModel);
            Assert.NotNull(mappedEntity);
            ClientMapper.Mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
