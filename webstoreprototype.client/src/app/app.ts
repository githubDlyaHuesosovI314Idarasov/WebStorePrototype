import { HttpClient } from '@angular/common/http';
import { Component, signal, inject} from '@angular/core';
import { CommonModule } from '@angular/common';
import { WeatherForecast } from './weather-forecast';
import { AuthComponent } from './components/auth-component/auth-component';
import { ActionsComponent } from './components/actions-component/actions-component';
import { FooterComponent } from './components/footer-component/footer-component';
import { SearchBarComponent } from './components/search-bar-component/search-bar-component';
import { RouterOutlet, RouterModule } from '@angular/router';
import { CatalogueButtonComponent } from "./components/catalogue-button-component/catalogue-button-component";


@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  imports: [CommonModule, AuthComponent, RouterModule, ActionsComponent, FooterComponent, SearchBarComponent, RouterOutlet, CatalogueButtonComponent],
  standalone: true,
  styleUrl: './app.css',
})
export class App  {
  public forecasts: WeatherForecast[] = [];
  private http = inject(HttpClient);

  getForecasts() {
    this.http.get<WeatherForecast[]>('/weatherforecast').subscribe(
      (result) => {
        this.forecasts = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  protected readonly title = signal('webstoreapp.client');
}
