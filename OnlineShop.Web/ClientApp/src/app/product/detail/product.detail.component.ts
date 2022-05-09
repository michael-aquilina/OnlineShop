import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { ProductService } from 'src/app/core/services/product.service';
import { BuyProductRequest } from '../../shared/models/product.api.buy';
import { Product } from '../../shared/models/product.model';
import { ApiEndpoints, SiteSettings } from '../../shared/settings';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product.detail.component.html',
  styleUrls: ['./product.detail.component.css']
})
export class ProductDetailComponent implements OnInit, OnDestroy {

  name: string;
  product: Product = <Product>{};
  authenticationSubscription = new Subscription();

  constructor(
    private http: HttpClient
    , private route: ActivatedRoute
    , private productService: ProductService) {
     
  }

  ngOnInit() {
    this.name = this.route.snapshot.paramMap.get('id');
    this.getProduct();
  }

  ngOnDestroy(): void {
    this.authenticationSubscription.unsubscribe();
  }


  private getProduct() {
    if (this.name) {
      this.http.get<Product>(ApiEndpoints.getProduct + '/' + this.name)
        .subscribe((data: Product) => {
          this.product = data;
        });
    }
  }

  buy() {
    this.productService.buy(this.name)
      .then()
      .catch(e => {
        // catch error here
      });
  }

}

