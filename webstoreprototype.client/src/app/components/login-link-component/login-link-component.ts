import { Component, effect, inject } from '@angular/core';
import { FontAwesomeModule, FaIconComponent } from '@fortawesome/angular-fontawesome';
import { faSignIn } from '@fortawesome/free-solid-svg-icons';
import { Router } from '@angular/router';
import { KEYCLOAK_EVENT_SIGNAL, KeycloakEventType, ReadyArgs, typeEventArgs } from 'keycloak-angular';
import Keycloak from 'keycloak-js';

@Component({
  selector: 'app-login-link-component',
  imports: [FaIconComponent, FontAwesomeModule],
  templateUrl: './login-link-component.html',
  styleUrl: './login-link-component.css',
})
export class LoginLinkComponent {

  faSignIn = faSignIn;
  
}
