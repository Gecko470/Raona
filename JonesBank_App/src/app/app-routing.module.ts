import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CuentasRoutingModule } from './cuentas/cuentas.routing';
import { LoginRoutingModule } from './login/login.routing';

const routes: Routes = [

];

@NgModule({
  imports: [RouterModule.forRoot(routes), CuentasRoutingModule, LoginRoutingModule],
  exports: [RouterModule]
})
export class AppRoutingModule { }
