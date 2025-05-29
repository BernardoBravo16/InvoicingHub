import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Customer } from '../models/customer.model';
import { map } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class CustomerService {
  private readonly api = 'https://localhost:7027/api/Customer';

  constructor(private http: HttpClient) {}

    getAll(): Observable<Customer[]> {
    return this.http.get<{ data: Customer[] }>(`${this.api}/customers`).pipe(
        map(res => res.data)
    );
    }
}