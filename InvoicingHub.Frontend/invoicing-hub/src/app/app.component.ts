import { Component } from '@angular/core';
import { MatTabsModule } from '@angular/material/tabs';
import { InvoicesCreateComponent } from './pages/invoices/components/invoices-create/invoices-create.component';
import { InvoicesListComponent } from './pages/invoices/components/invoices-list/invoices-list.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    MatTabsModule,
    InvoicesCreateComponent,
    InvoicesListComponent
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {}
