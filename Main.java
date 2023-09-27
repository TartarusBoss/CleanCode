public class Main {
    public static void main(String[] args) {
        // Crear una instancia de UdemBank
        UdemBank udemBank = new UdemBank();

        // Crear usuarios
        Usuario usuario1 = new Usuario("Usuario1", "contraseña1", 1000.0);
        Usuario usuario2 = new Usuario("Usuario2", "contraseña2", 2000.0);
        Usuario usuario3 = new Usuario("Usuario3", "contraseña3", 3000.0);

        // Registrar usuarios en UdemBank
        udemBank.registrarUsuario(usuario1);
        udemBank.registrarUsuario(usuario2);
        udemBank.registrarUsuario(usuario3);

        // Iniciar sesión para los usuarios
        usuario1.iniciarSesion("contraseña1");
        usuario2.iniciarSesion("contraseña2");
        usuario3.iniciarSesion("contraseña3");

        // Crear grupos de ahorro
        GrupoDeAhorro grupo1 = new GrupoDeAhorro("Grupo1", 5000.0);
        GrupoDeAhorro grupo2 = new GrupoDeAhorro("Grupo2", 7000.0);

        // Agregar usuarios a grupos de ahorro
        grupo1.agregarMiembro(usuario1);
        grupo1.agregarMiembro(usuario2);
        grupo2.agregarMiembro(usuario2);
        grupo2.agregarMiembro(usuario3);

        // Realizar transacciones
        Transaccion transaccion1 = new Transaccion(Transaccion.TipoTransaccion.CONSIGNACION, 500.0, usuario1.getCuentaAhorros().getSaldo());
        Transaccion transaccion2 = new Transaccion(Transaccion.TipoTransaccion.CONSIGNACION, 700.0, usuario2.getCuentaAhorros().getSaldo());
        Transaccion transaccion3 = new Transaccion(Transaccion.TipoTransaccion.CONSIGNACION, 1000.0, usuario3.getCuentaAhorros().getSaldo());

        usuario1.realizarTransaccion(transaccion1);
        usuario2.realizarTransaccion(transaccion2);
        usuario3.realizarTransaccion(transaccion3);

        // Solicitar préstamos
        usuario1.solicitarPrestamo(1000.0, 3);
        usuario2.solicitarPrestamo(1500.0, 4);
        usuario3.solicitarPrestamo(2000.0, 2);

        // Disolver grupos de ahorro
        grupo1.disolverGrupoAhorro();
        grupo2.disolverGrupoAhorro();

        // Premiar usuarios de grupo
        udemBank.premiarUsuariosDeGrupo();

        // Ver historial de transacciones
        usuario1.verHistorial();
        usuario2.verHistorial();
        usuario3.verHistorial();

        // Mostrar comisión total de UdemBank
        System.out.println("Comisión total de UdemBank: " + udemBank.getComisionTotal());
    }
}

//hol