import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpEvent, HttpResponse, HttpRequest, HttpHandler } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, filter } from 'rxjs/operators';
import { SiteSettings, SiteValues } from 'src/app/shared/settings';
import { CookieService } from 'ngx-cookie-service';

@Injectable()
export class AuthenticationHeaderInterceptor implements HttpInterceptor {

  constructor(public cookie: CookieService) {}


  intercept(httpRequest: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(httpRequest.clone(
      {
        headers: httpRequest.headers.set('userId', this.cookie.get("userId"))
      }));
  }
}
