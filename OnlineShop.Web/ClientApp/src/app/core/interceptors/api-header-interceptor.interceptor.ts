import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpEvent, HttpResponse, HttpRequest, HttpHandler } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, filter } from 'rxjs/operators';
import { SiteSettings, SiteValues } from 'src/app/shared/settings';

@Injectable()
export class ApiHeaderInterceptor implements HttpInterceptor {
  intercept(httpRequest: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(httpRequest.clone(
      { url: SiteValues.webApiEndpoint + httpRequest.url,
        headers: httpRequest.headers.set('Accept', 'application/json')
      }));
  }
}
