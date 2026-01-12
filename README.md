# BiteLogLibrary

.NET backend library for managing bite log and nutrition-related data.

⚠️ This project is a work in progress and is under active development.

[Dansk version](README.da.md)

## Purpose
BiteLogLibrary is a C# .NET class library designed to handle core backend
logic for a nutrition or bite-tracking application. The library focuses on
clean structure, validation, and testable business logic.

## Features (current)
- User domain model
- User-related business logic
- Input validation for user data
- Unit tests covering user logic

## Project structure
- `BiteLogLibrary/` – Core class library (domain models + business logic)
- `BiteLogRESTAPI/` – REST API wrapper (if runnable / work in progress)
- `Unit Test/` – Unit tests (MSTest)

## Planned features
- Additional domain models (e.g. bite log entries)
- Persistence layer (database or in-memory storage)
- REST API wrapper
- Extended validation and error handling

## Technologies
- C#
- .NET
- MSTest
- Git / GitHub

## Tests
Unit tests are implemented using MSTest.
