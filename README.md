# LeavePlanner
NET Core 7 web application for managing leaves in organization
______________________________________________________________________________________________________________________
This repository provied .NET core web application for managing employees and their leave requests.

Installation for local usage:
Clone repo
Start app using Visual studio or any familiar IDE
Update dotnet packages
Configure  app settings and connection string for your need
Run update-database to create datatabase and tables
Run application with F5 to seed database (users, roles, userRoles, codex tables, and business tables) 
Initial role:
- Admin
- Employee
Initial users:
- Admin - admin@admin.hr (ROLE: Admin)
- Employee 1 - employee@employee.hr (ROLE: Employee)
- Employee 2 - employee2@employee2.hr (ROLE: Employee)
- Employee 3 - employee3@employee3.hr (ROLE: Employee
Passwords: Password.1!

After user is created admin can edit each user and assign role "Employee"
After that employee is allowed to create leave request

Used technologies:
.NET 7
MS SQL Server
Jquery & Javascript
Custom theme

Features:
- Log4Net
- SignalR
- AutoMapper
- CodeFirst
- Microsoft Identity
- Unit of work
- Repository pattern
- Model configuration
- Datatables
- Smart navigation
