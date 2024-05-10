import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListadoComponent } from './Pages/listado/listado.component';
import { ModifSaldoComponent } from './Pages/modif-saldo/modif-saldo.component';
import { RouterModule } from '@angular/router'
import { ReactiveFormsModule } from '@angular/forms';
import { PilladoComponent } from './Pages/pillado/pillado.component';



@NgModule({
  declarations: [
    ListadoComponent,
    ModifSaldoComponent,
    PilladoComponent
  ],
  exports: [
    ListadoComponent,
    ModifSaldoComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule
  ]
})
export class CuentasModule { }
