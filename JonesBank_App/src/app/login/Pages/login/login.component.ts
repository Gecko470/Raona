import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoginService } from '../../../services/login.service';
import { Router } from '@angular/router';
import { UserLogin } from '../../../interfaces/interfaces';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: ``
})
export class LoginComponent {

  respuesta: string = "";

  myForm: FormGroup = this.fb.group({
    email: ['', [Validators.required]],
    pass: ['', [Validators.required]],
  });

  user: UserLogin = {
    Email: "",
    Pass: ""
  }

  constructor(private service: LoginService, private fb: FormBuilder, private router: Router) { }

  login() {
    this.user.Email = this.myForm.controls["email"].value;
    this.user.Pass = this.myForm.controls["pass"].value;

    this.service.login(this.user).subscribe({
      next: (response) => {
        const token = response.token;
        localStorage.setItem("token", token);
        this.router.navigateByUrl('/listado');
      },
      error: (response) => {
        if (response.status == 400) {
          this.respuesta = "Datos de acceso incorrectos..";
        }
        if (response.status == 401) {
          this.respuesta = "Tu sesión ha caducado debes volver a loguearte..";
        }
      }
    });
  }

  campoEsValido(campo: string) {

    return (this.myForm.controls[campo].value == '0');
  }
}
