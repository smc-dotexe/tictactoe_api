# TicTacToe.Api

## Final Question:

>Question: 
>>What is the appropriate OAuth 2/OIDC grant to use for a web application using a SPA (Single Page Application) and why?

The appropriate grant to use for SPAs is the Authorization Code Grant (OAuth2) / Authorization Code Flow (OIDC).

The pattern involves exchanging an authorization code for the access token by the Auth Server verifying the Auth Code, Client ID and Client Secret. The Auth Server responds with an Access Token that the application can now use to call the API to access information about the user, granting them special permissions.

The access tokens can be saved on the client side as a an http-only cookie. This way it can prevent Cross-Site-Scripting (XSS) attacks.




