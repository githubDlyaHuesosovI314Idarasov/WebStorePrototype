import { Component, effect, inject } from '@angular/core';
import { Router } from '@angular/router';
import { KEYCLOAK_EVENT_SIGNAL, KeycloakEventType, ReadyArgs, typeEventArgs } from 'keycloak-angular';
import Keycloak from 'keycloak-js';
import { faIdCard } from '@fortawesome/free-solid-svg-icons';
import { FaIconComponent, FontAwesomeModule } from "@fortawesome/angular-fontawesome";
@Component({
  selector: 'app-register-link-component',
  imports: [FaIconComponent, FontAwesomeModule],
  templateUrl: './register-link-component.html',
  styleUrl: './register-link-component.css',
})
export class RegisterLinkComponent {

  faIdCard = faIdCard;

}