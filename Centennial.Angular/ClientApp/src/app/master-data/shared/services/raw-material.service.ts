import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

import { DataService } from '../../../shared/services/data.service';
import { SecurityService } from '../../../shared/services/security.service';
import { ConfigurationService } from '../../../shared/services/configuration.service';

import { IRawMaterial } from '../../../shared/interfaces/raw-material.model';

@Injectable({
  providedIn: 'root'
})
export class RawMaterialService {

  private webApiUrl = "";

  constructor(private dataService: DataService, private identityService: SecurityService, private configurationService: ConfigurationService) {
    if (this.configurationService.isReady)
      this.webApiUrl = this.configurationService.serverSettings.webApiUrl;
    else
      this.configurationService.settingsLoaded$.subscribe(x => this.webApiUrl = this.configurationService.serverSettings.webApiUrl)
  }

  getRawMaterials(): Observable<IRawMaterial[]> {
    let url = this.webApiUrl + '/api/v1/RawMaterials';
    return this.dataService.get(url).pipe<IRawMaterial[]>(tap((res: any) => { return res; }));
  }

  getRawMaterial(id: string): Observable<IRawMaterial> {
    let url = this.webApiUrl + '/api/v1/RawMaterials/' + id;
    return this.dataService.get(url).pipe<IRawMaterial>(tap((res: any) => { return res; }));
  }

  postRawMaterial(rawMaterial: IRawMaterial) {
    let url = this.webApiUrl + '/api/v1/RawMaterials/';
    return this.dataService.post(url, rawMaterial).pipe<IRawMaterial>(tap((res: any) => { return res; }));
  }

  deleteRawMaterial(id: string): Observable<any> {
    let url = this.webApiUrl + '/api/v1/RawMaterials/' + id;
    return this.dataService.delete(url).pipe(tap((res: any) => {
      return res;
    }));
  }
}
