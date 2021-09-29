import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductionProcessesComponent } from './production-processes/production-processes.component';

const routes: Routes = [
  { path: 'configuration/productionprocesses/:id', component: ProductionProcessesComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ConfigurationRoutingModule { }
