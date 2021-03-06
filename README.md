# TicTacToe.Api

## To run on your machine
After cloning the repository, add the ".env" file (provided to the Launchpad Contact) to the the root of the repo.

To migrate the latest snapshot to the database:
In the Package Manager Console, select TicTacToe.App as the default project then run this command:

> EntityFrameworkCore\Update-Database -Context ApplicationDbContext -Project TicTacToe.App -StartupProject TicTacToe.Api

This was built using Visual Studio 2019 and Docker Desktop on Windows 10. 

Please contact me if you have any questions for running the application and I'll try my best to assist.

Thank you,

Shane

## Endpoint_1
http://localhost:33000/api/game/newgame

**POST**
```json
{
	"playeronename": "string",
	"playertwoname": "string"
}
```

**Returns**
```json
{
    "gameId": "Guid"
    "playerOneId": "Guid",
    "playerTwoId": "Guid"
}
```

## Endpoint_2
http://localhost:33000/api/player/placemove

**PUT**
```json
{
     "playerId": "Guid",
	"target":{ "row": 0, "col": 0 } 
}
```

**Returns**
```json
{
	"gameBoard": [[<<state of gameboard>>]],
	"playerId": "Guid",
	"message": "string"
}
```

## Endpoint_3
http://localhost:33000/api/game/activegames

**GET**

**Returns**
```json
[
	{
		"gameId": "Guid",
		"movesTaken": 0,
		"playerOneName": "string",
		"playerTwoName": "string"
	},
	{
		"gameId": "Guid",
		"movesTaken": 0,
		"playerOneName": "string",
		"playerTwoName": "string"
	},
	...
	....
]

```

## Final Question:

>Question: 
>>What is the appropriate OAuth 2/OIDC grant to use for a web application using a SPA (Single Page Application) and why?

The appropriate grant to use for SPAs is the Authorization Code Grant (OAuth2) / Authorization Code Flow (OIDC).

The pattern involves exchanging an authorization code for the access token by the Auth Server verifying the Auth Code, Client ID and Client Secret. The Auth Server responds with an Access Token that the application can now use to call the API to access information about the user, granting them special permissions.

The access tokens can be saved on the client side as a an http-only cookie. This way it can prevent Cross-Site-Scripting (XSS) attacks.




