import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { EmployeeService } from '../../employee/employee.service';
import { MatDialogRef } from '@angular/material/dialog';
import { AuthenticationService } from '../../../core/services/auth.service';
import { NotificationService } from '../../../core/services/notification.service';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrl: './edit-profile.component.scss'
})
export class EditProfileComponent {
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
    private formBuilder: FormBuilder,
    private authService: AuthenticationService,
    private notificationService: NotificationService
  ) {
    this.empForm = this.formBuilder.group({
      employeeId: [0],
      fullName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      salary: ['', Validators.required],
      joinDate: ['', Validators.required],
      phoneNumber: [''],
      address: ['']
    });
  }

  ngOnInit(): void {
    this.getEmployee();
  }
  getEmployee(){
    const empId = this.authService.getCurrentUser().id;

    this.empService.getEmployee(empId).subscribe({
      next: (val: any) => {
      this.empForm.patchValue(val);
      },
      error: (err: any) => {
        console.error(err);
        this.notificationService.openSnackBar("Error while getting the employee!");
      },
  });
  }
  onSubmit() {
    if (this.empForm.valid) {
      const empId = this.authService.getCurrentUser().id;

      if (empId) {
        this.empService
          .updateEmployee(empId, this.empForm.value)
          .subscribe({
            next: (val: any) => {
              this.notificationService.openSnackBar('Employee details updated!');
              this.getEmployee();
              // this.dialogRef.close(true);
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
            // this.dialogRef.close(true);
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
