using cuponesCharli.Back.Modelos;
using cuponesCharli.Back.Persitencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace cuponesCharli.Back.App
{
    public class ConsultaCupon
    {
        public class CuponUnico : IRequest<Codigos>
        {
            public int codigoCorrecto { get; set; }
            public DateTime? FechaAsignacion { get; set; }
        }
        public class Manejador : IRequestHandler<CuponUnico, Codigos>
        {
            private readonly CodigosContext _context;

            public Manejador(CodigosContext context)
            {
                _context = context;

            }

            public async Task<Codigos> Handle(CuponUnico request, CancellationToken cancellationToken)
            {
                var cupon = await _context.Codigos.Where(x => x.Codigo == request.codigoCorrecto).FirstOrDefaultAsync();

                if (cupon == null)
                {
                    throw new Exception("El cupon seleccionado no ha sido premiado, pruebe con otro cupon ");
                }
                else
                {
                    cupon.FechaAsignacion = DateTime.Now;

                    //GENERAR USUARIO ALEATORIO
                   var random = new Random();
                   var usuario = random.Next(1, 50);


                    //Ver si el cupon esta en uso

                    if (cupon.Usuario != null)
                    {
                        throw new Exception("El cupon ya esta en uso");
                    }

                    else
                    {
                        cupon.Usuario = usuario;
                        if (cupon.Contador == null)
                        {
                            cupon.Contador = 1;                           
                            
                        }else
                        {
                            cupon.Contador = cupon.Contador + 1;
                        }

                    }

                    ////TODO BUSQUEDA DE USUARIO
                   

                    //if (cupon.Usuario == null)
                    //{
                    //    cupon.Usuario = 1;
                    //    if (cupon.Contador == null)
                    //    {
                    //        cupon.Contador = 1;
                    //    }

                    //    else
                    //    {
                    //        cupon.Usuario = cupon.Usuario;
                    //        cupon.Contador = cupon.Contador + 1;
                    //    }

                    //EL Usuario ya existe suma 1 al contador




                    //El Usuario no existe genera uno nuevo y pone el contador a 1 


                    // Esto genera un usuario aletorio


                    //if (cupon.Usuario == null)
                    //{

                    //}
                    //else
                    //{
                    //    cupon.Usuario = cupon.Usuario;

                    //}



                    //// TODO EL CONTADOR DEL USUARIO SUME 1 
                    ////Si el contador es 0 lo pone a 1 , si es mayor que 0 suma 1 

                    //if (cupon.Contador == null)
                    //{
                    //    cupon.Contador = 1;
                    //}
                    //else
                    //{
                    //    cupon.Contador = cupon.Contador + 1;
                    //}

                    var result = await _context.SaveChangesAsync();
                }

                return cupon;

            }
        }
    }
}

