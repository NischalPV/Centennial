import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError, filter } from 'rxjs/operators';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';

import { ICustomer } from '../../shared/interfaces/customer.model';
import { CustomersService } from '../shared/services/customers.service';
import { ConfigurationService } from '../../shared/services/configuration.service';

import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { ComponentPageTitle } from '../../shared/classes/component-page-title';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css']
})
export class CustomersComponent implements OnInit {

  public customers: ICustomer[];
  errorReceived: boolean;
  readonly formControl: AbstractControl;
  public displayedColumns: string[] = ['sn', 'name', 'phoneNumber', 'createdDate', 'action'];
  public datasource = new MatTableDataSource<ICustomer>();

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  constructor(private service: CustomersService, private configurationService: ConfigurationService, private formBuilder: FormBuilder, public _componentPageTitle: ComponentPageTitle) { }

  public createCustomerForm = this.formBuilder.group({
    name: ['', Validators.required],
    phoneNumber: ['', Validators.required],
    address: ['']
  })

  ngOnInit(): void {

    if (this.configurationService.isReady) {
      this.loadData();

    }
    else {
      this.configurationService.settingsLoaded$.subscribe(x => {
        this.loadData();
      });

      this._componentPageTitle.title = "Customers";
    }

    this.datasource.paginator = this.paginator;
    this.datasource.sort = this.sort;
  }

  public onSubmit(): void {
    console.log(this.createCustomerForm.value);
    this.postCustomer(this.createCustomerForm.value);
    this.createCustomerForm.reset();
  }

  public applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.datasource.filter = filterValue.trim().toLowerCase();

    if (this.datasource.paginator) {
      this.datasource.paginator.firstPage();
    }
  }

  public deleteCustomer(id: string) {
    this.errorReceived = false;
    this.service.deleteCustomer(id)
      .pipe(catchError((err) => this.handleError(err)))
      .subscribe((res) => {
        console.log("customer deleted: " + id);
        this.loadData();
      });
  }

  private postCustomer(customer: ICustomer) {
    this.errorReceived = false;
    this.service.postCustomer(customer)
      .pipe(catchError((err) => this.handleError(err)))
      .subscribe(cust => {
        console.log("customer saved!", cust);
        this.loadData();
      });
  }

  private loadData() {
    //let url = this._baseUrl + 'api/devices/values';
    this.errorReceived = false;

    this.service.getCustomers()
      .pipe(catchError((err) => this.handleError(err)))
      .subscribe(custs => {
        this.customers = custs;
        this.datasource.data = custs;
        this.datasource.paginator = this.paginator;
        this.datasource.sort = this.sort;
        console.log("customers received: " + this.customers.length);
      });
  }

  private handleError(error: any) {
    this.errorReceived = true;
    return Observable.throw(error);
  }

}
