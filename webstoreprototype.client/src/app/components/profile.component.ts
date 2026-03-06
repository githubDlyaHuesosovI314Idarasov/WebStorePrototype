import { Component, inject } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { AsyncPipe, CommonModule } from '@angular/common';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, AsyncPipe],
  template: `
    @if (auth.isLoading$ | async) {
      <div class="loading-text">Loading profile...</div>
    }
    
    @if ((auth.isAuthenticated$ | async) && (auth.user$ | async); as user) {
      <div style="text-align: center;">
        <div class="profile-name" style=" font-size: 2rem;">
          {{ user.name }}
        </div>
        
      </div>
    }
  `
})
export class ProfileComponent {
  protected auth = inject(AuthService);
}
