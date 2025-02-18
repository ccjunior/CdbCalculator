import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { provideHttpClient } from '@angular/common/http';
import { registerLocaleData } from '@angular/common';
import localePt from '@angular/common/locales/pt';

import { AppComponent } from './app.component';
import { SimulatorComponent } from './page/simulator/simulator.component';

registerLocaleData(localePt, 'pt-BR');

@NgModule({
  declarations: [AppComponent, SimulatorComponent],
  imports: [BrowserModule,ReactiveFormsModule],
  providers: [provideHttpClient()],
  bootstrap: [AppComponent],
})

export class AppModule { }
