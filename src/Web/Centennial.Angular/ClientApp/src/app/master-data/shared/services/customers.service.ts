import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

import { DataService } from '../../../shared/services/data.service';
import { SecurityService } from '../../../shared/services/security.service';
import { ConfigurationService } from '../../../shared/services/configuration.service';

import { ICustomer } from '../../../shared/interfaces/customer.model';

@Injectable({
  providedIn: 'root'
})
export class CustomersService {
  private apiVersion = "";
  private webApiUrl = "";

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

  getCustomers(): Observable<ICustomer[]> {
    let url = `${this.webApiUrl}/api/${this.apiVersion}/customers/`;
    return this.dataService.get(url).pipe<ICustomer[]>(tap((res: any) => { return res; }));
  }

  getCustomer(id: string): Observable<ICustomer> {
    let url = `${this.webApiUrl}/api/${this.apiVersion}/customers/${id}`;
    return this.dataService.get(url).pipe<ICustomer>(tap((res: any) => { return res; }));
  }

  postCustomer(customer: ICustomer): Observable<ICustomer> {
    let url = `${this.webApiUrl}/api/${this.apiVersion}/customers/`;
    return this.dataService.post(url, customer).pipe<ICustomer>(tap((res: any) => { return res; }));
  }

  deleteCustomer(id: string): Observable<any> {
    let url = `${this.webApiUrl}/api/${this.apiVersion}/customers/${id}`;
    return this.dataService.delete(url).pipe(tap((res: any) => {
      return res;
    }));
  }
}
