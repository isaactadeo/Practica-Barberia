using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;

namespace Barberia.SERVICE
{
    public class Logica
    {
     public List<Cliente> clientes { get; set; }
     public List<Turno> turnos { get; set; }        
    
    public Logica() 
        {
        clientes = new List<Cliente>(); 
        turnos = new List<Turno>(); 
        } 
    
    public Resultado CargarDatosCliente(int dni, string nombre, string localidad, string domicilio, int nroDomicilio, string sexo) 
        {
         Resultado resultado = new Resultado();
            if (dni == 0 || string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(localidad) || string.IsNullOrEmpty(domicilio) || nroDomicilio == 0 || string.IsNullOrEmpty(sexo)) 
            {
             resultado.Success = false;
             resultado.Message = "Carga incorrecta";
             return resultado;   
            }
            Cliente cliente = new Cliente();
            cliente.Dni = dni;  
            cliente.Nombre = nombre;    
            cliente.Localidad = localidad;  
            cliente.Domicilio = domicilio;  
            cliente.NroDomicilio = nroDomicilio;    
            cliente.Sexo = sexo;
            clientes.Add(cliente);  
            
            resultado.Success = true;
            resultado.Message = "Carga correcta";
            return resultado;
        }
    public int nroTurnos;
    public Resultado CargarTurno(int dni, int importe, int numtipoTrabajo) 
        {
         Resultado resultado = new Resultado();
         Cliente cliente = clientes.FirstOrDefault(x=> x.Dni == dni);
            if (cliente == null) 
            {
                resultado.Success = false;
                resultado.Message = "Carga incorrecta";
                return resultado;
            }
            Turno turno = new Turno();
            nroTurnos++;
            turno.NumeroTurno = nroTurnos;  
            turno.DniCliente = cliente.Dni; 
            turno.Fecha = DateTime.Now;
            Enumerador.TipoTrabajo tipotrabajo = (Enumerador.TipoTrabajo) numtipoTrabajo;
            turno.TipoTrabajo = tipotrabajo.ToString();
            turno.Importe = importe;
            resultado.Success = true;
            resultado.Message = "Carga correcta";
            return resultado;
        }
    public int ImporteTurnosxSexo(string sexo, DateTime fecha) 
        {   
            int importeTotal= 0;    
            var turnosFiltrados = turnos.Where(t => t.Fecha == fecha && clientes.FirstOrDefault(c => c.Dni == t.DniCliente).Sexo == sexo);
            foreach (Turno turno in turnosFiltrados) 
            {
              importeTotal += turno.Importe;
            }
        return importeTotal;
        }   
    public string ClienteMasTrabajos(int numTipoTrabajo)
        {
            Enumerador.TipoTrabajo tipoTrabajo = (Enumerador.TipoTrabajo)numTipoTrabajo;
            string tipoFiltrar = tipoTrabajo.ToString();

            var trabajosPorCliente = turnos.Where(t => t.TipoTrabajo == tipoFiltrar).GroupBy(t => t.DniCliente).Select(x => new { dniCliente = x.Key, cantidadTrabajos = x.Count() });
            //tipo trabajo sea igual al que busco agrupo por el dni de cliente y obtengo el dni del cliente y el contador de la cantidad de trabajos
            var clientesMasTrabajos = trabajosPorCliente.OrderByDescending(x => x.cantidadTrabajos).FirstOrDefault(); 
            if (clientesMasTrabajos != null) 
            {
                Cliente cliente = clientes.FirstOrDefault(c => c.Dni == clientesMasTrabajos.dniCliente);
                if (cliente != null) 
                {
                    return cliente.Nombre;
                }
            }
            return " ";
        }
    
    public List<ImporteFecha> ImporteTotalEntreFechas(DateTime fechaDesde, DateTime fechaHasta) 
        {
            var turnosFiltrados = turnos.Where(t => t.Fecha > fechaDesde && t.Fecha < fechaHasta);
            List<ImporteFecha> listaImporteFecha = new List<ImporteFecha>();

            var turnosAgrupados = turnos.GroupBy(t => t.Fecha.Date);

            foreach (var grupo in turnosAgrupados) 
            { 
            ImporteFecha importe = new ImporteFecha();
                importe.Dia = grupo.Key.Day;    
                importe.Mes = grupo.Key.Month;  
                importe.Año = grupo.Key.Year;       
                importe.Importe = grupo.Sum(t => t.Importe);    
                listaImporteFecha.Add(importe); 
            }
            return listaImporteFecha;
            
        }

    }
}
