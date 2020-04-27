# IdentityServer4-adventures

This repo contains samples and prototypes for IdentityServer4 based on [docs](https://identityserver4.readthedocs.io/) 

## Projects
All projects are built against the latest ASP.NET Core 3.

* IdentityServer: IdentityServer4 instance 
* ConsoleClient: Console app calling IdentityServer to get access_token using Client Credentials Grant, calls the Api using the token 
* MvcClient: MVC web app authenticate via OpenIdConnect and Code Grant, using cookie, calls the Api using the token  
* JavaScriptClient: Spa app authenticate via OpenIdConnect and Code Grant, calls the Api using the token
* CoreApi: .Net Core Web Api, uses Bearer authorization
* Net4Api: .Net 4.5 Web Api, uses Bearer authorization
