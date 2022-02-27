import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';

import { PremiumCalcApiService } from '../services/premium-calc-api.service';

import { Customer } from './premium-calc.model.customer';

@Component({
  selector: 'app-premium-calc',
  templateUrl: './premium-calc.component.html',
  styleUrls: ['./premium-calc.component.sass'],
  providers: [ PremiumCalcApiService ]
})
export class PremiumCalcComponent implements OnInit {

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
    'mechhanic',
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
    if (input.valid) {
      this.errorMessages = [];
      var errors: string [] = this.customerInstance.validate();
      if (errors.length == 0) {
        // calculate premium
        var premium = this._premiumCalcApiService.calculatePremiumForCustomer(this.customerInstance);
      }
      else 
      {
        // bubble up errors
        this.errorMessages = errors;
      }
    } else {
      this.errorMessages = [
        "Please enter all mandatory values."
      ]
    }
  }

}
