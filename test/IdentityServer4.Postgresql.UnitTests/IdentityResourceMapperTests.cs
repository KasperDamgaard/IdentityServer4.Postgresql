using IdentityServer4.Models;
using IdentityServer4.Contrib.Postgresql.Mappers;
using Xunit;
using System.Collections.Generic;

namespace IdentityServer4.Postgresql.UnitTests
{
    public class IdentityResourceMapperTests
    {
        [Fact]
        public void IdentityResourceAutomapperConfigurationIsValid()
        {
            var model = new IdentityResource();
            model.Name = "fixed";
            model.UserClaims = new List<string> { "name" };
            var mappedEntity = model.ToEntity();
            var mappedModel = mappedEntity.ToModel();

            Assert.NotNull(mappedModel);
            Assert.NotNull(mappedEntity);
            IdentityResourceMappers.Mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
