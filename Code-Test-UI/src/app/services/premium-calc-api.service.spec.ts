import { TestBed } from '@angular/core/testing';
import { Customer } from '../premium-calc/premium-calc.model.customer';

import { PremiumCalcApiService } from './premium-calc-api.service';

describe('PremiumCalcApiService', () => {
  let service: PremiumCalcApiService;
  let compatibleAge: number;
  let compatibleBirthDate: Date;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PremiumCalcApiService);

    compatibleBirthDate = new Date(1983, 3, 2);
    var ageDifMs = Date.now() - compatibleBirthDate.getTime();
    var ageDate = new Date(ageDifMs); 
    compatibleAge = Math.abs(ageDate.getUTCFullYear() - 1970) 
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
/* */
  it('should calculate premium for author with 10,000 death benefit', () => {
    var result: number = service.calculatePremiumForCustomer(new Customer("Kevin", compatibleAge, compatibleBirthDate, "author", 10000 ));
    expect(result.toFixed(0)).toBe((1.04166667*compatibleAge).toFixed(0));
  });

  it('should calculate premium for 40 year old doctor with 20,000 death benefit', () => {
    var currentDate: Date = new Date();
    var birthDate: Date = new Date(currentDate.getFullYear() - 40, currentDate.getMonth() - 1, currentDate.getDay());
    var result: number = service.calculatePremiumForCustomer(new Customer("Kevin", 40, birthDate, "doctor", 20000 ));
    expect(result).toBe(1.04166667*compatibleAge);
  });
//*/
});
