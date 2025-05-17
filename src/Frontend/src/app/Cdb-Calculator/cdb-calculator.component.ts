import { Component } from '@angular/core';
import { CommonModule, NgIf } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { InputNumberModule } from 'primeng/inputnumber';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';

@Component({
  selector: 'app-cdb-calculator',
  standalone: true,
  imports: [
    CommonModule,
    NgIf,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    InputNumberModule,
    ButtonModule,
    CardModule
  ],
  templateUrl: './cdb-calculator.component.html',
  styleUrls: ['./cdb-calculator.component.scss']
})
export class CdbCalculatorComponent {
  initialAmount: number = 1000;
  months: number = 12;
  result: any = null;

  constructor(private http: HttpClient) {}

  calculate(): void {
    const body = {
      initialAmount: this.initialAmount,
      months: this.months
    };

    this.http.post('https://localhost:8443/api/Cdb/calculate', body)
      .subscribe({
        next: data => this.result = data,
        error: err => {
          console.error('Erro ao calcular CDB:', err);
          this.result = null;
        }
      });
  }
}