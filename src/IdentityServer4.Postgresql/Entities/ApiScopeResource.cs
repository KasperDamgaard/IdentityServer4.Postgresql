using System;
using System.Collections.Generic;

namespace IdentityServer4.Contrib.Postgresql.Entities
{
    public class ApiScopeResource : EntityKey
    {
        public ApiScopeResource()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
        public bool Emphasize { get; set; }
        public bool ShowInDiscoveryDocument { get; set; } = true;
        public List<ApiScopeClaim> UserClaims { get; set; }

    }
}
