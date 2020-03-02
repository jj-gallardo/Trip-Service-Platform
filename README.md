# Trip Service Platform

This repository contains the architecture code of Trip Platform.

The idea is to get a well structured scalable solution.

## Solution Structure
Run each service using kestrel / IIS or run the whole architecture using docker-compose.

```
docker-compose up
```

It'll start several services / containers:
 - Ocelot Api Gateway
 - WebApi Service (DDD Service)
 - Mongo Database 
 - Identity Server (Authentication / Authorization)
 - Consul (Service Discovering)
 - Seq (Development logs analysis and aggregation)


### Api Service

Implements:
 - SOLID principles
 - Domain Driven Development (DDD)
 - Command Query Responsibility Segregation (Using Mediatr)
 - Fluent Command Query Validators
 - Command Query Middlewares
 - MongoDB Database Persistence Layer
 - Basic Healthcheck
 - Service Registration (Consul)
 - Swagger Documentation

[Code](https://github.com/jj-gallardo/Trip-Service-Platform/tree/master/Backend/Src)

### Api Gateway

Built with Ocelot ApiGateway, implements:
- Service Routing
- Load Balancing (Using Consul Service Discovery)
- Rate Limiting
- Circuit Breaker
- Retry

[Code](https://github.com/jj-gallardo/Trip-Service-Platform/tree/master/ApiGateway)

### Service Discovery (Consul)

Support Realtime Service discovery with service healthchecks.

![Consul](https://user-images.githubusercontent.com/5726980/75719255-b522cb80-5cd4-11ea-97f8-05cde31776dd.gif)

### Seq Logging

Log aggregation and analysis. Used for development. 

 ![Seq Logging](https://user-images.githubusercontent.com/5726980/75713097-71769480-5cc9-11ea-8c2f-fa70f358f364.png)









