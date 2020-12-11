import { Component, OnInit } from '@angular/core';
import { AuthService } from './services/auth.service';
import { IUser } from './domain/user';
import { Router } from '@angular/router';
import { faSignOutAlt, faChartLine } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
    user: IUser = null;

    icons: any = {
        faSignOutAlt: faSignOutAlt,
        faChartLine: faChartLine
    };

    constructor(private authService: AuthService, private router: Router) { }

    ngOnInit(): void {
        this.authService.loggedUser.subscribe((user: IUser) => {
            this.user = user;
        });
    }

    logout = () => {
        this.authService.logout();
        this.router.navigate(['/login']);
    };

    isAuthenticated = () => {
        return this.authService.isAuthenticated();
    };
}
