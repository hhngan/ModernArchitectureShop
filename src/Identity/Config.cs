// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityModel;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new List<IdentityResource>
            {
                    new IdentityResources.OpenId(),
                    new IdentityResources.Profile(),
                    new IdentityResources.Email(),
            };


        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
            {
                    // By assigning the user claim to the api resource, we are instructing Identity Server to include that claim in Access tokens for this resource.
                    new ApiResource("storeapi", "Store API", new []{ JwtClaimTypes.Name, JwtClaimTypes.Email }),
                    new ApiResource("basketapi", "Basket API",new []{ JwtClaimTypes.Name, JwtClaimTypes.Email })
            };

        public static IEnumerable<Client> Clients =>
            new[]
            {
                    new Client
                    {
                        ClientId = "BlazorUI",
                        ClientName = "BlazorUI",
                        ClientSecrets = {new Secret("secret".Sha256())},
                        AllowedGrantTypes = GrantTypes.Code,
                        RequirePkce = true,
                        RequireConsent = false,

                        // where to redirect to after login
                        RedirectUris = {"http://localhost:5010/signin-oidc"},

                        // where to redirect to after logout
                        PostLogoutRedirectUris = {"http://localhost:5010/signout-callback-oidc"},

                        // allowed scopes - include Api Resources and Identity Resources that may be accessed by this client
                        AllowedScopes =
                        {
                            "openid",
                            "profile",
                            "email",
                            "offline_access",
                            "storeapi",
                            "basketapi"
                        },

                        // include the refresh token
                        AllowOfflineAccess = true,
                    }
            };

    }
}
