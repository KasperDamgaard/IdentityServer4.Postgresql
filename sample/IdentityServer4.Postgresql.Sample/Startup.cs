using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IdentityServer4.Contrib.Postgresql.Extensions;
using Marten;
using IdentityServer4.Models;
using IdentityServer4.Contrib.Postgresql.Mappers;
using IdentityServer4.Contrib.Postgresql.Entities;

namespace IdentityServer4.Contrib.Postgresql.Sample
{
	public class Startup
	{
		private const string connection = "host=localhost;database=sample;user id=postgres; Password=postgres";
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddIdentityServer().AddConfigurationStore(connection).AddOperationalStore().AddDeveloperSigningCredential();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			InitData(app);

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			app.UseIdentityServer();

			app.Run(async (context) =>
			{
				await context.Response.WriteAsync("Hello World!");
			});
		}

		private void InitData(IApplicationBuilder app)
		{
			var store = DocumentStore.For(connection);
			store.Advanced.Clean.CompletelyRemoveAll();
			using (var session = store.LightweightSession())
			{
				if (!session.Query<Entities.ApiResource>().Any())
				{
					var resources = new List<Entities.ApiResource> {
					 new Entities.ApiResource{ Name = "api1" , Description = "Api" , DisplayName ="api1" , Scopes = new List<ApiScopeResource> { new ApiScopeResource { Name = "api1" , DisplayName ="api1"  } } },

					};
					session.StoreObjects(resources);
				}

				if (!session.Query<Entities.IdentityResource>().Any())
				{
					var resources = new List<Entities.IdentityResource> {
						new IdentityResources.OpenId().ToEntity(),
						new IdentityResources.Profile().ToEntity(),
						new IdentityResources.Email().ToEntity(),
						new IdentityResources.Phone().ToEntity()
					};
					session.StoreObjects(resources);
				}
				if (!session.Query<Entities.Client>().Any())
				{
					var clients = new List<Entities.Client>
					{
						  new Entities.Client
							{
							  AllowOfflineAccess = true,
								Id = "ro.client",
								ClientId ="ro.client",
								ClientName = "mvc",
								AllowedGrantTypes =  new List<ClientGrantType>
								{
									new ClientGrantType { GrantType = GrantType.ClientCredentials },
									new ClientGrantType { GrantType = GrantType.Hybrid }
								},
								AllowedCorsOrigins =  new List<ClientCorsOrigin>
								{
									new ClientCorsOrigin { Origin = "http://localhost:5003" }
								},
								RequireClientSecret = true,
								ClientSecrets = new List<ClientSecret>
								{
									new ClientSecret { Value = "secret".Sha256() }
								},
								RequireConsent = false,
								AllowedScopes = new List<ClientScope>{
									 new ClientScope { Scope = IdentityServerConstants.StandardScopes.OpenId },
									 new ClientScope { Scope = IdentityServerConstants.StandardScopes.Profile },
									 new ClientScope { Scope ="api1" }
								},
								RedirectUris = new List<ClientRedirectUri>
								{
									new ClientRedirectUri { RedirectUri ="http://localhost:5003/signin-oidc" }
								}
							}

						};
					session.StoreObjects(clients);
				}
				session.SaveChanges();
			}
		}
	}
}
