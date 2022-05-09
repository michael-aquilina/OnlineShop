import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BuyProductRequest } from 'src/app/shared/models/product.api.buy';
import { Product } from 'src/app/shared/models/product.model';
import { ApiEndpoints } from 'src/app/shared/settings';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(
    private http: HttpClient
     ) { }


  public getProducts(): Promise<Array<Product>> {
    return new Promise<Array<Product>>((resolve, reject) =>
      this.http.get<Array<Product>>(ApiEndpoints.getProduct)
        .toPromise()
        .then((data: Array<Product>) => {
          resolve(data);
        })
    );
  }

  public async getProduct(name: string): Promise<Product> {
    return new Promise<Product>((resolve, reject) =>
      this.http.get<Product>(ApiEndpoints.getProduct + '/' + name)
        .toPromise()
        .then((data: Product) => {
          resolve(data);
        })
    );
  }

  public async buy(id: string) {
    return new Promise<Product>((resolve, reject) =>
      this.http.post<Product>(ApiEndpoints.buyProduct,
        <BuyProductRequest>{
          productName : id
        })
        .toPromise()
        .then((data: Product) => {
          resolve(data);
        })
    );
  }

}
