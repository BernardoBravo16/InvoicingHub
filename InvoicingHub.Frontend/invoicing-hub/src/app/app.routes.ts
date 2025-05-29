import { Routes } from '@angular/router';
import { InvoicesCreateComponent } from './pages/invoices/components/invoices-create/invoices-create.component';

export const routes: Routes = [
  { path: '', redirectTo: 'invoices/create', pathMatch: 'full' },
  { path: 'invoices/create', component: InvoicesCreateComponent }
];
