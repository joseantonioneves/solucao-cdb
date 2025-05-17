import { Component } from '@angular/core';
import { CdbCalculatorComponent } from './Cdb-Calculator/cdb-calculator.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CdbCalculatorComponent],
  template: `
    <h1>Simulador de CDB</h1>
    <app-cdb-calculator></app-cdb-calculator>
  `,
  styles: []
})
export class AppComponent {}