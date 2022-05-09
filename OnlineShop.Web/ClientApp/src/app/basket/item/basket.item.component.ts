import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { BasketService } from '../../core/services/basket.service';
import { BasketItem } from '../../shared/models/basket';
import { faTrash } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-basket-item',
  templateUrl: './basket.item.component.html',
  styleUrls: ['./basket.item.component.css']
})
export class BasketItemComponent implements OnInit, OnDestroy {

  faTrash = faTrash;
  @Input() item: BasketItem;
  @Output() update = new EventEmitter<boolean>();

  constructor(
    private basketService: BasketService) {

  }

  ngOnInit() {
  }

  ngOnDestroy(): void {
  }


  minus() {
    if (this.item.quantity > 0) {
      this.basketService.UpdateBasketItem(this.item.productName, this.item.quantity - 1)
        .then(p => {
          this.update.emit(true);
        });
    }
  }

  plus() {
    this.basketService.UpdateBasketItem(this.item.productName, this.item.quantity + 1)
      .then(p => {
        this.update.emit(true);
      });
  }

  delete() {

    this.basketService.deleteBasketItem(this.item.productName)
      .then(p => {
        this.update.emit(true);
      });
  }

}

