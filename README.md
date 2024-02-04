

# employee payroll app 

1) Angular v17
2) .Net 6

![app-demo2](https://github.com/AmilaKeerthi/PayrollApp/assets/154110349/34d8553d-8402-4441-96fa-0c7b11ad9494)


![image](https://github.com/AmilaKeerthi/PayrollApp/assets/154110349/3607eca0-31a8-4433-b9dc-cf98260553f9)

Features 
Employee Management:
  Add new employees with basic details (name, email, salary, join date).
  Send welcome email with temporary password to new employees.
  Allow employees to login and update their profile (except email, salary, join date).
  View employee list and individual profiles.
  Activate/inactivate employee profiles.
Salary Management:
  Record monthly employee salary payments.
  View salary payments by month and employee.
Security:
  Separate admin login with full access.
  Employee login with restricted access.
  Secure password management (temporary password, password change).
  Role-based access control.
Additional:
  Used Angular frontend and .NET 6 backend.
  Implemented REST API standards.
  Used design patterns and lazy loading.

Instructions

Backend

1)Clone and open the payrollAPI project in visual studio 2022.
2) build solution
3)Open PayrollAPI.Infrastructure project in nuget package manager console
4)Execute Update-Database command
5)run payrollAPI 

Frontend

1) open parollUI folder in command line.
2) execute npm install command 
3) execute ng s -o
