import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './home/home.component';
import { IdentityComponent } from './identity/identity.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';

//Angular Material Modules

import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';

// Routing

import { AppRoutingModule } from './app-routing.module';
import { MasterDataRoutingModule } from './master-data/master-data-routing.module';


// Modules

import { MasterDataModule } from './master-data/master-data.module';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    IdentityComponent,
    NavMenuComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({
      appId: 'ng-cli-universal'
    }),
    HttpClientModule,
    FormsModule,
    MatPaginatorModule,
    MatTableModule,
    MatSortModule,
    MatSidenavModule,
    MatInputModule,
    MatSelectModule,
    MatIconModule,
    MatMenuModule,
    MatToolbarModule,
    MatButtonModule,
    AppRoutingModule,
    MasterDataRoutingModule,
    BrowserAnimationsModule,
    MasterDataModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
