import { Component, OnInit } from '@angular/core';
import { ComponentPageTitle } from '../shared/classes/component-page-title';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(public _componentPageTitle: ComponentPageTitle) { }

  ngOnInit(): void {

    this._componentPageTitle.title = "Home";
  }

}
