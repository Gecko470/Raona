import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanLoad, Route, RouterStateSnapshot, UrlSegment, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { LoginService } from '../services/login.service'

@Injectable({
  providedIn: 'root'
})
export class PilladoGuard implements CanActivate {

  constructor(private service: LoginService, private router: Router){}

  
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
      
      if(localStorage.getItem("pillado") == "true"){
        this.router.navigateByUrl("/pillado");
        return false;
      }

      return true;
  }
}
