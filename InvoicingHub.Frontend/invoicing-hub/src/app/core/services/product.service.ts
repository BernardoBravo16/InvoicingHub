import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../models/product.model';
import { map } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class ProductService {
  private readonly api = 'https://localhost:7027/api/Product';

  constructor(private http: HttpClient) {}

    getAll(): Observable<Product[]> {
    return this.http.get<{ data: Product[] }>(`${this.api}/products`).pipe(
        map((response) => response.data)
    );
    }
}
