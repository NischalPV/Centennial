import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ComponentPageHeader } from './component-page-header.component';

describe('ComponentPageHeaderComponent', () => {
  let component: ComponentPageHeader;
  let fixture: ComponentFixture<ComponentPageHeader>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ComponentPageHeader ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ComponentPageHeader);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
