import { Component } from '@angular/core';
import { FormComponent } from './components/forms/form.component';
import { NgFor, NgIf } from '@angular/common';

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

@Component({
  standalone: true,
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  imports: [FormComponent, NgFor, NgIf]
})
export class AppComponent {
  public forecasts: WeatherForecast[] = [];
  //  private endpoint = '/WeatherForecast';
  //  private url: string | undefined;
}


//import { HttpClient } from '@angular/common/http';
//import { Component, OnInit } from '@angular/core';
//import { enviroment } from '../environments/environment';
//import { FormComponent } from './components/forms/form.component';

//interface WeatherForecast {
//  date: string;
//  temperatureC: number;
//  temperatureF: number;
//  summary: string;
//}

//@Component({
//  selector: 'app-root',
//  templateUrl: './app.component.html',
//  styleUrl: './app.component.css',
//  imports: [FormComponent]
//})
//export class AppComponent implements OnInit {
//  public forecasts: WeatherForecast[] = [];
//  private endpoint = '/WeatherForecast';
//  private url: string | undefined;

//  constructor(private http: HttpClient) {
//    this.url = enviroment.apiUrl;
//  }

//  ngOnInit() {
//    this.getForecasts();
//  }

//  getForecasts() {
//    this.http.get<WeatherForecast[]>(`${this.url}${this.endpoint}`).subscribe(
//      (result) => {
//        this.forecasts = result;
//      },  
//      (error) => {
//        console.error(error);
//      }
//    );
//  }

//  title = 'desafiob3.client';
//}
