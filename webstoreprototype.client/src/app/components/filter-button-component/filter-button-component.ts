import { Component } from '@angular/core';
import { FontAwesomeModule } from "@fortawesome/angular-fontawesome";
import { faFilter } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-filter-button-component',
  imports: [FontAwesomeModule],
  templateUrl: './filter-button-component.html',
  styleUrl: './filter-button-component.css',
})
export class FilterButtonComponent {
  faFilter = faFilter;
}
