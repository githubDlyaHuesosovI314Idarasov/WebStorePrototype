import { Component } from '@angular/core';
import { SelectOption } from '../../interfaces/select-option';

@Component({
  selector: 'app-profile-order-select-component',
  imports: [],
  templateUrl: './profile-order-select-component.html',
  styleUrl: './profile-order-select-component.css',
})
export class ProfileOrderSelectComponent {
  selectOptionsList: SelectOption[] = [];
}
