# ModernRestApi
*  Modern Restful API that takes in a name and address
*  Inputs are persisted in memory with Entity Framework Core
*  Name field is considered a unique set
*  Allows updates to inputs in memory
*  Allows creation of inputs to store in memory
*  Allows soft deletes to those inputs.
*  Ability to return all persisted inputs.

# Technical Specifications
*  Uses Swagger to test API easily
*  Utilizes CLEAN architecture
*  Written in .NetCore 6
*  Handles adding, updating, soft deleting and getting data for users names and addresses.
*  Utilizes FluentValidation to help enforce data constraints on name and address
*  Utilizes Mediator to automatically route to appropriate Handler based on response and request types provided
*  Utilizes Entity Framework Core and In Memory processing to retrieve, update, save data In Memory
*  Encompasses Unit Tests with MSTest and Moq
*  All test are rerunnable and test against different scenarios for validation and errors
