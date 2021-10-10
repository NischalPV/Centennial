import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MasterDataRoutingModule } from './master-data-routing.module';


// Angular Material Modules

import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatFormField, MatFormFieldModule } from '@angular/material/form-field';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';

// Components

import { ProcessesComponent } from './processes/processes.component';
import { MaterialsComponent } from './materials/materials.component';
import { CustomersComponent } from './customers/customers.component';
import { ProductsComponent } from './products/products.component';
import { RawMaterialComponent } from './raw-material/raw-material.component';
import { EmployeesComponent } from './employees/employees.component';
import { ProductionProcessesComponent } from './production-processes/production-processes.component';

@NgModule({
  declarations: [
    ProcessesComponent,
    MaterialsComponent,
    CustomersComponent,
    ProductsComponent,
    RawMaterialComponent,
    EmployeesComponent,
    ProductionProcessesComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatPaginatorModule,
    MatTableModule,
    MatSortModule,
    MatSidenavModule,
    MatInputModule,
    MatSelectModule,
    MatIconModule,
    MatMenuModule,
    MatToolbarModule,
    MatButtonModule,
    MatFormFieldModule,
    MatGridListModule,
    MatSlideToggleModule,
    MatCardModule,
    MatDividerModule,
    MasterDataRoutingModule
  ]
})
export class MasterDataModule { }
