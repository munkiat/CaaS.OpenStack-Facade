﻿using System;
using System.Configuration;

namespace Caas.OpenStack.API
{
	public static class ConfigurationHelpers
	{
		/// <summary>
		/// Gets the base URL for this endpoint.
		/// </summary>
		/// <returns>The Url</returns>
		public static string GetBaseUrl()
		{
			return ConfigurationManager.AppSettings["BaseUrl"];
		}

        public static string GetBaseUrl(string host)
        {
            return String.Format("http://{0}/", host); // TODO : Support for HTTPS
        }

		public static Uri GetApiUri()
		{
			return new Uri(ConfigurationManager.AppSettings["ApiEndpoint"]);
		}

        public static string GetTenantUrl(string host, string tenant)
        {
            return string.Format(
                "{0}{1}/{2}",
                ConfigurationHelpers.GetBaseUrl(host),
                Constants.CurrentApiVersion,
                tenant
                );
        }

		public static string GetTenantUrl(string tenant)
		{
			return string.Format(
				"{0}{1}/{2}",
				ConfigurationHelpers.GetBaseUrl(),
				Constants.CurrentApiVersion,
				tenant
				);
		}

        public static string GetBaseUrlVersion()
        {
            return string.Format(
                "{0}{1}",
                ConfigurationHelpers.GetBaseUrl(),
                Constants.CurrentApiVersion
                );
        }
	}
}