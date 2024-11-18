// A mini-application
import { Injectable, NgModule, Component } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

@Injectable()
export class Logger {
  log(message: string) { console.log(message); }
}

@Component({
  selector: 'app-root',
  template: 'Welcome to Angular'
})
export class AppComponent {
  constructor(logger: Logger) {
    logger.log('Let the fun begin!');
  }
}


@NgModule({
  imports: [BrowserModule],
  providers: [Logger,],
  declarations: [AppComponent],
  exports: [AppComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }

import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
platformBrowserDynamic().bootstrapModule(AppModule);
