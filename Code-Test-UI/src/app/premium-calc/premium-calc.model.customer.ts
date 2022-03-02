export class Customer {

    constructor(
        public name?: string,
        public age?: number,
        public dateOfBirth?: Date,
        public occupation?: string,
        public deathBenefit?: number
    ) {}
  
    public clear(): void {
      this.name = '';
      this.age = 0;
      this.dateOfBirth = new Date();
      this.occupation = '';
      this.deathBenefit = 0;
    }
  
    public validate(): string [] {

        var validationErrors = [];
        
        // validate name has no numbers
        if (this.name) {
            if (/\d/.test(this.name)) {
                validationErrors.push('Name cannot legally contain numbers.');
            }
        } else {
            validationErrors.push('Name is mandatory.');
        }

        // validate age is positive
        if (this.age) {
            if (this.age <= 0 || this.age > 110) {
                validationErrors.push('Age cannot be negative or zero, or greater than 110.');
            }
        } else {
            validationErrors.push('Age is mandatory.');
        }

        // validate year and DOB match
        if (this.dateOfBirth) {
            var ageDifMs = Date.now() - this.dateOfBirth.getTime();
            var ageDate = new Date(ageDifMs); 
            if (Math.abs(ageDate.getUTCFullYear() - 1970) != this.age) {
                validationErrors.push('Date of Birth does not match Age.');
            }

            // validate DOB is in the past
            if (this.dateOfBirth > new Date()) {
                validationErrors.push('Date of Birth is in the future.');
            }
        } else {
            validationErrors.push('Date of Birth is mandatory.');
        }

        // validate death benefit is positive
        if (this.deathBenefit) {
            if (this.deathBenefit <= 0) {
                validationErrors.push('Death benefit cannot be negative or zero.');
            }
        } else {
            validationErrors.push('Death Benefit is mandatory.');
        }

        return validationErrors;
    }
  }