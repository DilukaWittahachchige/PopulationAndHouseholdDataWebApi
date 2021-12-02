Project Information 
  This API project is implemented to get the population or households according to the endpoint. Therefore, the project contains two GET WEB APIs.
      Project architecture – N-tier
      ORM – EF Core 
      APIs- DOT NET Core 
      DB - SQLite

GET API with endpoint /<population | households>?state=<state_id,…>

Endpoint
-	APIs should return the value of Population or Households according to the path endpoint, for the states given in the comma-delimited query parameter state.
-	Example /population?state=1,10,12 will return the population data for states 1, 10 and 12.
-	CORS - enable for both endpoints.

Logic
-	For the required states, It looks up Actuals table first. If the data is available, return that data. If not, return a sum of the value over all districts for the required state in the Estimates table.
-	If any input state id is not available, then API return a 404 HTTP Code instead.

Response
-	The response should be in JSON format like this:
[{ “State”: 1, “Population”: 22805514.45 }, { “State”: 10, “Population”: 338888364.12 }, ….]
          
This is Dockerize application. Please use Docker file include in the project for create Docker image.



The web API project must contain two GET APIs after hosting.

1.	Households web API 
     It returns the household data

Query object 

{
  "state": [
    1,10,12 
  ]
}

Swagger API
API - https://localhost:44315/api/households?state=1&state=10&state=12


Response 

[
  {
    "state": 1,
    "household": 8523999.039
  },
  {
    "state": 10,
    "household": 18118047
  },
  {
    "state": 12,
    "household": 1073428
  }
]


2.	Population web API 
     It returns the population data

Query object 
    {
  "state": [
    3,12,4
  ]
}


Swagger API
     API - https://localhost:44315/api/population?state=3&state=12&state=4

Response 
 
[
  {
    "state": 3,
    "population": 13741180
  },
  {
    "state": 12,
    "population": 3343190
  },
  {
    "state": 4,
    "population": 13456843.76
  }
]

