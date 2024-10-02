# Take home assignment for Datamatrix Corp candidacy
<br>Target runtime of the project is .NET 7</br>
<br>The Contacts API is implemented with Domain Driven Design in mind, with Domain <- Services <- Presentation layers.</br>
<br>I can also achieve DDD with MediatR and CQRS, but I kept the standard service layer classes to keep things simple.</br>
<br>Although unspecified or out of scope, data persistence is chosen to be PostgreSQL with code-first EF Core. I chose PostgreSQL since it is mentioned in the job description, and decided to use code-first as it is convenient for such small projects.</br>
<br>Automated tests and logs are not included and deemed out of scope, however I think they should always be present.</br>
<br>The REST API has 5 endpoints:</br>
<br>GET api/contacts/?SearchToken=a&SortToken=FirstName&Descending=true&PageSize=10&PageNumber=1</br>

GET api/contacts/{id}

POST api/contacts/
body:<br>
{<br>
  "firstName": "string",<br>
  "lastName": "string",<br>
  "phone": "string",<br>
  "email": "string"<br>
}

PUT api/contacts/<br>
{<br>
  "id": int,<br>
  "firstName": "string",<br>
  "lastName": "string",<br>
  "phone": "string",<br>
  "email": "string"<br>
}

DELETE api/contacts/{id}
