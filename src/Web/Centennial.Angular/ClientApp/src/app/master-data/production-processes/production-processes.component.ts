import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { catchError, filter } from 'rxjs/operators';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';

import { ConfigurationService } from '../../shared/services/configuration.service';

import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { ComponentPageTitle } from '../../shared/classes/component-page-title';
import { IProductionProcess } from '../../shared/interfaces/production-process.model';
import { IProduct } from '../../shared/interfaces/product.model';
import { ProductsService } from '../shared/services/products.service';

@Component({
  selector: 'app-production-processes',
  templateUrl: './production-processes.component.html',
  styleUrls: ['./production-processes.component.css']
})
export class ProductionProcessesComponent implements OnInit {

  public productionProcesses: IProductionProcess[];
  public product: IProduct;
  errorReceived: boolean;
  readonly formControl: AbstractControl;
  public displayedColumns: string[] = ['sn', 'sequence', 'process', 'isMandatory', 'createdDate', 'action'];
  public datasource = new MatTableDataSource<IProductionProcess>();

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  constructor(private service: ProductsService, private configurationService: ConfigurationService, private formBuilder: FormBuilder, public _componentPageTitle: ComponentPageTitle, private route: ActivatedRoute) {

  }

  ngOnInit(): void {

    const id = this.route.snapshot.paramMap.get('id');
    console.log(`ProductId`, id);
  }



}
