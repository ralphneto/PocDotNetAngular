import { Component, NgModule } from '@angular/core';
import { Login } from "./login/login";
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Clientes } from './clientes/clientes';
import { RouterOutlet } from '@angular/router';


@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  styleUrls: ['./app.css'],
  imports: [RouterOutlet, CommonModule, FormsModule]
})


export class App {
  title = 'sign-in-with-google';
}
