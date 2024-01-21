
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SalaryPaymentDTO } from './models/salaryPayment';

@Injectable({
  providedIn: 'root'
})
export class PayrollService {
  private baseUrl = 'https://localhost:7091/api/SalaryPayments';

  constructor(private http: HttpClient) {}

  getSalaryPayments(paymentId: number): Observable<SalaryPaymentDTO[]> {
    return this.http.get<SalaryPaymentDTO[]>(`${this.baseUrl}/${paymentId}`);
  }

  getAllSalaryPayments(): Observable<SalaryPaymentDTO[]> {
    return this.http.get<SalaryPaymentDTO[]>(`${this.baseUrl}`);
  }

  updateSalaryPayment(id: number, salaryPayment: SalaryPaymentDTO): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`, salaryPayment);
  }

  deleteSalaryPayment(id: number): Observable<SalaryPaymentDTO> {
    return this.http.delete<SalaryPaymentDTO>(`${this.baseUrl}/${id}`);
  }

  createSalaryPayment(salaryPayment: SalaryPaymentDTO): Observable<SalaryPaymentDTO> {
    return this.http.post<SalaryPaymentDTO>(`${this.baseUrl}`, salaryPayment);
  }
}
