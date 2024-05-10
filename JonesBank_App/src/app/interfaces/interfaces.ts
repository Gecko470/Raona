export interface UserLogin {
    Email: string,
    Pass: string
}

export interface UserRegister {
    nombre: string,
    email: string,
    pass: string
}

export interface Cuenta {
    id: string,
    numeroCuenta: string,
    cliente: string,
    saldo: number
}

export interface ModifSaldo {
    NumCuenta: string,
    Importe: number
}

export interface respLogin {
    token: string
}

export interface respModifSaldo {
    pillado: boolean
}


