import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';
import { coreConfig } from './app/core';

bootstrapApplication(AppComponent, {
  providers: [...appConfig.providers, ...coreConfig.providers],
}).catch((err) => console.error(err));
