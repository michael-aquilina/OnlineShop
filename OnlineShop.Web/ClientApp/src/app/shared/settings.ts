export class SiteSettings {
  public static ApiEndpoints: ApiEndpoints;
  public static SiteValues: SiteValues;
}
export class ApiEndpoints {
  public static getProduct = '/product';
  public static getProducts = '/product';
  public static buyProduct = '/product/buy';
  public static getBasket = '/basket';
  public static updateBasketItem = '/basketitem/Update';
  public static deleteBasketItem = '/basketitem/Delete';
  public static deleteAllItems = '/basket';

}

export class SiteValues {
  public static webApiEndpoint = 'http://localhost:55055/api';
}
