import { Component, EventEmitter, NgModule, OnInit, Output } from '@angular/core';
import { ComponentPageTitle } from '../shared/classes/component-page-title';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'component-page-header',
  templateUrl: './component-page-header.html',
  styleUrls: ['./component-page-header.css']
})
export class ComponentPageHeader implements OnInit {
  constructor(public _componentPageTitle: ComponentPageTitle) { }

  ngOnInit(): void {
  }

  //@Output() toggleSidenav = new EventEmitter<void>();

  getTitle() {
    return this._componentPageTitle.title;
  }
}

