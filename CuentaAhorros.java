
public class CuentaAhorros {
    private double saldo;

    public CuentaAhorros(double saldoInicial) {
        this.saldo = saldoInicial;
    }

    public double getSaldo() {
        return saldo;
    }

    public void setSaldo(double saldo) {
        this.saldo = saldo;
    }

    public void actualizarSaldo(double monto) {
        saldo += monto;
    }
}

//hol