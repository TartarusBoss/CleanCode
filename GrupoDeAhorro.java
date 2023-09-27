import java.util.ArrayList;
import java.util.List;

public class GrupoDeAhorro {
    private String nombreGrupo;
    private double saldoGrupo;
    private List<Usuario> miembros;

    public GrupoDeAhorro(String nombreGrupo, double saldoInicial) {
        this.nombreGrupo = nombreGrupo;
        this.saldoGrupo = saldoInicial;
        this.miembros = new ArrayList<>();
    }

    public String getNombreGrupo() {
        return nombreGrupo;
    }

    public void setNombreGrupo(String nombreGrupo) {
        this.nombreGrupo = nombreGrupo;
    }

    public double getSaldoGrupo() {
        return saldoGrupo;
    }

    public void inyectarSaldo(double monto) {
        saldoGrupo += monto;
    }

    public List<Usuario> getMiembros() {
        return miembros;
    }

    public void agregarMiembro(Usuario usuario) {
        if (miembros.size() < 3) {
            miembros.add(usuario);
        }
    }

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
    public void disolverGrupoAhorro() {
        // Calcula la comisión del banco (5% del saldo actual)
        double comisionBanco = saldoGrupo * 0.05;
        
        // Divide el saldo restante entre los miembros en base a sus aportes
        List<Usuario> mejoresAportadores = encontrarMejoresAportadores();
        double saldoRestante = saldoGrupo - comisionBanco;
        
        if (!mejoresAportadores.isEmpty()) {
            double saldoPorUsuario = saldoRestante / mejoresAportadores.size();
            
            // Transfiere el saldo a las cuentas personales de los usuarios
            for (Usuario usuario : mejoresAportadores) {
                usuario.getCuentaAhorros().actualizarSaldo(saldoPorUsuario);
            }
        } else {
            // No hay aportadores, el saldo se queda en el banco
            System.out.println("No hay mejores aportadores en el grupo. El banco se queda con $" + comisionBanco);
        }
        
        // Limpia la lista de miembros y establece el saldo del grupo en cero
        miembros.clear();
        saldoGrupo = 0.0;
    }
}
//ola