import { provideKeycloak, withAutoRefreshToken, AutoRefreshTokenService, UserActivityService} from 'keycloak-angular';

export const provideKeycloakAngular = () =>
  provideKeycloak({
    config: {
      realm: 'WebStoreServerRealm', 
      url: 'http://localhost:8080',
      clientId: 'angular-client'
    },
    initOptions: {
      onLoad: 'check-sso',
      silentCheckSsoRedirectUri: window.location.origin + '/assets/silent-check-sso.html',
      pkceMethod: 'S256',
      checkLoginIframe: false,
    },
    features: [
      withAutoRefreshToken({
        onInactivityTimeout: 'logout',
        sessionTimeout: 60000
      })
    ],
    providers: [
      AutoRefreshTokenService,
      UserActivityService
    ]
  });