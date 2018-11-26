﻿using IdentityServer4.Models;
using IdentityServer4.Contrib.Postgresql.Mappers;
using Xunit;

namespace IdentityServer4.Contrib.Postgresql.UnitTests
{
    public class PersistedGrantMapperTests
    {
        [Fact]
        public void PersistedGrantAutomapperConfigurationIsValid()
        {
            var model = new PersistedGrant();
            var mappedEntity = model.ToEntity();
            var mappedModel = mappedEntity.ToModel();

            Assert.NotNull(mappedModel);
            Assert.NotNull(mappedEntity);
            PersistedGrantMappers.Mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
