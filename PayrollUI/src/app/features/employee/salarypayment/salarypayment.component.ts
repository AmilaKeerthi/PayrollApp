

// salary-payment-form.component.ts

import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SalaryPaymentUpdateDTO } from '../models/salaryPayment';
import { EmployeeService } from '../employee.service';
import { NotificationService } from '../../../core/services/notification.service';

@Component({
  selector: 'app-salarypayment',
  templateUrl: './salarypayment.component.html',
  styleUrl: './salarypayment.component.scss'
})
export class SalarypaymentComponent implements OnInit {
  salaryPaymentForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<SalarypaymentComponent>,
    @Inject(MAT_DIALOG_DATA) public data: SalaryPaymentUpdateDTO,
    private empService: EmployeeService,
    private notificationService: NotificationService

  ) {
    this.salaryPaymentForm = this.fb.group({
      salaryPaymentId: [data.salaryPaymentId],
      paymentDate: [data.paymentDate, Validators.required],
      employeeId: [data.employeeId, Validators.required],
      amount: [data.amount, Validators.required],
    });
  }

  ngOnInit(): void {}

  onSave(): void {
    if (this.salaryPaymentForm.valid) {
      const formValue = this.salaryPaymentForm.value as SalaryPaymentUpdateDTO;

      this.empService.addSalaryPayment(formValue).subscribe({
        next: (val: any) => {
          this.notificationService.openSnackBar('Payroll details added!');
          this.dialogRef.close(true);
        },
        error: (err: any) => {
          console.error(err);
          this.notificationService.openSnackBar("Error while adding the payroll!");
        },
      });
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
