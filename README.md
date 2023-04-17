# Sat.Recruitment

This is a solution for Code Challenge found in https://github.com/Paramo-Tech/Sat.Recruitment

## Change Notes

- Removed Startup.cs and use Net6.0 Top Level Statements in Program.cs.
- Use of Serilog as a Logger provider.
- Added some console logging.
- Persistance of data changed from using TXT to use SQLite (DB file in project folder).
- Changed general project structure, folders and file location.
- Use of FluentValidation library for Data model validations.
- Refactor to follow SOLID principles.
- Use of Mapster library for object mapping.
- Use of a Global Exception Handling middleware.
- Use of Entity Framework plus Repository Pattern.
- Unit test using Moq and Shouldly libraries.

## Aditional Notes
- Would have like to change the way parameters are passed to the controller, like an object FromBody instead of multiple parameters. But this breaks the current use of the API.
- Could have added Unit of Work implementation in the repository pattern to improve it even further.
- I considered the use of MediatR for CQRS (Command Query Responsibility Segregation). But I didn't have it clear. Maybe with a further refactor of the controller requests, as mentioned earlier.