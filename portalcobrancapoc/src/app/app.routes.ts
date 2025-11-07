import { RouterModule, Routes } from '@angular/router';
import { Login } from './login/login';
import { Clientes } from './clientes/clientes';
import { NgModule } from '@angular/core';

export const routes: Routes = [
  { path: 'login', component: Login },
  { path: 'clientes', component: Clientes },
  { path: 'error', component: Error },
  { path: '', component: Login },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
