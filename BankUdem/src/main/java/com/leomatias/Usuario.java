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
    public List<GrupoDeAhorro> gruposAhorroMiembro;

    // Constructor de la clase Usuario.
    public Usuario(String nombreUsuario, String contraseña, Double saldoInicial) {
        this.nombreUsuario = nombreUsuario;
        this.contraseña = contraseña;
        this.historialTransacciones = new ArrayList<>();
        this.gruposAhorro = new ArrayList<>();
        this.cuentaAhorros = new CuentaAhorros(saldoInicial);
        this.comision = 0.001;
        this.sesionIniciada = false;
        this.gruposAhorroMiembro = new ArrayList<>();
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

    public void unirseGrupoDeAhorro(GrupoDeAhorro grupo){
        if (gruposAhorroMiembro.size() < 3){
            gruposAhorroMiembro.add(grupo);
        }
    }

    // Método para crear un grupo de ahorro y agregarlo a la lista de grupos si se cumplen ciertas condiciones.
    public void crearGrupoAhorro(GrupoDeAhorro nuevoGrupo) {
        if (gruposAhorro.size() < 3) {
            gruposAhorro.add(nuevoGrupo);
        }
    }

    // Método para solicitar un préstamo si se cumplen ciertas condiciones.
    public void solicitarPrestamo(double monto, int plazo, GrupoDeAhorro grupoDelQuePidePrestamo) {
        if (sesionIniciada) {
            // Obtiene la tasa de interés del grupo
            double tasaInteres = grupoDelQuePidePrestamo.getTasaInteres();

            // Lógica de solicitud de préstamo
            Prestamo prestamo = new Prestamo(monto, plazo, tasaInteres);
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

    public void realizarPagoMensual(Prestamo prestamo, int mesesAPagar) {
        if (sesionIniciada) {
            if (prestamo != null && !prestamo.estaPagado()) {
                double cuotaMensual = prestamo.calcularCuotaMensual();
                double montoAPagar = cuotaMensual * mesesAPagar;
                
                if (montoAPagar <= cuentaAhorros.getSaldo()) {
                    cuentaAhorros.actualizarSaldo(-montoAPagar);
                    prestamo.pagarCuotaMensual(); // Actualiza el estado del préstamo
                    System.out.println("Pago mensual realizado con éxito.");
                    System.out.println("Monto pagado: " + montoAPagar);
                    System.out.println("Cuotas pendientes: " + prestamo.getPlazo());
                } else {
                    System.out.println("Saldo insuficiente para realizar el pago.");
                }
            } else {
                System.out.println("El préstamo no existe o ya ha sido pagado.");
            }
        } else {
            System.out.println("Debes iniciar sesión para realizar un pago mensual.");
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

    // Método para realizar un pago mensual de un préstamo
    public void realizarPagoMensual(double cuotaMensual) {
        if (sesionIniciada) {
            if (cuotaMensual > 0 && cuotaMensual <= cuentaAhorros.getSaldo()) {
                cuentaAhorros.actualizarSaldo(-cuotaMensual);
                // Actualizar el estado del préstamo (por ejemplo, reducir la deuda)
                // Aquí debes implementar la lógica para actualizar el estado del préstamo pendiente
                System.out.println("Pago mensual realizado con éxito.");
            } else {
                System.out.println("El monto ingresado no es válido.");
            }
        } else {
            System.out.println("Debes iniciar sesión para realizar un pago mensual.");
        }
    }
}
    
