import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError, filter } from 'rxjs/operators';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';

import { IMaterial } from '../../shared/interfaces/material.model';
import { MaterialsService } from '../shared/services/materials.service';
import { ConfigurationService } from '../../shared/services/configuration.service';

import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-materials',
  templateUrl: './materials.component.html',
  styleUrls: ['./materials.component.css']
})
export class MaterialsComponent implements OnInit {

  public materials: IMaterial[];
  errorReceived: boolean;
  readonly formControl: AbstractControl;
  public displayedColumns: string[] = ['sn', 'name', 'createdDate', 'action'];
  public datasource = new MatTableDataSource<IMaterial>();

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  constructor(private service: MaterialsService, private configurationService: ConfigurationService, private formBuilder: FormBuilder) { }

  public createMaterialForm = this.formBuilder.group({
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

    this.datasource.paginator = this.paginator;
    this.datasource.sort = this.sort;
  }

  public onSubmit(): void {
    console.log(this.createMaterialForm.value);
    this.postMaterial(this.createMaterialForm.value);
    this.createMaterialForm.reset();
  }

  public applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.datasource.filter = filterValue.trim().toLowerCase();

    if (this.datasource.paginator) {
      this.datasource.paginator.firstPage();
    }
  }

  public deleteMaterial(id: string) {
    this.errorReceived = false;
    this.service.deleteMaterial(id)
      .pipe(catchError((err) => this.handleError(err)))
      .subscribe((res) => {
        console.log("material deleted: " + id);
        this.loadData();
      });
  }

  private postMaterial(material: IMaterial) {
    this.errorReceived = false;
    this.service.postMaterial(material)
      .pipe(catchError((err) => this.handleError(err)))
      .subscribe(material => {
        console.log("material saved!", material);
        this.loadData();
      });
  }

  private loadData() {
    //let url = this._baseUrl + 'api/devices/values';
    this.errorReceived = false;

    this.service.getMaterials()
      .pipe(catchError((err) => this.handleError(err)))
      .subscribe(mats => {
        this.materials = mats;
        this.datasource.data = mats;
        this.datasource.paginator = this.paginator;
        this.datasource.sort = this.sort;
        console.log("materials received: " + this.materials.length);
      });
  }

  private handleError(error: any) {
    this.errorReceived = true;
    return Observable.throw(error);
  }

}
