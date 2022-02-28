import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

import { Customer } from '../premium-calc/premium-calc.model.customer';
import { MonthlyPremiumResponse } from '../premium-calc/premium-calc.model.monthlyPremium';

import { environment } from '../../environments/environment';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class PremiumCalcApiService {

  serviceURL: string = environment.apiEndpoint.concat(`premiumCalc`);

  constructor(private _httpClient: HttpClient) { }

  private handleError(error: HttpErrorResponse) {
    var userErrorMsg: string;
    if (error.status === 0) {
      userErrorMsg = 'Could not connect to server.';
    } else {
      userErrorMsg = 'Internal server error';
    }
    return throwError(() => new Error(userErrorMsg));
  }

  // TODO: finish wiring up to backend
  public calculatePremiumForCustomer(customer: Customer): Observable<MonthlyPremiumResponse> {
    var result: MonthlyPremiumResponse = new MonthlyPremiumResponse();
    result.premium = 0;
    return this._httpClient.post<MonthlyPremiumResponse>( environment.apiEndpoint + 'calculatePremium', { 
      body: JSON.stringify(customer), 
      httpOptions: httpOptions
    }).pipe(
      catchError(this.handleError)
    );
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
