import { TestBed, inject } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { Customer } from '../premium-calc/premium-calc.model.customer';
import { MonthlyPremiumResponse } from '../premium-calc/premium-calc.model.monthlyPremium';

import { PremiumCalcApiService } from './premium-calc-api.service';

import { environment } from '../../environments/environment';

describe('PremiumCalcApiService', () => {
  //let service: PremiumCalcApiService;
  let compatibleAge: number;
  let compatibleBirthDate: Date;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [PremiumCalcApiService]
    });
    //service = TestBed.inject(PremiumCalcApiService);

    compatibleBirthDate = new Date(1983, 3, 2);
    var ageDifMs = Date.now() - compatibleBirthDate.getTime();
    var ageDate = new Date(ageDifMs); 
    compatibleAge = Math.abs(ageDate.getUTCFullYear() - 1970) 
  });

  afterEach(inject([HttpTestingController], (httpMock: HttpTestingController) => {
    httpMock.verify();
  }));

  it('should calculate premium for author with 10,000 death benefit', 
  inject([HttpTestingController, PremiumCalcApiService],
  (httpMock: HttpTestingController, service: PremiumCalcApiService) => {
    service.calculatePremiumForCustomer(new Customer("Kevin", compatibleAge, compatibleBirthDate, "author", 10000 ))
    .subscribe({
      next: (monthlyPremium: MonthlyPremiumResponse) => {
        var premium: number 
        if (monthlyPremium.premium) {
          premium = monthlyPremium.premium;
          expect(premium.toFixed(0)).toBe((1.04166667*compatibleAge).toFixed(0));
        }
      }
    });

    const req = httpMock.expectOne(environment.apiEndpoint + 'calculatePremium');
    expect(req.request.method).toEqual('POST');
    
    const mockResponse = {
      monthlyPremium: 1.04166667*compatibleAge,
      errors: []
    };
    req.flush({data: mockResponse});
  }));

  it('should calculate premium for 40 year old doctor with 20,000 death benefit',
  inject([HttpTestingController, PremiumCalcApiService],
  (httpMock: HttpTestingController, service: PremiumCalcApiService) => {
    var currentDate: Date = new Date();
    var birthDate: Date = new Date(currentDate.getFullYear() - 40, currentDate.getMonth() - 1, currentDate.getDay());
    service.calculatePremiumForCustomer(new Customer("Kevin", 40, birthDate, "doctor", 20000 ))
    .subscribe({
      next: (monthlyPremium: MonthlyPremiumResponse) => {
        var premium: number 
        if (monthlyPremium.premium) {
          premium = monthlyPremium.premium;
          expect(premium.toFixed(0)).toBe((1.04166667*40).toFixed(0));
        }
      }
    });

    const req = httpMock.expectOne(environment.apiEndpoint + 'calculatePremium');
    expect(req.request.method).toEqual('POST');
    
    const mockResponse = {
      monthlyPremium: 1.04166667*compatibleAge,
      errors: []
    };
    req.flush({data: mockResponse});
  }));

});
