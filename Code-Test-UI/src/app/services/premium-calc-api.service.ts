import { Injectable } from '@angular/core';

import { Customer } from '../premium-calc/premium-calc.model.customer';

import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PremiumCalcApiService {

  serviceURL: string = environment.apiEndpoint.concat(`premiumCalc`);

  constructor() { }

  // TODO: finish wiring up to backend
  public calculatePremiumForCustomer(customer: Customer): number {
    return 0;
  }

  public getListOfOccupations(): string [] {
    var result: string [] = [
      'cleaner',
      'doctor',
      'author' ,
      'farmer',
      'mechanic',
      'florist'
    ];
    return result;

  }

}
