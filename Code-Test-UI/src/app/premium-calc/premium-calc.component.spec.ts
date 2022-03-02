import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { FormsModule } from '@angular/forms';

import { PremiumCalcComponent } from './premium-calc.component';
import { Customer } from './premium-calc.model.customer';

describe('PremiumCalcComponent', () => {
  let component: PremiumCalcComponent;
  let fixture: ComponentFixture<PremiumCalcComponent>;
  let compatibleAge: number;
  let compatibleBirthDate: Date;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        FormsModule,
        HttpClientTestingModule
      ],
      declarations: [ PremiumCalcComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PremiumCalcComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();

    compatibleBirthDate = new Date(1983, 3, 2);
    var ageDifMs = Date.now() - compatibleBirthDate.getTime();
    var ageDate = new Date(ageDifMs); 
    compatibleAge = Math.abs(ageDate.getUTCFullYear() - 1970) 

  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should not error if Name is alphanumeric', () => {
    component.customerInstance = new Customer( 'Kevin Purnama', compatibleAge, compatibleBirthDate, 'Doctor', 10000 );
    var errors: string [] = component.customerInstance.validate();
    expect(errors.length).toBe(0);
  });

  it('should error if Name contains numbers', () => {
    component.customerInstance = new Customer( 'K3vin Purnama', compatibleAge, compatibleBirthDate, 'Doctor', 10000 );
    var errors: string [] = component.customerInstance.validate();
    expect(errors.length).toBe(1);
    expect(errors[0]).toBe('Name cannot legally contain numbers.');
  });

  it('should not error if Age is greater than 110', () => {
    component.customerInstance = new Customer( 'Test Age range', 115, compatibleBirthDate, 'Doctor', 10000 );
    var errors: string [] = component.customerInstance.validate();
    expect(errors.length).toBe(2);
    expect(errors[0]).toBe('Age cannot be negative or zero, or greater than 110.');
    expect(errors[1]).toBe('Date of Birth does not match Age.');
  });

  it('should error if Age is negative', () => {
    component.customerInstance = new Customer( 'Test Age range', -1, compatibleBirthDate, 'Doctor', 10000 );
    var errors: string [] = component.customerInstance.validate();
    expect(errors.length).toBe(2);
    expect(errors[0]).toBe('Age cannot be negative or zero, or greater than 110.');
    expect(errors[1]).toBe('Date of Birth does not match Age.');
  });

  it('should not error if Age is within range', () => {
    component.customerInstance = new Customer( 'Test Age range', compatibleAge, compatibleBirthDate, 'Doctor', 10000 );
    var errors: string [] = component.customerInstance.validate();
    expect(errors.length).toBe(0);
  });

  it('should error if DOB is future date', () => {
    component.customerInstance = new Customer( 'Test future DOB', 18, new Date(2023, 3, 2), 'Doctor', 10000 );
    var errors: string [] = component.customerInstance.validate();
    expect(errors.length).toBe(2);
    expect(errors[0]).toBe('Date of Birth does not match Age.');
    expect(errors[1]).toBe('Date of Birth is in the future.');
  });

  it('should error if DOB does not match age', () => {
    component.customerInstance = new Customer( 'Test Age matches DOB', 25, new Date(1983, 3, 2), 'Doctor', 10000 );
    var errors: string [] = component.customerInstance.validate();
    expect(errors.length).toBe(1);
    expect(errors[0]).toBe('Date of Birth does not match Age.');
  });
});
