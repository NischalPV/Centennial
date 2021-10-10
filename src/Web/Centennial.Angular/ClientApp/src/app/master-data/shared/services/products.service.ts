import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

import { DataService } from '../../../shared/services/data.service';
import { SecurityService } from '../../../shared/services/security.service';
import { ConfigurationService } from '../../../shared/services/configuration.service';

import { IProduct } from '../../../shared/interfaces/product.model';
import { IProductionProcess } from '../../../shared/interfaces/production-process.model';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
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

  getProducts(): Observable<IProduct[]> {
    let url = `${this.webApiUrl}/api/${this.apiVersion}/products/`;
    return this.dataService.get(url).pipe<IProduct[]>(tap((res: any) => { return res; }));
  }

  getProduct(id: string): Observable<IProduct> {
    let url = `${this.webApiUrl}/api/${this.apiVersion}/products/${id}`;
    return this.dataService.get(url).pipe<IProduct>(tap((res: any) => { return res; }));
  }

  postProduct(product: IProduct): Observable<IProduct> {
    let url = `${this.webApiUrl}/api/${this.apiVersion}/products/`;
    return this.dataService.post(url, product).pipe<IProduct>(tap((res: any) => { return res; }));
  }

  deleteProduct(id: string): Observable<any> {
    let url = `${this.webApiUrl}/api/${this.apiVersion}/products/${id}`;
    return this.dataService.delete(url).pipe(tap((res: any) => {
      return res;
    }));
  }

  postProductionProcesses(id: string, productionProcesses: IProductionProcess[]): Observable<IProduct> {
    let url = `${this.webApiUrl}/api/${this.apiVersion}/products/${id}`;
    return this.dataService.post(url, productionProcesses).pipe<IProduct>(tap((res: any) => { return res; }));
  }
}
