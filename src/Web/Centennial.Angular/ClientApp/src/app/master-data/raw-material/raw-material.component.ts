import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError, filter } from 'rxjs/operators';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';

import { IRawMaterial } from '../../shared/interfaces/raw-material.model';
import { IMaterial } from '../../shared/interfaces/material.model';
import { RawMaterialService } from '../shared/services/raw-material.service';
import { MaterialsService } from '../shared/services/materials.service';
import { ConfigurationService } from '../../shared/services/configuration.service';

import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { ComponentPageTitle } from '../../shared/classes/component-page-title';



@Component({
  selector: 'app-raw-material',
  templateUrl: './raw-material.component.html',
  styleUrls: ['./raw-material.component.css'],
  host: { 'class': 'w-100 mt-3' }
})
export class RawMaterialComponent implements OnInit {

  public rawMaterials: IRawMaterial[];
  public materials: IMaterial[];
  errorReceived: boolean;
  readonly formControl: AbstractControl;
  public displayedColumns: string[] = ['sn', 'name', 'size', 'material', 'createdDate', 'action'];
  public datasource = new MatTableDataSource<IRawMaterial>();

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;

  constructor(private service: RawMaterialService, private materialService: MaterialsService, private configurationService: ConfigurationService, private formBuilder: FormBuilder, public _componentPageTitle: ComponentPageTitle) { }

  public createRawMaterialForm = this.formBuilder.group({
    name: ['', Validators.required],
    size: ['', Validators.required],
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

    this._componentPageTitle.title = "Raw materials";

    this.datasource.paginator = this.paginator;
    this.datasource.sort = this.sort;

  }

  public onSubmit(): void {
    //console.log(this.createRawMaterialForm.value);
    this.postRawMaterial(this.createRawMaterialForm.value);
    this.createRawMaterialForm.reset();
  }

  public applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.datasource.filter = filterValue.trim().toLowerCase();

    if (this.datasource.paginator) {
      this.datasource.paginator.firstPage();
    }
  }

  public deleteRawMaterial(id: string) {
    this.errorReceived = false;
    this.service.deleteRawMaterial(id)
      .pipe(catchError((err) => this.handleError(err)))
      .subscribe((res) => {
        console.log("raw material deleted: " + id);
        this.loadData();
      });
  }

  private postRawMaterial(rawMaterial: IRawMaterial) {
    this.errorReceived = false;
    this.service.postRawMaterial(rawMaterial)
      .pipe(catchError((err) => this.handleError(err)))
      .subscribe(rmat => {
        console.log("raw material saved!", rmat);
        this.loadData();
      });
  }

  private loadData() {
    //let url = this._baseUrl + 'api/devices/values';
    this.errorReceived = false;

    this.service.getRawMaterials()
      .pipe(catchError((err) => this.handleError(err)))
      .subscribe(rmats => {
        this.rawMaterials = rmats;
        this.datasource.data = rmats;
        this.datasource.paginator = this.paginator;
        this.datasource.sort = this.sort;
        console.log("raw materials received: " + this.rawMaterials.length);
        //console.log("raw materials received", this.rawMaterials);
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
