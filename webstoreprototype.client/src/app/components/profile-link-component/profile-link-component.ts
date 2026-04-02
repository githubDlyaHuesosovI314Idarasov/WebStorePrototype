import { Component} from '@angular/core';
import { RouterLink } from '@angular/router';
import { faUser } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
@Component({
  selector: 'app-profile-link-component',
  imports: [RouterLink, FontAwesomeModule],
  templateUrl: './profile-link-component.html',
  styleUrl: './profile-link-component.css',
})
export class ProfileLinkComponent {

  faUser = faUser;

}
