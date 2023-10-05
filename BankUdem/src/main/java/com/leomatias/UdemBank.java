    import java.util.ArrayList;
import java.util.List;

// La clase UdemBank representa la entidad principal del banco.
public class UdemBank {
    private List<Usuario> usuarios; // Lista de usuarios registrados en el banco.
    private List<GrupoDeAhorro> gruposAhorro; // Lista de grupos de ahorro creados.
    private List<Transaccion> transacciones; // Lista de transacciones realizadas.
    private double comisionTotal; // Variable para llevar un registro de la comisión total ganada.

    public UdemBank() {
        usuarios = new ArrayList<>(); // Inicialización de la lista de usuarios.
        gruposAhorro = new ArrayList<>(); // Inicialización de la lista de grupos de ahorro.
        transacciones = new ArrayList<>(); // Inicialización de la lista de transacciones.
        comisionTotal = 0.0; // Inicialización de la comisión total en cero.
    }

    // Método para registrar un usuario en el banco.
    public void registrarUsuario(Usuario usuario) {
        usuarios.add(usuario);
    }

    // Método para crear un grupo de ahorro y agregarlo a la lista de grupos.
    public void crearGrupoAhorro(GrupoDeAhorro grupo) {
        gruposAhorro.add(grupo);
    }

    // Método para registrar una transacción y aplicar la comisión correspondiente.
    public void realizarTransaccion(Transaccion transaccion) {
        transacciones.add(transaccion);
        aplicarComision(transaccion.getMonto());
    }

    // Método para obtener la lista de usuarios registrados en el banco.
    public List<Usuario> obtenerUsuarios() {
        return usuarios;
    }

    // Método para obtener la lista de grupos de ahorro creados.
    public List<GrupoDeAhorro> obtenerGruposAhorro() {
        return gruposAhorro;
    }

    // Método para obtener la lista de transacciones realizadas.
    public List<Transaccion> obtenerTransacciones() {
        return transacciones;
    }

    // Método para premiar a los usuarios de los grupos de ahorro con mejores aportes.
    public void premiarUsuariosDeGrupo() {
        for (GrupoDeAhorro grupo : gruposAhorro) {
            List<Usuario> mejoresAportadores = grupo.encontrarMejoresAportadores();
            for (Usuario usuario : mejoresAportadores) {
                usuario.reducirComision(0.01); // Reducir la comisión de los mejores aportadores.
            }
        }
    }

    // Método para obtener la comisión total ganada por el banco.
    public double getComisionTotal() {
        return comisionTotal;
    }

    // Método privado para aplicar una comisión a una transacción.
    private void aplicarComision(double montoTransaccion) {
        double comision = 0.001 * montoTransaccion;
        comisionTotal += comision; // Añadir la comisión al total.
    }
}
    
