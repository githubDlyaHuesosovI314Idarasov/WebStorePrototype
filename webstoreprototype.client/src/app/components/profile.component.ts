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
      <div style="display: flex; flex-direction: column; align-items: center; gap: 1rem;">
        <div style="text-align: center;">
          <div class="profile-name" 
            style="
              font-size: 2rem; 
              font-weight: 600; 
              color: #f7fafc; 
              margin-bottom: 0.5rem;">
            {{ user.name }}
          </div>
          
        </div>
      </div>
    }
  `
})
export class ProfileComponent {
  protected auth = inject(AuthService);
}
