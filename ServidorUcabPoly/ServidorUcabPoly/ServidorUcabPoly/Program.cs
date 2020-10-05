using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace ServidorUcabPoly
{
    class Program
    {
        static TcpListener tcpListener = new TcpListener(8000);
        static Controladora control = new Controladora();
        static BaseDatos datos = new BaseDatos("root", "15121995");
        static List<Socket> usuarios = new List<Socket>();
        static bool turnoInicial = true;

        static void Listeners()
        {
            while (true)
            {
                Socket socketForClient = tcpListener.AcceptSocket();
                if (socketForClient.Connected)
                {

                    Console.WriteLine("**  Se conectó al servidor:" + IPAddress.Parse(((IPEndPoint)socketForClient.RemoteEndPoint).Address.ToString()) + "::" + ((IPEndPoint)socketForClient.RemoteEndPoint).Port.ToString() + "  **");
                    usuarios.Add(socketForClient);

                    NetworkStream networkStream = new NetworkStream(socketForClient);
                    System.IO.StreamWriter streamw = new System.IO.StreamWriter(networkStream);
                    System.IO.StreamReader streamr = new System.IO.StreamReader(networkStream);
                    while (socketForClient.Connected)
                    {
                        try
                        {

                            string mensaje = streamr.ReadLine();

                            if (mensaje != null)
                            {
                                Console.WriteLine("Se Recibio: " + mensaje);
                                switch (mensaje.Substring(0, 3))
                                {
                                    case "000": //Validar el usuario en la base de datos
                                                //mensaje xxxusuario-contraseña
                                        mensaje = mensaje.Substring(3); // usuario-contraseña
                                        string[] separadas = mensaje.Split('-'); //[usuario][contraseña]
                                        string usuario = separadas[0];
                                        string contraseña = separadas[1];
                                        Console.WriteLine("- Intento de Login - Usuario:" + usuario + " Contraseña:" + contraseña);
                                        if (datos.validarUsuario(usuario, contraseña) == 1)
                                        {
                                            streamw.WriteLine("000Y");
                                            streamw.Flush();
                                            Console.WriteLine("Login Exitoso de: " + usuario);
                                            control.asignarJugadores(usuario, usuarios.Count());
                                        }
                                        else
                                        {
                                            Console.WriteLine("Login Fallido, Datos Invalidos");
                                            streamw.WriteLine("000N");
                                            streamw.Flush();
                                            control.cerrarConexion(usuarios, socketForClient);
                                        }
                                        break;
                                    case "001": //Todos los jugadores listos mandar al tablero
                                        control.sendBroadCast(usuarios, "001Y");

                                        break;
                                    case "002": //Asignar turnos a los jugadores

                                        Console.WriteLine("Cantidad de usuarios:" + usuarios.Count());// borrar
                                        if (turnoInicial)
                                        {
                                            turnoInicial = false;
                                            if (usuarios.Count() == 1)
                                            {
                                                control.asignarTurnos(control.nickname1, "", "", "");
                                                control.sendBroadCast(usuarios, "002Y" + control.nickname1 + "-" + control.turno1);
                                            }
                                            if (usuarios.Count() == 2)
                                            {
                                                control.asignarTurnos(control.nickname1, control.nickname2,"","");
                                                control.sendBroadCast(usuarios, "002Y" + control.nickname1 + "-" + control.turno1);
                                                control.sendBroadCast(usuarios, "002Y" + control.nickname2 + "-" + control.turno2);

                                            }
                                            if (usuarios.Count() == 3)
                                            {
                                                control.asignarTurnos(control.nickname1, control.nickname2, control.nickname3, "");
                                                control.sendBroadCast(usuarios, "002Y" + control.nickname1 + "-" + control.turno1);
                                                control.sendBroadCast(usuarios, "002Y" + control.nickname2 + "-" + control.turno2);
                                                control.sendBroadCast(usuarios, "002Y" + control.nickname3 + "-" + control.turno3);
                                            }
                                            if (usuarios.Count() == 4)
                                            {
                                                control.asignarTurnos(control.nickname1, control.nickname2, control.nickname3, control.nickname4);
                                                control.sendBroadCast(usuarios, "002Y" + control.nickname1 + "-" + control.turno1);
                                                control.sendBroadCast(usuarios, "002Y" + control.nickname2 + "-" + control.turno2);
                                                control.sendBroadCast(usuarios, "002Y" + control.nickname3 + "-" + control.turno3);
                                                control.sendBroadCast(usuarios, "002Y" + control.nickname4 + "-" + control.turno4);
                                            }
                                            control.pasarTurno(usuarios);
                                        }
                                        break;
                                    case "003": //Verificar de quien es el turno
                                        control.pasarTurno(usuarios);
                                        break;
                                    case "004": //Tirar el dado
                                        control.rollDice();
                                        streamw.WriteLine("004Y" + control.ultimodado);
                                        streamw.Flush();
                                        break;
                                    case "005": //Usuario quiere moverse
                                        string paramoverse = mensaje.Substring(3); // dado-casilla-blabalbla
                                        string[] separadasmoverse = paramoverse.Split('-'); //[dado][casilla][nick][blablabla]
                                        string dado = separadasmoverse[0];
                                        string casilla = separadasmoverse[1];
                                        string nickmovimiento = separadasmoverse[2];
                                        int dado1 = int.Parse(dado);
                                        int casilla1 = int.Parse(casilla);
                                        string mensajeproto = control.moverse(dado1, casilla1);//[codigo][mover]
                                        string[] sepmsjproto = mensajeproto.Split('-');
                                        if(sepmsjproto[0] == "1")
                                        {                                           
                                            int mover = int.Parse(sepmsjproto[1]);
                                            streamw.WriteLine("005Y" + nickmovimiento + "-" + mover + "-" + 1);
                                            streamw.Flush();
                                        }
                                        if (sepmsjproto[0] == "2")
                                        {
                                            int mover = int.Parse(sepmsjproto[1]);
                                            if(nickmovimiento == control.nickname1)
                                            {
                                                control.dinero1 = control.dinero1 + 200;
                                                streamw.WriteLine("005Y" + nickmovimiento + "-" + mover + "-" + 2 + "-" + control.dinero1);
                                                streamw.Flush();
                                            }
                                            if (nickmovimiento == control.nickname2)
                                            {
                                                control.dinero2 = control.dinero2 + 200;
                                                streamw.WriteLine("005Y" + nickmovimiento + "-" + mover + "-" + 2 + "-" + control.dinero2);
                                                streamw.Flush();
                                            }
                                            if (nickmovimiento == control.nickname3)
                                            {
                                                control.dinero3 = control.dinero3 + 200;
                                                streamw.WriteLine("005Y" + nickmovimiento + "-" + mover + "-" + 2 + "-" + control.dinero3);
                                                streamw.Flush();
                                            }
                                            if (nickmovimiento == control.nickname4)
                                            {
                                                control.dinero4 = control.dinero4 + 200;
                                                streamw.WriteLine("005Y" + nickmovimiento + "-" + mover + "-" + 2 + "-" + control.dinero4);
                                                streamw.Flush();
                                            }
                                        }
                                        break;
                                    case "006": //Verificar casillas
                                        string paraverificar = mensaje.Substring(3); // nick-idcasilla-blabalbla
                                        string[] separadasverificar = paraverificar.Split('-'); //[nick][idcasilla][blablabla]
                                        string nickverificar = separadasverificar[0];
                                        int idcasilla = int.Parse(separadasverificar[1]);
                                        //LLAMADA A VERIFICAR
                                        string nickcasilla = control.verificarCasilla(idcasilla, nickverificar); //[codigo][dinerocambiado][nick][propiedad][dineropagado][nickpagado]
                                        string[] separadasnickcasilla = nickcasilla.Split('-'); //[codigo][dinerocambiado][nick][propiedad][dineropagado][nickpagado]
                                        int codigo = int.Parse(separadasnickcasilla[0]);
                                        if (codigo == 1)
                                        {
                                            int dinerocambiado = int.Parse(separadasnickcasilla[1]);
                                            string nickdinero = separadasnickcasilla[2];
                                            streamw.WriteLine("006Y" + codigo + "-" + dinerocambiado + "-" + nickdinero);
                                            streamw.Flush();
                                        }
                                        if (codigo == 2)
                                        {
                                            int dinerocambiado = int.Parse(separadasnickcasilla[1]);
                                            string nickdinero = separadasnickcasilla[2];
                                            int propiedad = int.Parse(separadasnickcasilla[3]);
                                            streamw.WriteLine("006Y" + codigo + "-" + dinerocambiado + "-" + nickdinero + "-" + propiedad);
                                            streamw.Flush();
                                        }
                                        if (codigo == 3)
                                        {
                                            int dinerocambiado = int.Parse(separadasnickcasilla[1]);
                                            string nickdinero = separadasnickcasilla[2];
                                            int propiedad = int.Parse(separadasnickcasilla[3]);
                                            int dineropagado = int.Parse(separadasnickcasilla[4]);
                                            string nickpagado = separadasnickcasilla[5];
                                            streamw.WriteLine("006Y" + codigo + "-" + dinerocambiado + "-" + nickdinero + "-" + propiedad + "-" + dineropagado + "-" + nickpagado);
                                            streamw.Flush();
                                        }
                                        if (codigo == 4)
                                        {
                                            int dinerocambiado = int.Parse(separadasnickcasilla[1]);
                                            string nickdinero = separadasnickcasilla[2];
                                            string tipo = separadasnickcasilla[3];
                                            int propiedad = int.Parse(separadasnickcasilla[4]);
                                            streamw.WriteLine("006Y" + codigo + "-" + dinerocambiado + "-" + nickdinero + "-" + tipo + "-" + propiedad);
                                            streamw.Flush();
                                        }
                                        if (codigo == 5)
                                        {
                                            int dinerocambiado = int.Parse(separadasnickcasilla[1]);
                                            string nickdinero = separadasnickcasilla[2];
                                            string tarjeta = separadasnickcasilla[3];
                                            streamw.WriteLine("006Y" + codigo + "-" + dinerocambiado + "-" + nickdinero + "-" + tarjeta);
                                            streamw.Flush();
                                        }
                                        /* int dinero = int.Parse(separadasnickcasilla[0]);
                                         string nickdinero = separadasnickcasilla[1];
                                         streamw.WriteLine("006Y" + dinero + "-" + nickdinero);
                                         streamw.Flush();*/
                                        break;
                                    case "007":
                                        string verbancarota = mensaje.Substring(3); //nick-bancarota
                                        string[] sepbancarota = verbancarota.Split('-'); //[nick][bancarota]
                                        string nicknamebanca = sepbancarota[0];
                                        string bancarota = sepbancarota[1];
                                        string ganador = control.verificarGanador(nicknamebanca, bancarota);
                                        if(ganador != "Nadie")
                                        { 
                                            control.sendBroadCast(usuarios, "007Y" + ganador);
                                            streamw.Flush();
                                        }
                                        break;
                                    case "008":
                                        break;
                                    case "009": //Refrescar tableros
                                        string pararefrescar = mensaje.Substring(3); // nick-posx-posy-blablablabla
                                        string[] separadasfrescar = pararefrescar.Split('-'); //[nick][posx][posy][blablabla]
                                        string nickname = separadasfrescar[0];
                                        string posx = separadasfrescar[1];
                                        string posy = separadasfrescar[2];
                                        string nickname1 = nickname;
                                        int posx1 = int.Parse(posx);
                                        int posy1 = int.Parse(posy);
                                        control.sendBroadCast(usuarios, "009Y" + nickname1 + "-" + posx1 + "-" + posy1);
                                        streamw.Flush();
                                        break;
                                    case "010": //Pasar turnos
                                        string parapasar = mensaje.Substring(3); // turno-blablabla
                                        string[] separadaspasar = parapasar.Split('-'); //[turno][blablabla]
                                        int turnop = int.Parse(separadaspasar[0]);
                                        control.contador4 = 4;
                                        if (control.contador == 0)
                                        {
                                            if (control.turnoactual == turnop)
                                            {
                                                control.contador = 1;
                                                control.pasarTurno(usuarios);
                                            }
                                        }
                                        if(control.contador4 == 4)
                                        {
                                            control.contador = 0;
                                            control.contador4 = 0;
                                        }
                                        break;
                                    case "011":
                                        break;
                                    case "012":
                                        break;
                                    case "013":
                                        break;
                                    case "014":
                                        break;
                                    case "015":
                                        break;
                                }
                            }
                        }
                        catch (Exception e)
                        {

                            Console.WriteLine("Error " + e);
                            streamr.Close();
                            networkStream.Close();
                            streamw.Close();
                            break;
                        };
                    }


                }
                Console.WriteLine("EL SOCKET SE DESCONECTO");


            }

        }

        public static void Main()
        {

            tcpListener.Start();
            Console.WriteLine("************Este es el Servidor de UCAB Poly************");
            for (int i = 0; i < 4; i++)
            {
                Thread newThread = new Thread(new ThreadStart(Listeners));
                newThread.Start();
            }
            Console.WriteLine("**** ESPERANDO CONEXION DE LOS JUGADORES ****");

            // Controladora control = new Controladora();


            /*   

                  control.SetupInicial(cantidadJugadores); //Le asigna a cada jugador en control un numero de turno

                  //mientras haya almenos 2 jugadores activos
                  while (control.CheckCondicionVictoria()!=0)
                  {
                      control.turno = control.turno + 1;
                      numeroJugadas = numeroJugadas + 1;
                      //CheckCondicionVictoria devuelve 0 si se cumple la victoria o el numero de jugadores activos si no.
                      if (control.turno > control.CheckCondicionVictoria())
                          control.turno = 1;
                      //Notificar A los clientes el turno
                      //Comienza el juego
                  }
                  System.Console.ReadLine();*/



        }
    }
}
