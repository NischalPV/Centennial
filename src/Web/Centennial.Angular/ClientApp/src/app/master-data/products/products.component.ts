import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError, filter } from 'rxjs/operators';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';

import { IProduct } from '../../shared/interfaces/product.model';
import { IMaterial } from '../../shared/interfaces/material.model';
import { ProductsService } from '../shared/services/products.service';
import { MaterialsService } from '../shared/services/materials.service';
import { ConfigurationService } from '../../shared/services/configuration.service';

import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { ComponentPageTitle } from '../../shared/classes/component-page-title';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  public products: IProduct[];
  public materials: IMaterial[];
  errorReceived: boolean;
  readonly formControl: AbstractControl;
  public displayedColumns: string[] = ['sn', 'name', 'dimensions', 'price', 'uniqueIdentifier', 'material', 'createdDate', 'updatedDate', 'action'];
  public datasource = new MatTableDataSource<IProduct>();

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  constructor(private service: ProductsService, private materialService: MaterialsService, private configurationService: ConfigurationService, private formBuilder: FormBuilder, public _componentPageTitle: ComponentPageTitle) { }

  public createProductForm = this.formBuilder.group({
    name: ['', Validators.required],
    dimensions: ['', Validators.required],
    price: ['', Validators.required],
    materialId: ['', Validators.required],
  })

  ngOnInit(): void {

    if (this.configurationService.isReady) {
      this.loadData();

    }
    else {
      this.configurationService.settingsLoaded$.subscribe(x => {
        this.loadData();
      });
    }

    this._componentPageTitle.title = "Products";

    this.datasource.paginator = this.paginator;
    this.datasource.sort = this.sort;
  }

  public onSubmit(): void {
    console.log(this.createProductForm.value);
    this.postProduct(this.createProductForm.value);
    this.createProductForm.reset();
  }

  public applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.datasource.filter = filterValue.trim().toLowerCase();

    if (this.datasource.paginator) {
      this.datasource.paginator.firstPage();
    }
  }

  public deleteProduct(id: string) {
    this.errorReceived = false;
    this.service.deleteProduct(id)
      .pipe(catchError((err) => this.handleError(err)))
      .subscribe((res) => {
        console.log("product deleted: " + id);
        this.loadData();
      });
  }

  private postProduct(product: IProduct) {
    this.errorReceived = false;
    this.service.postProduct(product)
      .pipe(catchError((err) => this.handleError(err)))
      .subscribe(prod => {
        console.log("product saved!", prod);
        this.loadData();
      });
  }

  private loadData() {
    //let url = this._baseUrl + 'api/devices/values';
    this.errorReceived = false;

    this.service.getProducts()
      .pipe(catchError((err) => this.handleError(err)))
      .subscribe(prods => {
        this.products = prods;
        this.datasource.data = prods;
        this.datasource.paginator = this.paginator;
        this.datasource.sort = this.sort;
        console.log("products received: " + this.products.length);
      });

    this.materialService.getMaterials()
      .pipe(catchError((err) => this.handleError(err)))
      .subscribe(mats => {
        this.materials = mats;
        console.log("materials received: " + this.materials.length);
      });
  }

  private handleError(error: any) {
    this.errorReceived = true;
    return Observable.throw(error);
  }

}
