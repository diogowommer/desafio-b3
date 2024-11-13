import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  public forecasts: WeatherForecast[] = [];
  private endpoint = '/WeatherForecast';
  private url: string | undefined;

  constructor(private http: HttpClient) {
    this.url = 'https://localhost:49153';
  }

  ngOnInit() {
    this.getForecasts();
  }

  getForecasts() {
    this.http.get<WeatherForecast[]>(`${this.url}${this.endpoint}`).subscribe(
      (result) => {
        this.forecasts = result;
      },  
      (error) => {
        console.error(error);
      }
    );
  }

  title = 'desafiob3.client';
}
