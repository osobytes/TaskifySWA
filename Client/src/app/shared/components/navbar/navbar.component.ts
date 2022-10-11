import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  public userLoggedIn: boolean = false;
  constructor(private auth: AuthService, private router: Router) { }

  ngOnInit(): void {
    this.auth.getLoggedInUser().subscribe(user =>{
      if(user.clientPrincipal){
        this.userLoggedIn = true;
      }
    });
  }

  public logout(){
    this.auth.logOut().subscribe(() => {
      this.router.navigate(['/login']);
    });

  }

}
