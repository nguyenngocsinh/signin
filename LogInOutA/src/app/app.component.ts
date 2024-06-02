import { Component } from '@angular/core';
import { LocalService } from './service/local.service';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/compiler';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'LogInOutA';
}
