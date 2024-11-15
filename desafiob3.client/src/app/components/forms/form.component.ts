import { ChangeDetectionStrategy, Component, signal } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { FormBuilder, FormGroup, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormCalculation } from './form.model';
import { CalculateAmountService } from '../../services/calculate.service';
import { Observable } from 'rxjs';
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
  formGroup: FormGroup;
  post: FormCalculation | undefined;

  // Inicializando calculationResult$ com um Observable vazio
  calculationResult$: Observable<CalculationResponse> = new Observable();

  constructor(private formBuilder: FormBuilder, private calculateService: CalculateAmountService) {
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

  // Lógica para o envio do formulário
  onSubmit(post: FormCalculation) {
    this.post = post;

    // Atribuindo o Observable que será assinado pelo async pipe no template
    this.calculationResult$ = this.calculateService.calculateAmount(post);
  }
}
