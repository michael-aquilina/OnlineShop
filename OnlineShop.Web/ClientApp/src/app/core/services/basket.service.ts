import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product } from 'src/app/shared/models/product.model';
import { ApiEndpoints } from 'src/app/shared/settings';
import { Basket, BasketItem } from '../../shared/models/basket';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  constructor(
    private http: HttpClient
  ) { }

  public getBasket(): Promise<Basket> {
    return new Promise<Basket>((resolve, reject) =>
      this.http.get<Basket>(ApiEndpoints.getBasket)
        .toPromise()
        .then((data: Basket) => {
          resolve(data);
        })
    );
  }

  public async UpdateBasketItem(productName: string, quantity: number) {
    return new Promise<Product>((resolve, reject) =>
      this.http.post<Product>(ApiEndpoints.updateBasketItem,
        <BasketItem>{
          productName: productName,
          quantity: quantity
        })
        .toPromise()
        .then((data: Product) => {
          resolve(data);
        })
    );
  }

  public async deleteBasketItem(productName: string) {
    return new Promise<Product>((resolve, reject) =>
      this.http.post<Product>(ApiEndpoints.deleteBasketItem,
        <BasketItem>{
          productName: productName 
        })
        .toPromise()
        .then((data: Product) => {
          resolve(data);
        })
    );

  }
  public async deleteAllItems() {
    return new Promise((resolve, reject) =>
      this.http.delete(ApiEndpoints.deleteAllItems)
        .toPromise()
        .then((data: any) => {
          resolve(data);
        })
    );

  }
}
