import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

import { Customer } from '../premium-calc/premium-calc.model.customer';
import { GetOccupationsResponse } from '../premium-calc/premium-calc.model.getOccupationsResponse';
import { MonthlyPremiumResponse } from '../premium-calc/premium-calc.model.monthlyPremiumResponse';

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

  serviceURL: string = environment.apiEndpoint;

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

  public calculatePremiumForCustomer(customer: Customer): Observable<MonthlyPremiumResponse> {
    var result: MonthlyPremiumResponse = new MonthlyPremiumResponse();
    result.premium = 0;
    return this._httpClient.post<MonthlyPremiumResponse>( this.serviceURL.concat(`CalculateMonthlyPremium`), { 
      body: JSON.stringify(customer), 
      httpOptions: httpOptions
    }).pipe(
      catchError(this.handleError)
    );
  }

  public getListOfOccupations(): Observable<GetOccupationsResponse> {  
    return this._httpClient.get<GetOccupationsResponse>( this.serviceURL.concat(`GetOccupations`)).pipe(
      catchError(this.handleError)
    );
/*
    var result: string [] = [
      'cleaner',
      'doctor',
      'author' ,
      'farmer',
      'mechanic',
      'florist'
    ];
    return result;
*/
  }

}
