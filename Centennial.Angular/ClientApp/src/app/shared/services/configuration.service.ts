import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from "@angular/common/http";
import { IConfiguration } from '../interfaces/configuration.model';
import { StorageService } from '../services/storage.service';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ConfigurationService {

  serverSettings: IConfiguration;
  private settingsLoadedSource = new Subject();
  settingsLoaded$ = this.settingsLoadedSource.asObservable();
  isReady: boolean = false;

  constructor(private http: HttpClient, private storageService: StorageService) { }

  load() {
    const baseURI = document.baseURI.endsWith('/') ? document.baseURI : `${document.baseURI}/`;

    let url = `${baseURI}Home/Configuration`;

    this.http.get(url).subscribe((response) => {
      console.log('server settings loaded');
      this.serverSettings = response as IConfiguration;
      console.log(this.serverSettings);
      this.storageService.store('identityUrl', this.serverSettings.identityUrl);
      this.storageService.store('webApiUrl', this.serverSettings.webApiUrl);
      this.storageService.store('apiVersion', this.serverSettings.apiVersion);
      this.isReady = true;
      this.settingsLoadedSource.next();
    });
  }
}
