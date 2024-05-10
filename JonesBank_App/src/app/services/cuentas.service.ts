import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Cuenta, ModifSaldo, respModifSaldo } from '../interfaces/interfaces';
import { environment } from '../../../enviroments/environment';

@Injectable({
  providedIn: 'root'
})
export class CuentasService {

  constructor(private http: HttpClient) { }

  getCuentas(): Observable<Cuenta[]> {
    return this.http.get<Cuenta[]>(`${environment.baseUrlCuentas}/getAll`);
  }

  
  setIngreso(ingreso: ModifSaldo): Observable<respModifSaldo> {
    return this.http.post<respModifSaldo>(`${environment.baseUrlCuentas}/modificarSaldo`, ingreso);
  }
}




