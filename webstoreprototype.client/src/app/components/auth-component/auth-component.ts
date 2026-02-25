import { Component, inject } from '@angular/core';
import { LoginButtonComponent } from '../login-button.component';
import { LogoutButtonComponent } from '../logout-button.component';
import { ProfileComponent } from '../profile.component';
import { AuthService } from '@auth0/auth0-angular';
import { AsyncPipe, CommonModule } from '@angular/common';

@Component({
  selector: 'app-auth-component',
  imports: [LoginButtonComponent, LogoutButtonComponent, ProfileComponent, AsyncPipe, CommonModule],
  templateUrl: './auth-component.html',
  standalone: true,
  styleUrl: './auth-component.css',
})
export class AuthComponent {
  protected auth = inject(AuthService);
}
