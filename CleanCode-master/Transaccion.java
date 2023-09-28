import java.time.LocalDateTime;

// La clase Transaccion representa una transacción bancaria.
public class Transaccion {
    private LocalDateTime fechaHora; // Fecha y hora de la transacción.
    private TipoTransaccion tipo; // Tipo de transacción (Consignación, Transferencia, Préstamo).
    private double monto; // Monto de la transacción.
    private double saldoFinal; // Saldo final después de la transacción.

    // Enumeración que define los tipos de transacción posibles.
    public enum TipoTransaccion {
        CONSIGNACION,
        TRANSFERENCIA,
        PRESTAMO,
    }

    // Constructor de la clase Transaccion.
    public Transaccion(TipoTransaccion tipo, double monto, double saldoFinal) {
        this.tipo = tipo;
        this.monto = monto;
        this.saldoFinal = saldoFinal;
        this.fechaHora = LocalDateTime.now(); // Obtener la fecha y hora actual al registrar la transacción.
    }

    // Método para obtener la fecha y hora de la transacción.
    public LocalDateTime getFechaHora() {
        return fechaHora;
    }

    // Método para obtener el tipo de transacción.
    public TipoTransaccion getTipoTransaccion() {
        return tipo;
    }

    // Método para obtener el monto de la transacción.
    public double getMonto() {
        return monto;
    }

    // Método para obtener el saldo final después de la transacción.
    public double getSaldoFinal() {
        return saldoFinal;
    }

    // (Los siguientes métodos no están implementados en la clase y están en blanco).
    public void registrarTransaccion() {
        // Aquí se puede implementar la lógica para registrar la transacción en el sistema.
    }

    public void calcularSaldoFinal() {
        // Aquí se puede implementar la lógica para calcular el saldo final después de la transacción.
    }
}
