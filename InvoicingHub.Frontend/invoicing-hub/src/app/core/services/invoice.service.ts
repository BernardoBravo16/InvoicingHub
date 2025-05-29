import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { InvoiceRequest } from '../models/invoice.model';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class InvoiceService {
  private readonly api = 'https://localhost:7027/api/Invoice';

  constructor(private http: HttpClient) {}

  createInvoice(request: InvoiceRequest): Observable<any> {
    return this.http.post(`${this.api}/create`, request);
  }

  getInvoicesByCustomer(customerId: number): Observable<any> {
  return this.http.get<any>(`${this.api}/by-customer/${customerId}`);
}

  getInvoice(invoiceNumber: string): Observable<any> {
    return this.http.get<any>(`${this.api}/by-number/${invoiceNumber}`);
  }
}
