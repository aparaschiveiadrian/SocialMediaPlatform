# Full-Stack Social Media Platform

A fully functional **social media app** built with **React**, **.NET Core**, **JavaScript**, **HTML/CSS**, and **PostgreSQL** (November 2024 – January 2025). This platform allows users to create accounts, post content, follow and unfollow other users, participate in group conversations, and much more.

---

## Table of Contents
1. [Features](#features)
2. [Technologies Used](#technologies-used)
3. [Getting Started](#getting-started)
4. [Running the Application](#running-the-application)
5. [Project Structure](#project-structure)
6. [Pictures for preview](#pictures-preview)
---

## Features

- **User Authentication**: 
  - JWT-based login and registration.
  - Profile management (profile pictures, descriptions).

- **Follow/Unfollow System**: 
  - Ability to follow/unfollow users.
  - Private profiles that require approval for viewing.

- **Interactive Posts and Comments**: 
  - Create, read, and comment on posts.
  - Manage followers/following lists.

- **Group Conversations**:
  - Secure, moderated discussion groups.
  - Users join via a unique code and require moderator approval.

- **Secure and Scalable**:
  - Built with .NET Core using Entity Framework Core.
  - REST APIs for backend-to-frontend communication.
  - PostgreSQL for robust database management.
  - OOP principles and dependency injection for clean, maintainable code.

---

## Technologies Used
- **Frontend**: 
  - [React](https://reactjs.org/) with JavaScript, HTML, and CSS.
- **Backend**: 
  - [.NET Core](https://dotnet.microsoft.com/) with Entity Framework Core.
- **Database**: 
  - [PostgreSQL](https://www.postgresql.org/) for data persistence.
- **Authentication**: 
  - JSON Web Tokens (JWT) for secure user sessions.

---

## Getting Started

### Prerequisites
- **Node.js** (v14+ recommended)
- **.NET 7 or later** (or the appropriate version to run your .NET Core project)
- **PostgreSQL** (running locally or on a server)

### Installation
1. **Clone the Repository**:
   ```bash
   git clone https://github.com/aparaschiveiadrian/SocialMediaPlatform.git
   cd socialmediaplatform
   ```
2. **Set Up the Database**:
   - Create a new PostgreSQL database.
   - Update your connection string in `appsettings.json` (or the appropriate environment variable) within the server project to point to your database.

3. **Install Server Dependencies**:
   ```bash
   cd socialmediaplatform.server
   dotnet restore
   ```
4. **Install Client Dependencies**:
   ```bash
   cd ../socialmediaplatform.client
   npm install
   ```

---

## Running the Application

### 1. Run Full Application (Server + Client)
From the root directory or directly in the `socialmediaplatform.server` folder:

```bash
cd socialmediaplatform.server
dotnet run
```

- This will start the **.NET server** (by default, on `http://localhost:5000` or as configured).
- It will also automatically serve the **client** on `http://localhost:5173/`.

### 2. Run Only the Frontend (Optional)
If you want to run **only the React client** (without the .NET backend):

```bash
cd socialmediaplatform.client
npm run dev
```

- By default, the frontend runs on `http://localhost:5173/`.

---

## Project Structure

```
socialmediaplatform
├── socialmediaplatform.server
│   ├── Controllers
│   ├── Models
│   ├── Repositories
│   ├── Services
│   ├── appsettings.json
│   └── socialmediaplatform.server.csproj
├── socialmediaplatform.client
│   ├── public
│   ├── src
│   │   ├── components
│   │   ├── pages
│   │   └── services
│   ├── package.json
│   └── vite.config.js
└── README.md
```

- **`socialmediaplatform.server`**: Contains the ASP.NET Core project.
- **`socialmediaplatform.client`**: Contains the React frontend.

---





**Thank you for checking out the Full-Stack Social Media Platform!** If you have any questions or feedback, please open an issue or reach out. Happy coding!


### Pictures Preview

![preview1](https://imgur.com/8GPMWtU.jpg)
![preview2](https://imgur.com/95L4U6P.jpg)
![preview3](https://imgur.com/pYyQhfd.jpg)
![preview4](https://imgur.com/qJTx3IK.jpg)
![preview5](https://imgur.com/AZJun38.jpg)
![preview6](https://imgur.com/7b3ttxb.jpg)
![preview7](https://imgur.com/CuSsG3Q.jpg)
![preview8](https://imgur.com/3nLefnn.jpg)
![preview9](https://imgur.com/1zzKZ0p.jpg)
![preview10](https://imgur.com/8hAdoxT.jpg)
