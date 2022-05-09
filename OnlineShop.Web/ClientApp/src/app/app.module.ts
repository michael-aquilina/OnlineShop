import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { ApiHeaderInterceptor } from './core/interceptors/api-header-interceptor.interceptor';
import { ProductDetailComponent } from './product/detail/product.detail.component';
import { ProductListComponent } from './product/list/product.list.component';
import { ProductDetailModule } from './product/detail/product.detail.module';
import { ProductListModule } from './product/list/product.list.module'; 
import { CookieService } from 'ngx-cookie-service';
import { AuthenticationHeaderInterceptor } from './core/interceptors/authentication-header.interceptor copy';
import { BasketComponent } from './basket/basket.component';
import { BasketModule } from './basket/basket.module';

import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    FontAwesomeModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    ProductDetailModule,
    ProductListModule,
    BasketModule,
    RouterModule.forRoot([ 
      { path: 'catalogue', component: ProductListComponent },
      { path: 'basket', component: BasketComponent },
      { path: 'item/:id', component: ProductDetailComponent },
      { path: '', redirectTo: '/catalogue' , pathMatch: 'full' }
    ])
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ApiHeaderInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthenticationHeaderInterceptor,
      multi: true
    },
    CookieService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
