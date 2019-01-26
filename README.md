# Movies Database API

The server-side is targetting __.NET Core v2.2__.
__"MoviesDatabase.Api"__ is the startup project which is compatible with .NET Core 2.2 only.

__There are several environments, please use the default (development) ones.__

_Brief overview:_
* I've used Swagger in order to document the web API
* I am using in-memory Entity framework for storage
* Clean architecture based approach. Business logic and contracts are located in the Core project.
* Database, external system's logic and implementations are located in the Infrastructure project.

_Api overview:_
 * You can filter the movies by title, release date, genres and users using query parameters, for each one respectively.
 * You can sort them by title, rating and year of release in ascending and descending order.
 * Supports pagination with skip and take.

For more details - have a look at the Swagger page.
