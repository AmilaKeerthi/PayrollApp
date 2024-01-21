import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EmployeeDTO } from './models/employee';
import { SalaryPaymentUpdateDTO } from './models/salaryPayment';

@Injectable({
  providedIn: 'root',
})
export class EmployeeService {
  private baseUrl = 'https://localhost:7091/api/Employee';

  constructor(private http: HttpClient) {}

  getEmployee(userId: number): Observable<EmployeeDTO[]> {
    return this.http.get<EmployeeDTO[]>(`${this.baseUrl}/${userId}`);
  }

  getAllEmployee(): Observable<EmployeeDTO[]> {
    return this.http.get<EmployeeDTO[]>(`${this.baseUrl}`);
  }

  updateEmployee(id: number, employee: EmployeeDTO): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`, employee);
  }

  deleteEmployee(id: number): Observable<EmployeeDTO> {
    return this.http.delete<EmployeeDTO>(`${this.baseUrl}/${id}`);
  }

  createEmployee(employee: EmployeeDTO): Observable<EmployeeDTO> {
    return this.http.post<EmployeeDTO>(`${this.baseUrl}`, employee);
  }

  addSalaryPayment(salary: SalaryPaymentUpdateDTO): Observable<SalaryPaymentUpdateDTO> {
    return this.http.post<SalaryPaymentUpdateDTO>(`${this.baseUrl}/SalaryPayment`, salary);
  }
}
