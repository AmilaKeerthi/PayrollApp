

# employee payroll app 

1) Angular
2) .Net 6

Class Diagram
+----------------------+     +------------------------+
|    Employee          |     | SalaryPayment          |
+----------------------+     +------------------------+
| - EmployeeId: int    |     | - SalaryPaymentId: int |
| - FullName: string   |     | - EmployeeId: int      |
| - Email: string      |     | - PaymentDate: DateTime|
| - Salary: decimal    |     | - Amount: decimal      |
| - JoinDate: DateTime |     |                        |
| - PhoneNumber: string|     |                        |
| - Address: string    |     |                        |
| - IsActive: bool     |     |                        |
| - User: User         |     |                        |
+----------------------+     +------------------------+
  ^         ^                           |
 1|        1|                           |1..*
  |         +---------------------------+
  |
  |
  |
  |
  |  +------------------------+
 1|  |      User              |
  |  +------------------------+
  |  | - UserId: int          |
  +--| - EmployeeId: int      |
     | - Email: string        |
     | - PasswordHash: string |
     | - IsAdmin: bool        |
     | - Employee: Employee   |
     |                        |
     |                        |
     |                        |
     +------------------------+


