import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

import { DataService } from '../../../shared/services/data.service';
import { SecurityService } from '../../../shared/services/security.service';
import { ConfigurationService } from '../../../shared/services/configuration.service';

import { IProduct } from '../../../shared/interfaces/product.model';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
  private webApiUrl = "";

  constructor(private dataService: DataService, private identityService: SecurityService, private configurationService: ConfigurationService) {
    if (this.configurationService.isReady)
      this.webApiUrl = this.configurationService.serverSettings.webApiUrl;
    else
      this.configurationService.settingsLoaded$.subscribe(x => this.webApiUrl = this.configurationService.serverSettings.webApiUrl)
  }

  getProducts(): Observable<IProduct[]> {
    let url = this.webApiUrl + '/api/v1/products';
    return this.dataService.get(url).pipe<IProduct[]>(tap((res: any) => { return res; }));
  }

  getProduct(id: string): Observable<IProduct> {
    let url = this.webApiUrl + '/api/v1/products/' + id;
    return this.dataService.get(url).pipe<IProduct>(tap((res: any) => { return res; }));
  }

  postProduct(product: IProduct): Observable<IProduct> {
    let url = this.webApiUrl + '/api/v1/products';
    return this.dataService.post(url, product).pipe<IProduct>(tap((res: any) => { return res; }));
  }

  deleteProduct(id: string): Observable<any> {
    let url = this.webApiUrl + '/api/v1/products/' + id;
    return this.dataService.delete(url).pipe(tap((res: any) => {
      return res;
    }));
  }
}
