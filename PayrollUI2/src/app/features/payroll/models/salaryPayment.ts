export interface SalaryPaymentDTO {
  salaryPaymentId: number;
  paymentDate: string; // Assuming a date-time string format
  amount: number;
  employee:any;
  fullName: string;
}
