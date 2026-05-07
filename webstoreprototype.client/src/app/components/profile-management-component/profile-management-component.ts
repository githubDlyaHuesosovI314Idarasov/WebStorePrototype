import { Component, inject } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faHeart, faStar, faList, faPenToSquare, faRightFromBracket } from  '@fortawesome/free-solid-svg-icons';
import { RouterLink } from "@angular/router";
import Keycloak from 'keycloak-js';
@Component({
  selector: 'app-profile-management-component',
  imports: [FontAwesomeModule, RouterLink],
  templateUrl: './profile-management-component.html',
  styleUrl: './profile-management-component.css',
})
export class ProfileManagementComponent {

  private readonly keycloak = inject(Keycloak);
  faHeart = faHeart;
  faStar = faStar;
  faList = faList;
  faPenToSquare = faPenToSquare;
  faRightFromBracket = faRightFromBracket;

  logout(){
    this.keycloak.logout();
  }

}
