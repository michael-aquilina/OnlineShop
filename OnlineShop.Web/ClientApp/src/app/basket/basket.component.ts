import { Component, OnDestroy, OnInit } from '@angular/core'; 
import { BasketService } from '../core/services/basket.service';
import { Basket } from '../shared/models/basket';
import { faTrash } from '@fortawesome/free-solid-svg-icons';
 
@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.css']
})
export class BasketComponent implements OnInit, OnDestroy {

  get hasItems(): boolean {
    return this.basket && this.basket.items && this.basket.items.length > 0;
  }

  basket: Basket;

  faTrash = faTrash;

  constructor(
    private basketService: BasketService) {     
  }

  ngOnInit() {
    this.getBasket();
  }

  ngOnDestroy(): void { 
  }

  getBasket() {
    console.log('getting basket');
    this.basketService.getBasket()
      .then(b => {
        this.basket = b;
      });
  }

  update() {
    this.getBasket();
  }

  clearBasket() {
    this.basketService.deleteAllItems()
      .then(() => {
        this.update();
      });
  }
}

