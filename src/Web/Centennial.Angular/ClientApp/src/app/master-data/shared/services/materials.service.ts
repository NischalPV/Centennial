import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

import { DataService } from '../../../shared/services/data.service';
import { SecurityService } from '../../../shared/services/security.service';
import { ConfigurationService } from '../../../shared/services/configuration.service';

import { IMaterial } from '../../../shared/interfaces/material.model';

@Injectable({
  providedIn: 'root'
})
export class MaterialsService {
  private webApiUrl = "";
  private apiVersion = "";

  constructor(private dataService: DataService, private identityService: SecurityService, private configurationService: ConfigurationService) {
    if (this.configurationService.isReady) {
      this.webApiUrl = this.configurationService.serverSettings.webApiUrl;
      this.apiVersion = this.configurationService.serverSettings.apiVersion;
    }

    else {
      this.configurationService.settingsLoaded$.subscribe(x => {
        this.webApiUrl = this.configurationService.serverSettings.webApiUrl;
        this.apiVersion = this.configurationService.serverSettings.apiVersion;
      });

    }
  }

  getMaterials(): Observable<IMaterial[]> {
    let url = `${this.webApiUrl}/api/${this.apiVersion}/materials/`;
    return this.dataService.get(url).pipe<IMaterial[]>(tap((res: any) => { return res; }));
  }

  getMaterial(id: string): Observable<IMaterial> {
    let url = `${this.webApiUrl}/api/${this.apiVersion}/materials/${id}`;
    return this.dataService.get(url).pipe<IMaterial>(tap((res: any) => { return res; }));
  }

  postMaterial(material: IMaterial): Observable<IMaterial> {
    let url = `${this.webApiUrl}/api/${this.apiVersion}/materials/`;
    return this.dataService.post(url, material).pipe<IMaterial>(tap((res: any) => { return res; }));
  }

  deleteMaterial(id: string): Observable<any> {
    let url = `${this.webApiUrl}/api/${this.apiVersion}/materials/${id}`;
    return this.dataService.delete(url).pipe(tap((res: any) => {
      return res;
    }));
  }
}
