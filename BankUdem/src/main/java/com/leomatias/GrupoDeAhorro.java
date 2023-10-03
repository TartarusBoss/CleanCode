import java.util.ArrayList;
import java.util.List;

// La clase GrupoDeAhorro representa un grupo de ahorro en el banco.
public class GrupoDeAhorro {
    // Propiedades de un grupo de ahorro.
    private String nombreGrupo;
    private double saldoGrupo;
    private List<Usuario> miembros;
    private double tasaInteres;

    // Constructor de la clase GrupoDeAhorro.
    public GrupoDeAhorro(String nombreGrupo, double saldoInicial, double tasaInteres) {
        this.nombreGrupo = nombreGrupo;
        this.saldoGrupo = saldoInicial;
        this.miembros = new ArrayList<>();
        this.tasaInteres = tasaInteres;
    }

    // Métodos getter y setter para las propiedades de un grupo de ahorro.
    public String getNombreGrupo() {
        return nombreGrupo;
    }

    public void setNombreGrupo(String nombreGrupo) {
        this.nombreGrupo = nombreGrupo;
    }
    public double getTasaInteres(){
        return tasaInteres;
    }

    public double getSaldoGrupo() {
        return saldoGrupo;
    }

    // Método para inyectar saldo en el grupo.
    public void inyectarSaldo(double monto) {
        saldoGrupo += monto;
    }

    public List<Usuario> getMiembros() {
        return miembros;
    }

    // Método para agregar un miembro al grupo si se cumplen ciertas condiciones.
    public void agregarMiembro(Usuario usuario) {
        if (miembros.size() < 3) {
            miembros.add(usuario);
        }
    }
    public boolean perteneceUsuario(Usuario usuario) {
        return miembros.contains(usuario);
    }

    // Método para encontrar a los mejores aportadores del grupo.
    public List<Usuario> encontrarMejoresAportadores() {
        List<Usuario> mejoresAportadores = new ArrayList<>();
        double mayorAporte = 0;

        for (Usuario usuario : miembros) {
            // Cambia saldoInicial por saldo actual
            double aporte = usuario.getCuentaAhorros().getSaldo();
            if (aporte > mayorAporte) {
                mejoresAportadores.clear();
                mejoresAportadores.add(usuario);
                mayorAporte = aporte;
            } else if (aporte == mayorAporte) {
                mejoresAportadores.add(usuario);
            }
        }

        return mejoresAportadores;
    }

    // Método para calcular las ganancias del grupo y distribuirlas entre los mejores aportadores.
    public double calcularGanancias() {
        List<Usuario> mejoresAportadores = encontrarMejoresAportadores();

        if (mejoresAportadores.isEmpty()) {
            System.out.println("No hay mejores aportadores en el grupo.");
            return 0.0;
        } else {
            double ganancias = saldoGrupo * 0.1;
            double gananciasPorMiembro = ganancias / mejoresAportadores.size();

            System.out.println("El grupo ha obtenido ganancias por ser los mejores aportadores:");
            for (Usuario usuario : mejoresAportadores) {
                usuario.getCuentaAhorros().actualizarSaldo(gananciasPorMiembro);
                System.out.println(usuario.getNombreUsuario() + " recibe $" + gananciasPorMiembro);
            }
            return ganancias;
        }
    }

    // Método para disolver el grupo de ahorro.
    public void disolverGrupoAhorro() {
        // Calcula la comisión del banco (5% del saldo actual)
        double comisionBanco = saldoGrupo * 0.05;

        // Calcula las ganancias del grupo y las distribuye
        double gananciasGrupo = calcularGanancias();

        // Transfiere las ganancias al banco
        if (gananciasGrupo > 0) {
            inyectarSaldo(gananciasGrupo);
        }

        // Transfiere la comisión del banco
        if (comisionBanco > 0) {
            // El grupo disuelve y transfiere la comisión al banco
            inyectarSaldo(-comisionBanco);
        }

        // Elimina el grupo de ahorro
        nombreGrupo = null;
        saldoGrupo = 0;
        miembros.clear();
    }
}
