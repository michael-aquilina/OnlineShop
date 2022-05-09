import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BasketComponent } from './basket.component';
import { BasketItemComponent } from './Item/basket.item.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';



@NgModule({
  declarations: [BasketComponent, BasketItemComponent],
  imports: [
    CommonModule,
    FontAwesomeModule
  ],
  exports: [BasketComponent]
})
export class BasketModule { }
