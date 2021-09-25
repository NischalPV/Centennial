import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductionProcessesComponent } from './production-processes.component';

describe('ProductionProcessesComponent', () => {
  let component: ProductionProcessesComponent;
  let fixture: ComponentFixture<ProductionProcessesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProductionProcessesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductionProcessesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
