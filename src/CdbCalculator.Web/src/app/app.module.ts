import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { provideHttpClient } from '@angular/common/http';
import localePt from '@angular/common/locales/pt';
import { registerLocaleData } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { HomeComponent } from './page/home/home.component';

registerLocaleData(localePt, 'pt-BR');

@NgModule({
  declarations: [AppComponent, HomeComponent],
  imports: [BrowserModule,ReactiveFormsModule],
  providers: [provideHttpClient()],
  bootstrap: [AppComponent]
})
export class AppModule { }
