import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError, filter } from 'rxjs/operators';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';

import { IProcess } from '../../shared/interfaces/process.model';
import { ProcessesService } from '../shared/services/processes.service';
import { ConfigurationService } from '../../shared/services/configuration.service';

import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { ComponentPageTitle } from '../../shared/classes/component-page-title';

@Component({
  selector: 'app-processes',
  templateUrl: './processes.component.html',
  styleUrls: ['./processes.component.css'],
  host: { 'class': 'w-100 mt-3' }
})
export class ProcessesComponent implements OnInit {

  public processes: IProcess[];
  errorReceived: boolean;
  readonly formControl: AbstractControl;
  public displayedColumns: string[] = ['sn', 'name', 'isMandatory', 'isRemovable', 'isOutBound', 'createdDate', 'action'];
  public datasource = new MatTableDataSource<IProcess>();

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;


  constructor(private service: ProcessesService, private configurationService: ConfigurationService, private formBuilder: FormBuilder, public _componentPageTitle: ComponentPageTitle) {
  }

  public createProcessForm = this.formBuilder.group({
    name: ['', Validators.required],
    isMandatory: [false],
    isRemovable: [false],
    isOutBound: [false]

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

    this._componentPageTitle.title = "Processes";

    this.datasource.paginator = this.paginator;
    this.datasource.sort = this.sort;

  }

  onSubmit(): void {
    console.log(this.createProcessForm.value);
    this.postProcess(this.createProcessForm.value);
    this.createProcessForm.reset();
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.datasource.filter = filterValue.trim().toLowerCase();

    if (this.datasource.paginator) {
      this.datasource.paginator.firstPage();
    }
  }

  public deleteProcess(id: number) {
    this.errorReceived = false;
    this.service.deleteProcess(id)
      .pipe(catchError((err) => this.handleError(err)))
      .subscribe((res) => {
        console.log("process deleted: " + id);
        this.loadData();
      });
  }

  private postProcess(process: IProcess) {
    this.errorReceived = false;
    this.service.postProcess(process)
      .pipe(catchError((err) => this.handleError(err)))
      .subscribe(process => {
        console.log("process saved!", process);
        this.loadData();
      });
  }

  private loadData() {
    //let url = this._baseUrl + 'api/devices/values';
    this.errorReceived = false;

    this.service.getProcesses()
      .pipe(catchError((err) => this.handleError(err)))
      .subscribe(procs => {
        this.processes = procs;
        this.datasource.data = procs;
        this.datasource.paginator = this.paginator;
        this.datasource.sort = this.sort;
        console.log("processes received: " + this.processes.length);
      });
  }

  private handleError(error: any) {
    this.errorReceived = true;
    return Observable.throw(error);
  }
}
