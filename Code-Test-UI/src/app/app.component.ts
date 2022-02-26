import { Component, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass'],
  providers: []
})
export class AppComponent {
  public title : string = 'Code-Test-UI';

  public minDate : Date;
  public maxDate : Date;
  public dobValue : Date;

  // TODO: dynamically read in via supporting service
  public occupations = [
    { name: 'cleaner' },
    { name: 'doctor' },
    { name: 'author' },
    { name: 'farmer' },
    { name: 'mechanic' },
    { name: 'florist' }
  ]
  
  public modalRef: BsModalRef = new BsModalRef(); 

  constructor(private modalService: BsModalService) {
    var currentDate : Date = new Date();

    this.minDate = new Date(currentDate.getFullYear() - 100, currentDate.getMonth(), currentDate.getDate());
    this.maxDate = new Date(currentDate.getFullYear() - 18, currentDate.getMonth(), currentDate.getDate());
    this.dobValue = this.maxDate;
  }

  public openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

}
