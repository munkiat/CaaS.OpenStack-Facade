﻿using System.Runtime.Serialization;

namespace Caas.OpenStack.API.Models.api
{
	/* As per documentation.
	 * {
    "versions": [
        {
            
        },
	 */
	[DataContract]
	public class ApiVersionList
	{
		private string _baseUrl;

		/// <summary>
		/// Initializes a new instance of the <see cref="ApiVersionList"/> class.
		/// </summary>
		/// <param name="baseUrl">The base URL.</param>
		public ApiVersionList(string baseUrl)
		{
			_baseUrl = baseUrl;

			Versions = new ApiVersion[1];

			Versions[0] = new ApiVersion(baseUrl, "CURRENT", "v2.0");
		}

		/// <summary>
		/// Gets or sets the versions available.
		/// </summary>
		/// <value>
		/// The versions of API available.
		/// </value>
		[DataMember(Name = "versions")]
		public ApiVersion[] Versions { get; set; }
	}
}