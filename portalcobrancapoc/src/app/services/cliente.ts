import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

// Defina uma interface para o tipo de cliente (opcional, mas recomendado)
export interface Cliente {
  id: number;
  nome: string;
  cpf_cnpj: string;
  email: string;
  telefone: string;
  status: string;
  valor_total: number;
  valor_atraso: number;
  dias_atraso: number;
  analista: string;
}

@Injectable({
  providedIn: 'root'
})
export class ClienteService {
  private apiURL = 'http://localhost:5191/api/Cliente'; // Substitua pela sua URL da API

  constructor(private http: HttpClient) { }

  listarClientes(): Observable<Cliente[]> {
    console.log('Get Clientes')
    return this.http.get<Cliente[]>(this.apiURL, { withCredentials: true });
  }
}
