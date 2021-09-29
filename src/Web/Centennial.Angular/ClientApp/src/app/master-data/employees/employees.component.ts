import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError, filter } from 'rxjs/operators';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';

import { IEmployee } from '../../shared/interfaces/employee.model';
import { EmployeesService } from '../shared/services/employees.service';
import { ConfigurationService } from '../../shared/services/configuration.service';

import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { ComponentPageTitle } from '../../shared/classes/component-page-title';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css']
})
export class EmployeesComponent implements OnInit {

  public employees: IEmployee[];
  errorReceived: boolean;
  readonly formControl: AbstractControl;
  public displayedColumns: string[] = ['sn', 'name', 'createdDate', 'action'];
  public datasource = new MatTableDataSource<IEmployee>();

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  constructor(private service: EmployeesService, private configurationService: ConfigurationService, private formBuilder: FormBuilder, public _componentPageTitle: ComponentPageTitle) { }

  public createEmployeeForm = this.formBuilder.group({
    name: ['', Validators.required],
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

    this._componentPageTitle.title = "Employees";

    this.datasource.paginator = this.paginator;
    this.datasource.sort = this.sort;
  }

  public onSubmit(): void {
    //console.log(this.createRawMaterialForm.value);
    this.postEmployee(this.createEmployeeForm.value);
    this.createEmployeeForm.reset();
  }

  public applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.datasource.filter = filterValue.trim().toLowerCase();

    if (this.datasource.paginator) {
      this.datasource.paginator.firstPage();
    }
  }

  public deleteEmployee(id: string) {
    this.errorReceived = false;
    this.service.deleteEmployee(id)
      .pipe(catchError((err) => this.handleError(err)))
      .subscribe((res) => {
        console.log("employee deleted: " + id);
        this.loadData();
      });
  }

  private postEmployee(employee: IEmployee) {
    this.errorReceived = false;
    this.service.postEmployee(employee)
      .pipe(catchError((err) => this.handleError(err)))
      .subscribe(rmat => {
        console.log("employee saved!", rmat);
        this.loadData();
      });
  }

  private loadData() {
    //let url = this._baseUrl + 'api/devices/values';
    this.errorReceived = false;

    this.service.getEmployees()
      .pipe(catchError((err) => this.handleError(err)))
      .subscribe(emps => {
        this.employees = emps;
        this.datasource.data = emps;
        this.datasource.paginator = this.paginator;
        this.datasource.sort = this.sort;
        console.log("employees received: " + this.employees.length);
        //console.log("raw materials received", this.rawMaterials);
      });
  }

  private handleError(error: any) {
    this.errorReceived = true;
    return Observable.throw(error);
  }

}
