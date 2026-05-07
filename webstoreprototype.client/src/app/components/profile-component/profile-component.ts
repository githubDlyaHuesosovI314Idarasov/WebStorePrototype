import { Component, effect ,inject } from '@angular/core';
import { Router } from '@angular/router';
import { KEYCLOAK_EVENT_SIGNAL, KeycloakEventType, ReadyArgs, typeEventArgs } from 'keycloak-angular';
import { ProfileContentComponent } from '../profile-content-component/profile-content-component';
import Keycloak from 'keycloak-js';
import { ProfileManagementComponent } from '../profile-management-component/profile-management-component';

@Component({
  selector: 'app-profile-component',
  imports: [ProfileManagementComponent, ProfileContentComponent],
  templateUrl: './profile-component.html',
  styleUrl: './profile-component.css',
})
export class ProfileComponent {

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

  async getProfile(){
    return await this.keycloak.loadUserProfile();
  }

  
}
