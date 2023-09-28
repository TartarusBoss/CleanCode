// Clase CuentaAhorros
public class CuentaAhorros {
    // Propiedad para el saldo de la cuenta de ahorros.
    private double saldo;

    // Constructor de la clase CuentaAhorros.
    public CuentaAhorros(double saldoInicial) {
        saldo = saldoInicial;
    }

    // Método para obtener el saldo de la cuenta.
    public double getSaldo() {
        return saldo;
    }

    // Método para establecer el saldo de la cuenta.
    public void setSaldo(double saldo) {
        this.saldo = saldo;
    }

    // Método para actualizar el saldo de la cuenta.
    public void actualizarSaldo(double monto) {
        saldo += monto;
    }
}
