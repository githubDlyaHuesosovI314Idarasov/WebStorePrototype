import { Component, inject, effect } from '@angular/core';
import { RouterLink } from "@angular/router";
import Keycloak from 'keycloak-js';
import { KEYCLOAK_EVENT_SIGNAL, KeycloakEventType, ReadyArgs, typeEventArgs } from 'keycloak-angular';
import {faBalanceScale, faHeart} from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { FaIconComponent } from "@fortawesome/angular-fontawesome";
import { LoginLinkComponent } from "../login-link-component/login-link-component";
import { RegisterLinkComponent } from "../register-link-component/register-link-component";
import { ProfileLinkComponent } from "../profile-link-component/profile-link-component";
import { LogoutLinkComponent } from "../logout-link-component/logout-link-component";
@Component({
  selector: 'app-actions-component',
  imports: [RouterLink, FaIconComponent, FontAwesomeModule, LoginLinkComponent, RegisterLinkComponent, ProfileLinkComponent, LogoutLinkComponent],
  templateUrl: './actions-component.html',
  styleUrl: './actions-component.css',
})
export class ActionsComponent {

  authenticated = false;
  faBalanceScale = faBalanceScale;
  faHeart = faHeart;

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
