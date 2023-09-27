import java.time.LocalDateTime;

public class Transaccion {
    private LocalDateTime fechaHora;
    private TipoTransaccion tipo;
    private double monto;
    private double saldoFinal;

    public enum TipoTransaccion{
        CONSIGNACION,
        TRANSFERENCIA,
        PRESTAMO,
    }

    public Transaccion(TipoTransaccion tipo,double monto,double saldoFinal){
        this.tipo = tipo;
        this.monto = monto;
        this.saldoFinal = saldoFinal;
        this.fechaHora = LocalDateTime.now();
    }

    public LocalDateTime getFechaHora(){
        return fechaHora;
    }
    public TipoTransaccion getTipoTransaccion(){
        return tipo;
    }
    public double getMonto(){
        return monto;
    }
    public double getSaldoFinal(){
        return saldoFinal;
    }



    

    







    public void registrarTransaccion(){
        
    }
    public void calcularSaldoFinal(){
        
    }
}
//hol