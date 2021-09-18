import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ProcessesComponent } from './processes/processes.component';
import { MaterialsComponent } from './materials/materials.component';
import { CustomersComponent } from './customers/customers.component';
import { ProductsComponent } from './products/products.component';
import { RawMaterialComponent } from './raw-material/raw-material.component';

const routes: Routes = [
  { path: 'masterdata/processes', component: ProcessesComponent },
  { path: 'masterdata/materials', component: MaterialsComponent },
  { path: 'masterdata/customers', component: CustomersComponent },
  { path: 'masterdata/products', component: ProductsComponent },
  { path: 'masterdata/rawmaterials', component: RawMaterialComponent },
];

@NgModule({
  declarations: [],
  imports: [[RouterModule.forRoot(routes)], CommonModule],
  exports: [RouterModule]
})

export class MasterDataRoutingModule {}
