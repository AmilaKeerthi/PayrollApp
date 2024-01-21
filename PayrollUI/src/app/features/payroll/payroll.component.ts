import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PayrollService } from './payroll.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { SalaryPaymentDTO } from './models/salaryPayment';
import { PayrollAddEditComponent } from './payroll-add-edit/payroll-add-edit.component';
import { NotificationService } from '../../core/services/notification.service';
import { AuthenticationService } from '../../core/services/auth.service';

@Component({
  selector: 'payroll-root',
  templateUrl: './payroll.component.html',
  styleUrls: ['./payroll.component.css'],
})
export class PayrollComponent implements OnInit {
  displayedColumns: string[] = ['payrollId', 'fullName','paymentDate','salary'];
  dataSource = new MatTableDataSource<SalaryPaymentDTO>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private dialog: MatDialog,
    private payrollService: PayrollService,
    private notificationService: NotificationService,
    private authService: AuthenticationService
  ) {}

  ngOnInit(): void {
    this.getPayrollList();
  }

  openAddEditPayrollDialog() {
    const dialogRef = this.dialog.open(PayrollAddEditComponent);
    dialogRef.afterClosed().subscribe({
      next: (val) => {
        if (val) {
          this.getPayrollList();
        }
      },
    });
  }

  getPayrollList() {
    const empName = this.authService.getCurrentUser().fullName;

    this.payrollService.getAllSalaryPayments().subscribe({
      next: (res) => {
        res.forEach(i=>{ i.fullName=i.employee?i.employee.fullName:empName });
        this.dataSource = new MatTableDataSource(res);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
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

  deletePayroll(id: number) {
    let confirm = window.confirm("Do you want to delete this payroll?");
    if(confirm) {
      this.payrollService.deleteSalaryPayment(id).subscribe({
        next: (res) => {
          this.notificationService.openSnackBar('Payroll deleted!');
          this.getPayrollList();
        },
        error: (err) => {
          console.log(err);
        },
      });
    }
  }

  openEditForm(data: any) {
    const dialogRef = this.dialog.open(PayrollAddEditComponent, {
      data,
    });

    dialogRef.afterClosed().subscribe({
      next: (val) => {
        if (val) {
          this.getPayrollList();
        }
      }
    });
  }
}
