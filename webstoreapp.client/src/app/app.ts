import { HttpClient } from '@angular/common/http';
import { Component, OnInit, signal, inject } from '@angular/core';
import { WeatherForecast } from './weather-forecast';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: true,
  styleUrl: './app.css'
})
export class App implements OnInit {
  public forecasts: WeatherForecast[] = [];
  private http = inject(HttpClient);

  ngOnInit() {
    this.getForecasts();
  }

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
