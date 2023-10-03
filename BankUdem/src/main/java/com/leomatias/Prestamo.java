// Clase Prestamo
public class Prestamo {
    // Propiedades de un préstamo.
    private double monto;
    private int plazo;
    private boolean pagado;
    private double tasaInteres;

    // Constructor de la clase Prestamo.
    public Prestamo(double monto, int plazo, double tasaInteres) {
        this.monto = monto;
        this.plazo = plazo;
        this.pagado = false;    
    }

    // Métodos para obtener y establecer propiedades del préstamo (getters y setters).

    public double getMonto() {
        return monto;
    }

    public void setMonto(double monto) {
        this.monto = monto;
    }

    public int getPlazo() {
        return plazo;
    }

    public void setPlazo(int plazo) {
        this.plazo = plazo;
    }

    public double getTasaInteres() {
        return tasaInteres;
    }

    public void setTasaInteres(double tasaInteres) {
        this.tasaInteres = tasaInteres;
    }

    // Método para calcular la cuota mensual del préstamo.
    public double calcularCuotaMensual() {
        double tasaMensual = tasaInteres / 12.0;
        double cuotaMensual = (monto * tasaMensual) / (1 - Math.pow(1 + tasaMensual, -plazo));
        return cuotaMensual;
    }

    // Método para pagar una cuota mensual del préstamo.
    public void pagarCuotaMensual() {
        if (!pagado) {
            double cuotaMensual = calcularCuotaMensual();
            if (monto >= cuotaMensual) {
                monto -= cuotaMensual;
                plazo--;
                if (plazo == 0) {
                    pagado = true;
                }
            }
        }
    }

    // Método para verificar si el préstamo ha sido pagado completamente.
    public boolean estaPagado() {
        return pagado;
    }

    // Método para marcar el préstamo como pagado.
    public void marcarComoPagado() {
        pagado = true;
    }
}
