import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { Subscription } from 'rxjs';
import { Title } from '@angular/platform-browser';

import { SecurityService } from './shared/services/security.service';
import { ConfigurationService } from './shared/services/configuration.service';
import { ComponentPageTitle } from './shared/classes/component-page-title';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  title = 'app';

  Authenticated: boolean = false;
  subscription: Subscription;

  constructor(private titleService: Title, private securityService: SecurityService, private configurationService: ConfigurationService, vcr: ViewContainerRef, public _componentPageTitle: ComponentPageTitle) {
    this.Authenticated = this.securityService.IsAuthorized;
  }

  ngOnInit() {
    console.log('app on init');
    this.subscription = this.securityService.authenticationChallenge$.subscribe(res => this.Authenticated = res);
    console.log('configuration');
    this.configurationService.load();
    this._componentPageTitle.title = "Login to continue..."
  }
  public setTitle(newTitle: string) {
    this.titleService.setTitle(`${newTitle} | Centennial`);

  }

  login() {
    this.securityService.Authorize();
  }
}
