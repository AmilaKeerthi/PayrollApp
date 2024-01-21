import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { EmpAddEditComponent } from './emp-add-edit/emp-add-edit.component';
import { EmployeeService } from './employee.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { EmployeeDTO } from './models/employee';
import { SalarypaymentComponent } from './salarypayment/salarypayment.component';
import { SalaryPaymentUpdateDTO } from './models/salaryPayment';
import { NotificationService } from '../../core/services/notification.service';

@Component({
  selector: 'employee-root',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css'],
})
export class EmployeeComponent implements OnInit {
  displayedColumns: string[] = ['employeeId', 'fullName', 'email', 'salary', 'joinDate', 'phoneNumber', 'address','active','action'];
  dataSource = new MatTableDataSource<EmployeeDTO>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private dialog: MatDialog,
    private empService: EmployeeService,
    private notificationService: NotificationService
  ) {}

  ngOnInit(): void {
    this.getEmployeeList();
  }

  openAddEditEmployeeDialog() {
    const dialogRef = this.dialog.open(EmpAddEditComponent);
    dialogRef.afterClosed().subscribe({
      next: (val) => {
        if (val) {
          this.getEmployeeList();
        }
      },
    });
  }

  getEmployeeList() {
    this.empService.getAllEmployee().subscribe({
      next: (res) => {
        this.dataSource = new MatTableDataSource(res);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
        console.log(res);
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  deleteEmployee(id: number) {
    let confirm = window.confirm("Do you want to delete this employee?");
    if(confirm) {
      this.empService.deleteEmployee(id).subscribe({
        next: (res) => {
          this.notificationService.openSnackBar('Employee deleted!');
          this.getEmployeeList();
        },
        error: (err) => {
          console.log(err);
        },
      });
    }
  }

  openEditForm(data: any) {
    const dialogRef = this.dialog.open(EmpAddEditComponent, {
      data,
    });

    dialogRef.afterClosed().subscribe({
      next: (val) => {
        if (val) {
          this.getEmployeeList();
        }
      }
    });
  }

  openSalaryPayment(employee?: EmployeeDTO): void {
    const dialogRef = this.dialog.open(SalarypaymentComponent, {
      width: '400px',
      data: { salaryPaymentId: 0, paymentDate: '', employeeId: employee?.employeeId, amount: employee?.salary },
    });

    dialogRef.afterClosed().subscribe((result: SalaryPaymentUpdateDTO | undefined) => {
      if (result) {

        // Handle the result, e.g., save or update the salary payment
        console.log('Salary Payment result:', result);
      }
    });
  }
}
