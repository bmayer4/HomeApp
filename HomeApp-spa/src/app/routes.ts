import { Routes } from '@angular/router';
import { LandingComponent } from './landing/landing.component';
import { HomeListComponent } from './home/home-list/home-list.component';
import { HomeDetailComponent } from './home/home-detail/home-detail.component';
import { HomeDetailResolver } from './_resolvers/home-detail.resolver';
import { RegisterComponent } from './auth/register/register.component';
import { LoginComponent } from './auth/login/login.component';
import { AddHomeComponent } from './home/add-home/add-home.component';
import { EditHomeComponent } from './home/edit-home/edit-home.component';
import { UserHomesComponent } from './home/user-homes/user-homes.component';
import { HomeEditResolver } from './_resolvers/home-edit.resolver';
import { HomesResolver } from './_resolvers/homes.resolver';
import { UserHomesResolver } from './_resolvers/user-homes.resolver';
import { FavHomesComponent } from './home/fav-homes/fav-homes.component';
import { FavHomesResolver } from './_resolvers/fav-homes.resolver';

// more specific routes should be placed above less specific routes
export const appRoutes: Routes = [
    { path: '', component: LandingComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'login', component: LoginComponent },
    { path: 'homes/add', component: AddHomeComponent },
    { path: 'homes/myHomes', component: UserHomesComponent, resolve: { homes: UserHomesResolver }},
    { path: 'homes/favorites', component: FavHomesComponent, resolve: { homes: FavHomesResolver }},
    { path: 'homes/edit/:id', component: EditHomeComponent, resolve: { home: HomeEditResolver }},
    { path: 'homes/:id', component: HomeDetailComponent, resolve: { home: HomeDetailResolver }},
    { path: 'homes', component: HomeListComponent, resolve: { homes: HomesResolver }},
    { path: '**', redirectTo: '', pathMatch: 'full' }
];

