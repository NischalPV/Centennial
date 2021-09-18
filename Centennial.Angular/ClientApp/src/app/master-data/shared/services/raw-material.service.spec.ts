import { TestBed } from '@angular/core/testing';

import { RawMaterialService } from './raw-material.service';

describe('RawMaterialServiceService', () => {
  let service: RawMaterialService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RawMaterialService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
