import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CollapseModule, TabsModule } from 'ngx-bootstrap';
import { NgxGalleryModule } from 'ngx-gallery';

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
      MortgagecalcComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      RouterModule.forRoot(appRoutes),
      CollapseModule.forRoot(),
      TabsModule.forRoot(),
      NgxGalleryModule
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
