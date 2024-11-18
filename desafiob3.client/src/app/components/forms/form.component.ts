import { ChangeDetectionStrategy, Component, signal } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { FormBuilder, FormGroup, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormCalculation } from './form.model';
import { CalculateAmountService } from '../../services/calculate.service';
import { Observable, catchError, of } from 'rxjs';
import { CalculationResponse } from '../../services/calculate.model';
import { CommonModule, NgIf } from '@angular/common';

/** @title Form field with prefix & suffix */
@Component({
  selector: 'form-component',
  templateUrl: 'form.component.html',
  styleUrls: ['form.component.css'],
  standalone: true,
  imports: [MatFormFieldModule, MatInputModule, MatButtonModule, MatIconModule, FormsModule, ReactiveFormsModule, NgIf, CommonModule],
  providers: [CalculateAmountService],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class FormComponent {
  [x: string]: any;
  formGroup: FormGroup;
  post: FormCalculation | undefined;
  calculationResult$: Observable<CalculationResponse> = new Observable();

  constructor(private readonly formBuilder: FormBuilder, private readonly calculateService: CalculateAmountService) {
    this.formGroup = this.formBuilder.group({
      'initialvalue': [null, Validators.required],
      'quantity': [null, Validators.required],
    });
  }

  hide = signal(true);

  clickEvent(event: MouseEvent) {
    this.hide.set(!this.hide());
    event.stopPropagation();
  }

  onSubmit(post: FormCalculation) {
    this.post = post;

    this.calculationResult$ = this.calculateService.calculateAmount(post).pipe(
      catchError(error => {
        return of(error);
      })
    );
  }
}
