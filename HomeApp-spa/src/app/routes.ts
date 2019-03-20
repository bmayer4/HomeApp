import { Routes } from '@angular/router';
import { LandingComponent } from './landing/landing.component';
import { HomeListComponent } from './home/home-list/home-list.component';
import { HomeDetailComponent } from './home/home-detail/home-detail.component';
import { HomeDetailResolver } from './_resolvers/home-detail.resolver';

// more specific routes should be placed above less specific routes
export const appRoutes: Routes = [
    { path: '', component: LandingComponent },
    { path: 'homes/:id', component: HomeDetailComponent, resolve: { home: HomeDetailResolver } },
    { path: 'homes', component: HomeListComponent },
    { path: '**', redirectTo: '', pathMatch: 'full' }
];
