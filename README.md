# Multi Contact Management	

<div>
  Multi Contact Management is a web API application developed with .NET 6, 
  utilizing the Domain-Driven Design (DDD) architecture to efficiently and securely manage contacts. 
  This project includes JWT Token authentication to ensure the security of user data.
</div>  
  
# Features
<div >
   Creation, editing, deletion, and listing of contacts.<br/>
   User authentication using JWT Token.<br/>
Project structure based on Domain-Driven Design (DDD) for clearer and more maintainable organization.
</div>  

# Technologies Used
<div>
   .NET 6<br/> 
   ASP.NET Core Web API<br/> 
   Entity Framework Core<br/>
   JWT (JSON Web Tokens)
</div>

# Project Structure
<div>
  The project follows the Domain-Driven Design (DDD) structure, organized into three main layers:
  Application Layer: Responsible for orchestrating the application's use cases.
  Domain Layer: Contains entities, aggregates, services, and events representing the application domain.
  Infrastructure Layer: Responsible for concrete implementation of interfaces defined in the domain and integration with external frameworks (database, external services, etc.).
</div>


# Configuration and Execution
<div>
  Clone this repository to your local machine.<br/>
  Make sure you have the .NET 6 SDK installed on your machine.<br/>
  Navigate to the root directory of the project.<br/>
  Configure the necessary environment variables for JWT Token authentication.<br/>
  Run the dotnet run command to start the application.<br/>
  Access the API through the endpoint provided by the local server.
</div>


# License
This project is licensed under the MIT License. See the LICENSE file for more details.
