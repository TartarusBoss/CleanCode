import java.util.ArrayList;
import java.util.List;


public class UdemBank {
    private List<Usuario> usuarios;
    private List<GrupoDeAhorro> gruposAhorro;
    private List<Transaccion> transacciones;
    private double comisionTotal;

    public UdemBank() {
        usuarios = new ArrayList<>();
        gruposAhorro = new ArrayList<>();
        transacciones = new ArrayList<>();
        comisionTotal = 0.0;
    }

    public void registrarUsuario(Usuario usuario) {
        usuarios.add(usuario);
    }

    public void crearGrupoAhorro(GrupoDeAhorro grupo) {
        gruposAhorro.add(grupo);
    }

    public void realizarTransaccion(Transaccion transaccion) {
        transacciones.add(transaccion);
        aplicarComision(transaccion.getMonto());
    }

    public List<Usuario> obtenerUsuarios() {
        return usuarios;
    }

    public List<GrupoDeAhorro> obtenerGruposAhorro() {
        return gruposAhorro;
    }

    public List<Transaccion> obtenerTransacciones() {
        return transacciones;
    }

    public void premiarUsuariosDeGrupo() {
        for (GrupoDeAhorro grupo : gruposAhorro) {
            List<Usuario> mejoresAportadores = grupo.encontrarMejoresAportadores();
            for (Usuario usuario : mejoresAportadores) {
                usuario.reducirComision(0.01);
            }
        }
    }

    public double getComisionTotal() {
        return comisionTotal;
    }

    private void aplicarComision(double montoTransaccion) {
        double comision = 0.001 * montoTransaccion;
        comisionTotal += comision;
    }

}
//hol