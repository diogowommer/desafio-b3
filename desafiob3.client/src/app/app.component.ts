import { Component } from '@angular/core';
import { FormComponent } from './components/forms/form.component';
import { NgFor, NgIf } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  imports: [FormComponent, NgFor, NgIf]
})
export class AppComponent {
}
