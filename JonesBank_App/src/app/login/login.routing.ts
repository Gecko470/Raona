import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { LoginComponent } from './Pages/login/login.component';
import { RegisterComponent } from './Pages/register/register.component';
import { PilladoGuard } from '../guards/pillado.guard';


const routes: Routes = [
    { path: "register", component: RegisterComponent, canActivate: [ PilladoGuard ] },
    { path: "login", component: LoginComponent, canActivate: [ PilladoGuard ] }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class LoginRoutingModule { }
