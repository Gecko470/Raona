import { Component, OnInit } from '@angular/core';
import { CuentasService } from '../../../services/cuentas.service';
import { Cuenta } from '../../../interfaces/interfaces';

@Component({
  selector: 'app-listado',
  templateUrl: './listado.component.html',
  styles: ``
})
export class ListadoComponent implements OnInit {
 
  respuesta : string = "";
  listaCuentas: Cuenta[] = [];

  constructor(private service: CuentasService){}

  ngOnInit(): void {
    this.service.getCuentas().subscribe( {
      next: (response) => {
        this.listaCuentas = response
      },
      error: (response) => {
        if(response.status == 401){
          this.respuesta = "Su sesión ha caducado, debe loguearse de nuevo..";
        }
      }
    } );
  }

}
