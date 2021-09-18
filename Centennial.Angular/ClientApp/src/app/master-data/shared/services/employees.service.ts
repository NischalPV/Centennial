import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

import { DataService } from '../../../shared/services/data.service';
import { SecurityService } from '../../../shared/services/security.service';
import { ConfigurationService } from '../../../shared/services/configuration.service';

import { IEmployee } from '../../../shared/interfaces/employee.model';

@Injectable({
  providedIn: 'root'
})
export class EmployeesService {

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

  getEmployees(): Observable<IEmployee[]> {
    let url = `${this.webApiUrl}/api/${this.apiVersion}/Employees/`;
    return this.dataService.get(url).pipe<IEmployee[]>(tap((res: any) => { return res; }));
  }

  getEmployee(id: string): Observable<IEmployee> {
    let url = `${this.webApiUrl}/api/${this.apiVersion}/Employees/${id}`;
    return this.dataService.get(url).pipe<IEmployee>(tap((res: any) => { return res; }));
  }

  postEmployee(employee: IEmployee) {
    let url = `${this.webApiUrl}/api/${this.apiVersion}/Employees/`;
    return this.dataService.post(url, employee).pipe<IEmployee>(tap((res: any) => { return res; }));
  }

  deleteEmployee(id: string): Observable<any> {
    let url = `${this.webApiUrl}/api/${this.apiVersion}/Employees/${id}`;
    return this.dataService.delete(url).pipe(tap((res: any) => {
      return res;
    }));
  }
}
