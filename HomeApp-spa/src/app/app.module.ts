import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { JwtModule } from '@auth0/angular-jwt';
import { CollapseModule, TabsModule, BsDatepickerModule, PaginationModule, BsDropdownModule, ModalModule } from 'ngx-bootstrap';
import { NgxGalleryModule } from 'ngx-gallery';
import { FileUploadModule } from 'ng2-file-upload';

import { AppComponent } from './app.component';
import { LandingComponent } from './landing/landing.component';
import { appRoutes } from './routes';
import { NavComponent } from './nav/nav.component';
import { HomeListComponent } from './home/home-list/home-list.component';
import { HomeItemComponent } from './home/home-item/home-item.component';
import { FooterComponent } from './footer/footer.component';
import { HomeDetailComponent } from './home/home-detail/home-detail.component';
import { CollapseComponent } from './collapse/collapse.component';
import { MortgagecalcComponent } from './mortgagecalc/mortgagecalc.component';
import { RegisterComponent } from './auth/register/register.component';
import { LoginComponent } from './auth/login/login.component';
import { AddHomeComponent } from './home/add-home/add-home.component';
import { PhotoUploadComponent } from './photo-upload/photo-upload.component';
import { EditHomeComponent } from './home/edit-home/edit-home.component';
import { UserHomesComponent } from './home/user-homes/user-homes.component';
import { FavHomesComponent } from './home/fav-homes/fav-homes.component';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { HasRoleDirective } from './_directives/has-role.directive';
import { AdminComponent } from './admin/admin/admin.component';
import { UserManagementComponent } from './admin/user-management/user-management.component';
import { RolesModalComponent } from './admin/roles-modal/roles-modal.component';
import { HomeManagementComponent } from './admin/home-management/home-management.component';


export function tokenGetter() {
   return localStorage.getItem('token');
}

@NgModule({
   declarations: [
      AppComponent,
      LandingComponent,
      NavComponent,
      HomeListComponent,
      HomeItemComponent,
      HomeDetailComponent,
      FooterComponent,
      CollapseComponent,
      MortgagecalcComponent,
      RegisterComponent,
      LoginComponent,
      AddHomeComponent,
      EditHomeComponent,
      PhotoUploadComponent,
      UserHomesComponent,
      FavHomesComponent,
      AdminComponent,
      UserManagementComponent,
      RolesModalComponent,
      HomeManagementComponent,
      HasRoleDirective
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      FileUploadModule,
      NgxGalleryModule,
      RouterModule.forRoot(appRoutes),
      CollapseModule.forRoot(),
      BsDatepickerModule.forRoot(),
      BsDropdownModule.forRoot(),
      TabsModule.forRoot(),
      PaginationModule.forRoot(),
      ModalModule.forRoot(),
      JwtModule.forRoot({
         config: {
            tokenGetter: tokenGetter,
            whitelistedDomains: ['localhost:5000'],
            blacklistedRoutes: ['localhost:5000/api/auth']
         }
      })
   ],
   providers: [
      ErrorInterceptorProvider
   ],
   entryComponents: [
      RolesModalComponent
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
