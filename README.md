# IdentityServer4-adventures

This repo contains samples and prototypes for IdentityServer4 based on [docs](https://identityserver4.readthedocs.io/) 

## Projects

* IdentityServer: IdentityServer4 varsion `4.1.1` instance 

* Clients: 
  * ConsoleClient(.NET 5): Console app calling IdentityServer to get access_token using `Client Credentials` Grant, calls the Api using the access_token. 
  * MvcClient(.NET 5): MVC web app authenticate via OpenIdConnect, `Code` Grant, Cookie authentication, calls the Api using the access_token.  
  * JavaScriptClient(.NET 5): Spa app authenticate via OpenIdConnect, `Code` Grant, calls the Api using the access_token.  
  * RazorClient(.NET 5): Razor app authenticate via OpenIdConnect, `Implicit` Grant.
  * Net4MvcClient(.NET 4.5.2): MVC web app authenticate via OpenIdConnect, `Implicit` Grant, Cookie authentication, calls the Api using the access_token.  

* APIs
  * CoreApi(.NET 5): Api, uses Bearer authorization
  * Net4Api(.NET 4.5.2): Api, uses Bearer authorization
