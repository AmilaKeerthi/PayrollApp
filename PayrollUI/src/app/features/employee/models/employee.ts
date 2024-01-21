export interface EmployeeDTO {
  employeeId: number;
  fullName?: string | null;
  email?: string | null;
  salary: number;
  joinDate: string; // Assuming a date-time string format
  phoneNumber?: string | null;
  address?: string | null;
}
