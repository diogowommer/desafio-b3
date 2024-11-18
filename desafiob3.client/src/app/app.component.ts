import { Component } from '@angular/core';
import { FormComponent } from './components/forms/form.component';
import { NgFor, NgIf } from '@angular/common';
import { MatCardModule } from '@angular/material/card';

@Component({
  standalone: true,
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  imports: [FormComponent, NgFor, NgIf, MatCardModule]
})
export class AppComponent {
}
