import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { ListadoComponent } from './Pages/listado/listado.component';
import { ModifSaldoComponent } from './Pages/modif-saldo/modif-saldo.component';
import { AuthGuard } from '../guards/auth.guard';
import { PilladoComponent } from './Pages/pillado/pillado.component';
import { PilladoGuard } from '../guards/pillado.guard';




const routes: Routes = [
    { path: "listado", component: ListadoComponent, canActivate: [ AuthGuard, PilladoGuard ] },
    { path: "modifSaldo", component: ModifSaldoComponent, canActivate: [ AuthGuard, PilladoGuard ] },
    { path: "pillado", component: PilladoComponent },
    { path: "", redirectTo: "listado", pathMatch: "full" }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class CuentasRoutingModule { }
