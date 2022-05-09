export interface Basket {
  identifier: string;
  items: BasketItem[];
}

export interface BasketItem {
  productName: string;
  quantity: number;
  totalPrice: number;
  image: string
}
