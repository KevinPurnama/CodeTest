import { Component, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass'],
  providers: []
})
export class AppComponent {

  public modalRef: BsModalRef = new BsModalRef(); 

  public title : string = 'Code-Test-UI';

  constructor(private modalService: BsModalService) {

  }

  // "openModal(template)"
  public openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

}
