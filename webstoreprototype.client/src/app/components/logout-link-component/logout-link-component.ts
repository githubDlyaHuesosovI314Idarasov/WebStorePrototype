import { Component, effect, inject } from '@angular/core';
import { Router } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faRightFromBracket } from '@fortawesome/free-solid-svg-icons';
import { KEYCLOAK_EVENT_SIGNAL, KeycloakEventType, ReadyArgs, typeEventArgs } from 'keycloak-angular';
import Keycloak from 'keycloak-js';
import { FaIconComponent } from "@fortawesome/angular-fontawesome";

@Component({
  selector: 'app-logout-link-component',
  imports: [FaIconComponent, FontAwesomeModule],
  templateUrl: './logout-link-component.html',
  styleUrl: './logout-link-component.css',
})
export class LogoutLinkComponent {

  faRightFromBracket = faRightFromBracket;

}
