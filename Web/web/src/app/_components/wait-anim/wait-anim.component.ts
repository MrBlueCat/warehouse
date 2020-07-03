import { Component} from '@angular/core';

@Component({
  selector: 'app-wait-anim',
  templateUrl: './wait-anim.component.html',
  styleUrls: ['./wait-anim.component.css']
})
export class WaitAnimComponent {
  isProcessing: boolean;
  constructor() { }

}
