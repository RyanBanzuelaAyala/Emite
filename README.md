# EMITE - Call Center Management API

This project is a Call Center Management API built with ASP.NET Core (.NET 6). It provides endpoints for managing call center operations, including agents, calls, customers, and tickets. The API demonstrates skills in API design, database interactions, authentication, and testing.

## Features

- CRUD operations for Agents, Calls, Customers, and Tickets
- JWT authentication
- Dependency injection
- Basic error handling and logging
- Unit tests for service layer
- Integration tests for API endpoints
- Use of Entity Framework Core for database operations
- Real-time notifications for new calls using SignalR (Optional)
- Pagination, search functionality, in-memory caching, basic statistics endpoint, rate limiting (Optional)
- Call routing algorithm to assign calls to available agents (Optional)

## Getting Started

### Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Node.js](https://nodejs.org/)
- [npm](https://www.npmjs.com/)
- [Swagger Codegen](https://swagger.io/tools/swagger-codegen/)

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/RyanBanzuelaAyala/Emite.git
   cd emite-call-center-management-api
   ```

2. Restore .NET dependencies:

   ```bash
   dotnet restore
   ```

3. Install npm dependencies:

   ```bash
   npm install
   ```

4. Install Swagger Codegen CLI:

   ```bash
   npm install -g swagger-codegen-cli
   ```

### Running the Application

1. Start the application:

   ```bash
   dotnet run
   ```

2. Open your browser and navigate to `https://localhost:5001/swagger` to view the Swagger UI documentation.

### API Endpoints

#### Agents

- Retrieve all agents: `GET /api/agents`
- Retrieve a specific agent: `GET /api/agents/{id}`
- Add a new agent: `POST /api/agents`
- Update an existing agent: `PUT /api/agents/{id}`
- Delete an agent: `DELETE /api/agents/{id}`
- Update agent status: `PATCH /api/agents/{id}/status`

#### Calls

- Retrieve all calls: `GET /api/calls`
- Retrieve a specific call: `GET /api/calls/{id}`
- Create a new call: `POST /api/calls`
- Update an existing call: `PUT /api/calls/{id}`
- Delete a call: `DELETE /api/calls/{id}`
- Assign a call to an agent: `PATCH /api/calls/{id}/assign`

#### Customers

- Retrieve all customers: `GET /api/customers`
- Retrieve a specific customer: `GET /api/customers/{id}`
- Add a new customer: `POST /api/customers`
- Update an existing customer: `PUT /api/customers/{id}`
- Delete a customer: `DELETE /api/customers/{id}`

#### Tickets

- Retrieve all tickets: `GET /api/tickets`
- Retrieve a specific ticket: `GET /api/tickets/{id}`
- Create a new ticket: `POST /api/tickets`
- Update an existing ticket: `PUT /api/tickets/{id}`
- Delete a ticket: `DELETE /api/tickets/{id}`
- Assign a ticket to an agent: `PATCH /api/tickets/{id}/assign`

## Contributing

Contributions are welcome! Please follow the guidelines outlined in `CONTRIBUTING.md`.

## License

This project is licensed under the MIT License. See `LICENSE.md` for more information.
