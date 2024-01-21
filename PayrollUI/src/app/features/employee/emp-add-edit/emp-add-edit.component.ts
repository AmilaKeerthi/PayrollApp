import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { EmployeeService } from '../employee.service';
import { NotificationService } from '../../../core/services/notification.service';

@Component({
  selector: 'employee-emp-add-edit',
  templateUrl: './emp-add-edit.component.html',
  styleUrls: ['./emp-add-edit.component.css'],
})
export class EmpAddEditComponent implements OnInit {
  empForm: FormGroup;

  education: string[] = [
    'Matric',
    'Diploma',
    'Intermediate',
    'Graduate',
    'Post Graduate',
  ];

  constructor(
    private empService: EmployeeService,
    private dialogRef: MatDialogRef<EmpAddEditComponent>,
    private formBuilder: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private notificationService: NotificationService
  ) {
    this.empForm = this.formBuilder.group({
      employeeId: [0],
      fullName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      salary: ['', Validators.required],
      joinDate: ['', Validators.required],
      phoneNumber: [''],
      address: [''],
      isActive: [true]
    });
  }

  ngOnInit(): void {
    this.empForm.patchValue(this.data);
  }

  onSubmit() {
    if (this.empForm.valid) {
      if (this.data) {
        this.empService
          .updateEmployee(this.data.employeeId, this.empForm.value)
          .subscribe({
            next: (val: any) => {
              this.notificationService.openSnackBar('Employee details updated!');
              this.dialogRef.close(true);
            },
            error: (err: any) => {
              console.error(err);
              this.notificationService.openSnackBar("Error while updating the employee!");
            },
          });
      } else {
        this.empService.createEmployee(this.empForm.value).subscribe({
          next: (val: any) => {
            this.notificationService.openSnackBar('Employee added successfully!');
            this.empForm.reset();
            this.dialogRef.close(true);
          },
          error: (err: any) => {
            console.error(err);
            this.notificationService.openSnackBar("Error while adding the employee!");
          },
        });
      }
    }
  }
}
