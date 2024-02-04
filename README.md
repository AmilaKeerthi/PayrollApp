

# employee payroll app 

 1) Used Angular v17 frontend and .NET 6 backend.
 2) Implemented REST API standards.
 3) Used design patterns and lazy loading.

![app-demo2](https://github.com/AmilaKeerthi/PayrollApp/assets/154110349/34d8553d-8402-4441-96fa-0c7b11ad9494)

Features 

  Employee Management:
  
    1) Add new employees with basic details (name, email, salary, join date).
    2) Send welcome email with temporary password to new employees.
    3) Allow employees to login and update their profile (except email, salary, join date).
    4) View employee list and individual profiles.
    5) Activate/inactivate employee profiles.
    
  Salary Management:
  
    1) Record monthly employee salary payments.
    2) View salary payments by month and employee.
    
  Security:
  
    1) Separate admin login with full access.
    2) Employee login with restricted access.
    3) Secure password management (temporary password, password change).
    4) Role-based access control.


![image](https://github.com/AmilaKeerthi/PayrollApp/assets/154110349/3607eca0-31a8-4433-b9dc-cf98260553f9)


  
   

Instructions

Backend

1) Clone and open the payrollAPI project in visual studio 2022.
2)  build solution
3) Open PayrollAPI.Infrastructure project in nuget package manager console
4) Execute Update-Database command
5) run payrollAPI 

Frontend

1) open parollUI folder in command line.
2) execute npm install command 
3) execute ng s -o
