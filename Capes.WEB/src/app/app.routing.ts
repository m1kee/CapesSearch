import { Routes, RouterModule } from '@angular/router';
import { SearchComponent } from './components/search/search.component';
import { AuthGuard } from '@app/helpers/auth-guard/auth.guard';
import { LoginComponent } from './components/login/login.component';

const routes: Routes = [
    { path: '', redirectTo: '/home', pathMatch: 'full' },
    { path: 'home', component: SearchComponent, canActivate: [AuthGuard] },
    { path: 'login', component: LoginComponent, canActivate: [AuthGuard] },
    { path: '**', component: SearchComponent }
];

export const APP_ROUTING = RouterModule.forRoot(routes);
