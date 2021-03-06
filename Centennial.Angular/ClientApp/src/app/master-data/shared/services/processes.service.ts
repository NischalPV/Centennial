import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

import { DataService } from '../../../shared/services/data.service';
import { SecurityService } from '../../../shared/services/security.service';
import { ConfigurationService } from '../../../shared/services/configuration.service';

import { IProcess } from '../../../shared/interfaces/process.model';

@Injectable({
  providedIn: 'root'
})
export class ProcessesService {

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

  getProcesses(): Observable<IProcess[]> {
    let url = `${this.webApiUrl}/api/${this.apiVersion}/processes/`;
    return this.dataService.get(url).pipe<IProcess[]>(tap((res: any) => { return res; }));
  }

  getProcess(id: number): Observable<IProcess[]> {
    let url = `${this.webApiUrl}/api/${this.apiVersion}/processes/${id}`;
    return this.dataService.get(url).pipe<IProcess[]>(tap((res: any) => { return res; }));
  }

  postProcess(process: IProcess): Observable<IProcess> {
    let url = `${this.webApiUrl}/api/${this.apiVersion}/processes/`;
    return this.dataService.post(url, process).pipe<IProcess>(tap((res: any) => { return res; }));
  }

  deleteProcess(id: number): Observable<any> {
    let url = `${this.webApiUrl}/api/${this.apiVersion}/processes/${id}`;
    return this.dataService.delete(url).pipe(tap((res: any) => {
      return res;
    }));
  }
}
