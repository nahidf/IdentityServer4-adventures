# IdentityServer4-adventures

This repo contains samples and prototypes for IdentityServer4 based on [docs](https://identityserver4.readthedocs.io/) 

## Projects

All projects except Net4Api are built against the latest ASP.NET Core 3, Net4Api using .NET 4.5.2.

* IdentityServer: IdentityServer4 varsion `3.1.0` instance 

* Clients: 
  * ConsoleClient: Console app calling IdentityServer to get access_token using `Client Credentials` Grant, calls the Api using the access_token. 
  * MvcClient: MVC web app authenticate via OpenIdConnect, `Code` Grant, Cookie authentication, calls the Api using the access_token.  
  * Net4MvcClient: .Net 4.5 MVC web app authenticate via OpenIdConnect, `Implicit` Grant, Cookie authentication, calls the Api using the access_token.  
  * JavaScriptClient: Spa app authenticate via OpenIdConnect, `Code` Grant, calls the Api using the access_token.  
  * RazorClient: Razor app authenticate via OpenIdConnect, `Implicit` Grant.

* APIs
  * CoreApi: .Net Core Web Api, uses Bearer authorization
  * Net4Api: .Net 4.5 Web Api, uses Bearer authorization
