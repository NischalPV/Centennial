import { Component, OnInit, Input } from '@angular/core';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { combineLatest, Observable, Subscription } from 'rxjs';
import { MenuItems } from '../shared/classes/menu-items';
import { ActivatedRoute, Params, RouterModule, Routes } from '@angular/router';

@Component({
  selector: 'app-component-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
  animations: [
    trigger('bodyExpansion', [
      state('collapsed', style({ height: '0px', display: 'none' })),
      state('expanded', style({ height: '*', display: 'block' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4,0.0,0.2,1)')),
    ]),
  ],
})
export class NavComponent {

  @Input() params: Observable<Params> | undefined;
  currentItemId: string | undefined;

  constructor(public menuItems: MenuItems) { }

}
