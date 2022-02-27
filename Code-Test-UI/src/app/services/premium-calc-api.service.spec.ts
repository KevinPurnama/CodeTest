import { TestBed } from '@angular/core/testing';

import { PremiumCalcApiService } from './premium-calc-api.service';

describe('PremiumCalcApiService', () => {
  let service: PremiumCalcApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PremiumCalcApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
