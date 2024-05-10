import { Component } from '@angular/core';
import { CuentasService } from '../../../services/cuentas.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ModifSaldo } from '../../../interfaces/interfaces';

@Component({
  selector: 'app-modif-saldo',
  templateUrl: './modif-saldo.component.html',
  styles: ``
})
export class ModifSaldoComponent {

  respuesta: string = "";

  myForm: FormGroup = this.fb.group({
    numCuenta: ['', [Validators.required]],
    importe: ['', [Validators.required]],
  });

  modifSaldo: ModifSaldo = {
    NumCuenta: '',
    Importe: 0
  }

  constructor(private service: CuentasService, private fb: FormBuilder, private router: Router) { }

  setIngreso() {
    this.modifSaldo.NumCuenta = this.myForm.controls['numCuenta'].value;
    this.modifSaldo.Importe = this.myForm.controls['importe'].value;

    this.service.setIngreso(this.modifSaldo).subscribe({
      next: (response) => {
        console.log(response);
        this.router.navigateByUrl('/listado');
       
        if(response.pillado && response.pillado == true){
          localStorage.setItem("pillado", "true");
          this.router.navigateByUrl("/pillado");
        }
      },
      error: (response) => {
        console.log(response);
        if(response.status == 404){
          this.respuesta = "La cuenta no existe";
        }
        else if(response.status == 401){
          this.respuesta = "Su sesión ha caducado, debe loguearse de nuevo..";
        }
        else if(response.status == 400){
          this.respuesta = "Esa no es una cantidad válida ..";
        }
      }
    });
  }

  campoEsValido(campo: string) {

    return (this.myForm.controls[campo].value == '0');
  }
}
