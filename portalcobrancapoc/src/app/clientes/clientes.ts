import { Component, OnInit } from '@angular/core';
import { ClienteService, Cliente } from '../services/cliente';
import { CommonModule } from '@angular/common'; // Necessário para diretivas como *ngFor
import { AuthService } from '../services/auth';


@Component({
  selector: 'app-clientes',
  imports: [CommonModule],
  templateUrl: './clientes.html',
  styleUrl: './clientes.css',
})
export class Clientes {
  clientes: Cliente[] = [];

  constructor(private clienteService: ClienteService, private auth: AuthService) { }
  ngOnInit(): void {
    console.log('Clientes')
    this.carregarClientes();
  }

  logout() {
    this.auth.logout().subscribe(() => {
      alert('Logout efetuado');
      window.location.href = '/login';
    });
  }

  carregarClientes(): void {
    this.clienteService.listarClientes().subscribe({
      next: (data) => {
        this.clientes = data;
      },
      error: (error) => {
        console.error('Erro ao buscar clientes:', error);
      }
    });
  }

}
