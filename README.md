# UdemyNLayerProject; Generic N-Layer ASP.Net Core API Project

##   >   PROJECT CONTENTS   <

## 1)UdemyNLayerProject.Core:
######  a)The project includes entities and interfaces.
######  b)The project has no dependencies. 
  
## 2)UdemyNLayerProject.Data:
######  a)The project includes repositories via Generic Repository approach, migration folders.
######  b)The project has dependencies that are UdemyNLayerProject.Core project and EntityFramework.  
######  c) The project is capable to migrate with seed and configuration codes.  
######  d) The project changes data on database via UnitOfWork approach.
  
## 3)UdemyNLayerProject.Services
######  a)The project includes only services.
######  b)The project has depenencies that are UdemyNLayerProject.Core and UdemyNLayerProject.Data. 
  
## 4)UdemyNLayerProject.API:
######  a)The project includes controllers,data transfer objects, filters, mapping and extensions.
######  b)The project has depdenencies that are UdemyNLayerProject.Service project,AutoMapper and EntityFramework. 

## 5)UdemyNLayerProject.Test
######  a)The project includes unit tests.
######  b)The project has dependencies that are UdemyNLayerProject.API project, Moq Framework and XUnit. 
