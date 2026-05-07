import { Component, effect, inject } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faShoppingCart, faBalanceScale, faHeart, faPenToSquare, faStar, faList } from '@fortawesome/free-solid-svg-icons';
import { RouterLink } from "@angular/router";
import Keycloak from 'keycloak-js';
import { KEYCLOAK_EVENT_SIGNAL, KeycloakEventType, ReadyArgs, typeEventArgs} from 'keycloak-angular';
import { RegisterLinkComponent } from "../register-link-component/register-link-component";
import { LoginLinkComponent } from "../login-link-component/login-link-component";
import { LogoutLinkComponent } from "../logout-link-component/logout-link-component";
import { ProfileLinkComponent } from "../profile-link-component/profile-link-component";

@Component({
  selector: 'app-actions-off-canvas-component',
  imports: [FontAwesomeModule, RouterLink, RegisterLinkComponent, LoginLinkComponent, LogoutLinkComponent, ProfileLinkComponent],
  templateUrl: './actions-off-canvas-component.html',
  styleUrl: './actions-off-canvas-component.css',
})
export class ActionsOffCanvasComponent {
  faShoppingCart = faShoppingCart;
  faBalanceScale = faBalanceScale;
  faHeart = faHeart;
  faPenToSquare = faPenToSquare;
  faStar = faStar;
  faList = faList;

  authenticated = false;

    constructor(private readonly keycloak: Keycloak) {
    const keycloakSignal = inject(KEYCLOAK_EVENT_SIGNAL);
    
    effect(() => {
      const keycloakEvent = keycloakSignal();

      if(keycloakEvent.type === KeycloakEventType.Ready){
        this.authenticated = typeEventArgs<ReadyArgs>(keycloakEvent.args);
      }
      if(keycloakEvent.type === KeycloakEventType.AuthLogout){
        this.authenticated = false;
      }
    });
  }

  login(){
    this.keycloak.login();
  }

  register(){
    this.keycloak.register();
  }

  logout(){
    this.keycloak.logout();
  }

}
