import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError, filter } from 'rxjs/operators';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from "@angular/router";

import { IProductionProcess } from '../../shared/interfaces/production-process.model';
import { IProduct } from '../../shared/interfaces/product.model';
import { IProcess } from '../../shared/interfaces/process.model';
import { ProductionProcessService } from '../shared/services/production-process.service';
import { ConfigurationService } from '../../shared/services/configuration.service';

import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { ComponentPageTitle } from '../../shared/classes/component-page-title';

@Component({
  selector: 'app-production-processes',
  templateUrl: './production-processes.component.html',
  styleUrls: ['./production-processes.component.css']
})
export class ProductionProcessesComponent implements OnInit {

  public product: IProduct;
  public processes: IProcess[];
  public productionProcesses: IProductionProcess[];
  private productId: string;

  errorReceived: boolean;
  readonly formControl: AbstractControl;
  public displayedColumns: string[] = ['sn', 'process', 'sequence', 'isMandatory', 'createdDate', 'action'];
  public datasource = new MatTableDataSource<IProductionProcess>();

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  constructor(private service: ProductionProcessService, private configurationService: ConfigurationService, private formBuilder: FormBuilder, public _componentPageTitle: ComponentPageTitle, private route: ActivatedRoute) {
    this.route.params.subscribe(params => {
      this.productId = params['id'];
    });
  }

  ngOnInit(): void {
    if (this.configurationService.isReady) {
      this.loadData();

    }
    else {
      this.configurationService.settingsLoaded$.subscribe(x => {
        this.loadData();
      });
    }

    

    this.datasource.paginator = this.paginator;
    this.datasource.sort = this.sort;
  }

  private loadData() {
    this.errorReceived = false;

    this.service.getProduct(this.productId)
      .pipe(catchError((err) => this.handleError(err)))
      .subscribe(prod => {
        this.product = prod;
        console.log(`Product received: ${this.product.name}`);
        console.log(this.product);
        this._componentPageTitle.title = `${this.product.name} Processes`;
      });

    this.service.getProcesses()
      .pipe(catchError((err) => this.handleError(err)))
      .subscribe(procs => {
        this.processes = procs;
        console.log("processes received: " + this.processes.length);
      });
  }

  private handleError(error: any) {
    this.errorReceived = true;
    return Observable.throw(error);
  }

}
