import { Component, OnInit } from '@angular/core';
import { ApiCallsService } from 'src/app/Services/Utils/api-calls.service';
import { LoginService } from 'src/app/Services/login.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(
    private loginService: LoginService,
    private apiService: ApiCallsService
  ) { }

  ngOnInit() {
    this.initUserData();
  }

  public usernameLoggedIn: string | undefined = this.loginService.currentUsername;
  public isLoggedIn: boolean = false;
  private initUserData() {
    if (this.apiService.jwtToken) {
      this.isLoggedIn = true;
    }
  }

  public logout() {
    this.loginService.currentUsername = '';
    this.isLoggedIn = false;
  }

}
