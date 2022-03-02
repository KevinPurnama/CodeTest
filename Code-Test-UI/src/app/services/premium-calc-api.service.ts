import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

import { Customer } from '../premium-calc/premium-calc.model.customer';
import { GetOccupationsResponse } from '../premium-calc/premium-calc.model.getOccupationsResponse';
import { MonthlyPremiumResponse } from '../premium-calc/premium-calc.model.monthlyPremiumResponse';

import { environment } from '../../environments/environment';

// 'application/x-www-form-urlencoded; charset=UTF-8'
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json' 
  })
};

@Injectable({
  providedIn: 'root'
})
export class PremiumCalcApiService {

  serviceURL: string = environment.apiEndpoint.concat(`PremiumCalc`);

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
    /*
    return this._httpClient.post<MonthlyPremiumResponse>( this.serviceURL, { 
      body: JSON.stringify(customer), 
      httpOptions: httpOptions
    }).pipe(
      catchError(this.handleError)
    );
    */
    return this._httpClient.post( this.serviceURL, customer
    ).pipe(
      catchError(this.handleError)
    );

  }

  public getListOfOccupations(): Observable<GetOccupationsResponse> {  
    return this._httpClient.get<GetOccupationsResponse>( this.serviceURL).pipe(
      catchError(this.handleError)
    );
  }

}
