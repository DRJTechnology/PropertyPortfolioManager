# Property Portfolio Manager

A website for managing a number of rental properties.

This project is work-in-porgress.

The C# aspnetcore application is intended for use by landlords to manage a portfolio of rental properties.

## User Interface - Blazor

The User interface is written as a Blazor Webassembly application hosted in a C# Web API project.

## Data access

The Web API uses service and repository layers to access a SQL Server database.

## Authentication

TODO

## Document & Image storage - MS Graph

TODO

## Finance - Double entry bookkeeping

TODO

## Other packages used

### Importing bank statements

The importing of bank statement csv files is completed using [CsvHelper](https://github.com/JoshClose/CsvHelper).
The initial implementation during development uses hard-coded column mappings and CultureInfo. This will eventually be stored against the individual portfolios.

### Caching

Redis is used for caching and the [DRJTechnology.Cache](https://github.com/DRJTechnology/DRJTechnology.Cache) package is used to implement this.
