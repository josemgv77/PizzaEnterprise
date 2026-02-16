# Pizza Enterprise - Online Ordering & Delivery Management System

[![.NET](https://img.shields.io/badge/.NET-8.0-blue)](https://dotnet.microsoft.com/download)
[![EF Core](https://img.shields.io/badge/EF%20Core-8.0-blue)](https://docs.microsoft.com/ef/)
[![License](https://img.shields.io/badge/license-MIT-green)](LICENSE)

Sistema completo de gestión de pedidos y reparto de pizzas implementado con **Domain-Driven Design (DDD)** y **Clean Architecture**.

## 📋 Descripción

Pizza Enterprise es una aplicación empresarial completa para gestionar ventas online de pizzas y su reparto. El proyecto implementa patrones avanzados de arquitectura incluyendo DDD, CQRS, Repository Pattern, Unit of Work, y Domain Events.

## 🏗️ Arquitectura

El proyecto sigue una arquitectura en capas basada en DDD:

```
src/
├── PizzaEnterprise.Domain/           # Capa de Dominio
├── PizzaEnterprise.Application/      # Capa de Aplicación (CQRS)
├── PizzaEnterprise.Infrastructure/   # Capa de Infraestructura
└── PizzaEnterprise.API/              # Capa de Presentación (REST API)
```

### Principios Arquitectónicos

- **Domain-Driven Design (DDD)**: Modelado del dominio con agregados, entidades, value objects y eventos de dominio
- **CQRS**: Separación de comandos (escritura) y queries (lectura)
- **Clean Architecture**: Dependencias apuntando hacia el dominio
- **Repository Pattern**: Abstracción del acceso a datos
- **Unit of Work**: Gestión de transacciones
- **Domain Events**: Eventos de negocio con MediatR

## 🚀 Tecnologías

- **.NET 8** - Framework principal
- **C# 12** - Lenguaje de programación
- **Entity Framework Core 8** - ORM para persistencia
- **SQLite** - Base de datos para desarrollo (SQL Server para producción)
- **MediatR** - Patrón mediador para CQRS y eventos
- **AutoMapper** - Mapeo objeto-objeto
- **FluentValidation** - Validación de comandos
- **Swagger/OpenAPI** - Documentación de API

## 📦 Estructura del Proyecto

### Domain Layer (PizzaEnterprise.Domain)

**Common/**
- `BaseEntity` - Clase base para todas las entidades
- `ValueObject` - Clase base para value objects
- `IDomainEvent` / `DomainEvent` - Eventos de dominio

**Entities/**
- `Order` - Aggregate Root para pedidos
- `OrderItem` - Líneas de pedido
- `Customer` - Clientes
- `Pizza` - Pizzas del menú
- `DeliveryPerson` - Repartidores
- `Payment` - Pagos

**Value Objects/**
- `Money` - Valor monetario con moneda
- `Address` - Dirección postal

**Enums/**
- `OrderStatus` - Estados del pedido (Pending, Confirmed, Preparing, etc.)
- `PizzaSize` - Tamaños (Small, Medium, Large, ExtraLarge)
- `PaymentStatus` - Estados del pago
- `PaymentMethod` - Métodos de pago

**Events/**
- `OrderCreatedEvent` - Pedido creado
- `OrderConfirmedEvent` - Pedido confirmado
- `OrderDeliveredEvent` - Pedido entregado
- `PaymentCompletedEvent` - Pago completado

### Application Layer (PizzaEnterprise.Application)

**Commands/**
- `CreateOrderCommand` - Crear nuevo pedido
- `ConfirmOrderCommand` - Confirmar pedido

**Queries/**
- `GetOrderByIdQuery` - Obtener pedido por ID
- `GetAvailablePizzasQuery` - Listar pizzas disponibles

**Event Handlers/**
- Manejo de eventos de dominio con logging y sincronización AS400

**Behaviors/**
- `ValidationBehavior` - Validación automática con FluentValidation

### Infrastructure Layer (PizzaEnterprise.Infrastructure)

**Persistence/**
- `ApplicationDbContext` - DbContext con dispatch de eventos de dominio
- `Repository<T>` - Implementación genérica de repositorio
- `UnitOfWork` - Gestión de transacciones
- Entity Configurations con Fluent API

**AS400/**
- `AS400Service` - Servicio de integración con sistemas legacy (placeholder)

### API Layer (PizzaEnterprise.API)

**Controllers/**
- `OrdersController` - CRUD de pedidos
- `PizzasController` - Listado de pizzas

**Middleware/**
- `ExceptionHandlingMiddleware` - Manejo centralizado de excepciones

## 🔧 Instalación y Configuración

### Prerrequisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- IDE recomendado: Visual Studio 2022, Visual Studio Code, o Rider

### Pasos de Instalación

1. **Clonar el repositorio**
```bash
git clone https://github.com/josemgv77/PizzaEnterprise.git
cd PizzaEnterprise
```

2. **Restaurar paquetes NuGet**
```bash
cd src
dotnet restore
```

3. **Compilar la solución**
```bash
dotnet build
```

4. **Configurar la cadena de conexión** (opcional)

Editar `src/PizzaEnterprise.API/appsettings.json` para cambiar la base de datos:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=PizzaEnterpriseDb;..."
  }
}
```

5. **Ejecutar la aplicación**
```bash
cd PizzaEnterprise.API
dotnet run
```

La API estará disponible en:
- HTTP: `http://localhost:5288`
- Swagger UI: `http://localhost:5288/swagger`

## 📚 Uso de la API

### Endpoints Disponibles

#### Pizzas

**GET** `/api/pizzas/available`
- Obtiene todas las pizzas disponibles
- Respuesta: `200 OK` con lista de pizzas

#### Pedidos

**POST** `/api/orders`
- Crea un nuevo pedido
- Body:
```json
{
  "customerId": "guid",
  "deliveryAddress": {
    "street": "Calle Principal 123",
    "city": "Madrid",
    "state": "Madrid",
    "zipCode": "28001",
    "country": "España"
  },
  "items": [
    {
      "pizzaId": "guid",
      "quantity": 2
    }
  ]
}
```
- Respuesta: `201 Created` con ID del pedido

**GET** `/api/orders/{id}`
- Obtiene un pedido por ID
- Respuesta: `200 OK` con datos del pedido o `404 Not Found`

**POST** `/api/orders/{id}/confirm`
- Confirma un pedido existente
- Respuesta: `204 No Content`

### Ejemplos con cURL

```bash
# Obtener pizzas disponibles
curl -X GET "http://localhost:5288/api/pizzas/available"

# Crear un pedido
curl -X POST "http://localhost:5288/api/orders" \
  -H "Content-Type: application/json" \
  -d '{
    "customerId": "customer-guid",
    "deliveryAddress": {
      "street": "Av. Reforma 123",
      "city": "Ciudad de México",
      "state": "CDMX",
      "zipCode": "06600",
      "country": "México"
    },
    "items": [
      {
        "pizzaId": "pizza-guid",
        "quantity": 2
      }
    ]
  }'

# Confirmar pedido
curl -X POST "http://localhost:5288/api/orders/{order-id}/confirm"
```

## 🗄️ Modelo de Datos

### Entidades Principales

**Order (Aggregate Root)**
- OrderNumber (auto-generado: "ORD-YYYYMMDD-XXXX")
- Customer, OrderDate, Status
- TotalAmount (Money)
- DeliveryAddress (Address)
- Items (colección)

**Pizza**
- Name, Description
- BasePrice (Money)
- Size (enum)
- IsAvailable

**Customer**
- FirstName, LastName
- Email, PhoneNumber
- DefaultAddress (Address)

### Value Objects

**Money**
- Amount (decimal)
- Currency (string, default "USD")
- Operaciones: Add, Multiply

**Address**
- Street, City, State, ZipCode, Country

## 🎯 Patrones y Prácticas

### Domain-Driven Design

- **Aggregate Roots**: Order es el AR principal, gestiona OrderItems
- **Value Objects**: Money y Address son inmutables
- **Domain Events**: Eventos disparados por cambios en agregados
- **Entities**: Identificadas por Id (Guid)
- **Repository Interfaces**: Definidas en el dominio

### CQRS (Command Query Responsibility Segregation)

- **Commands**: Operaciones de escritura (CreateOrder, ConfirmOrder)
- **Queries**: Operaciones de lectura (GetOrderById, GetAvailablePizzas)
- **Handlers**: Un handler por comando/query
- **MediatR**: Mediador entre API y handlers

### Event-Driven Architecture

- **Domain Events**: Eventos publicados por entidades
- **Event Handlers**: Suscriptores que reaccionan a eventos
- **Automatic Dispatch**: Eventos despachados automáticamente en SaveChanges

## 🔒 Seguridad

- Validación de entrada con FluentValidation
- Manejo centralizado de excepciones
- Parámetros de base de datos escapados por EF Core
- Sin secrets en código fuente

## 🧪 Testing

```bash
# Ejecutar tests (cuando se implementen)
dotnet test
```

## 📝 Datos de Prueba

La aplicación incluye datos de prueba (seed data) que se cargan automáticamente:

**Clientes** (3)
- Juan Pérez, María González, Carlos López

**Pizzas** (5)
- Margherita (Small, $8.99)
- Pepperoni (Medium, $12.99)
- Hawaiian (Large, $15.99)
- Supreme (Large, $17.99)
- Vegetarian (Medium, $11.99)

**Repartidores** (2)
- Pedro Martínez, Ana Rodríguez

## 🚦 Estado del Proyecto

- ✅ Arquitectura DDD completa
- ✅ CQRS con MediatR
- ✅ Domain Events
- ✅ Entity Framework Core
- ✅ API REST con Swagger
- ✅ Validación con FluentValidation
- ✅ Manejo de excepciones
- ✅ Seed data
- ⏳ Tests unitarios y de integración
- ⏳ Autenticación y autorización
- ⏳ Integración AS400 real

## 🤝 Contribuir

Las contribuciones son bienvenidas. Por favor:

1. Fork el proyecto
2. Crear una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abrir un Pull Request

## 📄 Licencia

Este proyecto está bajo la Licencia MIT. Ver el archivo `LICENSE` para más detalles.

## 👥 Autores

- **José Miguel** - [josemgv77](https://github.com/josemgv77)

## 🙏 Agradecimientos

- Inspirado en los principios de Domain-Driven Design de Eric Evans
- Arquitectura Clean Architecture de Robert C. Martin
- Patrones CQRS y Event Sourcing

## 📧 Contacto

Para preguntas o soporte, por favor abrir un issue en GitHub.
