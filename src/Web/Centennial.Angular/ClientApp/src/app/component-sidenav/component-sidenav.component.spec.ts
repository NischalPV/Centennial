import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ComponentSidenav} from './component-sidenav.component';

describe('ComponentSidenavComponent', () => {
  let component: ComponentSidenav;
  let fixture: ComponentFixture<ComponentSidenav>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ComponentSidenav ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ComponentSidenav);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
