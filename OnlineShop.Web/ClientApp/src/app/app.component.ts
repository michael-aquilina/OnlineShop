import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { v4 as uuidv4 } from 'uuid';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  title = 'app';

  constructor(
    private cookie: CookieService) {

  }

  ngOnInit() {
    var id = this.cookie.get("userId");
    if (!id) {
      this.cookie.set("userId", uuidv4())
    }
  }
}
