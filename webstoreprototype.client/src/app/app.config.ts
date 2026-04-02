import { ApplicationConfig, provideBrowserGlobalErrorListeners, provideZoneChangeDetection} from '@angular/core';
import { provideRouter, withInMemoryScrolling } from '@angular/router';
import { provideHttpClient, withInterceptors} from '@angular/common/http';
// import { environment } from '../environments/environment';
import { createInterceptorCondition, INCLUDE_BEARER_TOKEN_INTERCEPTOR_CONFIG, IncludeBearerTokenCondition, provideKeycloak } from 'keycloak-angular';
import { includeBearerTokenInterceptor } from 'keycloak-angular';
import routes from './app.routes';
import { provideKeycloakAngular } from './keycloak.config';
import { provideClientHydration } from '@angular/platform-browser';

const urlCondition = createInterceptorCondition<IncludeBearerTokenCondition>({
  urlPattern: /^(http:\/\/localhost:8181)(\/.*)?$/i,
  bearerPrefix: 'Bearer'
});


export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes, withInMemoryScrolling({
      scrollPositionRestoration: 'enabled',
      anchorScrolling: 'enabled'
    })),
    provideKeycloakAngular(),
    {
      provide: INCLUDE_BEARER_TOKEN_INTERCEPTOR_CONFIG,
      useValue: [urlCondition]
    },
    /*
    provideKeycloak({
      config:{
        url: 'http://localhost:8080',
        realm: 'WebStoreServerRealm',
        clientId: 'angular-client',
      },
      initOptions:{
        onLoad: 'check-sso',
        silentCheckSsoRedirectUri: `${window.location.origin}/silent-check-sso.html`
      },
      
      
    }),
    */
    provideHttpClient(withInterceptors([includeBearerTokenInterceptor])),
    

  ]
};
