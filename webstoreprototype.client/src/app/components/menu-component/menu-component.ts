import { Component, effect, inject } from '@angular/core';
import { Router } from '@angular/router';
import { KEYCLOAK_EVENT_SIGNAL, KeycloakEventType, ReadyArgs, typeEventArgs } from 'keycloak-angular';
import Keycloak from 'keycloak-js';

@Component({
  selector: 'app-menu-component',
  imports: [],
  templateUrl: './menu-component.html',
  styleUrl: './menu-component.css',
})
export class MenuComponent {

  private readonly keycloak = inject(Keycloak);
  private readonly keycloakSignal = inject(KEYCLOAK_EVENT_SIGNAL);
  authenticated = false;
  keycloakStatus: string | undefined;


  constructor(private router: Router) {
    effect(() => {
      const keycloakEvent = this.keycloakSignal();
      this.keycloakStatus =  keycloakEvent.type;

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

  logout(){
    this.keycloak.logout();
  }

  register(){
    this.keycloak.register();
  }

  async getProfile(){
    return await this.keycloak.loadUserProfile();
  }
}
