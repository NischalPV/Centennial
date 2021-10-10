import { Component } from '@angular/core';
import { SecurityService } from '../shared/services/security.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  Authenticated: boolean = false;

  constructor(private securityService: SecurityService) {
    this.Authenticated = this.securityService.IsAuthorized;
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
