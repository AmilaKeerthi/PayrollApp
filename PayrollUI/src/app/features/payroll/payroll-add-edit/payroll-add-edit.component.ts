import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PayrollService } from '../payroll.service';
import { NotificationService } from '../../../core/services/notification.service';

@Component({
  selector: 'payroll-emp-add-edit',
  templateUrl: './payroll-add-edit.component.html',
  styleUrls: ['./payroll-add-edit.component.css'],
})
export class PayrollAddEditComponent implements OnInit {
  empForm: FormGroup;

  education: string[] = [
    'Matric',
    'Diploma',
    'Intermediate',
    'Graduate',
    'Post Graduate',
  ];

  constructor(
    private payrollService: PayrollService,
    private dialogRef: MatDialogRef<PayrollAddEditComponent>,
    private formBuilder: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private notificationService: NotificationService
  ) {
    this.empForm = this.formBuilder.group({
      payrollId: [0],
      fullName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      salary: ['', Validators.required],
      joinDate: ['', Validators.required],
      phoneNumber: [''],
      address: [''],
    });
  }

  ngOnInit(): void {
    this.empForm.patchValue(this.data);
  }

  onSubmit() {
    if (this.empForm.valid) {
      if (this.data) {
        this.payrollService
          .updateSalaryPayment(this.data.id, this.empForm.value)
          .subscribe({
            next: (val: any) => {
              this.notificationService.openSnackBar('Payroll details updated!');
              this.dialogRef.close(true);
            },
            error: (err: any) => {
              console.error(err);
              this.notificationService.openSnackBar("Error while updating the payroll!");
            },
          });
      } else {
        this.payrollService.createSalaryPayment(this.empForm.value).subscribe({
          next: (val: any) => {
            this.notificationService.openSnackBar('Payroll added successfully!');
            this.empForm.reset();
            this.dialogRef.close(true);
          },
          error: (err: any) => {
            console.error(err);
            this.notificationService.openSnackBar("Error while adding the payroll!");
          },
        });
      }
    }
  }
}
