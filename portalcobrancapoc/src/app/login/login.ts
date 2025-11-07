import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../services/auth';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})


  export class Login implements OnInit {
  public username: string = "";
  public password: string = "";
  constructor(private auth: AuthService) {}

  ngOnInit() {}

  onSubmit() {
    /*this.auth.login().subscribe({
      next: () => alert('Login com sucesso!'),
      error: () => alert('Usuário ou senha inválidos')
    });*/

    this.auth.login();

    console.log(`Username: ${this.username}, Password: ${this.password}`);
  }

}
