import { TestBed } from '@angular/core/testing';

import { ProductionProcessService } from './production-process.service';

describe('ProductionProcessService', () => {
  let service: ProductionProcessService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProductionProcessService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
