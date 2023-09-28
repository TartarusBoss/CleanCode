import java.util.ArrayList;
import java.util.List;

// La clase Usuario representa a los usuarios del banco.
public class Usuario {
    // Propiedades de un usuario.
    private String nombreUsuario;
    private String contraseña;
    private List<Transaccion> historialTransacciones;
    private List<GrupoDeAhorro> gruposAhorro;
    private CuentaAhorros cuentaAhorros;
    private double comision;
    private boolean sesionIniciada;

    // Constructor de la clase Usuario.
    public Usuario(String nombreUsuario, String contraseña, Double saldoInicial) {
        this.nombreUsuario = nombreUsuario;
        this.contraseña = contraseña;
        this.historialTransacciones = new ArrayList<>();
        this.gruposAhorro = new ArrayList<>();
        this.cuentaAhorros = new CuentaAhorros(saldoInicial);
        this.comision = 0.001;
        this.sesionIniciada = false;
    }

    // Método para iniciar sesión de un usuario.
    public boolean iniciarSesion(String contraseñaIngresada) {
        if (contraseñaIngresada.equals(contraseña)) {
            sesionIniciada = true;
            return true;
        }
        return false;
    }

    // Método para cerrar la sesión de un usuario.
    public void cerrarSesion() {
        sesionIniciada = false;
    }

    // Método para crear un grupo de ahorro y agregarlo a la lista de grupos si se cumplen ciertas condiciones.
    public void crearGrupoAhorro(GrupoDeAhorro nuevoGrupo) {
        if (gruposAhorro.size() < 3) {
            gruposAhorro.add(nuevoGrupo);
        }
    }

    // Método para solicitar un préstamo si se cumplen ciertas condiciones.
    public void solicitarPrestamo(double monto, int plazo) {
        if (sesionIniciada) {
            // Lógica de solicitud de préstamo
            Prestamo prestamo = new Prestamo(monto, plazo);
            double interesMensual = prestamo.getTasaInteres() * monto;
            double cuotaMensual = monto / plazo + interesMensual;

            if (plazo >= 2 && cuentaAhorros.getSaldo() >= monto) {
                cuentaAhorros.actualizarSaldo(-monto);
                historialTransacciones.add(new Transaccion(Transaccion.TipoTransaccion.PRESTAMO, monto, cuentaAhorros.getSaldo()));
                System.out.println("Préstamo solicitado con éxito.");
                System.out.println("Cuota mensual: " + cuotaMensual);
            } else {
                System.out.println("No cumples con los requisitos para solicitar un préstamo.");
            }
        } else {
            System.out.println("Debes iniciar sesión para solicitar un préstamo.");
        }
    }

    // Método para realizar una transacción y actualizar el historial y saldo de la cuenta.
    public void realizarTransaccion(Transaccion transaccion) {
        historialTransacciones.add(transaccion);
        cuentaAhorros.actualizarSaldo(transaccion.getMonto());
    }

    // Método para ver el historial de transacciones si se ha iniciado sesión.
    public void verHistorial() {
        if (sesionIniciada) {
            System.out.println("Historial de transacciones:");
            for (Transaccion transaccion : historialTransacciones) {
                System.out.println("Fecha y Hora: " + transaccion.getFechaHora());
                System.out.println("Tipo de Transacción: " + transaccion.getTipoTransaccion());
                System.out.println("Monto: " + transaccion.getMonto());
                System.out.println("Saldo Final: " + transaccion.getSaldoFinal());
                System.out.println("----------------------------------");
            }
        } else {
            System.out.println("Debes iniciar sesión para ver el historial.");
        }
    }

    // Métodos getter y setter para las propiedades de un usuario.
    public String getNombreUsuario() {
        return nombreUsuario;
    }

    public void setNombreUsuario(String nombreUsuario) {
        this.nombreUsuario = nombreUsuario;
    }

    public Double getSaldoCuenta() {
        return cuentaAhorros.getSaldo();
    }

    public void setSaldoCuenta(Double saldoCuenta) {
        cuentaAhorros.setSaldo(saldoCuenta);
    }

    public List<GrupoDeAhorro> getGruposAhorro() {
        return gruposAhorro;
    }

    public double getComision() {
        return comision;
    }

    public void reducirComision(double reduccion) {
        comision -= reduccion;
    }

    public CuentaAhorros getCuentaAhorros() {
        return cuentaAhorros;
    }
}
