import { Component } from '@angular/core';
import { faSearch, faMagnifyingGlass } from '@fortawesome/free-solid-svg-icons';
import { FaIconComponent } from "@fortawesome/angular-fontawesome";
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

@Component({
  selector: 'app-search-bar-component',
  imports: [FaIconComponent, FontAwesomeModule],
  templateUrl: './search-bar-component.html',
  styleUrl: './search-bar-component.css',
})

export class SearchBarComponent {
  faSearch = faSearch;
  faMagnifyingGlass = faMagnifyingGlass;
}
