import { Component, effect ,inject } from '@angular/core';
import { Router } from '@angular/router';
import { faPenToSquare, faStar, faHeart, faList, faRightFromBracket } from '@fortawesome/free-solid-svg-icons';
import { KEYCLOAK_EVENT_SIGNAL, KeycloakEventType, ReadyArgs, typeEventArgs } from 'keycloak-angular';
import Keycloak from 'keycloak-js';
import { FaIconComponent } from "@fortawesome/angular-fontawesome";


@Component({
  selector: 'app-profile-component',
  imports: [FaIconComponent],
  templateUrl: './profile-component.html',
  styleUrl: './profile-component.css',
})
export class ProfileComponent {

  private readonly keycloak = inject(Keycloak);
  private readonly keycloakSignal = inject(KEYCLOAK_EVENT_SIGNAL);
  authenticated = false;
  keycloakStatus: string | undefined;
  faPenToSquare = faPenToSquare;
  faStar = faStar;
  faHeart = faHeart;
  faList = faList;
  faRightFromBracket = faRightFromBracket;

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
