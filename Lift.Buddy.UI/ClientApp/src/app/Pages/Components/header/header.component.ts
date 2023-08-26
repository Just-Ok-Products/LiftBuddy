import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiCallsService } from 'src/app/Services/Utils/api-calls.service';
import { SnackBarService } from 'src/app/Services/Utils/snack-bar.service';
import { LoginService } from 'src/app/Services/login.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(
    private loginService: LoginService,
    private snackbarService: SnackBarService,
    private router: Router
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

  public logout() {
    if (this.loginService.logout()) {
      this.isLoggedIn = false;
      this.snackbarService.openSuccessSnackbar('Succesfully logged out');
    } else {
      this.snackbarService.operErrorSnackbar('Error during loggin out');
    }
    this.router.navigate(['login']);
  }

}
