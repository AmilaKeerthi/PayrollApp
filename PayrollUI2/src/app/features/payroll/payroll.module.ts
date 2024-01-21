
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { PayrollsRoutingModule } from './payroll-routing.module';
import { PayrollComponent } from './payroll.component';
import { PayrollAddEditComponent } from './payroll-add-edit/payroll-add-edit.component';

@NgModule({
    imports: [
        CommonModule,
        PayrollsRoutingModule,
        SharedModule
    ],
    declarations: [
        PayrollComponent,
        PayrollAddEditComponent
    ]
})
export class PayrollsModule { }
