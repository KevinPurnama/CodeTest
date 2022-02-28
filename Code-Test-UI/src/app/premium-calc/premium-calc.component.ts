import { Component, OnInit, Input } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';

import { PremiumCalcApiService } from '../services/premium-calc-api.service';

import { Customer } from './premium-calc.model.customer';
import { MonthlyPremiumResponse } from './premium-calc.model.monthlyPremium';

@Component({
  selector: 'app-premium-calc',
  templateUrl: './premium-calc.component.html',
  styleUrls: ['./premium-calc.component.sass'],
  providers: [ PremiumCalcApiService ]
})
export class PremiumCalcComponent implements OnInit {

  @Input() modalFunction?: (premium: number, errors: string[]) => void;

  public premiumCalcForm?: NgForm;

  public customerInstance: Customer;
  public errorMessages: string [];
  public calculatedPremium: number;

  public minDate : Date;
  public maxDate : Date;
  //public dobValue : Date;

  // TODO: dynamically read in via supporting service
  public occupations = [
    'cleaner',
    'doctor',
    'author' ,
    'farmer',
    'mechanic',
    'florrist'
  ]
  
  constructor(private _premiumCalcApiService: PremiumCalcApiService) { 
    this.customerInstance = new Customer();
    this.errorMessages = [];
    this.calculatedPremium = 0;

    var currentDate : Date = new Date();

    this.minDate = new Date(currentDate.getFullYear() - 100, currentDate.getMonth(), currentDate.getDate());
    this.maxDate = new Date(currentDate.getFullYear() - 18, currentDate.getMonth(), currentDate.getDate());
  }

  ngOnInit(): void {
    this.occupations = this._premiumCalcApiService.getListOfOccupations();
  }

  onSubmit(input: NgForm) {
    this.errorMessages = [];
    this.calculatedPremium = 0;
    if (input.valid) {
      var errors: string [] = this.customerInstance.validate();
      if (errors.length == 0) {
        // calculate premium
        this._premiumCalcApiService.calculatePremiumForCustomer(this.customerInstance).subscribe({
          next: (monthlyPremium) => {
            if (monthlyPremium.premium) {
              this.calculatedPremium = monthlyPremium.premium;
              if (this.modalFunction) {
                this.modalFunction(this.calculatedPremium, []);
              }
            } else {
              if (this.modalFunction) {
                this.modalFunction(0, ["Server failed to calculate and return a monthly premium."]);
              }
            }
          },
          error: (e) => {
            this.errorMessages.push("Error calculating premium: " + e)
            if (this.modalFunction) {
              this.modalFunction(0, this.errorMessages);
              this.customerInstance.occupation = 'null';
            }
          }
        });
      }
      else 
      {
        // bubble up validation errors
        this.errorMessages = errors;
      }
    } else {
      this.errorMessages = [
        "Please enter all mandatory values."
      ]
    }
    if (this.errorMessages.length > 0 && this.modalFunction) {
      this.modalFunction(0, this.errorMessages);
      this.customerInstance.occupation = 'null';
    }
  }

}
