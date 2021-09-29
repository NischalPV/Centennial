import { Component, OnInit, OnChanges, Output, Input, EventEmitter } from '@angular/core';
import { Subscription } from 'rxjs';

import { SecurityService } from '../shared/services/security.service';


@Component({
  selector: 'app-identity',
  templateUrl: './identity.component.html',
  styleUrls: ['./identity.component.css']
})
export class IdentityComponent implements OnInit {

  authenticated: boolean = false;
  private subscription: Subscription;
  public userName: string = '';

  constructor(private service: SecurityService) { }

  ngOnInit() {
    this.subscription = this.service.authenticationChallenge$.subscribe(res => {
      this.authenticated = res;
      this.userName = this.service.UserData.name;
    });

    if (window.location.hash) {
      this.service.AuthorizedCallback();
    }

    console.log('identity component, checking authorized' + this.service.IsAuthorized);
    this.authenticated = this.service.IsAuthorized;

    if (this.authenticated) {
      if (this.service.UserData)
        //console.log('userdata', this.service.UserData);

        this.userName = this.service.UserData.name;
    }
  }

  logoutClicked(event: any) {
    event.preventDefault();
    console.log('Logout clicked');
    this.logout();
  }

  login() {
    this.service.Authorize();
  }

  logout() {
    this.service.Logoff();
  }

}
