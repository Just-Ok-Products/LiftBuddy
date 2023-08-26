import { Component, OnInit } from '@angular/core';
import { LoginService } from '../../Services/login.service';
import { ApiCallsService } from 'src/app/Services/Utils/api-calls.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {

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
    if (ApiCallsService.jwtToken) {
      this.isLoggedIn = true;
    }
  }

  public changePage() {

  }

}
