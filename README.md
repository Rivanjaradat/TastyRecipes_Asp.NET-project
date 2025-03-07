
# 🥘 TastyRecipes API

🔍 Description:TastyRecipes API is a system for managing food recipes, allowing users to register, log in, add recipes, manage comments, and categorize recipes. The project follows Onion Architecture, ensuring a clean separation of concerns and maintainability.

## 🏗️ Architecture
This project is built using Onion Architecture, which promotes loose coupling and high testability by structuring the application into layers:

- Core Layer (Domain)

- Application Layer 

- Infrastructure Layer 

This approach makes it easy to replace or modify components without affecting the entire system

## 🛠 Technologies Used
ASP.NET Core Web API

Entity Framework Core

SQL Server

Identity for Authentication

JWT (JSON Web Token) for Authorization

Onion Architecture

Repository Pattern

DbContext for Database Management

## 🚀Features


🔐 Authentication & Authorization:

Secure JWT-based authentication.

- User registration, login, and password management using ASP.NET Identity.
  
🍲 Recipe Management:

- Users can add, update, and delete their recipes.
- Retrieve all recipes or recipes by a specific user.
  
📂 Category Management:

- Create, update, and delete recipe categories.
- Retrieve all available categories.
💬 Comment System:

- Users can add, update, and delete comments on recipes.
- Retrieve comments by recipe or comments by user.
