﻿using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Caas.OpenStack.API.Models.identity;
using Caas.OpenStack.API.Models.serviceCatalog;
using DD.CBU.Compute.Api.Client.Interfaces;

namespace Caas.OpenStack.API.Controllers
{
	public class IdentityController : ApiController
	{
		private IComputeApiClient _computeClient;

		/// <summary>
		/// Initializes a new instance of the <see cref="ServerController"/> class.
		/// </summary>
		/// <param name="apiClient">The API client.</param>
		public IdentityController(Func<Uri, IComputeApiClient> apiClient)
		{
			_computeClient = apiClient(ConfigurationHelpers.GetApiUri());
		}

	    [Route("tokens")]
	    [Route(Constants.CurrentApiVersion + "/tokens")]
	    [HttpPost]
	    public async Task<TokenIssueResponse> IssueToken(TokenIssueRequest request)
	    {
			// Login to CaaS
		    _computeClient.LoginAsync(
				new NetworkCredential(
					request.Message.Credentials.UserName,
					request.Message.Credentials.Password));

		    return new TokenIssueResponse()
		    {
			    AccessToken = new AccessToken()
			    {
				    Token = new Token(request.Message.TenantName, request.Message.TenantName),
					Catalog = new ServiceCatalogEntry[]
					{
						new ServiceCatalogEntry()
						{
							Endpoints = new Endpoint[]
							{
								new Endpoint(){
									Url = ConfigurationHelpers.GetTenantUrl(request.Message.TenantName),
									Id = Guid.NewGuid().ToString(), // TODO: Map to cloud id?
									InternalURL = ConfigurationHelpers.GetTenantUrl(request.Message.TenantName),
									PublicURL = ConfigurationHelpers.GetTenantUrl(request.Message.TenantName),
									Region = "au1" // TODO: Map to region.
								}
							},
							EndpointsLinks = new string[]{},
							Name = "test",
							Type = EndpointType.compute
						}
					},
					User = new User()
					{
						Id = Guid.NewGuid().ToString(),
						Name = request.Message.Credentials.UserName,
						Roles = new User.Role[] { },
						RolesLinks = new string[]{},
						UserName = request.Message.Credentials.UserName
					}
			    }
		    };
	    }
	}
}
