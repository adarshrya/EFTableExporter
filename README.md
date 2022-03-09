# EFTableExporter

## Software Requirements
- Visual Studio
- Microsoft SQL Server
- PerfView [Download here](https://github.com/microsoft/perfview/releases)

## Setup Database
- Setup windows login user for SQL Server
- Run Script from [here](https://github.com/adarshrya/EFTableExporter/tree/master/DBScripts)

## Setup Code
- Open EFTableExporter.sln 
- Restore Nuget Packages
- Update Connection String in optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=MasterDb;Integrated Security=true;");[MasterDbContext.cs](https://github.com/adarshrya/EFTableExporter/blob/master/DBLayer/MasterDbContext.cs)

## Raw Result for Thesis
- Open Zip and View Nettrace and log from [Results](https://github.com/adarshrya/EFTableExporter/tree/master/Results)

## Plug N Play
- Copy the class code from [ExportHelper](https://github.com/adarshrya/EFTableExporter/blob/master/PlugPlay/ExportHelper.cs)
- Usage dbContext.ExportToCSV<EntityTable>("Export.CSV");
