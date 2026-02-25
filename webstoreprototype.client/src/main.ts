// import { provideBrowserGlobalErrorListeners, importProvidersFrom } from "@angular/core";
import { bootstrapApplication } from '@angular/platform-browser'; // BrowserModule,
// import { provideHttpClient, HttpClientModule } from "@angular/common/http";
import { App } from "./app/app";
import { appConfig } from "./app/app.config";

bootstrapApplication(App, appConfig).catch(err => console.error(err));
