
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { EmployeesRoutingModule } from './employee-routing.module';
import { EmployeeComponent } from './employee.component';
import { EmpAddEditComponent } from './emp-add-edit/emp-add-edit.component';
import { SalarypaymentComponent } from './salarypayment/salarypayment.component';

@NgModule({
    imports: [
        CommonModule,
        EmployeesRoutingModule,
        SharedModule
    ],
    declarations: [
        EmployeeComponent,
        EmpAddEditComponent,
        SalarypaymentComponent
    ]
})
export class EmployeesModule { }
