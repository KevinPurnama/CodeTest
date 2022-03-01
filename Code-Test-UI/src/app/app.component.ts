import { Template } from '@angular/compiler/src/render3/r3_ast';
import { Component, TemplateRef, ViewChild } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass'],
  providers: []
})
export class AppComponent {

  @ViewChild('modalTemplate', {read: TemplateRef}) modalTemplate?: TemplateRef<any>;

  public modalMsg?: string;
  public modalRef: BsModalRef = new BsModalRef(); 
  public modalFunction = (premium: number, errors: string []): void => {
    this.triggerModal(premium, errors);
  }

  public title : string = 'Code-Test-UI';

  constructor(private modalService: BsModalService) {
  }

  public triggerModal(premium: number, errors: string []) : void {
    if (premium && premium > 0) {
      this.modalMsg = "Your monthly premium is $" + premium;
    } else {
      var errorText = errors.length > 1 ? ' errors encountered.' : ' error encountered.'
      this.modalMsg = errors.length + errorText;
    }
    if (this.modalTemplate) {
      this.openModal(this.modalTemplate);
    }
  };

  public openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

}
