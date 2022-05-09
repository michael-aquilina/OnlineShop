import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { ProductService as ProductService } from 'src/app/core/services/product.service';
import { BuyProductRequest } from '../../shared/models/product.api.buy';
import { Product } from '../../shared/models/product.model';
import { ApiEndpoints, SiteSettings } from '../../shared/settings';

@Component({
  selector: 'app-product-list',
  templateUrl: './product.list.component.html',
  styleUrls: ['./product.list.component.css']
})
export class ProductListComponent implements OnInit, OnDestroy {

  private _allproducts: Product[] = [];
  private _filteredProducts: Product[] = [];


  get products(): Product[] {
    return this._filteredProducts ? this._filteredProducts : [];
  }
  set products(value: Product[]) {
    this._filteredProducts = value ? value : [];
  }


  constructor(
    private route: Router,
    private productService: ProductService,
    private _change: ChangeDetectorRef
  ) {
  }

  public search(value: string) {
    if (value) {
      this._filteredProducts = this._allproducts.filter(p =>
        p.title.toLowerCase().indexOf(value.toLowerCase()) != -1
      );
    } else {
      this._filteredProducts = this._allproducts;
    }
  }

  ngOnInit() {
    this.productService.getProducts()
      .then(d => {
        this.products = d ? d : [];
        this._allproducts = d ? d : [];
      })
      .catch(e => {
        console.error(e);
      });

  }

  ngOnDestroy(): void {
  }

  public trackItem(index: number, item: Product) {
    return item.id;
  }


  click(name: string) {
    this.route.navigate(['/item', name],)
  }

  buy(name: string) {
    this.productService.buy(name)
      .then((p: Product) => {
      });
  }

}
