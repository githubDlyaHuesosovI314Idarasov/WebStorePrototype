import { HttpClient } from '@angular/common/http';
import { Component, signal, inject} from '@angular/core';
import { CommonModule } from '@angular/common';
import { WeatherForecast } from './weather-forecast';
import { AuthComponent } from './components/auth-component/auth-component';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  imports: [CommonModule, AuthComponent],
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
