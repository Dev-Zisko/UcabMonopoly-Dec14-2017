using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace ServidorUcabPoly
{
    class Controladora
    {
        Random random = new Random();
        public string nickname1, nickname2, nickname3, nickname4;
        public int posx1, posx2, posx3, posx4;
        public int posy1, posy2, posy3, posy4;
        public int turno1, turno2, turno3, turno4, turnoactual = 0;
        public string estado1, estado2, estado3, estado4;
        public int dinero1 = 1500, dinero2 = 1500, dinero3 = 1500, dinero4 = 1500;
        public int dado1, dado2, dado3, dado4, ultimodado = 0;
        public string tarjeta1="No", tarjeta2 = "No", tarjeta3 = "No", tarjeta4 = "No";
        public string nickdueño2 = "", nickdueño4 = "", nickdueño7 = "", nickdueño9 = "", nickdueño10 = "", nickdueño12 = "", nickdueño14 = "", nickdueño15 = "", nickdueño17 = "", nickdueño19 = "", nickdueño20 = "";
        public string nickdueño22 = "", nickdueño24 = "", nickdueño25 = "", nickdueño27 = "", nickdueño28 = "", nickdueño30 = "", nickdueño32 = "", nickdueño33 = "", nickdueño35 = "", nickdueño38 = "", nickdueño40 = "";
        public string nickdueño6 = "", nickdueño13 = "", nickdueño16 = "", nickdueño26 = "", nickdueño29 = "", nickdueño36 = "";
        public string casadueño2 = "", casadueño4 = "", casadueño7 = "", casadueño9 = "", casadueño10 = "", casadueño12 = "", casadueño14 = "", casadueño15 = "", casadueño17 = "", casadueño19 = "", casadueño20 = "";
        public string casadueño22 = "", casadueño24 = "", casadueño25 = "", casadueño27 = "", casadueño28 = "", casadueño30 = "", casadueño32 = "", casadueño33 = "", casadueño35 = "", casadueño38 = "", casadueño40 = "";
        public string casadueño6 = "", casadueño13 = "", casadueño16 = "", casadueño26 = "", casadueño29 = "", casadueño36 = "";
        public string hoteldueño2 = "", hoteldueño4 = "", hoteldueño7 = "", hoteldueño9 = "", hoteldueño10 = "", hoteldueño12 = "", hoteldueño14 = "", hoteldueño15 = "", hoteldueño17 = "", hoteldueño19 = "", hoteldueño20 = "";
        public string hoteldueño22 = "", hoteldueño24 = "", hoteldueño25 = "", hoteldueño27 = "", hoteldueño28 = "", hoteldueño30 = "", hoteldueño32 = "", hoteldueño33 = "", hoteldueño35 = "", hoteldueño38 = "", hoteldueño40 = "";
        public string hoteldueño6 = "", hoteldueño13 = "", hoteldueño16 = "", hoteldueño26 = "", hoteldueño29 = "", hoteldueño36 = "";
        public int contador = 0;
        public int contador4 = 0;
        public string ganador;
        public string bancarota1 = "No", bancarota2 = "No", bancarota3 = "No", bancarota4 = "No";

        public string verificarGanador(string nickname, string bancarota)
        {
            if(nickname == nickname1 && bancarota == "Si")
            {
                bancarota1 = "Si";
            }
            if (nickname == nickname2 && bancarota == "Si")
            {
                bancarota2 = "Si";
            }
            if (nickname == nickname3 && bancarota == "Si")
            {
                bancarota3 = "Si";
            }
            if (nickname == nickname4 && bancarota == "Si")
            {
                bancarota4 = "Si";
            }

            if(bancarota1 == "No" && bancarota2 == "Si" && bancarota3 == "Si" && bancarota4 == "Si")
            {
                return nickname1;
            }

            if (bancarota1 == "Si" && bancarota2 == "No" && bancarota3 == "Si" && bancarota4 == "Si")
            {
                return nickname2;
            }
            if (bancarota1 == "Si" && bancarota2 == "Si" && bancarota3 == "No" && bancarota4 == "Si")
            {
                return nickname3;
            }
            if (bancarota1 == "Si" && bancarota2 == "Si" && bancarota3 == "Si" && bancarota4 == "No")
            {
                return nickname4;
            }
            return "Nadie";
        }

        public void asignarJugadores(string usuario, int numeroJugador)
        {
            switch (numeroJugador)
            {
                case 1:
                    nickname1 = usuario;
                    posx1 = 0;
                    posy1 = 0;
                    turno1 = numeroJugador;
                    estado1 = "Libre";
                    dinero1 = 1500;
                    dado1 = 0;
                    break;
                case 2:
                    nickname2 = usuario;
                    posx2 = 0;
                    posy2 = 0;
                    turno2 = numeroJugador;
                    estado2 = "Libre";
                    dinero2 = 1500;
                    dado1 = 0;
                    break;
                case 3:
                    nickname3 = usuario;
                    posx3 = 0;
                    posy3 = 0;
                    turno3 = numeroJugador;
                    estado3 = "Libre";
                    dinero3 = 1500;
                    dado3 = 0;
                    break;
                case 4:
                    nickname4 = usuario;
                    posx4 = 0;
                    posy4 = 0;
                    turno4 = numeroJugador;
                    estado4 = "Libre";
                    dinero4 = 1500;
                    dado4 = 0;
                    break;
            }
        }

        public void cerrarConexion(List<Socket> usuarios, Socket socketForClient)
        {

            usuarios.Remove(socketForClient);
            Console.WriteLine("** Se desconecto del servidor: " + IPAddress.Parse(((IPEndPoint)socketForClient.RemoteEndPoint).Address.ToString()) + "::" + ((IPEndPoint)socketForClient.RemoteEndPoint).Port.ToString() + "**");
            socketForClient.Close();

            if (usuarios.Count() == 0)
            {
                Console.WriteLine("Se han cerrado todas las conexiones");
            }
        }

        public void sendBroadCast(List<Socket> sockets, string mensaje)
        {
            // Console.WriteLine("Mensaje a enviar:" + mensaje); //borrar
            NetworkStream networkStream;
            System.IO.StreamWriter streamw;
            foreach (Socket s in sockets)
            {
                networkStream = new NetworkStream(s);
                streamw = new System.IO.StreamWriter(networkStream);
                // Console.WriteLine("Trama: "+mensaje); //borrar
                streamw.WriteLine(mensaje);
                streamw.Flush();
            }
        }

        public void asignarTurnos(string jugador1, string jugador2, string jugador3, string jugador4)
        {
            if (jugador1 == "Azul")
            {
                turno1 = 1;
            }
            if (jugador2 == "Morado")
            {
                turno2 = 2;
            }
            if (jugador3 == "Verde")
            {
                turno3 = 3;
            }
            if (jugador4 == "Rojo")
            {
                turno4 = 4;
            }
        }

        public void pasarTurno(List<Socket> usuarios)
        {
            if ((turnoactual < 4) && (turnoactual > 0))
                turnoactual = turnoactual + 1;
            else
                turnoactual = 1;
            Console.WriteLine("*** #TURNO: " + turnoactual);
            sendBroadCast(usuarios, "003Y" + turnoactual);
            //Notificar Turno a Cientes
            //Esperar Accion Cliente
        }

        public void rollDice()
        {

            int valor = random.Next(1, 7);
            ultimodado = valor;
            //Notificar a los jugadores para FakeRollDice !!!!
        }

        public string moverse(int dado, int casilla)
        {
            int mover;
            mover = dado + casilla;
            if (mover > 40)
            {
                mover = mover - 40;
                return ("2" + "-" + mover);
            }
            return ("1" +  "-" + mover);
        }

        //METODOS DE CAFARO Y ALBERTO

        public string verificarCasilla(int numerocasilla, string nick)
        {
            switch (numerocasilla)
            {
                case 1: //SALIDA
                    if(nick == nickname1)
                    {
                        dinero1 = dinero1;
                        return ("1"+ "-" + dinero1 + "-" + nickname1);
                    }
                    if (nick == nickname2)
                    {
                        dinero2 = dinero2;
                        return ("1" + "-" + dinero2 + "-" + nickname2);
                    }
                    if (nick == nickname3)
                    {
                        dinero3 = dinero3;
                        return ("1" + "-" + dinero3 + "-" + nickname3);
                    }
                    if (nick == nickname4)
                    {
                        dinero4 = dinero4;
                        return ("1" + "-" + dinero4 + "-" + nickname4);
                    }
                    break;
                case 2: //SOLAR---POST GRADO
                    if (nickdueño2 == "")
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 60)
                            {
                                dinero1 = dinero1 - 60;
                                nickdueño2 = nickname1;
                                return ("2" + "-" + dinero1 + "-" + nickname1 + "-" + 2);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname2)
                        {
                            if (dinero2 >= 60)
                            {
                                dinero2 = dinero2 - 60;
                                nickdueño2 = nickname2;
                                return ("2" + "-" + dinero2 + "-" + nickname2 + "-" + 2);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname3)
                        {
                            if (dinero3 >= 60)
                            {
                                dinero3 = dinero3 - 60;
                                nickdueño2 = nickname3;
                                return ("2" + "-" + dinero3 + "-" + nickname3 + "-" + 2);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname4)
                        {
                            if (dinero4 >= 60)
                            {
                                dinero4 = dinero4 - 60;
                                nickdueño2 = nickname4;
                                return ("2" + "-" + dinero4 + "-" + nickname4 + "-" + 2);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                    }
                    else
                    {
                        if (nick == nickdueño2)
                        {
                            if (nick == nickname1)
                            {
                            	if(nick == nickdueño2 && nick == nickdueño4)
                            	{
                                	if(casadueño2 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero1 >= 10)
                                    	{ 
                                        	dinero1 = dinero1 - 10; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño2 = nickname1;
                                        	return ("6" + "-" + dinero1 + "-" + nickname1 + "-" + 2);
                                    	}
                                	}
                                	else if (casadueño2 == nickname1 && hoteldueño2 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero1 >= 250)
                                    	{
                                        	dinero1 = dinero1 - 250; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño2 = nickname1;
                                        	return ("7" + "-" + dinero1 + "-" + nickname1 + "-" + 2);
                                    	}
                                	}
                                	else if (casadueño2 == nickname1 && hoteldueño2 == nickname1) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero1 = dinero1;
                                    	return ("1" + "-" + dinero1 + "-" + nickname1);
                                	}
                                }
                            }
                            if (nick == nickname2)
                            {
                                if(nick == nickdueño2 && nick == nickdueño4)
                            	{
                                	if(casadueño2 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero2 >= 10)
                                    	{ 
                                        	dinero2 = dinero2 - 10; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño2 = nickname2;
                                        	return ("6" + "-" + dinero2 + "-" + nickname2 + "-" + 2);
                                    	}
                                	}
                                	else if (casadueño2 == nickname2 && hoteldueño2 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero2 >= 250)
                                    	{
                                        	dinero2 = dinero2 - 250; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño2 = nickname2;
                                        	return ("7" + "-" + dinero2 + "-" + nickname2 + "-" + 2);
                                    	}
                                	}
                                	else if (casadueño2 == nickname2 && hoteldueño2 == nickname2) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero2 = dinero2;
                                    	return ("1" + "-" + dinero2 + "-" + nickname2);
                                	}
                                }
                            }
                            if (nick == nickname3)
                            {
                                if(nick == nickdueño2 && nick == nickdueño4)
                            	{
                                	if(casadueño2 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero3 >= 10)
                                    	{ 
                                        	dinero3 = dinero3 - 10; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño2 = nickname3;
                                        	return ("6" + "-" + dinero3 + "-" + nickname3 + "-" + 2);
                                    	}
                                	}
                                	else if (casadueño2 == nickname3 && hoteldueño2 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero3 >= 250)
                                    	{
                                        	dinero3 = dinero3 - 250; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño2 = nickname3;
                                        	return ("7" + "-" + dinero3 + "-" + nickname3 + "-" + 2);
                                    	}
                                	}
                                	else if (casadueño2 == nickname3 && hoteldueño2 == nickname3) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero3 = dinero3;
                                    	return ("1" + "-" + dinero3 + "-" + nickname3);
                                	}
                                }
                            }
                            if (nick == nickname4)
                            {
                                if(nick == nickdueño2 && nick == nickdueño4)
                            	{
                                	if(casadueño2 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero4 >= 10)
                                    	{ 
                                        	dinero4 = dinero4 - 10; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño2 = nickname4;
                                        	return ("6" + "-" + dinero4 + "-" + nickname4 + "-" + 2);
                                    	}
                                	}
                                	else if (casadueño2 == nickname4 && hoteldueño2 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero4 >= 250)
                                    	{
                                        	dinero4 = dinero4 - 250; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño2 = nickname4;
                                        	return ("7" + "-" + dinero4 + "-" + nickname4 + "-" + 2);
                                    	}
                                	}
                                	else if (casadueño2 == nickname4 && hoteldueño2 == nickname4) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero4 = dinero4;
                                    	return ("1" + "-" + dinero4 + "-" + nickname4);
                                	}
                                }
                            }
                        }
                        else
                        {
                            if (nickdueño2 == nickname1)
                            {
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 2;
                                    dinero1 = dinero1 + 2;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 2 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 2;
                                    dinero1 = dinero1 + 2;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 2 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 2;
                                    dinero1 = dinero1 + 2;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 2 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño2 == nickname2)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 2;
                                    dinero2 = dinero2 + 2;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 2 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 2;
                                    dinero2 = dinero2 + 2;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 2 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 2;
                                    dinero2 = dinero2 + 2;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 2 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño2 == nickname3)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 2;
                                    dinero3 = dinero3 + 2;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 2 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 2;
                                    dinero3 = dinero3 + 2;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 2 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 2;
                                    dinero3 = dinero3 + 2;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 2 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño2 == nickname4)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 2;
                                    dinero4 = dinero4 + 2;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 2 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 2;
                                    dinero4 = dinero4 + 2;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 2 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 2;
                                    dinero4 = dinero4 + 2;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 2 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                            }
                        }
                    }
                    break;
                case 3: //TESORERÍA UCAB
                    int tesoreria = random.Next(1, 9);
                    if (tesoreria == 1)
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 150)
                            {
                                dinero1 = dinero1 - 150;
                                return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Tesoreria" + "-" + 1);
                            }
                        }
                        if (nick == nickname2)
                        {
                            if (dinero2 >= 150)
                            {
                                dinero2 = dinero2 - 150;
                                return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Tesoreria" + "-" + 1);
                            }
                        }
                        if (nick == nickname3)
                        {
                            if (dinero3 >= 150)
                            {
                                dinero3 = dinero3 - 150;
                                return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Tesoreria" + "-" + 1);
                            }
                        }
                        if (nick == nickname4)
                        {
                            if (dinero4 >= 150)
                            {
                                dinero4 = dinero4 - 150;
                                return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Tesoreria" + "-" + 1);
                            }
                        }

                    }
                    if (tesoreria == 2)
                    {
                        if (nick == nickname1)
                        {
                            dinero1 = dinero1 + 200;
                            return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Tesoreria" + "-" + 2);
                        }
                        if (nick == nickname2)
                        {
                            dinero2 = dinero2 + 200;
                            return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Tesoreria" + "-" + 2);
                        }
                        if (nick == nickname3)
                        {
                            dinero3 = dinero3 + 200;
                            return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Tesoreria" + "-" + 2);
                        }
                        if (nick == nickname4)
                        {
                            dinero4 = dinero4 + 200;
                            return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Tesoreria" + "-" + 2);
                        }
                    }
                    if (tesoreria == 3)
                    {
                        if (nick == nickname1)
                        {
                            dinero1 = dinero1 + 100;
                            return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Tesoreria" + "-" + 3);
                        }
                        if (nick == nickname2)
                        {
                            dinero2 = dinero2 + 100;
                            return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Tesoreria" + "-" + 3);
                        }
                        if (nick == nickname3)
                        {
                            dinero3 = dinero3 + 100;
                            return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Tesoreria" + "-" + 3);
                        }
                        if (nick == nickname4)
                        {
                            dinero4 = dinero4 + 100;
                            return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Tesoreria" + "-" + 3);
                        }
                    }
                    if (tesoreria == 4)
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 100)
                            {
                                dinero1 = dinero1 - 100;
                                return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Tesoreria" + "-" + 4);
                            }
                        }
                        if (nick == nickname2)
                        {
                            if (dinero2 >= 100)
                            {
                                dinero2 = dinero2 - 100;
                                return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Tesoreria" + "-" + 4);
                            }
                        }
                        if (nick == nickname3)
                        {
                            if (dinero3 >= 100)
                            {
                                dinero3 = dinero3 - 100;
                                return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Tesoreria" + "-" + 4);
                            }
                        }
                        if (nick == nickname4)
                        {
                            if (dinero4 >= 100)
                            {
                                dinero4 = dinero4 - 100;
                                return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Tesoreria" + "-" + 4);
                            }
                        }
                    }
                    if (tesoreria == 5)
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 100)
                            {
                                dinero1 = dinero1 - 100;
                                return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Tesoreria" + "-" + 5);
                            }
                        }
                        if (nick == nickname2)
                        {
                            if (dinero2 >= 100)
                            {
                                dinero2 = dinero2 - 100;
                                return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Tesoreria" + "-" + 5);
                            }
                        }
                        if (nick == nickname3)
                        {
                            if (dinero3 >= 100)
                            {
                                dinero3 = dinero3 - 100;
                                return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Tesoreria" + "-" + 5);
                            }
                        }
                        if (nick == nickname4)
                        {
                            if (dinero4 >= 100)
                            {
                                dinero4 = dinero4 - 100;
                                return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Tesoreria" + "-" + 5);
                            }
                        }
                    }
                    if (tesoreria == 6)
                    {
                        if (nick == nickname1)
                        {
                            dinero1 = dinero1 + 150;
                            return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Tesoreria" + "-" + 6);
                        }
                        if (nick == nickname2)
                        {
                            dinero2 = dinero2 + 150;
                            return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Tesoreria" + "-" + 6);
                        }
                        if (nick == nickname3)
                        {
                            dinero3 = dinero3 + 150;
                            return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Tesoreria" + "-" + 6);
                        }
                        if (nick == nickname4)
                        {
                            dinero4 = dinero4 + 150;
                            return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Tesoreria" + "-" + 6);
                        }
                    }
                    if (tesoreria == 7)
                    {
                        if (nick == nickname1)
                        {
                            dinero1 = dinero1 + 10;
                            return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Tesoreria" + "-" + 7);
                        }
                        if (nick == nickname2)
                        {
                            dinero2 = dinero2 + 10;
                            return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Tesoreria" + "-" + 7);
                        }
                        if (nick == nickname3)
                        {
                            dinero3 = dinero3 + 10;
                            return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Tesoreria" + "-" + 7);
                        }
                        if (nick == nickname4)
                        {
                            dinero4 = dinero4 + 10;
                            return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Tesoreria" + "-" + 7);
                        }
                    }
                    if (tesoreria == 8)
                    {
                        if (nick == nickname1)
                        {
                            dinero1 = dinero1 + 200;
                            return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Tesoreria" + "-" + 8);
                        }
                        if (nick == nickname2)
                        {
                            dinero2 = dinero2 + 200;
                            return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Tesoreria" + "-" + 8);
                        }
                        if (nick == nickname3)
                        {
                            dinero3 = dinero3 + 200;
                            return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Tesoreria" + "-" + 8);
                        }
                        if (nick == nickname4)
                        {
                            dinero4 = dinero4 + 200;
                            return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Tesoreria" + "-" + 8);
                        }
                    }
                    break;
                case 4: //SOLAR---FERIA
                    if (nickdueño4 == "")
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 60)
                            {
                                dinero1 = dinero1 - 60;
                                nickdueño4 = nickname1;
                                return ("2" + "-" + dinero1 + "-" + nickname1 + "-" + 4);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname2)
                        {
                            if (dinero2 >= 60)
                            {
                                dinero2 = dinero2 - 60;
                                nickdueño4 = nickname2;
                                return ("2" + "-" + dinero2 + "-" + nickname2 + "-" + 4);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname3)
                        {
                            if (dinero3 >= 60)
                            {
                                dinero3 = dinero3 - 60;
                                nickdueño4 = nickname3;
                                return ("2" + "-" + dinero3 + "-" + nickname3 + "-" + 4);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname4)
                        {
                            if (dinero4 >= 60)
                            {
                                dinero4 = dinero4 - 60;
                                nickdueño4 = nickname4;
                                return ("2" + "-" + dinero4 + "-" + nickname4 + "-" + 4);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                    }
                    else
                    {
                        if (nick == nickdueño4)
                        {
                            if (nick == nickname1)
                            {
                                if(nick == nickdueño2 && nick == nickdueño4)
                            	{
                                	if(casadueño4 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero1 >= 20)
                                    	{ 
                                        	dinero1 = dinero1 - 20; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño4 = nickname1;
                                        	return ("6" + "-" + dinero1 + "-" + nickname1 + "-" + 2);
                                    	}
                                	}
                                	else if (casadueño4 == nickname1 && hoteldueño4 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero1 >= 450)
                                    	{
                                        	dinero1 = dinero1 - 450; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño4 = nickname1;
                                        	return ("7" + "-" + dinero1 + "-" + nickname1 + "-" + 2);
                                    	}
                                	}
                                	else if (casadueño4 == nickname1 && hoteldueño4 == nickname1) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero1 = dinero1;
                                    	return ("1" + "-" + dinero1 + "-" + nickname1);
                                	}
                                }
                            }
                            if (nick == nickname2)
                            {
                                if(nick == nickdueño2 && nick == nickdueño4)
                            	{
                                	if(casadueño4 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero2 >= 20)
                                    	{ 
                                        	dinero2 = dinero2 - 20; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño4 = nickname2;
                                        	return ("6" + "-" + dinero2 + "-" + nickname2 + "-" + 4);
                                    	}
                                	}
                                	else if (casadueño4 == nickname2 && hoteldueño4 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero2 >= 450)
                                    	{
                                        	dinero2 = dinero2 - 450; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño4 = nickname2;
                                        	return ("7" + "-" + dinero2 + "-" + nickname2 + "-" + 4);
                                    	}
                                	}
                                	else if (casadueño4 == nickname2 && hoteldueño4 == nickname2) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero2 = dinero2;
                                    	return ("1" + "-" + dinero2 + "-" + nickname2);
                                	}
                                }
                            }
                            if (nick == nickname3)
                            {
                                if(nick == nickdueño2 && nick == nickdueño4)
                            	{
                                	if(casadueño4 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero3 >= 20)
                                    	{ 
                                        	dinero3 = dinero3 - 20; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño4 = nickname3;
                                        	return ("6" + "-" + dinero3 + "-" + nickname3 + "-" + 4);
                                    	}
                                	}
                                	else if (casadueño4 == nickname3 && hoteldueño4 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero3 >= 450)
                                    	{
                                        	dinero3 = dinero3 - 450; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño4 = nickname3;
                                        	return ("7" + "-" + dinero3 + "-" + nickname3 + "-" + 4);
                                    	}
                                	}
                                	else if (casadueño4 == nickname3 && hoteldueño4 == nickname3) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero3 = dinero3;
                                    	return ("1" + "-" + dinero3 + "-" + nickname3);
                                	}
                                }
                            }
                            if (nick == nickname4)
                            {
                                if(nick == nickdueño2 && nick == nickdueño4)
                            	{
                                	if(casadueño4 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero4 >= 20)
                                    	{ 
                                        	dinero4 = dinero4 - 20; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño4 = nickname4;
                                        	return ("6" + "-" + dinero4 + "-" + nickname4 + "-" + 4);
                                    	}
                                	}
                                	else if (casadueño4 == nickname4 && hoteldueño4 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero4 >= 450)
                                    	{
                                        	dinero4 = dinero4 - 450; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño4 = nickname4;
                                        	return ("7" + "-" + dinero4 + "-" + nickname4 + "-" + 4);
                                    	}
                                	}
                                	else if (casadueño4 == nickname4 && hoteldueño4 == nickname4) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero4 = dinero4;
                                    	return ("1" + "-" + dinero4 + "-" + nickname4);
                                	}
                                }
                            }
                        }
                        else
                        {
                            if (nickdueño4 == nickname1)
                            {
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 4;
                                    dinero1 = dinero1 + 4;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 4 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 4;
                                    dinero1 = dinero1 + 4;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 4 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 4;
                                    dinero1 = dinero1 + 4;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 4 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño4 == nickname2)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 4;
                                    dinero2 = dinero2 + 4;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 4 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 4;
                                    dinero2 = dinero2 + 4;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 4 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 4;
                                    dinero2 = dinero2 + 4;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 4 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño4 == nickname3)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 4;
                                    dinero3 = dinero3 + 4;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 4 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 4;
                                    dinero3 = dinero3 + 4;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 4 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 4;
                                    dinero3 = dinero3 + 4;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 4 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño4 == nickname4)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 4;
                                    dinero4 = dinero4 + 4;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 4 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 4;
                                    dinero4 = dinero4 + 4;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 4 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 4;
                                    dinero4 = dinero4 + 4;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 4 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                            }
                        }
                    }
                    break;
                case 5: //IMPUESTO---PAGO MATRÍCULA
                    if (nick == nickname1)
                    {
                        if (dinero1 >= 200)
                        {
                            dinero1 = dinero1 - 200;
                            return ("1" + "-" + dinero1 + "-" + nickname1);
                        }
                        else
                        {
                            return "0";
                        }

                    }
                    if (nick == nickname2)
                    {
                        if (dinero2 >= 200)
                        {
                            dinero2 = dinero2 - 200;
                            return ("1" + "-" + dinero2 + "-" + nickname2);
                        }
                        else
                        {
                            return "0";
                        }
                    }
                    if (nick == nickname3)
                    {
                        if (dinero3 >= 200)
                        {
                            dinero3 = dinero3 - 200;
                            return ("1" + "-" + dinero3 + "-" + nickname3);
                        }
                        else
                        {
                            return "0";
                        }
                    }
                    if (nick == nickname4)
                    {
                        if (dinero4 >= 200)
                        {
                            dinero4 = dinero4 - 200;
                            return ("1" + "-" + dinero4 + "-" + nickname4);
                        }
                        else
                        {
                            return "0";
                        }
                    }
                    break;
                case 6: //TAXI UCAB SUR
                    if (nickdueño6 == "")
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 200)
                            {
                                dinero1 = dinero1 - 200;
                                nickdueño6 = nickname1;
                                return ("2" + "-" + dinero1 + "-" + nickname1 + "-" + 6);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname2)
                        {
                            if (dinero2 >= 200)
                            {
                                dinero2 = dinero2 - 200;
                                nickdueño6 = nickname2;
                                return ("2" + "-" + dinero2 + "-" + nickname2 + "-" + 6);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname3)
                        {
                            if (dinero3 >= 200)
                            {
                                dinero3 = dinero3 - 200;
                                nickdueño6 = nickname3;
                                return ("2" + "-" + dinero3 + "-" + nickname3 + "-" + 6);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname4)
                        {
                            if (dinero4 >= 200)
                            {
                                dinero4 = dinero4 - 200;
                                nickdueño6 = nickname4;
                                return ("2" + "-" + dinero4 + "-" + nickname4 + "-" + 6);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                    }
                    else
                    {
                        if (nick == nickdueño6)
                        {
                            if (nick == nickname1)
                            {
                                dinero1 = dinero1;
                                return ("1" + "-" + dinero1 + "-" + nickname1);
                            }
                            if (nick == nickname2)
                            {
                                dinero2 = dinero2;
                                return ("1" + "-" + dinero2 + "-" + nickname2);
                            }
                            if (nick == nickname3)
                            {
                                dinero3 = dinero3;
                                return ("1" + "-" + dinero3 + "-" + nickname3);
                            }
                            if (nick == nickname4)
                            {
                                dinero4 = dinero4;
                                return ("1" + "-" + dinero4 + "-" + nickname4);
                            }
                        }
                        else
                        {
                            if (nickdueño6 == nickname1)
                            {
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 25;
                                    dinero1 = dinero1 + 25;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 25 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 25;
                                    dinero1 = dinero1 + 25;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 25 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 25;
                                    dinero1 = dinero1 + 25;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 25 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño6 == nickname2)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 25;
                                    dinero2 = dinero2 + 25;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 25 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 25;
                                    dinero2 = dinero2 + 25;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 25 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 25;
                                    dinero2 = dinero2 + 25;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 25 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño6 == nickname3)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 25;
                                    dinero3 = dinero3 + 25;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 25 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 25;
                                    dinero3 = dinero3 + 25;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 25 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 25;
                                    dinero3 = dinero3 + 25;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 25 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño6 == nickname4)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 25;
                                    dinero4 = dinero4 + 25;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 25 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 25;
                                    dinero4 = dinero4 + 25;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 25 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 25;
                                    dinero4 = dinero4 + 25;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 25 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                            }
                        }
                    }
                    break;
                case 7: //SOLAR---MODULO 6
                    if (nickdueño7 == "")
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 100)
                            {
                                dinero1 = dinero1 - 100;
                                nickdueño7 = nickname1;
                                return ("2" + "-" + dinero1 + "-" + nickname1 + "-" + 7);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname2)
                        {
                            if (dinero2 >= 100)
                            {
                                dinero2 = dinero2 - 100;
                                nickdueño7 = nickname2;
                                return ("2" + "-" + dinero2 + "-" + nickname2 + "-" + 7);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname3)
                        {
                            if (dinero3 >= 100)
                            {
                                dinero3 = dinero3 - 100;
                                nickdueño7 = nickname3;
                                return ("2" + "-" + dinero3 + "-" + nickname3 + "-" + 7);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname4)
                        {
                            if (dinero4 >= 100)
                            {
                                dinero4 = dinero4 - 100;
                                nickdueño7 = nickname4;
                                return ("2" + "-" + dinero4 + "-" + nickname4 + "-" + 7);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                    }
                    else
                    {
                        if (nick == nickdueño7)
                        {
                            if (nick == nickname1)
                            {
                                if(nick == nickdueño7 && nick == nickdueño9 && nick == nickdueño10)
                            	{
                                	if(casadueño7 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero1 >= 30)
                                    	{ 
                                        	dinero1 = dinero1 - 30; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño7 = nickname1;
                                        	return ("6" + "-" + dinero1 + "-" + nickname1 + "-" + 7);
                                    	}
                                	}
                                	else if (casadueño7 == nickname1 && hoteldueño7 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero1 >= 550)
                                    	{
                                        	dinero1 = dinero1 - 550; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño7 = nickname1;
                                        	return ("7" + "-" + dinero1 + "-" + nickname1 + "-" + 7);
                                    	}
                                	}
                                	else if (casadueño7 == nickname1 && hoteldueño7 == nickname1) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero1 = dinero1;
                                    	return ("1" + "-" + dinero1 + "-" + nickname1);
                                	}
                                }
                            }
                            if (nick == nickname2)
                            {
                                if(nick == nickdueño7 && nick == nickdueño9 && nick == nickdueño10)
                            	{
                                	if(casadueño7 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero2 >= 30)
                                    	{ 
                                        	dinero2 = dinero2 - 30; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño7 = nickname2;
                                        	return ("6" + "-" + dinero2 + "-" + nickname2 + "-" + 7);
                                    	}
                                	}
                                	else if (casadueño7 == nickname2 && hoteldueño7 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero2 >= 550)
                                    	{
                                        	dinero2 = dinero2 - 550; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño7 = nickname2;
                                        	return ("7" + "-" + dinero2 + "-" + nickname2 + "-" + 7);
                                    	}
                                	}
                                	else if (casadueño7 == nickname2 && hoteldueño7 == nickname2) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero2 = dinero2;
                                    	return ("1" + "-" + dinero2 + "-" + nickname2);
                                	}
                                }
                            }
                            if (nick == nickname3)
                            {
                                if(nick == nickdueño7 && nick == nickdueño9 && nick == nickdueño10)
                            	{
                                	if(casadueño7 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero3 >= 30)
                                    	{ 
                                        	dinero3 = dinero3 - 30; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño7 = nickname3;
                                        	return ("6" + "-" + dinero3 + "-" + nickname3 + "-" + 7);
                                    	}
                                	}
                                	else if (casadueño7 == nickname3 && hoteldueño7 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero3 >= 550)
                                    	{
                                        	dinero3 = dinero3 - 550; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño7 = nickname3;
                                        	return ("7" + "-" + dinero3 + "-" + nickname3 + "-" + 7);
                                    	}
                                	}
                                	else if (casadueño7 == nickname3 && hoteldueño7 == nickname3) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero3 = dinero3;
                                    	return ("1" + "-" + dinero3 + "-" + nickname3);
                                	}
                                }
                            }
                            if (nick == nickname4)
                            {
                                if(nick == nickdueño7 && nick == nickdueño9 && nick == nickdueño10)
                            	{
                                	if(casadueño7 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero4 >= 30)
                                    	{ 
                                        	dinero4 = dinero4 - 30; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño7 = nickname4;
                                        	return ("6" + "-" + dinero4 + "-" + nickname4 + "-" + 7);
                                    	}
                                	}
                                	else if (casadueño7 == nickname4 && hoteldueño7 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero4 >= 550)
                                    	{
                                        	dinero4 = dinero4 - 550; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño7 = nickname4;
                                        	return ("7" + "-" + dinero4 + "-" + nickname4 + "-" + 7);
                                    	}
                                	}
                                	else if (casadueño7 == nickname4 && hoteldueño7 == nickname4) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero4 = dinero4;
                                    	return ("1" + "-" + dinero4 + "-" + nickname4);
                                	}
                                }
                            }
                        }
                        else
                        {
                            if (nickdueño7 == nickname1)
                            {
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 6;
                                    dinero1 = dinero1 + 6;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 6 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 6;
                                    dinero1 = dinero1 + 6;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 6 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 6;
                                    dinero1 = dinero1 + 6;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 6 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño7 == nickname2)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 6;
                                    dinero2 = dinero2 + 6;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 6 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 6;
                                    dinero2 = dinero2 + 6;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 6 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 6;
                                    dinero2 = dinero2 + 6;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 6 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño7 == nickname3)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 6;
                                    dinero3 = dinero3 + 6;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 6 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 6;
                                    dinero3 = dinero3 + 6;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 6 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 6;
                                    dinero3 = dinero3 + 6;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 6 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño7 == nickname4)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 6;
                                    dinero4 = dinero4 + 6;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 6 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 6;
                                    dinero4 = dinero4 + 6;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 6 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 6;
                                    dinero4 = dinero4 + 6;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 6 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                            }
                        }
                    }
                    break;
                case 8: //CASUALIDAD
                    int casualidad = random.Next(1, 6);
                    if (casualidad == 1)
                    {
                        if (nick == nickname1)
                        {
                            dinero1 = dinero1 + 200;
                            return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Casualidad" + "-" + 1);
                        }
                        if (nick == nickname2)
                        {
                            dinero2 = dinero2 + 200;
                            return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Casualidad" + "-" + 1);
                        }
                        if (nick == nickname3)
                        {
                            dinero3 = dinero3 + 200;
                            return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Casualidad" + "-" + 1);
                        }
                        if (nick == nickname4)
                        {
                            dinero4 = dinero4 + 200;
                            return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Casualidad" + "-" + 1);
                        }
                    }
                    if (casualidad == 2)
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 15)
                            {
                                dinero1 = dinero1 - 15;
                                return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Casualidad" + "-" + 2);
                            }
                        }
                        if (nick == nickname2)
                        {
                            if (dinero2 >= 15)
                            {
                                dinero2 = dinero2 - 15;
                                return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Casualidad" + "-" + 2);
                            }
                        }
                        if (nick == nickname3)
                        {
                            if (dinero3 >= 15)
                            {
                                dinero3 = dinero3 - 15;
                                return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Casualidad" + "-" + 2);
                            }
                        }
                        if (nick == nickname4)
                        {
                            if (dinero4 >= 15)
                            {
                                dinero4 = dinero4 - 15;
                                return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Casualidad" + "-" + 2);
                            }
                        }
                    }
                    if (casualidad == 3)
                    {
                        if (nick == nickname1)
                        {
                            dinero1 = dinero1 + 50;
                            return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Casualidad" + "-" + 3);
                        }
                        if (nick == nickname2)
                        {
                            dinero2 = dinero2 + 50;
                            return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Casualidad" + "-" + 3);
                        }
                        if (nick == nickname3)
                        {
                            dinero3 = dinero3 + 50;
                            return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Casualidad" + "-" + 3);
                        }
                        if (nick == nickname4)
                        {
                            dinero4 = dinero4 + 50;
                            return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Casualidad" + "-" + 3);
                        }
                    }
                    if (casualidad == 4)
                    {
                        if (nick == nickname1)
                        {
                            dinero1 = dinero1 + 150;
                            return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Casualidad" + "-" + 4);
                        }
                        if (nick == nickname2)
                        {
                            dinero2 = dinero2 + 150;
                            return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Casualidad" + "-" + 4);
                        }
                        if (nick == nickname3)
                        {
                            dinero3 = dinero3 + 150;
                            return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Casualidad" + "-" + 4);
                        }
                        if (nick == nickname4)
                        {
                            dinero4 = dinero4 + 150;
                            return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Casualidad" + "-" + 4);
                        }
                    }
                    if (casualidad == 5)
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 15)
                            {
                                dinero1 = dinero1 - 100;
                                return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Casualidad" + "-" + 5);
                            }
                        }
                        if (nick == nickname2)
                        {
                            if (dinero2 >= 15)
                            {
                                dinero2 = dinero2 - 100;
                                return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Casualidad" + "-" + 5);
                            }
                        }
                        if (nick == nickname3)
                        {
                            if (dinero3 >= 15)
                            {
                                dinero3 = dinero3 - 100;
                                return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Casualidad" + "-" + 5);
                            }
                        }
                        if (nick == nickname4)
                        {
                            if (dinero4 >= 15)
                            {
                                dinero4 = dinero4 - 100;
                                return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Casualidad" + "-" + 5);
                            }
                        }
                    }
                    if (casualidad == 6)
                    {
                        if (nick == nickname1)
                        {
                            tarjeta1 = "Si";  
                            return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Casualidad" + "-" + 6);
                        }
                        if (nick == nickname2)
                        {
                            tarjeta2 = "Si";
                            return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Casualidad" + "-" + 6);
                        }
                        if (nick == nickname3)
                        {
                            tarjeta3 = "Si";
                            return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Casualidad" + "-" + 6);
                        }
                        if (nick == nickname4)
                        {
                            tarjeta4 = "Si";
                            return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Casualidad" + "-" + 6);
                        }
                    }
                    break;
                case 9: //SOLAR---MODULO 5
                    if (nickdueño9 == "")
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 100)
                            {
                                dinero1 = dinero1 - 100;
                                nickdueño9 = nickname1;
                                return ("2" + "-" + dinero1 + "-" + nickname1 + "-" + 9);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname2)
                        {
                            if (dinero2 >= 100)
                            {
                                dinero2 = dinero2 - 100;
                                nickdueño9 = nickname2;
                                return ("2" + "-" + dinero2 + "-" + nickname2 + "-" + 9);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname3)
                        {
                            if (dinero3 >= 100)
                            {
                                dinero3 = dinero3 - 100;
                                nickdueño9 = nickname3;
                                return ("2" + "-" + dinero3 + "-" + nickname3 + "-" + 9);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname4)
                        {
                            if (dinero4 >= 100)
                            {
                                dinero4 = dinero4 - 100;
                                nickdueño9 = nickname4;
                                return ("2" + "-" + dinero4 + "-" + nickname4 + "-" + 9);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                    }
                    else
                    {
                        if (nick == nickdueño9)
                        {
                            if (nick == nickname1)
                            {
                                if(nick == nickdueño7 && nick == nickdueño9 && nick == nickdueño10)
                            	{
                                	if(casadueño9 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero1 >= 30)
                                    	{ 
                                        	dinero1 = dinero1 - 30; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño9 = nickname1;
                                        	return ("6" + "-" + dinero1 + "-" + nickname1 + "-" + 9);
                                    	}
                                	}
                                	else if (casadueño9 == nickname1 && hoteldueño9 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero1 >= 550)
                                    	{
                                        	dinero1 = dinero1 - 550; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño9 = nickname1;
                                        	return ("7" + "-" + dinero1 + "-" + nickname1 + "-" + 9);
                                    	}
                                	}
                                	else if (casadueño9 == nickname1 && hoteldueño9 == nickname1) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero1 = dinero1;
                                    	return ("1" + "-" + dinero1 + "-" + nickname1);
                                	}
                                }
                            }
                            if (nick == nickname2)
                            {
                                if(nick == nickdueño7 && nick == nickdueño9 && nick == nickdueño10)
                            	{
                                	if(casadueño9 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero2 >= 30)
                                    	{ 
                                        	dinero2 = dinero2 - 30; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño9 = nickname2;
                                        	return ("6" + "-" + dinero2 + "-" + nickname2 + "-" + 9);
                                    	}
                                	}
                                	else if (casadueño9 == nickname2 && hoteldueño9 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero2 >= 550)
                                    	{
                                        	dinero2 = dinero2 - 550; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño9 = nickname2;
                                        	return ("7" + "-" + dinero2 + "-" + nickname2 + "-" + 9);
                                    	}
                                	}
                                	else if (casadueño9 == nickname2 && hoteldueño9 == nickname2) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero2 = dinero2;
                                    	return ("1" + "-" + dinero2 + "-" + nickname2);
                                	}
                                }
                            }
                            if (nick == nickname3)
                            {
                                if(nick == nickdueño7 && nick == nickdueño9 && nick == nickdueño10)
                            	{
                                	if(casadueño9 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero3 >= 30)
                                    	{ 
                                        	dinero3 = dinero3 - 30; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño9 = nickname3;
                                        	return ("6" + "-" + dinero3 + "-" + nickname3 + "-" + 9);
                                    	}
                                	}
                                	else if (casadueño9 == nickname3 && hoteldueño9 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero3 >= 550)
                                    	{
                                        	dinero3 = dinero3 - 550; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño9 = nickname3;
                                        	return ("7" + "-" + dinero3 + "-" + nickname3 + "-" + 9);
                                    	}
                                	}
                                	else if (casadueño9 == nickname3 && hoteldueño9 == nickname3) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero3 = dinero3;
                                    	return ("1" + "-" + dinero3 + "-" + nickname3);
                                	}
                                }
                            }
                            if (nick == nickname4)
                            {
                                if(nick == nickdueño7 && nick == nickdueño9 && nick == nickdueño10)
                            	{
                                	if(casadueño9 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero4 >= 30)
                                    	{ 
                                        	dinero4 = dinero4 - 30; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño9 = nickname4;
                                        	return ("6" + "-" + dinero4 + "-" + nickname4 + "-" + 9);
                                    	}
                                	}
                                	else if (casadueño9 == nickname4 && hoteldueño9 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero4 >= 550)
                                    	{
                                        	dinero4 = dinero4 - 550; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño9 = nickname4;
                                        	return ("7" + "-" + dinero4 + "-" + nickname4 + "-" + 9);
                                    	}
                                	}
                                	else if (casadueño9 == nickname4 && hoteldueño9 == nickname4) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero4 = dinero4;
                                    	return ("1" + "-" + dinero4 + "-" + nickname4);
                                	}
                                }
                            }
                        }
                        else
                        {
                            if (nickdueño9 == nickname1)
                            {
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 6;
                                    dinero1 = dinero1 + 6;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 6 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 6;
                                    dinero1 = dinero1 + 6;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 6 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 6;
                                    dinero1 = dinero1 + 6;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 6 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño9 == nickname2)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 6;
                                    dinero2 = dinero2 + 6;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 6 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 6;
                                    dinero2 = dinero2 + 6;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 6 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 6;
                                    dinero2 = dinero2 + 6;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 6 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño9 == nickname3)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 6;
                                    dinero3 = dinero3 + 6;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 6 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 6;
                                    dinero3 = dinero3 + 6;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 6 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 6;
                                    dinero3 = dinero3 + 6;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 6 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño9 == nickname4)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 6;
                                    dinero4 = dinero4 + 6;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 6 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 6;
                                    dinero4 = dinero4 + 6;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 6 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 6;
                                    dinero4 = dinero4 + 6;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 6 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                            }
                        }
                    }
                    break;
                case 10: //SOLAR---MODULO 4
                    if (nickdueño10 == "")
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 120)
                            {
                                dinero1 = dinero1 - 120;
                                nickdueño10 = nickname1;
                                return ("2" + "-" + dinero1 + "-" + nickname1 + "-" + 10);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname2)
                        {
                            if (dinero2 >= 120)
                            {
                                dinero2 = dinero2 - 120;
                                nickdueño10 = nickname2;
                                return ("2" + "-" + dinero2 + "-" + nickname2 + "-" + 10);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname3)
                        {
                            if (dinero3 >= 120)
                            {
                                dinero3 = dinero3 - 120;
                                nickdueño10 = nickname3;
                                return ("2" + "-" + dinero3 + "-" + nickname3 + "-" + 10);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname4)
                        {
                            if (dinero4 >= 120)
                            {
                                dinero4 = dinero4 - 120;
                                nickdueño10 = nickname4;
                                return ("2" + "-" + dinero4 + "-" + nickname4 + "-" + 10);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                    }
                    else
                    {
                        if (nick == nickdueño10)
                        {
                            if (nick == nickname1)
                            {
                                if(nick == nickdueño7 && nick == nickdueño9 && nick == nickdueño10)
                            	{
                                	if(casadueño10 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero1 >= 40)
                                    	{ 
                                        	dinero1 = dinero1 - 40; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño10 = nickname1;
                                        	return ("6" + "-" + dinero1 + "-" + nickname1 + "-" + 10);
                                    	}
                                	}
                                	else if (casadueño10 == nickname1 && hoteldueño10 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero1 >= 600)
                                    	{
                                        	dinero1 = dinero1 - 600; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño10 = nickname1;
                                        	return ("7" + "-" + dinero1 + "-" + nickname1 + "-" + 10);
                                    	}
                                	}
                                	else if (casadueño10 == nickname1 && hoteldueño10 == nickname1) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero1 = dinero1;
                                    	return ("1" + "-" + dinero1 + "-" + nickname1);
                                	}
                                }
                            }
                            if (nick == nickname2)
                            {
                                if(nick == nickdueño7 && nick == nickdueño9 && nick == nickdueño10)
                            	{
                                	if(casadueño10 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero2 >= 40)
                                    	{ 
                                        	dinero2 = dinero2 - 40; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño10 = nickname2;
                                        	return ("6" + "-" + dinero2 + "-" + nickname2 + "-" + 10);
                                    	}
                                	}
                                	else if (casadueño10 == nickname2 && hoteldueño10 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero2 >= 600)
                                    	{
                                        	dinero2 = dinero2 - 600; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño10 = nickname2;
                                        	return ("7" + "-" + dinero2 + "-" + nickname2 + "-" + 10);
                                    	}
                                	}
                                	else if (casadueño10 == nickname2 && hoteldueño10 == nickname2) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero2 = dinero2;
                                    	return ("1" + "-" + dinero2 + "-" + nickname2);
                                	}
                                }
                            }
                            if (nick == nickname3)
                            {
                                if(nick == nickdueño7 && nick == nickdueño9 && nick == nickdueño10)
                            	{
                                	if(casadueño10 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero3 >= 40)
                                    	{ 
                                        	dinero3 = dinero3 - 40; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño10 = nickname3;
                                        	return ("6" + "-" + dinero3 + "-" + nickname3 + "-" + 10);
                                    	}
                                	}
                                	else if (casadueño10 == nickname3 && hoteldueño10 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero3 >= 600)
                                    	{
                                        	dinero3 = dinero3 - 600; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño10 = nickname3;
                                        	return ("7" + "-" + dinero3 + "-" + nickname3 + "-" + 10);
                                    	}
                                	}
                                	else if (casadueño10 == nickname3 && hoteldueño10 == nickname3) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero3 = dinero3;
                                    	return ("1" + "-" + dinero3 + "-" + nickname3);
                                	}
                                }
                            }
                            if (nick == nickname4)
                            {
                                if(nick == nickdueño7 && nick == nickdueño9 && nick == nickdueño10)
                            	{
                                	if(casadueño10 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero4 >= 40)
                                    	{ 
                                        	dinero4 = dinero4 - 40; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño10 = nickname4;
                                        	return ("6" + "-" + dinero4 + "-" + nickname4 + "-" + 10);
                                    	}
                                	}
                                	else if (casadueño10 == nickname4 && hoteldueño10 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero4 >= 600)
                                    	{
                                        	dinero4 = dinero4 - 600; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño10 = nickname4;
                                        	return ("7" + "-" + dinero4 + "-" + nickname4 + "-" + 10);
                                    	}
                                	}
                                	else if (casadueño10 == nickname4 && hoteldueño10 == nickname4) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero4 = dinero4;
                                    	return ("1" + "-" + dinero4 + "-" + nickname4);
                                	}
                                }
                            }
                        }
                        else
                        {
                            if (nickdueño10 == nickname1)
                            {
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 8;
                                    dinero1 = dinero1 + 8;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 8 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 8;
                                    dinero1 = dinero1 + 8;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 8 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 8;
                                    dinero1 = dinero1 + 8;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 8 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño10 == nickname2)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 8;
                                    dinero2 = dinero2 + 8;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 8 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 8;
                                    dinero2 = dinero2 + 8;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 8 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 8;
                                    dinero2 = dinero2 + 8;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 8 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño10 == nickname3)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 8;
                                    dinero3 = dinero3 + 8;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 8 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 8;
                                    dinero3 = dinero3 + 8;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 8 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 8;
                                    dinero3 = dinero3 + 8;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 8 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño10 == nickname4)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 8;
                                    dinero4 = dinero4 + 8;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 8 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 8;
                                    dinero4 = dinero4 + 8;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 8 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 8;
                                    dinero4 = dinero4 + 8;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 8 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                            }
                        }
                    }
                    break;
               case 11: //CARCEL
                    if (nick == nickname1 && estado1 == "Libre")
                    {
                        dinero1 = dinero1;
                        return ("1" + "-" + dinero1 + "-" + nickname1);
                    }
                    if (nick == nickname2 && estado2 == "Libre")
                    {
                        dinero2 = dinero2;
                        return ("1" + "-" + dinero2 + "-" + nickname2);
                    }
                    if (nick == nickname3 && estado3 == "Libre")
                    {
                        dinero3 = dinero3;
                        return ("1" + "-" + dinero3 + "-" + nickname3);
                    }
                    if (nick == nickname4 && estado4 == "Libre")
                    {
                        dinero4 = dinero4;
                        return ("1" + "-" + dinero4 + "-" + nickname4);
                    }
                    //FALTAN SI ESTA PRESO
                    break;
                case 12: //SOLAR---BIBLIOTECA
                    if (nickdueño12 == "")
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 140)
                            {
                                dinero1 = dinero1 - 140;
                                nickdueño12 = nickname1;
                                return ("2" + "-" + dinero1 + "-" + nickname1 + "-" + 12);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname2)
                        {
                            if (dinero2 >= 140)
                            {
                                dinero2 = dinero2 - 140;
                                nickdueño12 = nickname2;
                                return ("2" + "-" + dinero2 + "-" + nickname2 + "-" + 12);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname3)
                        {
                            if (dinero3 >= 140)
                            {
                                dinero3 = dinero3 - 140;
                                nickdueño12 = nickname3;
                                return ("2" + "-" + dinero3 + "-" + nickname3 + "-" + 12);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname4)
                        {
                            if (dinero4 >= 140)
                            {
                                dinero4 = dinero4 - 140;
                                nickdueño12 = nickname4;
                                return ("2" + "-" + dinero4 + "-" + nickname4 + "-" + 12);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                    }
                    else
                    {
                        if (nick == nickdueño12)
                        {
                            if (nick == nickname1)
                            {
                                if(nick == nickdueño12 && nick == nickdueño14 && nick == nickdueño15)
                            	{
                                	if(casadueño12 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero1 >= 50)
                                    	{ 
                                        	dinero1 = dinero1 - 50; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño12 = nickname1;
                                        	return ("6" + "-" + dinero1 + "-" + nickname1 + "-" + 12);
                                    	}
                                	}
                                	else if (casadueño12 == nickname1 && hoteldueño12 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero1 >= 750)
                                    	{
                                        	dinero1 = dinero1 - 750; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño12 = nickname1;
                                        	return ("7" + "-" + dinero1 + "-" + nickname1 + "-" + 12);
                                    	}
                                	}
                                	else if (casadueño12 == nickname1 && hoteldueño12 == nickname1) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero1 = dinero1;
                                    	return ("1" + "-" + dinero1 + "-" + nickname1);
                                	}
                                }
                            }
                            if (nick == nickname2)
                            {
                                if(nick == nickdueño12 && nick == nickdueño14 && nick == nickdueño15)
                            	{
                                	if(casadueño12 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero2 >= 50)
                                    	{ 
                                        	dinero2 = dinero2 - 50; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño12 = nickname2;
                                        	return ("6" + "-" + dinero2 + "-" + nickname2 + "-" + 12);
                                    	}
                                	}
                                	else if (casadueño12 == nickname2 && hoteldueño12 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero2 >= 750)
                                    	{
                                        	dinero2 = dinero2 - 750; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño12 = nickname2;
                                        	return ("7" + "-" + dinero2 + "-" + nickname2 + "-" + 12);
                                    	}
                                	}
                                	else if (casadueño12 == nickname2 && hoteldueño12 == nickname2) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero2 = dinero2;
                                    	return ("1" + "-" + dinero2 + "-" + nickname2);
                                	}
                                }
                            }
                            if (nick == nickname3)
                            {
                                if(nick == nickdueño12 && nick == nickdueño14 && nick == nickdueño15)
                            	{
                                	if(casadueño12 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero3 >= 50)
                                    	{ 
                                        	dinero3 = dinero3 - 50; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño12 = nickname3;
                                        	return ("6" + "-" + dinero3 + "-" + nickname3 + "-" + 12);
                                    	}
                                	}
                                	else if (casadueño12 == nickname3 && hoteldueño12 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero3 >= 750)
                                    	{
                                        	dinero3 = dinero3 - 750; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño12 = nickname3;
                                        	return ("7" + "-" + dinero3 + "-" + nickname3 + "-" + 12);
                                    	}
                                	}
                                	else if (casadueño12 == nickname3 && hoteldueño12 == nickname3) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero3 = dinero3;
                                    	return ("1" + "-" + dinero3 + "-" + nickname3);
                                	}
                                }
                            }
                            if (nick == nickname4)
                            {
                                if(nick == nickdueño12 && nick == nickdueño14 && nick == nickdueño15)
                            	{
                                	if(casadueño12 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero4 >= 50)
                                    	{ 
                                        	dinero4 = dinero4 - 50; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño12 = nickname4;
                                        	return ("6" + "-" + dinero4 + "-" + nickname4 + "-" + 12);
                                    	}
                                	}
                                	else if (casadueño12 == nickname4 && hoteldueño12 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero4 >= 750)
                                    	{
                                        	dinero4 = dinero4 - 750; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño12 = nickname4;
                                        	return ("7" + "-" + dinero4 + "-" + nickname4 + "-" + 12);
                                    	}
                                	}
                                	else if (casadueño12 == nickname4 && hoteldueño12 == nickname4) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero4 = dinero4;
                                    	return ("1" + "-" + dinero4 + "-" + nickname4);
                                	}
                                }
                            }
                        }
                        else
                        {
                            if (nickdueño12 == nickname1)
                            {
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 10;
                                    dinero1 = dinero1 + 10;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 10 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 10;
                                    dinero1 = dinero1 + 10;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 10 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 10;
                                    dinero1 = dinero1 + 10;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 10 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño12 == nickname2)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 10;
                                    dinero2 = dinero2 + 10;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 10 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 10;
                                    dinero2 = dinero2 + 10;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 10 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 10;
                                    dinero2 = dinero2 + 10;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 10 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño12 == nickname3)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 10;
                                    dinero3 = dinero3 + 10;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 10 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 10;
                                    dinero3 = dinero3 + 10;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 10 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 10;
                                    dinero3 = dinero3 + 10;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 10 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño12 == nickname4)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 10;
                                    dinero4 = dinero4 + 10;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 10 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 10;
                                    dinero4 = dinero4 + 10;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 10 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 10;
                                    dinero4 = dinero4 + 10;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 10 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                            }
                        }
                    }
                    break;
                case 13: //SERVICIO ELECTRICO
                    if (nickdueño13 == "")
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 150)
                            {
                                dinero1 = dinero1 - 150;
                                nickdueño13 = nickname1;
                                return ("2" + "-" + dinero1 + "-" + nickname1 + "-" + 13);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname2)
                        {
                            if (dinero2 >= 150)
                            {
                                dinero2 = dinero2 - 150;
                                nickdueño13 = nickname2;
                                return ("2" + "-" + dinero2 + "-" + nickname2 + "-" + 13);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname3)
                        {
                            if (dinero3 >= 150)
                            {
                                dinero3 = dinero3 - 150;
                                nickdueño13 = nickname3;
                                return ("2" + "-" + dinero3 + "-" + nickname3 + "-" + 13);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname4)
                        {
                            if (dinero4 >= 150)
                            {
                                dinero4 = dinero4 - 150;
                                nickdueño13 = nickname4;
                                return ("2" + "-" + dinero4 + "-" + nickname4 + "-" + 13);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                    }
                    else
                    {
                        if (nick == nickdueño13)
                        {
                            if (nick == nickname1)
                            {
                                dinero1 = dinero1;
                                return ("1" + "-" + dinero1 + "-" + nickname1);
                            }
                            if (nick == nickname2)
                            {
                                dinero2 = dinero2;
                                return ("1" + "-" + dinero2 + "-" + nickname2);
                            }
                            if (nick == nickname3)
                            {
                                dinero3 = dinero3;
                                return ("1" + "-" + dinero3 + "-" + nickname3);
                            }
                            if (nick == nickname4)
                            {
                                dinero4 = dinero4;
                                return ("1" + "-" + dinero4 + "-" + nickname4);
                            }
                        }
                        else
                        {
                            if (nickdueño13 == nickname1)
                            {
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 50;
                                    dinero1 = dinero1 + 50;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 50 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 50;
                                    dinero1 = dinero1 + 50;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 50 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 50;
                                    dinero1 = dinero1 + 50;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 50 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño13 == nickname2)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 50;
                                    dinero2 = dinero2 + 50;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 50 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 50;
                                    dinero2 = dinero2 + 50;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 50 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 50;
                                    dinero2 = dinero2 + 50;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 50 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño13 == nickname3)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 50;
                                    dinero3 = dinero3 + 50;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 50 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 50;
                                    dinero3 = dinero3 + 50;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 50 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 50;
                                    dinero3 = dinero3 + 50;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 50 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño13 == nickname4)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 50;
                                    dinero4 = dinero4 + 50;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 50 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 50;
                                    dinero4 = dinero4 + 50;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 50 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 50;
                                    dinero4 = dinero4 + 50;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 50 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                            }
                        }
                    }
                    break;
                case 14: //SOLAR---MODULO 3
                    if (nickdueño14 == "")
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 140)
                            {
                                dinero1 = dinero1 - 140;
                                nickdueño14 = nickname1;
                                return ("2" + "-" + dinero1 + "-" + nickname1 + "-" + 14);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname2)
                        {
                            if (dinero2 >= 140)
                            {
                                dinero2 = dinero2 - 140;
                                nickdueño14 = nickname2;
                                return ("2" + "-" + dinero2 + "-" + nickname2 + "-" + 14);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname3)
                        {
                            if (dinero3 >= 140)
                            {
                                dinero3 = dinero3 - 140;
                                nickdueño14 = nickname3;
                                return ("2" + "-" + dinero3 + "-" + nickname3 + "-" + 14);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname4)
                        {
                            if (dinero4 >= 140)
                            {
                                dinero4 = dinero4 - 140;
                                nickdueño14 = nickname4;
                                return ("2" + "-" + dinero4 + "-" + nickname4 + "-" + 14);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                    }
                    else
                    {
                        if (nick == nickdueño14)
                        {
                            if (nick == nickname1)
                            {
                                if(nick == nickdueño12 && nick == nickdueño14 && nick == nickdueño15)
                            	{
                                	if(casadueño14 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero1 >= 50)
                                    	{ 
                                        	dinero1 = dinero1 - 50; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño14 = nickname1;
                                        	return ("6" + "-" + dinero1 + "-" + nickname1 + "-" + 14);
                                    	}
                                	}
                                	else if (casadueño14 == nickname1 && hoteldueño14 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero1 >= 750)
                                    	{
                                        	dinero1 = dinero1 - 750; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño14 = nickname1;
                                        	return ("7" + "-" + dinero1 + "-" + nickname1 + "-" + 14);
                                    	}
                                	}
                                	else if (casadueño14 == nickname1 && hoteldueño14 == nickname1) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero1 = dinero1;
                                    	return ("1" + "-" + dinero1 + "-" + nickname1);
                                	}
                                }
                            }
                            if (nick == nickname2)
                            {
                                if(nick == nickdueño12 && nick == nickdueño14 && nick == nickdueño15)
                            	{
                                	if(casadueño14 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero2 >= 50)
                                    	{ 
                                        	dinero2 = dinero2 - 50; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño14 = nickname2;
                                        	return ("6" + "-" + dinero2 + "-" + nickname2 + "-" + 14);
                                    	}
                                	}
                                	else if (casadueño14 == nickname2 && hoteldueño14 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero2 >= 750)
                                    	{
                                        	dinero2 = dinero2 - 750; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño14 = nickname2;
                                        	return ("7" + "-" + dinero2 + "-" + nickname2 + "-" + 14);
                                    	}
                                	}
                                	else if (casadueño14 == nickname2 && hoteldueño14 == nickname2) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero2 = dinero2;
                                    	return ("1" + "-" + dinero2 + "-" + nickname2);
                                	}
                                }
                            }
                            if (nick == nickname3)
                            {
                                if(nick == nickdueño12 && nick == nickdueño14 && nick == nickdueño15)
                            	{
                                	if(casadueño14 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero3 >= 50)
                                    	{ 
                                        	dinero3 = dinero3 - 50; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño14 = nickname3;
                                        	return ("6" + "-" + dinero3 + "-" + nickname3 + "-" + 14);
                                    	}
                                	}
                                	else if (casadueño14 == nickname3 && hoteldueño14 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero3 >= 750)
                                    	{
                                        	dinero3 = dinero3 - 750; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño14 = nickname3;
                                        	return ("7" + "-" + dinero3 + "-" + nickname3 + "-" + 14);
                                    	}
                                	}
                                	else if (casadueño14 == nickname3 && hoteldueño14 == nickname3) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero3 = dinero3;
                                    	return ("1" + "-" + dinero3 + "-" + nickname3);
                                	}
                                }
                            }
                            if (nick == nickname4)
                            {
                                if(nick == nickdueño12 && nick == nickdueño14 && nick == nickdueño15)
                            	{
                                	if(casadueño14 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero4 >= 50)
                                    	{ 
                                        	dinero4 = dinero4 - 50; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño14 = nickname4;
                                        	return ("6" + "-" + dinero4 + "-" + nickname4 + "-" + 14);
                                    	}
                                	}
                                	else if (casadueño14 == nickname4 && hoteldueño14 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero4 >= 750)
                                    	{
                                        	dinero4 = dinero4 - 750; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño14 = nickname4;
                                        	return ("7" + "-" + dinero4 + "-" + nickname4 + "-" + 14);
                                    	}
                                	}
                                	else if (casadueño14 == nickname4 && hoteldueño14 == nickname4) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero4 = dinero4;
                                    	return ("1" + "-" + dinero4 + "-" + nickname4);
                                	}
                                }
                            }
                        }
                        else
                        {
                            if (nickdueño14 == nickname1)
                            {
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 10;
                                    dinero1 = dinero1 + 10;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 10 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 10;
                                    dinero1 = dinero1 + 10;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 10 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 10;
                                    dinero1 = dinero1 + 10;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 10 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño14 == nickname2)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 10;
                                    dinero2 = dinero2 + 10;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 10 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 10;
                                    dinero2 = dinero2 + 10;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 10 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 10;
                                    dinero2 = dinero2 + 10;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 10 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño14 == nickname3)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 10;
                                    dinero3 = dinero3 + 10;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 10 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 10;
                                    dinero3 = dinero3 + 10;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 10 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 10;
                                    dinero3 = dinero3 + 10;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 10 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño14 == nickname4)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 10;
                                    dinero4 = dinero4 + 10;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 10 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 10;
                                    dinero4 = dinero4 + 10;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 10 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 10;
                                    dinero4 = dinero4 + 10;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 10 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                            }
                        }
                    }
                    break;
                case 15: //SOLAR---CAFETÍN
                    if (nickdueño15 == "")
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 140)
                            {
                                dinero1 = dinero1 - 140;
                                nickdueño15 = nickname1;
                                return ("2" + "-" + dinero1 + "-" + nickname1 + "-" + 15);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname2)
                        {
                            if (dinero2 >= 140)
                            {
                                dinero2 = dinero2 - 140;
                                nickdueño15 = nickname2;
                                return ("2" + "-" + dinero2 + "-" + nickname2 + "-" + 15);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname3)
                        {
                            if (dinero3 >= 140)
                            {
                                dinero3 = dinero3 - 140;
                                nickdueño15 = nickname3;
                                return ("2" + "-" + dinero3 + "-" + nickname3 + "-" + 15);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname4)
                        {
                            if (dinero4 >= 140)
                            {
                                dinero4 = dinero4 - 140;
                                nickdueño15 = nickname4;
                                return ("2" + "-" + dinero4 + "-" + nickname4 + "-" + 15);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                    }
                    else
                    {
                        if (nick == nickdueño15)
                        {
                            if (nick == nickname1)
                            {
                                if(nick == nickdueño12 && nick == nickdueño14 && nick == nickdueño15)
                            	{
                                	if(casadueño15 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero1 >= 60)
                                    	{ 
                                        	dinero1 = dinero1 - 60; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño15 = nickname1;
                                        	return ("6" + "-" + dinero1 + "-" + nickname1 + "-" + 15);
                                    	}
                                	}
                                	else if (casadueño15 == nickname1 && hoteldueño15 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero1 >= 900)
                                    	{
                                        	dinero1 = dinero1 - 900; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño15 = nickname1;
                                        	return ("7" + "-" + dinero1 + "-" + nickname1 + "-" + 15);
                                    	}
                                	}
                                	else if (casadueño15 == nickname1 && hoteldueño15 == nickname1) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero1 = dinero1;
                                    	return ("1" + "-" + dinero1 + "-" + nickname1);
                                	}
                                }
                            }
                            if (nick == nickname2)
                            {
                                if(nick == nickdueño12 && nick == nickdueño14 && nick == nickdueño15)
                            	{
                                	if(casadueño15 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero2 >= 60)
                                    	{ 
                                        	dinero2 = dinero2 - 60; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño15 = nickname2;
                                        	return ("6" + "-" + dinero2 + "-" + nickname2 + "-" + 15);
                                    	}
                                	}
                                	else if (casadueño15 == nickname2 && hoteldueño15 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero2 >= 900)
                                    	{
                                        	dinero2 = dinero2 - 900; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño15 = nickname2;
                                        	return ("7" + "-" + dinero2 + "-" + nickname2 + "-" + 15);
                                    	}
                                	}
                                	else if (casadueño15 == nickname2 && hoteldueño15 == nickname2) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero2 = dinero2;
                                    	return ("1" + "-" + dinero2 + "-" + nickname2);
                                	}
                                }
                            }
                            if (nick == nickname3)
                            {
                                if(nick == nickdueño12 && nick == nickdueño14 && nick == nickdueño15)
                            	{
                                	if(casadueño15 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero3 >= 60)
                                    	{ 
                                        	dinero3 = dinero3 - 60; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño15 = nickname3;
                                        	return ("6" + "-" + dinero3 + "-" + nickname3 + "-" + 15);
                                    	}
                                	}
                                	else if (casadueño15 == nickname3 && hoteldueño15 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero3 >= 900)
                                    	{
                                        	dinero3 = dinero3 - 900; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño15 = nickname3;
                                        	return ("7" + "-" + dinero3 + "-" + nickname3 + "-" + 15);
                                    	}
                                	}
                                	else if (casadueño15 == nickname3 && hoteldueño15 == nickname3) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero3 = dinero3;
                                    	return ("1" + "-" + dinero3 + "-" + nickname3);
                                	}
                                }
                            }
                            if (nick == nickname4)
                            {
                                if(nick == nickdueño12 && nick == nickdueño14 && nick == nickdueño15)
                            	{
                                	if(casadueño15 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero4 >= 60)
                                    	{ 
                                        	dinero4 = dinero4 - 60; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño15 = nickname4;
                                        	return ("6" + "-" + dinero4 + "-" + nickname4 + "-" + 15);
                                    	}
                                	}
                                	else if (casadueño15 == nickname4 && hoteldueño15 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero4 >= 900)
                                    	{
                                        	dinero4 = dinero4 - 900; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño15 = nickname4;
                                        	return ("7" + "-" + dinero4 + "-" + nickname4 + "-" + 15);
                                    	}
                                	}
                                	else if (casadueño15 == nickname4 && hoteldueño15 == nickname4) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero4 = dinero4;
                                    	return ("1" + "-" + dinero4 + "-" + nickname4);
                                	}
                                }
                            }
                        }
                        else
                        {
                            if (nickdueño15 == nickname1)
                            {
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 12;
                                    dinero1 = dinero1 + 12;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 12 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 12;
                                    dinero1 = dinero1 + 12;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 12 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 12;
                                    dinero1 = dinero1 + 12;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 12 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño15 == nickname2)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 12;
                                    dinero2 = dinero2 + 12;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 12 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 12;
                                    dinero2 = dinero2 + 12;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 12 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 12;
                                    dinero2 = dinero2 + 12;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 12 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño15 == nickname3)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 12;
                                    dinero3 = dinero3 + 12;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 12 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 12;
                                    dinero3 = dinero3 + 12;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 12 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 12;
                                    dinero3 = dinero3 + 12;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 12 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño15 == nickname4)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 12;
                                    dinero4 = dinero4 + 12;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 12 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 12;
                                    dinero4 = dinero4 + 12;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 12 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 12;
                                    dinero4 = dinero4 + 12;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 12 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                            }
                        }
                    }
                    break;
                case 16: //TAXI UCAB OESTE
                    if (nickdueño16 == "")
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 200)
                            {
                                dinero1 = dinero1 - 200;
                                nickdueño16 = nickname1;
                                return ("2" + "-" + dinero1 + "-" + nickname1 + "-" + 16);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname2)
                        {
                            if (dinero2 >= 200)
                            {
                                dinero2 = dinero2 - 200;
                                nickdueño16 = nickname2;
                                return ("2" + "-" + dinero2 + "-" + nickname2 + "-" + 16);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname3)
                        {
                            if (dinero3 >= 200)
                            {
                                dinero3 = dinero3 - 200;
                                nickdueño16 = nickname3;
                                return ("2" + "-" + dinero3 + "-" + nickname3 + "-" + 16);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname4)
                        {
                            if (dinero4 >= 200)
                            {
                                dinero4 = dinero4 - 200;
                                nickdueño16 = nickname4;
                                return ("2" + "-" + dinero4 + "-" + nickname4 + "-" + 16);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                    }
                    else
                    {
                        if (nick == nickdueño16)
                        {
                            if (nick == nickname1)
                            {
                                dinero1 = dinero1;
                                return ("1" + "-" + dinero1 + "-" + nickname1);
                            }
                            if (nick == nickname2)
                            {
                                dinero2 = dinero2;
                                return ("1" + "-" + dinero2 + "-" + nickname2);
                            }
                            if (nick == nickname3)
                            {
                                dinero3 = dinero3;
                                return ("1" + "-" + dinero3 + "-" + nickname3);
                            }
                            if (nick == nickname4)
                            {
                                dinero4 = dinero4;
                                return ("1" + "-" + dinero4 + "-" + nickname4);
                            }
                        }
                        else
                        {
                            if (nickdueño16 == nickname1)
                            {
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 25;
                                    dinero1 = dinero1 + 25;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 25 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 25;
                                    dinero1 = dinero1 + 25;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 25 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 25;
                                    dinero1 = dinero1 + 25;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 25 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño16 == nickname2)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 25;
                                    dinero2 = dinero2 + 25;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 25 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 25;
                                    dinero2 = dinero2 + 25;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 25 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 25;
                                    dinero2 = dinero2 + 25;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 25 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño16 == nickname3)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 25;
                                    dinero3 = dinero3 + 25;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 25 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 25;
                                    dinero3 = dinero3 + 25;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 25 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 25;
                                    dinero3 = dinero3 + 25;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 25 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño16 == nickname4)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 25;
                                    dinero4 = dinero4 + 25;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 25 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 25;
                                    dinero4 = dinero4 + 25;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 25 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 25;
                                    dinero4 = dinero4 + 25;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 25 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                            }
                        }
                    }
                    break;
                case 17: //SOLAR---MODULO 2
                    if (nickdueño17 == "")
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 180)
                            {
                                dinero1 = dinero1 - 180;
                                nickdueño17 = nickname1;
                                return ("2" + "-" + dinero1 + "-" + nickname1 + "-" + 17);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname2)
                        {
                            if (dinero2 >= 180)
                            {
                                dinero2 = dinero2 - 180;
                                nickdueño17 = nickname2;
                                return ("2" + "-" + dinero2 + "-" + nickname2 + "-" + 17);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname3)
                        {
                            if (dinero3 >= 180)
                            {
                                dinero3 = dinero3 - 180;
                                nickdueño17 = nickname3;
                                return ("2" + "-" + dinero3 + "-" + nickname3 + "-" + 17);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname4)
                        {
                            if (dinero4 >= 180)
                            {
                                dinero4 = dinero4 - 180;
                                nickdueño17 = nickname4;
                                return ("2" + "-" + dinero4 + "-" + nickname4 + "-" + 17);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                    }
                    else
                    {
                        if (nick == nickdueño17)
                        {
                            if (nick == nickname1)
                            {
                                if(nick == nickdueño17 && nick == nickdueño19 && nick == nickdueño20)
                            	{
                                	if(casadueño17 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero1 >= 70)
                                    	{ 
                                        	dinero1 = dinero1 - 70; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño17 = nickname1;
                                        	return ("6" + "-" + dinero1 + "-" + nickname1 + "-" + 17);
                                    	}
                                	}
                                	else if (casadueño17 == nickname1 && hoteldueño17 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero1 >= 950)
                                    	{
                                        	dinero1 = dinero1 - 950; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño17 = nickname1;
                                        	return ("7" + "-" + dinero1 + "-" + nickname1 + "-" + 17);
                                    	}
                                	}
                                	else if (casadueño17 == nickname1 && hoteldueño17 == nickname1) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero1 = dinero1;
                                    	return ("1" + "-" + dinero1 + "-" + nickname1);
                                	}
                                }
                            }
                            if (nick == nickname2)
                            {
                                if(nick == nickdueño17 && nick == nickdueño19 && nick == nickdueño20)
                            	{
                                	if(casadueño17 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero2 >= 70)
                                    	{ 
                                        	dinero2 = dinero2 - 70; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño17 = nickname2;
                                        	return ("6" + "-" + dinero2 + "-" + nickname2 + "-" + 17);
                                    	}
                                	}
                                	else if (casadueño17 == nickname2 && hoteldueño17 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero2 >= 950)
                                    	{
                                        	dinero2 = dinero2 - 950; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño17 = nickname2;
                                        	return ("7" + "-" + dinero2 + "-" + nickname2 + "-" + 17);
                                    	}
                                	}
                                	else if (casadueño17 == nickname2 && hoteldueño17 == nickname2) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero2 = dinero2;
                                    	return ("1" + "-" + dinero2 + "-" + nickname2);
                                	}
                                }
                            }
                            if (nick == nickname3)
                            {
                                if(nick == nickdueño17 && nick == nickdueño19 && nick == nickdueño20)
                            	{
                                	if(casadueño17 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero3 >= 70)
                                    	{ 
                                        	dinero3 = dinero3 - 70; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño17 = nickname3;
                                        	return ("6" + "-" + dinero3 + "-" + nickname3 + "-" + 17);
                                    	}
                                	}
                                	else if (casadueño17 == nickname3 && hoteldueño17 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero3 >= 950)
                                    	{
                                        	dinero3 = dinero3 - 950; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño17 = nickname3;
                                        	return ("7" + "-" + dinero3 + "-" + nickname3 + "-" + 17);
                                    	}
                                	}
                                	else if (casadueño17 == nickname3 && hoteldueño17 == nickname3) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero3 = dinero3;
                                    	return ("1" + "-" + dinero3 + "-" + nickname3);
                                	}
                                }
                            }
                            if (nick == nickname4)
                            {
                                if(nick == nickdueño17 && nick == nickdueño19 && nick == nickdueño20)
                            	{
                                	if(casadueño17 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero4 >= 70)
                                    	{ 
                                        	dinero4 = dinero4 - 70; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño17 = nickname4;
                                        	return ("6" + "-" + dinero4 + "-" + nickname4 + "-" + 17);
                                    	}
                                	}
                                	else if (casadueño17 == nickname4 && hoteldueño17 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero4 >= 950)
                                    	{
                                        	dinero4 = dinero4 - 950; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño17 = nickname4;
                                        	return ("7" + "-" + dinero4 + "-" + nickname4 + "-" + 17);
                                    	}
                                	}
                                	else if (casadueño17 == nickname4 && hoteldueño17 == nickname4) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero4 = dinero4;
                                    	return ("1" + "-" + dinero4 + "-" + nickname4);
                                	}
                                }
                            }
                        }
                        else
                        {
                            if (nickdueño17 == nickname1)
                            {
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 14;
                                    dinero1 = dinero1 + 14;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 14 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 14;
                                    dinero1 = dinero1 + 14;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 14 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 14;
                                    dinero1 = dinero1 + 14;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 14 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño17 == nickname2)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 14;
                                    dinero2 = dinero2 + 14;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 14 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 14;
                                    dinero2 = dinero2 + 14;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 14 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 14;
                                    dinero2 = dinero2 + 14;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 14 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño17 == nickname3)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 14;
                                    dinero3 = dinero3 + 14;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 14 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 14;
                                    dinero3 = dinero3 + 14;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 14 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 14;
                                    dinero3 = dinero3 + 14;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 14 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño17 == nickname4)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 14;
                                    dinero4 = dinero4 + 14;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 14 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 14;
                                    dinero4 = dinero4 + 14;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 14 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 14;
                                    dinero4 = dinero4 + 14;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 14 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                            }
                        }
                    }
                    break;
                case 18: //TESORERÍA UCAB
                    int tesoreria1 = random.Next(1, 9);
                    if (tesoreria1 == 1)
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 150)
                            {
                                dinero1 = dinero1 - 150;
                                return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Tesorería" + "-" + 1);
                            }
                        }
                        if (nick == nickname2)
                        {
                            if (dinero2 >= 150)
                            {
                                dinero2 = dinero2 - 150;
                                return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Tesorería" + "-" + 1);
                            }
                        }
                        if (nick == nickname3)
                        {
                            if (dinero3 >= 150)
                            {
                                dinero3 = dinero3 - 150;
                                return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Tesorería" + "-" + 1);
                            }
                        }
                        if (nick == nickname4)
                        {
                            if (dinero4 >= 150)
                            {
                                dinero4 = dinero4 - 150;
                                return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Tesorería" + "-" + 1);
                            }
                        }

                    }
                    if (tesoreria1 == 2)
                    {
                        if (nick == nickname1)
                        {
                            dinero1 = dinero1 + 200;
                            return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Tesorería" + "-" + 2);
                        }
                        if (nick == nickname2)
                        {
                            dinero2 = dinero2 + 200;
                            return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Tesorería" + "-" + 2);
                        }
                        if (nick == nickname3)
                        {
                            dinero3 = dinero3 + 200;
                            return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Tesorería" + "-" + 2);
                        }
                        if (nick == nickname4)
                        {
                            dinero4 = dinero4 + 200;
                            return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Tesorería" + "-" + 2);
                        }
                    }
                    if (tesoreria1 == 3)
                    {
                        if (nick == nickname1)
                        {
                            dinero1 = dinero1 + 100;
                            return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Tesorería" + "-" + 3);
                        }
                        if (nick == nickname2)
                        {
                            dinero2 = dinero2 + 100;
                            return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Tesorería" + "-" + 3);
                        }
                        if (nick == nickname3)
                        {
                            dinero3 = dinero3 + 100;
                            return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Tesorería" + "-" + 3);
                        }
                        if (nick == nickname4)
                        {
                            dinero4 = dinero4 + 100;
                            return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Tesorería" + "-" + 3);
                        }
                    }
                    if (tesoreria1 == 4)
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 100)
                            {
                                dinero1 = dinero1 - 100;
                                return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Tesorería" + "-" + 4);
                            }
                        }
                        if (nick == nickname2)
                        {
                            if (dinero2 >= 100)
                            {
                                dinero2 = dinero2 - 100;
                                return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Tesorería" + "-" + 4);
                            }
                        }
                        if (nick == nickname3)
                        {
                            if (dinero3 >= 100)
                            {
                                dinero3 = dinero3 - 100;
                                return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Tesorería" + "-" + 4);
                            }
                        }
                        if (nick == nickname4)
                        {
                            if (dinero4 >= 100)
                            {
                                dinero4 = dinero4 - 100;
                                return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Tesorería" + "-" + 4);
                            }
                        }
                    }
                    if (tesoreria1 == 5)
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 100)
                            {
                                dinero1 = dinero1 - 100;
                                return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Tesorería" + "-" + 5);
                            }
                        }
                        if (nick == nickname2)
                        {
                            if (dinero2 >= 100)
                            {
                                dinero2 = dinero2 - 100;
                                return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Tesorería" + "-" + 5);
                            }
                        }
                        if (nick == nickname3)
                        {
                            if (dinero3 >= 100)
                            {
                                dinero3 = dinero3 - 100;
                                return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Tesorería" + "-" + 5);
                            }
                        }
                        if (nick == nickname4)
                        {
                            if (dinero4 >= 100)
                            {
                                dinero4 = dinero4 - 100;
                                return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Tesorería" + "-" + 5);
                            }
                        }
                    }
                    if (tesoreria1 == 6)
                    {
                        if (nick == nickname1)
                        {
                            dinero1 = dinero1 + 150;
                            return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Tesorería" + "-" + 6);
                        }
                        if (nick == nickname2)
                        {
                            dinero2 = dinero2 + 150;
                            return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Tesorería" + "-" + 6);
                        }
                        if (nick == nickname3)
                        {
                            dinero3 = dinero3 + 150;
                            return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Tesorería" + "-" + 6);
                        }
                        if (nick == nickname4)
                        {
                            dinero4 = dinero4 + 150;
                            return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Tesorería" + "-" + 6);
                        }
                    }
                    if (tesoreria1 == 7)
                    {
                        if (nick == nickname1)
                        {
                            dinero1 = dinero1 + 10;
                            return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Tesorería" + "-" + 7);
                        }
                        if (nick == nickname2)
                        {
                            dinero2 = dinero2 + 10;
                            return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Tesorería" + "-" + 7);
                        }
                        if (nick == nickname3)
                        {
                            dinero3 = dinero3 + 10;
                            return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Tesorería" + "-" + 7);
                        }
                        if (nick == nickname4)
                        {
                            dinero4 = dinero4 + 10;
                            return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Tesorería" + "-" + 7);
                        }
                    }
                    if (tesoreria1 == 8)
                    {
                        if (nick == nickname1)
                        {
                            dinero1 = dinero1 + 200;
                            return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Tesorería" + "-" + 8);
                        }
                        if (nick == nickname2)
                        {
                            dinero2 = dinero2 + 200;
                            return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Tesorería" + "-" + 8);
                        }
                        if (nick == nickname3)
                        {
                            dinero3 = dinero3 + 200;
                            return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Tesorería" + "-" + 8);
                        }
                        if (nick == nickname4)
                        {
                            dinero4 = dinero4 + 200;
                            return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Tesorería" + "-" + 8);
                        }
                    }
                    break;
                case 19: //SOLAR---MODULO 1
                    if (nickdueño19 == "")
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 180)
                            {
                                dinero1 = dinero1 - 180;
                                nickdueño19 = nickname1;
                                return ("2" + "-" + dinero1 + "-" + nickname1 + "-" + 19);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname2)
                        {
                            if (dinero2 >= 180)
                            {
                                dinero2 = dinero2 - 180;
                                nickdueño19 = nickname2;
                                return ("2" + "-" + dinero2 + "-" + nickname2 + "-" + 19);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname3)
                        {
                            if (dinero3 >= 180)
                            {
                                dinero3 = dinero3 - 180;
                                nickdueño19 = nickname3;
                                return ("2" + "-" + dinero3 + "-" + nickname3 + "-" + 19);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname4)
                        {
                            if (dinero4 >= 180)
                            {
                                dinero4 = dinero4 - 180;
                                nickdueño19 = nickname4;
                                return ("2" + "-" + dinero4 + "-" + nickname4 + "-" + 19);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                    }
                    else
                    {
                        if (nick == nickdueño19)
                        {
                            if (nick == nickname1)
                            {
                                if(nick == nickdueño17 && nick == nickdueño19 && nick == nickdueño20)
                            	{
                                	if(casadueño19 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero1 >= 70)
                                    	{ 
                                        	dinero1 = dinero1 - 70; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño19 = nickname1;
                                        	return ("6" + "-" + dinero1 + "-" + nickname1 + "-" + 19);
                                    	}
                                	}
                                	else if (casadueño19 == nickname1 && hoteldueño19 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero1 >= 950)
                                    	{
                                        	dinero1 = dinero1 - 950; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño19 = nickname1;
                                        	return ("7" + "-" + dinero1 + "-" + nickname1 + "-" + 19);
                                    	}
                                	}
                                	else if (casadueño19 == nickname1 && hoteldueño19 == nickname1) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero1 = dinero1;
                                    	return ("1" + "-" + dinero1 + "-" + nickname1);
                                	}
                                }
                            }
                            if (nick == nickname2)
                            {
                                if(nick == nickdueño17 && nick == nickdueño19 && nick == nickdueño20)
                            	{
                                	if(casadueño19 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero2 >= 70)
                                    	{ 
                                        	dinero2 = dinero2 - 70; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño19 = nickname2;
                                        	return ("6" + "-" + dinero2 + "-" + nickname2 + "-" + 19);
                                    	}
                                	}
                                	else if (casadueño19 == nickname2 && hoteldueño19 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero2 >= 950)
                                    	{
                                        	dinero2 = dinero2 - 950; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño19 = nickname2;
                                        	return ("7" + "-" + dinero2 + "-" + nickname2 + "-" + 19);
                                    	}
                                	}
                                	else if (casadueño19 == nickname2 && hoteldueño19 == nickname2) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero2 = dinero2;
                                    	return ("1" + "-" + dinero2 + "-" + nickname2);
                                	}
                                }
                            }
                            if (nick == nickname3)
                            {
                                if(nick == nickdueño17 && nick == nickdueño19 && nick == nickdueño20)
                            	{
                                	if(casadueño19 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero3 >= 70)
                                    	{ 
                                        	dinero3 = dinero3 - 70; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño19 = nickname3;
                                        	return ("6" + "-" + dinero3 + "-" + nickname3 + "-" + 19);
                                    	}
                                	}
                                	else if (casadueño19 == nickname3 && hoteldueño19 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero3 >= 950)
                                    	{
                                        	dinero3 = dinero3 - 950; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño19 = nickname3;
                                        	return ("7" + "-" + dinero3 + "-" + nickname3 + "-" + 19);
                                    	}
                                	}
                                	else if (casadueño19 == nickname3 && hoteldueño19 == nickname3) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero3 = dinero3;
                                    	return ("1" + "-" + dinero3 + "-" + nickname3);
                                	}
                                }
                            }
                            if (nick == nickname4)
                            {
                                if(nick == nickdueño17 && nick == nickdueño19 && nick == nickdueño20)
                            	{
                                	if(casadueño19 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero4 >= 70)
                                    	{ 
                                        	dinero4 = dinero4 - 70; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño19 = nickname4;
                                        	return ("6" + "-" + dinero4 + "-" + nickname4 + "-" + 19);
                                    	}
                                	}
                                	else if (casadueño19 == nickname4 && hoteldueño19 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero4 >= 950)
                                    	{
                                        	dinero4 = dinero4 - 950; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño19 = nickname4;
                                        	return ("7" + "-" + dinero4 + "-" + nickname4 + "-" + 19);
                                    	}
                                	}
                                	else if (casadueño19 == nickname4 && hoteldueño19 == nickname4) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero4 = dinero4;
                                    	return ("1" + "-" + dinero4 + "-" + nickname4);
                                	}
                                }
                            }
                        }
                        else
                        {
                            if (nickdueño19 == nickname1)
                            {
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 14;
                                    dinero1 = dinero1 + 14;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 14 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 14;
                                    dinero1 = dinero1 + 14;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 14 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 14;
                                    dinero1 = dinero1 + 14;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 14 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño19 == nickname2)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 14;
                                    dinero2 = dinero2 + 14;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 14 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 14;
                                    dinero2 = dinero2 + 14;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 14 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 14;
                                    dinero2 = dinero2 + 14;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 14 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño19 == nickname3)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 14;
                                    dinero3 = dinero3 + 14;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 14 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 14;
                                    dinero3 = dinero3 + 14;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 14 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 14;
                                    dinero3 = dinero3 + 14;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 14 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño19 == nickname4)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 14;
                                    dinero4 = dinero4 + 14;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 14 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 14;
                                    dinero4 = dinero4 + 14;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 14 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 14;
                                    dinero4 = dinero4 + 14;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 14 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                            }
                        }
                    }
                    break;
                case 20: //SOLAR---SOLARIUM
                    if (nickdueño20 == "")
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 200)
                            {
                                dinero1 = dinero1 - 200;
                                nickdueño20 = nickname1;
                                return ("2" + "-" + dinero1 + "-" + nickname1 + "-" + 20);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname2)
                        {
                            if (dinero2 >= 200)
                            {
                                dinero2 = dinero2 - 200;
                                nickdueño20 = nickname2;
                                return ("2" + "-" + dinero2 + "-" + nickname2 + "-" + 20);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname3)
                        {
                            if (dinero3 >= 200)
                            {
                                dinero3 = dinero3 - 200;
                                nickdueño20 = nickname3;
                                return ("2" + "-" + dinero3 + "-" + nickname3 + "-" + 20);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname4)
                        {
                            if (dinero4 >= 200)
                            {
                                dinero4 = dinero4 - 200;
                                nickdueño20 = nickname4;
                                return ("2" + "-" + dinero4 + "-" + nickname4 + "-" + 20);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                    }
                    else
                    {
                        if (nick == nickdueño20)
                        {
                            if (nick == nickname1)
                            {
                                if(nick == nickdueño17 && nick == nickdueño19 && nick == nickdueño20)
                            	{
                                	if(casadueño20 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero1 >= 80)
                                    	{ 
                                        	dinero1 = dinero1 - 80; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño20 = nickname1;
                                        	return ("6" + "-" + dinero1 + "-" + nickname1 + "-" + 20);
                                    	}
                                	}
                                	else if (casadueño20 == nickname1 && hoteldueño20 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero1 >= 1000)
                                    	{
                                        	dinero1 = dinero1 - 1000; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño20 = nickname1;
                                        	return ("7" + "-" + dinero1 + "-" + nickname1 + "-" + 20);
                                    	}
                                	}
                                	else if (casadueño20 == nickname1 && hoteldueño20 == nickname1) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero1 = dinero1;
                                    	return ("1" + "-" + dinero1 + "-" + nickname1);
                                	}
                                }
                            }
                            if (nick == nickname2)
                            {
                                if(nick == nickdueño17 && nick == nickdueño19 && nick == nickdueño20)
                            	{
                                	if(casadueño20 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero2 >= 80)
                                    	{ 
                                        	dinero2 = dinero2 - 80; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño20 = nickname2;
                                        	return ("6" + "-" + dinero2 + "-" + nickname2 + "-" + 20);
                                    	}
                                	}
                                	else if (casadueño20 == nickname2 && hoteldueño20 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero2 >= 1000)
                                    	{
                                        	dinero2 = dinero2 - 1000; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño20 = nickname2;
                                        	return ("7" + "-" + dinero2 + "-" + nickname2 + "-" + 20);
                                    	}
                                	}
                                	else if (casadueño20 == nickname2 && hoteldueño20 == nickname2) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero2 = dinero2;
                                    	return ("1" + "-" + dinero2 + "-" + nickname2);
                                	}
                                }
                            }
                            if (nick == nickname3)
                            {
                                if(nick == nickdueño17 && nick == nickdueño19 && nick == nickdueño20)
                            	{
                                	if(casadueño20 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero3 >= 80)
                                    	{ 
                                        	dinero3 = dinero3 - 80; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño20 = nickname3;
                                        	return ("6" + "-" + dinero3 + "-" + nickname3 + "-" + 20);
                                    	}
                                	}
                                	else if (casadueño20 == nickname3 && hoteldueño20 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero3 >= 1000)
                                    	{
                                        	dinero3 = dinero3 - 1000; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño20 = nickname3;
                                        	return ("7" + "-" + dinero3 + "-" + nickname3 + "-" + 20);
                                    	}
                                	}
                                	else if (casadueño20 == nickname3 && hoteldueño20 == nickname3) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero3 = dinero3;
                                    	return ("1" + "-" + dinero3 + "-" + nickname3);
                                	}
                                }
                            }
                            if (nick == nickname4)
                            {
                                if(nick == nickdueño17 && nick == nickdueño19 && nick == nickdueño20)
                            	{
                                	if(casadueño20 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero4 >= 80)
                                    	{ 
                                        	dinero4 = dinero4 - 80; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño20 = nickname4;
                                        	return ("6" + "-" + dinero4 + "-" + nickname4 + "-" + 20);
                                    	}
                                	}
                                	else if (casadueño20 == nickname4 && hoteldueño20 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero4 >= 1000)
                                    	{
                                        	dinero4 = dinero4 - 1000; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño20 = nickname4;
                                        	return ("7" + "-" + dinero4 + "-" + nickname4 + "-" + 20);
                                    	}
                                	}
                                	else if (casadueño20 == nickname4 && hoteldueño20 == nickname4) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero4 = dinero4;
                                    	return ("1" + "-" + dinero4 + "-" + nickname4);
                                	}
                                }
                            }
                        }
                        else
                        {
                            if (nickdueño20 == nickname1)
                            {
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 16;
                                    dinero1 = dinero1 + 16;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 16 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 16;
                                    dinero1 = dinero1 + 16;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 16 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 16;
                                    dinero1 = dinero1 + 16;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 16 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño20 == nickname2)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 16;
                                    dinero2 = dinero2 + 16;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 16 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 16;
                                    dinero2 = dinero2 + 16;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 16 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 16;
                                    dinero2 = dinero2 + 16;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 16 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño20 == nickname3)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 16;
                                    dinero3 = dinero3 + 16;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 16 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 16;
                                    dinero3 = dinero3 + 16;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 16 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 16;
                                    dinero3 = dinero3 + 16;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 16 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño20 == nickname4)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 16;
                                    dinero4 = dinero4 + 16;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 16 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 16;
                                    dinero4 = dinero4 + 16;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 16 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 16;
                                    dinero4 = dinero4 + 16;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 16 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                            }
                        }
                    }
           
                    break;
                case 21: //APARCADO LIBRE
                    if (nick == nickname1)
                    {
                        dinero1 = dinero1;
                        return ("1" + "-" + dinero1 + "-" + nickname1);
                    }
                    if (nick == nickname2)
                    {
                        dinero2 = dinero2;
                        return ("1" + "-" + dinero2 + "-" + nickname2);
                    }
                    if (nick == nickname3)
                    {
                        dinero3 = dinero3;
                        return ("1" + "-" + dinero3 + "-" + nickname3);
                    }
                    if (nick == nickname4)
                    {
                        dinero4 = dinero4;
                        return ("1" + "-" + dinero4 + "-" + nickname4);
                    }
                    break;
                case 22: //SOLAR---LABORATORIOS
                    if (nickdueño22 == "")
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 200)
                            {
                                dinero1 = dinero1 - 200;
                                nickdueño22 = nickname1;
                                return ("2" + "-" + dinero1 + "-" + nickname1 + "-" + 22);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname2)
                        {
                            if (dinero2 >= 200)
                            {
                                dinero2 = dinero2 - 200;
                                nickdueño22 = nickname2;
                                return ("2" + "-" + dinero2 + "-" + nickname2 + "-" + 22);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname3)
                        {
                            if (dinero3 >= 200)
                            {
                                dinero3 = dinero3 - 200;
                                nickdueño22 = nickname3;
                                return ("2" + "-" + dinero3 + "-" + nickname3 + "-" + 22);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname4)
                        {
                            if (dinero4 >= 200)
                            {
                                dinero4 = dinero4 - 200;
                                nickdueño22 = nickname4;
                                return ("2" + "-" + dinero4 + "-" + nickname4 + "-" + 22);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                    }
                    else
                    {
                        if (nick == nickdueño22)
                        {
                            if (nick == nickname1)
                            {
                                if(nick == nickdueño22 && nick == nickdueño24 && nick == nickdueño25)
                            	{
                                	if(casadueño22 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero1 >= 90)
                                    	{ 
                                        	dinero1 = dinero1 - 90; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño22 = nickname1;
                                        	return ("6" + "-" + dinero1 + "-" + nickname1 + "-" + 22);
                                    	}
                                	}
                                	else if (casadueño22 == nickname1 && hoteldueño22 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero1 >= 1050)
                                    	{
                                        	dinero1 = dinero1 - 1050; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño22 = nickname1;
                                        	return ("7" + "-" + dinero1 + "-" + nickname1 + "-" + 22);
                                    	}
                                	}
                                	else if (casadueño22 == nickname1 && hoteldueño22 == nickname1) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero1 = dinero1;
                                    	return ("1" + "-" + dinero1 + "-" + nickname1);
                                	}
                                }
                            }
                            if (nick == nickname2)
                            {
                                if(nick == nickdueño22 && nick == nickdueño24 && nick == nickdueño25)
                            	{
                                	if(casadueño22 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero2 >= 90)
                                    	{ 
                                        	dinero2 = dinero2 - 90; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño22 = nickname2;
                                        	return ("6" + "-" + dinero2 + "-" + nickname2 + "-" + 22);
                                    	}
                                	}
                                	else if (casadueño22 == nickname2 && hoteldueño22 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero2 >= 1050)
                                    	{
                                        	dinero2 = dinero2 - 1050; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño22 = nickname2;
                                        	return ("7" + "-" + dinero2 + "-" + nickname2 + "-" + 22);
                                    	}
                                	}
                                	else if (casadueño22 == nickname2 && hoteldueño22 == nickname2) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero2 = dinero2;
                                    	return ("1" + "-" + dinero2 + "-" + nickname2);
                                	}
                                }
                            }
                            if (nick == nickname3)
                            {
                                if(nick == nickdueño22 && nick == nickdueño24 && nick == nickdueño25)
                            	{
                                	if(casadueño22 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero3 >= 90)
                                    	{ 
                                        	dinero3 = dinero3 - 90; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño22 = nickname3;
                                        	return ("6" + "-" + dinero3 + "-" + nickname3 + "-" + 22);
                                    	}
                                	}
                                	else if (casadueño22 == nickname3 && hoteldueño22 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero3 >= 1050)
                                    	{
                                        	dinero3 = dinero3 - 1050; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño22 = nickname3;
                                        	return ("7" + "-" + dinero3 + "-" + nickname3 + "-" + 22);
                                    	}
                                	}
                                	else if (casadueño22 == nickname3 && hoteldueño22 == nickname3) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero3 = dinero3;
                                    	return ("1" + "-" + dinero3 + "-" + nickname3);
                                	}
                                }
                            }
                            if (nick == nickname4)
                            {
                                if(nick == nickdueño22 && nick == nickdueño24 && nick == nickdueño25)
                            	{
                                	if(casadueño22 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero4 >= 90)
                                    	{ 
                                        	dinero4 = dinero4 - 90; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño22 = nickname4;
                                        	return ("6" + "-" + dinero4 + "-" + nickname4 + "-" + 22);
                                    	}
                                	}
                                	else if (casadueño22 == nickname4 && hoteldueño22 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero4 >= 1050)
                                    	{
                                        	dinero4 = dinero4 - 1050; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño22 = nickname4;
                                        	return ("7" + "-" + dinero4 + "-" + nickname4 + "-" + 22);
                                    	}
                                	}
                                	else if (casadueño22 == nickname4 && hoteldueño22 == nickname4) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero4 = dinero4;
                                    	return ("1" + "-" + dinero4 + "-" + nickname4);
                                	}
                                }
                            }
                        }
                        else
                        {
                            if (nickdueño22 == nickname1)
                            {
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 18;
                                    dinero1 = dinero1 + 18;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 18 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 18;
                                    dinero1 = dinero1 + 18;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 18 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 18;
                                    dinero1 = dinero1 + 18;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 18 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño22 == nickname2)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 18;
                                    dinero2 = dinero2 + 18;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 18 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 18;
                                    dinero2 = dinero2 + 18;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 18 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 18;
                                    dinero2 = dinero2 + 18;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 18 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño22 == nickname3)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 18;
                                    dinero3 = dinero3 + 18;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 18 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 18;
                                    dinero3 = dinero3 + 18;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 18 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 18;
                                    dinero3 = dinero3 + 18;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 18 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño22 == nickname4)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 18;
                                    dinero4 = dinero4 + 18;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 18 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 18;
                                    dinero4 = dinero4 + 18;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 18 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 18;
                                    dinero4 = dinero4 + 18;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 18 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                            }
                        }
                    }
            
                    break;
                case 23: //CASUALIDAD
                    int casualidad1 = random.Next(1, 6);
                    if (casualidad1 == 1)
                    {
                        if (nick == nickname1)
                        {
                            dinero1 = dinero1 + 200;
                            return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Casualidad" + "-" + 1);
                        }
                        if (nick == nickname2)
                        {
                            dinero2 = dinero2 + 200;
                            return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Casualidad" + "-" + 1);
                        }
                        if (nick == nickname3)
                        {
                            dinero3 = dinero3 + 200;
                            return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Casualidad" + "-" + 1);
                        }
                        if (nick == nickname4)
                        {
                            dinero4 = dinero4 + 200;
                            return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Casualidad" + "-" + 1);
                        }
                    }
                    if (casualidad1 == 2)
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 15)
                            {
                                dinero1 = dinero1 - 15;
                                return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Casualidad" + "-" + 2);
                            }
                        }
                        if (nick == nickname2)
                        {
                            if (dinero2 >= 15)
                            {
                                dinero2 = dinero2 - 15;
                                return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Casualidad" + "-" + 2);
                            }
                        }
                        if (nick == nickname3)
                        {
                            if (dinero3 >= 15)
                            {
                                dinero3 = dinero3 - 15;
                                return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Casualidad" + "-" + 2);
                            }
                        }
                        if (nick == nickname4)
                        {
                            if (dinero4 >= 15)
                            {
                                dinero4 = dinero4 - 15;
                                return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Casualidad" + "-" + 2);
                            }
                        }
                    }
                    if (casualidad1 == 3)
                    {
                        if (nick == nickname1)
                        {
                            dinero1 = dinero1 + 50;
                            return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Casualidad" + "-" + 3);
                        }
                        if (nick == nickname2)
                        {
                            dinero2 = dinero2 + 50;
                            return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Casualidad" + "-" + 3);
                        }
                        if (nick == nickname3)
                        {
                            dinero3 = dinero3 + 50;
                            return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Casualidad" + "-" + 3);
                        }
                        if (nick == nickname4)
                        {
                            dinero4 = dinero4 + 50;
                            return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Casualidad" + "-" + 3);
                        }
                    }
                    if (casualidad1 == 4)
                    {
                        if (nick == nickname1)
                        {
                            dinero1 = dinero1 + 150;
                            return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Casualidad" + "-" + 4);
                        }
                        if (nick == nickname2)
                        {
                            dinero2 = dinero2 + 150;
                            return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Casualidad" + "-" + 4);
                        }
                        if (nick == nickname3)
                        {
                            dinero3 = dinero3 + 150;
                            return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Casualidad" + "-" + 4);
                        }
                        if (nick == nickname4)
                        {
                            dinero4 = dinero4 + 150;
                            return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Casualidad" + "-" + 4);
                        }
                    }
                    if (casualidad1 == 5)
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 15)
                            {
                                dinero1 = dinero1 - 100;
                                return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Casualidad" + "-" + 5);
                            }
                        }
                        if (nick == nickname2)
                        {
                            if (dinero2 >= 15)
                            {
                                dinero2 = dinero2 - 100;
                                return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Casualidad" + "-" + 5);
                            }
                        }
                        if (nick == nickname3)
                        {
                            if (dinero3 >= 15)
                            {
                                dinero3 = dinero3 - 100;
                                return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Casualidad" + "-" + 5);
                            }
                        }
                        if (nick == nickname4)
                        {
                            if (dinero4 >= 15)
                            {
                                dinero4 = dinero4 - 100;
                                return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Casualidad" + "-" + 5);
                            }
                        }
                    }
                    if (casualidad1 == 6)
                    {
                        if (nick == nickname1)
                        {
                            tarjeta1 = "Si";
                            return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Casualidad" + "-" + 6);
                        }
                        if (nick == nickname2)
                        {
                            tarjeta2 = "Si";
                            return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Casualidad" + "-" + 6);
                        }
                        if (nick == nickname3)
                        {
                            tarjeta3 = "Si";
                            return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Casualidad" + "-" + 6);
                        }
                        if (nick == nickname4)
                        {
                            tarjeta4 = "Si";
                            return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Casualidad" + "-" + 6);
                        }
                    }
                    break;
                case 24: //SOLAR---FACULTADES
                    if (nickdueño24 == "")
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 220)
                            {
                                dinero1 = dinero1 - 220;
                                nickdueño24 = nickname1;
                                return ("2" + "-" + dinero1 + "-" + nickname1 + "-" + 24);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname2)
                        {
                            if (dinero2 >= 220)
                            {
                                dinero2 = dinero2 - 220;
                                nickdueño24 = nickname2;
                                return ("2" + "-" + dinero2 + "-" + nickname2 + "-" + 24);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname3)
                        {
                            if (dinero3 >= 220)
                            {
                                dinero3 = dinero3 - 220;
                                nickdueño24 = nickname3;
                                return ("2" + "-" + dinero3 + "-" + nickname3 + "-" + 24);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname4)
                        {
                            if (dinero4 >= 200)
                            {
                                dinero4 = dinero4 - 220;
                                nickdueño24 = nickname4;
                                return ("2" + "-" + dinero4 + "-" + nickname4 + "-" + 24);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                    }
                    else
                    {
                        if (nick == nickdueño24)
                        {
                            if (nick == nickname1)
                            {
                                if(nick == nickdueño22 && nick == nickdueño24 && nick == nickdueño25)
                            	{
                                	if(casadueño24 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero1 >= 90)
                                    	{ 
                                        	dinero1 = dinero1 - 90; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño24 = nickname1;
                                        	return ("6" + "-" + dinero1 + "-" + nickname1 + "-" + 24);
                                    	}
                                	}
                                	else if (casadueño24 == nickname1 && hoteldueño24 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero1 >= 1050)
                                    	{
                                        	dinero1 = dinero1 - 1050; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño24 = nickname1;
                                        	return ("7" + "-" + dinero1 + "-" + nickname1 + "-" + 24);
                                    	}
                                	}
                                	else if (casadueño24 == nickname1 && hoteldueño24 == nickname1) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero1 = dinero1;
                                    	return ("1" + "-" + dinero1 + "-" + nickname1);
                                	}
                                }
                            }
                            if (nick == nickname2)
                            {
                                if(nick == nickdueño22 && nick == nickdueño24 && nick == nickdueño25)
                            	{
                                	if(casadueño24 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero2 >= 90)
                                    	{ 
                                        	dinero2 = dinero2 - 90; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño24 = nickname2;
                                        	return ("6" + "-" + dinero2 + "-" + nickname2 + "-" + 24);
                                    	}
                                	}
                                	else if (casadueño24 == nickname2 && hoteldueño24 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero2 >= 1050)
                                    	{
                                        	dinero2 = dinero2 - 1050; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño24 = nickname2;
                                        	return ("7" + "-" + dinero2 + "-" + nickname2 + "-" + 24);
                                    	}
                                	}
                                	else if (casadueño24 == nickname2 && hoteldueño24 == nickname2) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero2 = dinero2;
                                    	return ("1" + "-" + dinero2 + "-" + nickname2);
                                	}
                                }
                            }
                            if (nick == nickname3)
                            {
                                if(nick == nickdueño22 && nick == nickdueño24 && nick == nickdueño25)
                            	{
                                	if(casadueño24 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero3 >= 90)
                                    	{ 
                                        	dinero3 = dinero3 - 90; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño24 = nickname3;
                                        	return ("6" + "-" + dinero3 + "-" + nickname3 + "-" + 24);
                                    	}
                                	}
                                	else if (casadueño24 == nickname3 && hoteldueño24 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero3 >= 1050)
                                    	{
                                        	dinero3 = dinero3 - 1050; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño24 = nickname3;
                                        	return ("7" + "-" + dinero3 + "-" + nickname3 + "-" + 24);
                                    	}
                                	}
                                	else if (casadueño24 == nickname3 && hoteldueño24 == nickname3) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero3 = dinero3;
                                    	return ("1" + "-" + dinero3 + "-" + nickname3);
                                	}
                                }
                            }
                            if (nick == nickname4)
                            {
                                if(nick == nickdueño22 && nick == nickdueño24 && nick == nickdueño25)
                            	{
                                	if(casadueño24 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero4 >= 90)
                                    	{ 
                                        	dinero4 = dinero4 - 90; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño24 = nickname4;
                                        	return ("6" + "-" + dinero4 + "-" + nickname4 + "-" + 24);
                                    	}
                                	}
                                	else if (casadueño24 == nickname4 && hoteldueño24 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero4 >= 1050)
                                    	{
                                        	dinero4 = dinero4 - 1050; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño24 = nickname4;
                                        	return ("7" + "-" + dinero4 + "-" + nickname4 + "-" + 24);
                                    	}
                                	}
                                	else if (casadueño24 == nickname4 && hoteldueño24 == nickname4) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero4 = dinero4;
                                    	return ("1" + "-" + dinero4 + "-" + nickname4);
                                	}
                                }
                            }
                        }
                        else
                        {
                            if (nickdueño24 == nickname1)
                            {
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 18;
                                    dinero1 = dinero1 + 18;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 18 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 18;
                                    dinero1 = dinero1 + 18;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 18 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 18;
                                    dinero1 = dinero1 + 18;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 18 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño24 == nickname2)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 18;
                                    dinero2 = dinero2 + 18;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 18 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 18;
                                    dinero2 = dinero2 + 18;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 18 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 18;
                                    dinero2 = dinero2 + 18;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 18 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño24 == nickname3)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 18;
                                    dinero3 = dinero3 + 18;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 18 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 18;
                                    dinero3 = dinero3 + 18;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 18 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 18;
                                    dinero3 = dinero3 + 18;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 18 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño24 == nickname4)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 18;
                                    dinero4 = dinero4 + 18;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 18 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 18;
                                    dinero4 = dinero4 + 18;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 18 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 18;
                                    dinero4 = dinero4 + 18;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 18 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                            }
                        }
                    }
         
                    break;
                case 25: //SOLAR---PLAYA
                    if (nickdueño25 == "")
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 240)
                            {
                                dinero1 = dinero1 - 240;
                                nickdueño25 = nickname1;
                                return ("2" + "-" + dinero1 + "-" + nickname1 + "-" + 25);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname2)
                        {
                            if (dinero2 >= 240)
                            {
                                dinero2 = dinero2 - 240;
                                nickdueño25 = nickname2;
                                return ("2" + "-" + dinero2 + "-" + nickname2 + "-" + 25);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname3)
                        {
                            if (dinero3 >= 240)
                            {
                                dinero3 = dinero3 - 240;
                                nickdueño25 = nickname3;
                                return ("2" + "-" + dinero3 + "-" + nickname3 + "-" + 25);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname4)
                        {
                            if (dinero4 >= 240)
                            {
                                dinero4 = dinero4 - 240;
                                nickdueño25 = nickname4;
                                return ("2" + "-" + dinero4 + "-" + nickname4 + "-" + 25);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                    }
                    else
                    {
                        if (nick == nickdueño25)
                        {
                            if (nick == nickname1)
                            {
                                if(nick == nickdueño22 && nick == nickdueño24 && nick == nickdueño25)
                            	{
                                	if(casadueño25 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero1 >= 100)
                                    	{ 
                                        	dinero1 = dinero1 - 100; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño25 = nickname1;
                                        	return ("6" + "-" + dinero1 + "-" + nickname1 + "-" + 25);
                                    	}
                                	}
                                	else if (casadueño25 == nickname1 && hoteldueño25 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero1 >= 1100)
                                    	{
                                        	dinero1 = dinero1 - 1100; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño25 = nickname1;
                                        	return ("7" + "-" + dinero1 + "-" + nickname1 + "-" + 25);
                                    	}
                                	}
                                	else if (casadueño25 == nickname1 && hoteldueño25 == nickname1) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero1 = dinero1;
                                    	return ("1" + "-" + dinero1 + "-" + nickname1);
                                	}
                                }
                            }
                            if (nick == nickname2)
                            {
                                if(nick == nickdueño22 && nick == nickdueño24 && nick == nickdueño25)
                            	{
                                	if(casadueño25 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero2 >= 100)
                                    	{ 
                                        	dinero2 = dinero2 - 100; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño25 = nickname2;
                                        	return ("6" + "-" + dinero2 + "-" + nickname2 + "-" + 25);
                                    	}
                                	}
                                	else if (casadueño25 == nickname2 && hoteldueño25 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero2 >= 1100)
                                    	{
                                        	dinero2 = dinero2 - 1100; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño25 = nickname2;
                                        	return ("7" + "-" + dinero2 + "-" + nickname2 + "-" + 25);
                                    	}
                                	}
                                	else if (casadueño25 == nickname2 && hoteldueño25 == nickname2) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero2 = dinero2;
                                    	return ("1" + "-" + dinero2 + "-" + nickname2);
                                	}
                                }
                            }
                            if (nick == nickname3)
                            {
                                if(nick == nickdueño22 && nick == nickdueño24 && nick == nickdueño25)
                            	{
                                	if(casadueño25 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero3 >= 100)
                                    	{ 
                                        	dinero3 = dinero3 - 100; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño25 = nickname3;
                                        	return ("6" + "-" + dinero3 + "-" + nickname3 + "-" + 25);
                                    	}
                                	}
                                	else if (casadueño25 == nickname3 && hoteldueño25 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero3 >= 1100)
                                    	{
                                        	dinero3 = dinero3 - 1100; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño25 = nickname3;
                                        	return ("7" + "-" + dinero3 + "-" + nickname3 + "-" + 25);
                                    	}
                                	}
                                	else if (casadueño25 == nickname3 && hoteldueño25 == nickname3) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero3 = dinero3;
                                    	return ("1" + "-" + dinero3 + "-" + nickname3);
                                	}
                                }
                            }
                            if (nick == nickname4)
                            {
                                if(nick == nickdueño22 && nick == nickdueño24 && nick == nickdueño25)
                            	{
                                	if(casadueño25 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero4 >= 100)
                                    	{ 
                                        	dinero4 = dinero4 - 100; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño25 = nickname4;
                                        	return ("6" + "-" + dinero4 + "-" + nickname4 + "-" + 25);
                                    	}
                                	}
                                	else if (casadueño25 == nickname4 && hoteldueño25 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero4 >= 1100)
                                    	{
                                        	dinero4 = dinero4 - 1100; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño25 = nickname4;
                                        	return ("7" + "-" + dinero4 + "-" + nickname4 + "-" + 25);
                                    	}
                                	}
                                	else if (casadueño25 == nickname4 && hoteldueño25 == nickname4) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero4 = dinero4;
                                    	return ("1" + "-" + dinero4 + "-" + nickname4);
                                	}
                                }
                            }
                        }
                        else
                        {
                            if (nickdueño25 == nickname1)
                            {
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 20;
                                    dinero1 = dinero1 + 20;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 20 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 20;
                                    dinero1 = dinero1 + 20;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 20 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 20;
                                    dinero1 = dinero1 + 20;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 20 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño25 == nickname2)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 20;
                                    dinero2 = dinero2 + 20;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 20 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 20;
                                    dinero2 = dinero2 + 20;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 20 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 20;
                                    dinero2 = dinero2 + 20;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 20 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño25 == nickname3)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 20;
                                    dinero3 = dinero3 + 20;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 20 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 20;
                                    dinero3 = dinero3 + 20;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 20 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 20;
                                    dinero3 = dinero3 + 20;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 20 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño25 == nickname4)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 20;
                                    dinero4 = dinero4 + 20;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 20 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 20;
                                    dinero4 = dinero4 + 20;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 20 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 20;
                                    dinero4 = dinero4 + 20;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 20 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                            }
                        }
                    }
        
                    break;
                case 26: //TAXI UCAB NORTE
                    if (nickdueño26 == "")
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 200)
                            {
                                dinero1 = dinero1 - 200;
                                nickdueño26 = nickname1;
                                return ("2" + "-" + dinero1 + "-" + nickname1 + "-" + 26);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname2)
                        {
                            if (dinero2 >= 200)
                            {
                                dinero2 = dinero2 - 200;
                                nickdueño26 = nickname2;
                                return ("2" + "-" + dinero2 + "-" + nickname2 + "-" + 26);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname3)
                        {
                            if (dinero3 >= 200)
                            {
                                dinero3 = dinero3 - 200;
                                nickdueño26 = nickname3;
                                return ("2" + "-" + dinero3 + "-" + nickname3 + "-" + 26);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname4)
                        {
                            if (dinero4 >= 200)
                            {
                                dinero4 = dinero4 - 200;
                                nickdueño26 = nickname4;
                                return ("2" + "-" + dinero4 + "-" + nickname4 + "-" + 26);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                    }
                    else
                    {
                        if (nick == nickdueño26)
                        {
                            if (nick == nickname1)
                            {
                                dinero1 = dinero1;
                                return ("1" + "-" + dinero1 + "-" + nickname1);
                            }
                            if (nick == nickname2)
                            {
                                dinero2 = dinero2;
                                return ("1" + "-" + dinero2 + "-" + nickname2);
                            }
                            if (nick == nickname3)
                            {
                                dinero3 = dinero3;
                                return ("1" + "-" + dinero3 + "-" + nickname3);
                            }
                            if (nick == nickname4)
                            {
                                dinero4 = dinero4;
                                return ("1" + "-" + dinero4 + "-" + nickname4);
                            }
                        }
                        else
                        {
                            if (nickdueño26 == nickname1)
                            {
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 25;
                                    dinero1 = dinero1 + 25;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 25 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 25;
                                    dinero1 = dinero1 + 25;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 25 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 25;
                                    dinero1 = dinero1 + 25;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 25 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño26 == nickname2)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 25;
                                    dinero2 = dinero2 + 25;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 25 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 25;
                                    dinero2 = dinero2 + 25;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 25 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 25;
                                    dinero2 = dinero2 + 25;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 25 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño26 == nickname3)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 25;
                                    dinero3 = dinero3 + 25;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 25 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 25;
                                    dinero3 = dinero3 + 25;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 25 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 25;
                                    dinero3 = dinero3 + 25;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 25 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño26 == nickname4)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 25;
                                    dinero4 = dinero4 + 25;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 25 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 25;
                                    dinero4 = dinero4 + 25;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 25 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 25;
                                    dinero4 = dinero4 + 25;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 25 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                            }
                        }
                    }
     
                    break;
                case 27: //SOLAR---SAMBILITO
                    if (nickdueño27 == "")
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 260)
                            {
                                dinero1 = dinero1 - 260;
                                nickdueño27 = nickname1;
                                return ("2" + "-" + dinero1 + "-" + nickname1 + "-" + 27);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname2)
                        {
                            if (dinero2 >= 260)
                            {
                                dinero2 = dinero2 - 260;
                                nickdueño27 = nickname2;
                                return ("2" + "-" + dinero2 + "-" + nickname2 + "-" + 27);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname3)
                        {
                            if (dinero3 >= 260)
                            {
                                dinero3 = dinero3 - 260;
                                nickdueño27 = nickname3;
                                return ("2" + "-" + dinero3 + "-" + nickname3 + "-" + 27);
                            }
                            else
                            {
                                return "0";
                            }  
                        }
                        else if (nick == nickname4)
                        {
                            if (dinero4 >= 260)
                            {
                                dinero4 = dinero4 - 260;
                                nickdueño27 = nickname4;
                                return ("2" + "-" + dinero4 + "-" + nickname4 + "-" + 27);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                    }
                    else
                    {
                        if (nick == nickdueño27)
                        {
                            if (nick == nickname1)
                            {
                                if(nick == nickdueño27 && nick == nickdueño28 && nick == nickdueño30)
                            	{
                                	if(casadueño27 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero1 >= 110)
                                    	{ 
                                        	dinero1 = dinero1 - 110; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño27 = nickname1;
                                        	return ("6" + "-" + dinero1 + "-" + nickname1 + "-" + 27);
                                    	}
                                	}
                                	else if (casadueño27 == nickname1 && hoteldueño27 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero1 >= 1150)
                                    	{
                                        	dinero1 = dinero1 - 1150; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño27 = nickname1;
                                        	return ("7" + "-" + dinero1 + "-" + nickname1 + "-" + 27);
                                    	}
                                	}
                                	else if (casadueño27 == nickname1 && hoteldueño27 == nickname1) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero1 = dinero1;
                                    	return ("1" + "-" + dinero1 + "-" + nickname1);
                                	}
                                }
                            }
                            if (nick == nickname2)
                            {
                                if(nick == nickdueño27 && nick == nickdueño28 && nick == nickdueño30)
                            	{
                                	if(casadueño27 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero2 >= 110)
                                    	{ 
                                        	dinero2 = dinero2 - 110; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño27 = nickname2;
                                        	return ("6" + "-" + dinero2 + "-" + nickname2 + "-" + 27);
                                    	}
                                	}
                                	else if (casadueño27 == nickname2 && hoteldueño27 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero2 >= 1150)
                                    	{
                                        	dinero2 = dinero2 - 1150; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño27 = nickname2;
                                        	return ("7" + "-" + dinero2 + "-" + nickname2 + "-" + 27);
                                    	}
                                	}
                                	else if (casadueño27 == nickname2 && hoteldueño27 == nickname2) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero2 = dinero2;
                                    	return ("1" + "-" + dinero2 + "-" + nickname2);
                                	}
                                }
                            }
                            if (nick == nickname3)
                            {
                                if(nick == nickdueño27 && nick == nickdueño28 && nick == nickdueño30)
                            	{
                                	if(casadueño27 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero3 >= 110)
                                    	{ 
                                        	dinero3 = dinero3 - 110; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño27 = nickname3;
                                        	return ("6" + "-" + dinero3 + "-" + nickname3 + "-" + 27);
                                    	}
                                	}
                                	else if (casadueño27 == nickname3 && hoteldueño27 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero3 >= 1150)
                                    	{
                                        	dinero3 = dinero3 - 1150; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño27 = nickname3;
                                        	return ("7" + "-" + dinero3 + "-" + nickname3 + "-" + 27);
                                    	}
                                	}
                                	else if (casadueño27 == nickname3 && hoteldueño27 == nickname3) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero3 = dinero3;
                                    	return ("1" + "-" + dinero3 + "-" + nickname3);
                                	}
                                }
                            }
                            if (nick == nickname4)
                            {
                                if(nick == nickdueño27 && nick == nickdueño28 && nick == nickdueño30)
                            	{
                                	if(casadueño27 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero4 >= 110)
                                    	{ 
                                        	dinero4 = dinero4 - 110; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño27 = nickname4;
                                        	return ("6" + "-" + dinero4 + "-" + nickname4 + "-" + 27);
                                    	}
                                	}
                                	else if (casadueño27 == nickname4 && hoteldueño27 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero4 >= 1150)
                                    	{
                                        	dinero4 = dinero4 - 1150; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño27 = nickname4;
                                        	return ("7" + "-" + dinero4 + "-" + nickname4 + "-" + 27);
                                    	}
                                	}
                                	else if (casadueño27 == nickname4 && hoteldueño27 == nickname4) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero4 = dinero4;
                                    	return ("1" + "-" + dinero4 + "-" + nickname4);
                                	}
                                }
                            }
                        }
                        else
                        {
                            if (nickdueño27 == nickname1)
                            {
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 22;
                                    dinero1 = dinero1 + 22;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 22 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 22;
                                    dinero1 = dinero1 + 22;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 22 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 22;
                                    dinero1 = dinero1 + 22;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 22 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño27 == nickname2)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 22;
                                    dinero2 = dinero2 + 22;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 22 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 22;
                                    dinero2 = dinero2 + 22;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 22 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 22;
                                    dinero2 = dinero2 + 22;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 22 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño27 == nickname3)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 22;
                                    dinero3 = dinero3 + 22;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 22 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 22;
                                    dinero3 = dinero3 + 22;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 22 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 22;
                                    dinero3 = dinero3 + 22;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 22 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño27 == nickname4)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 22;
                                    dinero4 = dinero4 + 22;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 22 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 22;
                                    dinero4 = dinero4 + 22;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 22 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 22;
                                    dinero4 = dinero4 + 22;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 22 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                            }
                        }
                    }
     
                    break;
                case 28: //SOLAR---CANCHAS
                    if (nickdueño28 == "")
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 260)
                            {
                                dinero1 = dinero1 - 260;
                                nickdueño28 = nickname1;
                                return ("2" + "-" + dinero1 + "-" + nickname1 + "-" + 28);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname2)
                        {
                            if (dinero2 >= 260)
                            {
                                dinero2 = dinero2 - 260;
                                nickdueño28 = nickname2;
                                return ("2" + "-" + dinero2 + "-" + nickname2 + "-" + 28);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname3)
                        {
                            if (dinero3 >= 260)
                            {
                                dinero3 = dinero3 - 260;
                                nickdueño28 = nickname3;
                                return ("2" + "-" + dinero3 + "-" + nickname3 + "-" + 28);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname4)
                        {
                            if (dinero4 >= 260)
                            {
                                dinero4 = dinero4 - 260;
                                nickdueño28 = nickname4;
                                return ("2" + "-" + dinero4 + "-" + nickname4 + "-" + 28);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                    }
                    else
                    {
                        if (nick == nickdueño28)
                        {
                            if (nick == nickname1)
                            {
                                if(nick == nickdueño27 && nick == nickdueño28 && nick == nickdueño30)
                            	{
                                	if(casadueño28 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero1 >= 110)
                                    	{ 
                                        	dinero1 = dinero1 - 110; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño28 = nickname1;
                                        	return ("6" + "-" + dinero1 + "-" + nickname1 + "-" + 28);
                                    	}
                                	}
                                	else if (casadueño28 == nickname1 && hoteldueño28 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero1 >= 1150)
                                    	{
                                        	dinero1 = dinero1 - 1150; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño28 = nickname1;
                                        	return ("7" + "-" + dinero1 + "-" + nickname1 + "-" + 28);
                                    	}
                                	}
                                	else if (casadueño28 == nickname1 && hoteldueño28 == nickname1) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero1 = dinero1;
                                    	return ("1" + "-" + dinero1 + "-" + nickname1);
                                	}
                                }
                            }
                            if (nick == nickname2)
                            {
                                if(nick == nickdueño27 && nick == nickdueño28 && nick == nickdueño30)
                            	{
                                	if(casadueño28 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero2 >= 110)
                                    	{ 
                                        	dinero2 = dinero2 - 110; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño28 = nickname2;
                                        	return ("6" + "-" + dinero2 + "-" + nickname2 + "-" + 28);
                                    	}
                                	}
                                	else if (casadueño28 == nickname2 && hoteldueño28 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero2 >= 1150)
                                    	{
                                        	dinero2 = dinero2 - 1150; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño28 = nickname2;
                                        	return ("7" + "-" + dinero2 + "-" + nickname2 + "-" + 28);
                                    	}
                                	}
                                	else if (casadueño28 == nickname2 && hoteldueño28 == nickname2) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero2 = dinero2;
                                    	return ("1" + "-" + dinero2 + "-" + nickname2);
                                	}
                                }
                            }
                            if (nick == nickname3)
                            {
                                if(nick == nickdueño27 && nick == nickdueño28 && nick == nickdueño30)
                            	{
                                	if(casadueño28 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero3 >= 110)
                                    	{ 
                                        	dinero3 = dinero3 - 110; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño28 = nickname3;
                                        	return ("6" + "-" + dinero3 + "-" + nickname3 + "-" + 28);
                                    	}
                                	}
                                	else if (casadueño28 == nickname3 && hoteldueño28 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero3 >= 1150)
                                    	{
                                        	dinero3 = dinero3 - 1150; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño28 = nickname3;
                                        	return ("7" + "-" + dinero3 + "-" + nickname3 + "-" + 28);
                                    	}
                                	}
                                	else if (casadueño28 == nickname3 && hoteldueño28 == nickname3) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero3 = dinero3;
                                    	return ("1" + "-" + dinero3 + "-" + nickname3);
                                	}
                                }
                            }
                            if (nick == nickname4)
                            {
                                if(nick == nickdueño27 && nick == nickdueño28 && nick == nickdueño30)
                            	{
                                	if(casadueño28 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero4 >= 110)
                                    	{ 
                                        	dinero4 = dinero4 - 110; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño28 = nickname4;
                                        	return ("6" + "-" + dinero4 + "-" + nickname4 + "-" + 28);
                                    	}
                                	}
                                	else if (casadueño28 == nickname4 && hoteldueño28 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero4 >= 1150)
                                    	{
                                        	dinero4 = dinero4 - 1150; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño28 = nickname4;
                                        	return ("7" + "-" + dinero4 + "-" + nickname4 + "-" + 28);
                                    	}
                                	}
                                	else if (casadueño28 == nickname4 && hoteldueño28 == nickname4) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero4 = dinero4;
                                    	return ("1" + "-" + dinero4 + "-" + nickname4);
                                	}
                                }
                            }
                        }
                        else
                        {
                            if (nickdueño28 == nickname1)
                            {
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 22;
                                    dinero1 = dinero1 + 22;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 22 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 22;
                                    dinero1 = dinero1 + 22;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 22 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 22;
                                    dinero1 = dinero1 + 22;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 22 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño28 == nickname2)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 22;
                                    dinero2 = dinero2 + 22;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 22 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 22;
                                    dinero2 = dinero2 + 22;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 22 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 22;
                                    dinero2 = dinero2 + 22;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 22 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño28 == nickname3)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 22;
                                    dinero3 = dinero3 + 22;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 22 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 22;
                                    dinero3 = dinero3 + 22;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 22 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 22;
                                    dinero3 = dinero3 + 22;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 22 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño28 == nickname4)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 22;
                                    dinero4 = dinero4 + 22;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 22 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 22;
                                    dinero4 = dinero4 + 22;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 22 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 22;
                                    dinero4 = dinero4 + 22;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 22 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                            }
                        }
                    }
      
                    break;
                case 29: //SERVICIO AGUA
                    if (nickdueño29 == "")
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 150)
                            {
                                dinero1 = dinero1 - 150;
                                nickdueño29 = nickname1;
                                return ("2" + "-" + dinero1 + "-" + nickname1 + "-" + 29);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname2)
                        {
                            if (dinero2 >= 150)
                            {
                                dinero2 = dinero2 - 150;
                                nickdueño29 = nickname2;
                                return ("2" + "-" + dinero2 + "-" + nickname2 + "-" + 29);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname3)
                        {
                            if (dinero3 >= 150)
                            {
                                dinero3 = dinero3 - 150;
                                nickdueño29 = nickname3;
                                return ("2" + "-" + dinero3 + "-" + nickname3 + "-" + 29);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname4)
                        {
                            if (dinero4 >= 150)
                            {
                                dinero4 = dinero4 - 150;
                                nickdueño29 = nickname4;
                                return ("2" + "-" + dinero4 + "-" + nickname4 + "-" + 29);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                    }
                    else
                    {
                        if (nick == nickdueño29)
                        {
                            if (nick == nickname1)
                            {
                                dinero1 = dinero1;
                                return ("1" + "-" + dinero1 + "-" + nickname1);
                            }
                            if (nick == nickname2)
                            {
                                dinero2 = dinero2;
                                return ("1" + "-" + dinero2 + "-" + nickname2);
                            }
                            if (nick == nickname3)
                            {
                                dinero3 = dinero3;
                                return ("1" + "-" + dinero3 + "-" + nickname3);
                            }
                            if (nick == nickname4)
                            {
                                dinero4 = dinero4;
                                return ("1" + "-" + dinero4 + "-" + nickname4);
                            }
                        }
                        else
                        {
                            if (nickdueño29 == nickname1)
                            {
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 50;
                                    dinero1 = dinero1 + 50;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 50 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 50;
                                    dinero1 = dinero1 + 50;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 50 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 50;
                                    dinero1 = dinero1 + 50;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 50 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño29 == nickname2)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 50;
                                    dinero2 = dinero2 + 50;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 50 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 50;
                                    dinero2 = dinero2 + 50;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 50 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 50;
                                    dinero2 = dinero2 + 50;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 50 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño29 == nickname3)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 50;
                                    dinero3 = dinero3 + 50;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 50 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 50;
                                    dinero3 = dinero3 + 50;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 50 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 50;
                                    dinero3 = dinero3 + 50;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 50 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño29 == nickname4)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 50;
                                    dinero4 = dinero4 + 50;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 50 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 50;
                                    dinero4 = dinero4 + 50;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 50 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 50;
                                    dinero4 = dinero4 + 50;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 50 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                            }
                        }
                    }

                    break;
                case 30: //SOLAR---GIMNASIO
                    if (nickdueño30 == "")
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 280)
                            {
                                dinero1 = dinero1 - 280;
                                nickdueño30 = nickname1;
                                return ("2" + "-" + dinero1 + "-" + nickname1 + "-" + 30);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname2)
                        {
                            if (dinero2 >= 280)
                            {
                                dinero2 = dinero2 - 280;
                                nickdueño30 = nickname2;
                                return ("2" + "-" + dinero2 + "-" + nickname2 + "-" + 30);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname3)
                        {
                            if (dinero3 >= 280)
                            {
                                dinero3 = dinero3 - 280;
                                nickdueño30 = nickname3;
                                return ("2" + "-" + dinero3 + "-" + nickname3 + "-" + 30);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname4)
                        {
                            if (dinero4 >= 280)
                            {
                                dinero4 = dinero4 - 280;
                                nickdueño30 = nickname4;
                                return ("2" + "-" + dinero4 + "-" + nickname4 + "-" + 30);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                    }
                    else
                    {
                        if (nick == nickdueño30)
                        {
                            if (nick == nickname1)
                            {
                                if(nick == nickdueño27 && nick == nickdueño28 && nick == nickdueño30)
                            	{
                                	if(casadueño30 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero1 >= 120)
                                    	{ 
                                        	dinero1 = dinero1 - 120; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño30 = nickname1;
                                        	return ("6" + "-" + dinero1 + "-" + nickname1 + "-" + 30);
                                    	}
                                	}
                                	else if (casadueño30 == nickname1 && hoteldueño30 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero1 >= 1200)
                                    	{
                                        	dinero1 = dinero1 - 1200; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño30 = nickname1;
                                        	return ("7" + "-" + dinero1 + "-" + nickname1 + "-" + 30);
                                    	}
                                	}
                                	else if (casadueño30 == nickname1 && hoteldueño30 == nickname1) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero1 = dinero1;
                                    	return ("1" + "-" + dinero1 + "-" + nickname1);
                                	}
                                }
                            }
                            if (nick == nickname2)
                            {
                                if(nick == nickdueño27 && nick == nickdueño28 && nick == nickdueño30)
                            	{
                                	if(casadueño30 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero2 >= 120)
                                    	{ 
                                        	dinero2 = dinero2 - 120; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño30 = nickname2;
                                        	return ("6" + "-" + dinero2 + "-" + nickname2 + "-" + 30);
                                    	}
                                	}
                                	else if (casadueño30 == nickname2 && hoteldueño30 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero2 >= 1200)
                                    	{
                                        	dinero2 = dinero2 - 1200; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño30 = nickname2;
                                        	return ("7" + "-" + dinero2 + "-" + nickname2 + "-" + 30);
                                    	}
                                	}
                                	else if (casadueño30 == nickname2 && hoteldueño30 == nickname2) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero2 = dinero2;
                                    	return ("1" + "-" + dinero2 + "-" + nickname2);
                                	}
                                }
                            }
                            if (nick == nickname3)
                            {
                                if(nick == nickdueño27 && nick == nickdueño28 && nick == nickdueño30)
                            	{
                                	if(casadueño30 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero3 >= 120)
                                    	{ 
                                        	dinero3 = dinero3 - 120; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño30 = nickname3;
                                        	return ("6" + "-" + dinero3 + "-" + nickname3 + "-" + 30);
                                    	}
                                	}
                                	else if (casadueño30 == nickname3 && hoteldueño30 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero3 >= 1200)
                                    	{
                                        	dinero3 = dinero3 - 1200; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño30 = nickname3;
                                        	return ("7" + "-" + dinero3 + "-" + nickname3 + "-" + 30);
                                    	}
                                	}
                                	else if (casadueño30 == nickname3 && hoteldueño30 == nickname3) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero3 = dinero3;
                                    	return ("1" + "-" + dinero3 + "-" + nickname3);
                                	}
                                }
                            }
                            if (nick == nickname4)
                            {
                                if(nick == nickdueño27 && nick == nickdueño28 && nick == nickdueño30)
                            	{
                                	if(casadueño30 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero4 >= 120)
                                    	{ 
                                        	dinero4 = dinero4 - 120; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño30 = nickname4;
                                        	return ("6" + "-" + dinero4 + "-" + nickname4 + "-" + 30);
                                    	}
                                	}
                                	else if (casadueño30 == nickname4 && hoteldueño30 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero4 >= 1200)
                                    	{
                                        	dinero4 = dinero4 - 1200; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño30 = nickname4;
                                        	return ("7" + "-" + dinero4 + "-" + nickname4 + "-" + 30);
                                    	}
                                	}
                                	else if (casadueño30 == nickname4 && hoteldueño30 == nickname4) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero4 = dinero4;
                                    	return ("1" + "-" + dinero4 + "-" + nickname4);
                                	}
                                }
                            }
                        }
                        else
                        {
                            if (nickdueño30 == nickname1)
                            {
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 24;
                                    dinero1 = dinero1 + 24;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 24 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 24;
                                    dinero1 = dinero1 + 24;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 24 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 24;
                                    dinero1 = dinero1 + 24;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 24 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño30 == nickname2)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 24;
                                    dinero2 = dinero2 + 24;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 24 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 24;
                                    dinero2 = dinero2 + 24;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 24 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 24;
                                    dinero2 = dinero2 + 24;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 24 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño30 == nickname3)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 24;
                                    dinero3 = dinero3 + 24;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 24 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 24;
                                    dinero3 = dinero3 + 24;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 24 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 24;
                                    dinero3 = dinero3 + 24;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 24 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño30 == nickname4)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 24;
                                    dinero4 = dinero4 + 24;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 24 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 24;
                                    dinero4 = dinero4 + 24;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 24 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 24;
                                    dinero4 = dinero4 + 24;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 24 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                            }
                        }
                    }

                    break;
                case 31: //IR A CÁRCEL
                    if (nick == nickname1)
                    {
                        if (tarjeta1 == "Si")
                        {
                            tarjeta1 = "No";
                            return ("5" + "-" + dinero1 + "-" + nickname1 + "-" + tarjeta1);
                            //Hacer return
                        }
                        else if (dinero1 >= 50)
                        {
                            //estado1 = "Preso";
                            dinero1 = dinero1 - 50;
                            return ("5" + "-" + dinero1 + "-" + nickname1 + "-" + tarjeta1);
                        }
                    }
                    if (nick == nickname2)
                    {
                        if (tarjeta2 == "Si")
                        {
                            tarjeta2 = "No";
                            return ("5" + "-" + dinero2 + "-" + nickname2 + "-" + tarjeta2);
                            //Hacer return
                        }
                        else if (dinero2 >= 50)
                        {
                            //estado2 = "Preso";
                            dinero2 = dinero2 - 50;
                            return ("5" + "-" + dinero2 + "-" + nickname2 + "-" + tarjeta2);
                        }
                    }
                    if (nick == nickname3)
                    {
                        if (tarjeta3 == "Si")
                        {
                            tarjeta3 = "No";
                            return ("5" + "-" + dinero3 + "-" + nickname3 + "-" + tarjeta3);
                            //Hacer return
                        }
                        else if (dinero3 >= 50)
                        {
                            //estado3 = "Preso";
                            dinero3 = dinero3 - 50;
                            return ("5" + "-" + dinero3 + "-" + nickname3 + "-" + tarjeta3);
                        }
                    }
                    if (nick == nickname4)
                    {
                        if (tarjeta4 == "Si")
                        {
                            tarjeta4 = "No";
                            return ("5" + "-" + dinero4 + "-" + nickname4 + "-" + tarjeta4);
                        }
                        else if (dinero4 >= 50)
                        {
                            dinero4 = dinero4 - 50;
                            return ("5" + "-" + dinero4 + "-" + nickname4 + "-" + tarjeta4);
                        }

                    }
                    break;
                case 32: //SOLAR---CINCUENTENARIO
                    if (nickdueño32 == "")
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 300)
                            {
                                dinero1 = dinero1 - 300;
                                nickdueño32 = nickname1;
                                return ("2" + "-" + dinero1 + "-" + nickname1 + "-" + 32);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname2)
                        {
                            if (dinero2 >= 300)
                            {
                                dinero2 = dinero2 - 300;
                                nickdueño32 = nickname2;
                                return ("2" + "-" + dinero2 + "-" + nickname2 + "-" + 32);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname3)
                        {
                            if (dinero3 >= 300)
                            {
                                dinero3 = dinero3 - 300;
                                nickdueño32 = nickname3;
                                return ("2" + "-" + dinero3 + "-" + nickname3 + "-" + 32);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname4)
                        {
                            if (dinero4 >= 300)
                            {
                                dinero4 = dinero4 - 300;
                                nickdueño32 = nickname4;
                                return ("2" + "-" + dinero4 + "-" + nickname4 + "-" + 32);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                    }
                    else
                    {
                        if (nick == nickdueño32)
                        {
                            if (nick == nickname1)
                            {
                                if(nick == nickdueño32 && nick == nickdueño33 && nick == nickdueño35)
                            	{
                                	if(casadueño32 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero1 >= 130)
                                    	{ 
                                        	dinero1 = dinero1 - 130; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño32 = nickname1;
                                        	return ("6" + "-" + dinero1 + "-" + nickname1 + "-" + 32);
                                    	}
                                	}
                                	else if (casadueño32 == nickname1 && hoteldueño32 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero1 >= 1275)
                                    	{
                                        	dinero1 = dinero1 - 1275; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño32 = nickname1;
                                        	return ("7" + "-" + dinero1 + "-" + nickname1 + "-" + 32);
                                    	}
                                	}
                                	else if (casadueño32 == nickname1 && hoteldueño32 == nickname1) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero1 = dinero1;
                                    	return ("1" + "-" + dinero1 + "-" + nickname1);
                                	}
                                }
                            }
                            if (nick == nickname2)
                            {
                                if(nick == nickdueño32 && nick == nickdueño33 && nick == nickdueño35)
                            	{
                                	if(casadueño32 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero2 >= 130)
                                    	{ 
                                        	dinero2 = dinero2 - 130; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño32 = nickname2;
                                        	return ("6" + "-" + dinero2 + "-" + nickname2 + "-" + 32);
                                    	}
                                	}
                                	else if (casadueño32 == nickname2 && hoteldueño32 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero2 >= 1275)
                                    	{
                                        	dinero2 = dinero2 - 1275; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño32 = nickname2;
                                        	return ("7" + "-" + dinero2 + "-" + nickname2 + "-" + 32);
                                    	}
                                	}
                                	else if (casadueño32 == nickname2 && hoteldueño32 == nickname2) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero2 = dinero2;
                                    	return ("1" + "-" + dinero2 + "-" + nickname2);
                                	}
                                }
                            }
                            if (nick == nickname3)
                            {
                                if(nick == nickdueño32 && nick == nickdueño33 && nick == nickdueño35)
                            	{
                                	if(casadueño32 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero3 >= 130)
                                    	{ 
                                        	dinero3 = dinero3 - 130; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño32 = nickname3;
                                        	return ("6" + "-" + dinero3 + "-" + nickname3 + "-" + 32);
                                    	}
                                	}
                                	else if (casadueño32 == nickname3 && hoteldueño32 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero3 >= 1275)
                                    	{
                                        	dinero3 = dinero3 - 1275; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño32 = nickname3;
                                        	return ("7" + "-" + dinero3 + "-" + nickname3 + "-" + 32);
                                    	}
                                	}
                                	else if (casadueño32 == nickname3 && hoteldueño32 == nickname3) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero3 = dinero3;
                                    	return ("1" + "-" + dinero3 + "-" + nickname3);
                                	}
                                }
                            }
                            if (nick == nickname4)
                            {
                                if(nick == nickdueño32 && nick == nickdueño33 && nick == nickdueño35)
                            	{
                                	if(casadueño32 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero4 >= 130)
                                    	{ 
                                        	dinero4 = dinero4 - 130; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño32 = nickname4;
                                        	return ("6" + "-" + dinero4 + "-" + nickname4 + "-" + 32);
                                    	}
                                	}
                                	else if (casadueño32 == nickname4 && hoteldueño32 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero4 >= 1275)
                                    	{
                                        	dinero4 = dinero4 - 1275; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño32 = nickname4;
                                        	return ("7" + "-" + dinero4 + "-" + nickname4 + "-" + 32);
                                    	}
                                	}
                                	else if (casadueño32 == nickname4 && hoteldueño32 == nickname4) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero4 = dinero4;
                                    	return ("1" + "-" + dinero4 + "-" + nickname4);
                                	}
                                }
                            }
                        }
                        else
                        {
                            if (nickdueño32 == nickname1)
                            {
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 26;
                                    dinero1 = dinero1 + 26;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 26 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 26;
                                    dinero1 = dinero1 + 26;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 26 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 26;
                                    dinero1 = dinero1 + 26;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 26 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño32 == nickname2)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 26;
                                    dinero2 = dinero2 + 26;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 26 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 26;
                                    dinero2 = dinero2 + 26;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 26 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 26;
                                    dinero2 = dinero2 + 26;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 26 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño32 == nickname3)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 26;
                                    dinero3 = dinero3 + 26;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 26 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 26;
                                    dinero3 = dinero3 + 26;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 26 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 26;
                                    dinero3 = dinero3 + 26;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 26 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño32 == nickname4)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 26;
                                    dinero4 = dinero4 + 26;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 26 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 26;
                                    dinero4 = dinero4 + 26;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 26 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 26;
                                    dinero4 = dinero4 + 26;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 26 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                            }
                        }
                    }

                    break;
                case 33: //SOLAR---AVENIDA NORTE
                    if (nickdueño33 == "")
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 300)
                            {
                                dinero1 = dinero1 - 300;
                                nickdueño33 = nickname1;
                                return ("2" + "-" + dinero1 + "-" + nickname1 + "-" + 33);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname2)
                        {
                            if (dinero2 >= 300)
                            {
                                dinero2 = dinero2 - 300;
                                nickdueño33 = nickname2;
                                return ("2" + "-" + dinero2 + "-" + nickname2 + "-" + 33);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname3)
                        {
                            if (dinero3 >= 300)
                            {
                                dinero3 = dinero3 - 300;
                                nickdueño33 = nickname3;
                                return ("2" + "-" + dinero3 + "-" + nickname3 + "-" + 33);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname4)
                        {
                            if (dinero4 >= 300)
                            {
                                dinero4 = dinero4 - 300;
                                nickdueño33 = nickname4;
                                return ("2" + "-" + dinero4 + "-" + nickname4 + "-" + 33);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                    }
                    else
                    {
                        if (nick == nickdueño33)
                        {
                            if (nick == nickname1)
                            {
                                if(nick == nickdueño32 && nick == nickdueño33 && nick == nickdueño35)
                            	{
                                	if(casadueño33 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero1 >= 130)
                                    	{ 
                                        	dinero1 = dinero1 - 130; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño33 = nickname1;
                                        	return ("6" + "-" + dinero1 + "-" + nickname1 + "-" + 33);
                                    	}
                                	}
                                	else if (casadueño33 == nickname1 && hoteldueño33 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero1 >= 1275)
                                    	{
                                        	dinero1 = dinero1 - 1275; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño33 = nickname1;
                                        	return ("7" + "-" + dinero1 + "-" + nickname1 + "-" + 33);
                                    	}
                                	}
                                	else if (casadueño33 == nickname1 && hoteldueño33 == nickname1) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero1 = dinero1;
                                    	return ("1" + "-" + dinero1 + "-" + nickname1);
                                	}
                                }
                            }
                            if (nick == nickname2)
                            {
                                if(nick == nickdueño32 && nick == nickdueño33 && nick == nickdueño35)
                            	{
                                	if(casadueño33 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero2 >= 130)
                                    	{ 
                                        	dinero2 = dinero2 - 130; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño33 = nickname2;
                                        	return ("6" + "-" + dinero2 + "-" + nickname2 + "-" + 33);
                                    	}
                                	}
                                	else if (casadueño33 == nickname2 && hoteldueño33 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero2 >= 1275)
                                    	{
                                        	dinero2 = dinero2 - 1275; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño33 = nickname2;
                                        	return ("7" + "-" + dinero2 + "-" + nickname2 + "-" + 33);
                                    	}
                                	}
                                	else if (casadueño33 == nickname2 && hoteldueño33 == nickname2) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero2 = dinero2;
                                    	return ("1" + "-" + dinero2 + "-" + nickname2);
                                	}
                                }
                            }
                            if (nick == nickname3)
                            {
                                if(nick == nickdueño32 && nick == nickdueño33 && nick == nickdueño35)
                            	{
                                	if(casadueño33 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero3 >= 130)
                                    	{ 
                                        	dinero3 = dinero3 - 130; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño33 = nickname3;
                                        	return ("6" + "-" + dinero3 + "-" + nickname3 + "-" + 33);
                                    	}
                                	}
                                	else if (casadueño33 == nickname3 && hoteldueño33 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero3 >= 1275)
                                    	{
                                        	dinero3 = dinero3 - 1275; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño33 = nickname3;
                                        	return ("7" + "-" + dinero3 + "-" + nickname3 + "-" + 33);
                                    	}
                                	}
                                	else if (casadueño33 == nickname3 && hoteldueño33 == nickname3) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero3 = dinero3;
                                    	return ("1" + "-" + dinero3 + "-" + nickname3);
                                	}
                                }
                            }
                            if (nick == nickname4)
                            {
                                if(nick == nickdueño32 && nick == nickdueño33 && nick == nickdueño35)
                            	{
                                	if(casadueño33 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero4 >= 130)
                                    	{ 
                                        	dinero4 = dinero4 - 130; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño33 = nickname4;
                                        	return ("6" + "-" + dinero4 + "-" + nickname4 + "-" + 33);
                                    	}
                                	}
                                	else if (casadueño33 == nickname4 && hoteldueño33 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero4 >= 1275)
                                    	{
                                        	dinero4 = dinero4 - 1275; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño33 = nickname4;
                                        	return ("7" + "-" + dinero4 + "-" + nickname4 + "-" + 33);
                                    	}
                                	}
                                	else if (casadueño33 == nickname4 && hoteldueño33 == nickname4) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero4 = dinero4;
                                    	return ("1" + "-" + dinero4 + "-" + nickname4);
                                	}
                                }
                            }
                        }
                        else
                        {
                            if (nickdueño33 == nickname1)
                            {
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 26;
                                    dinero1 = dinero1 + 26;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 26 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 26;
                                    dinero1 = dinero1 + 26;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 26 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 26;
                                    dinero1 = dinero1 + 26;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 26 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño33 == nickname2)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 26;
                                    dinero2 = dinero2 + 26;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 26 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 26;
                                    dinero2 = dinero2 + 26;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 26 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 26;
                                    dinero2 = dinero2 + 26;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 26 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño33 == nickname3)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 26;
                                    dinero3 = dinero3 + 26;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 26 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 26;
                                    dinero3 = dinero3 + 26;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 26 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 26;
                                    dinero3 = dinero3 + 26;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 26 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño33 == nickname4)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 26;
                                    dinero4 = dinero4 + 26;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 26 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 26;
                                    dinero4 = dinero4 + 26;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 26 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 26;
                                    dinero4 = dinero4 + 26;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 26 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                            }
                        }
                    }

                    break;
                case 34: //TESORERÍA UCAB
                    int tesoreria2 = random.Next(1, 9);
                    if (tesoreria2 == 1)
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 150)
                            {
                                dinero1 = dinero1 - 150;
                                return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Tesoreria" + "-" + 1);
                            }
                        }
                        if (nick == nickname2)
                        {
                            if (dinero2 >= 150)
                            {
                                dinero2 = dinero2 - 150;
                                return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Tesoreria" + "-" + 1);
                            }
                        }
                        if (nick == nickname3)
                        {
                            if (dinero3 >= 150)
                            {
                                dinero3 = dinero3 - 150;
                                return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Tesoreria" + "-" + 1);
                            }
                        }
                        if (nick == nickname4)
                        {
                            if (dinero4 >= 150)
                            {
                                dinero4 = dinero4 - 150;
                                return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Tesoreria" + "-" + 1);
                            }
                        }

                    }
                    if (tesoreria2 == 2)
                    {
                        if (nick == nickname1)
                        {
                            dinero1 = dinero1 + 200;
                            return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Tesoreria" + "-" + 2);
                        }
                        if (nick == nickname2)
                        {
                            dinero2 = dinero2 + 200;
                            return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Tesoreria" + "-" + 2);
                        }
                        if (nick == nickname3)
                        {
                            dinero3 = dinero3 + 200;
                            return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Tesoreria" + "-" + 2);
                        }
                        if (nick == nickname4)
                        {
                            dinero4 = dinero4 + 200;
                            return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Tesoreria" + "-" + 2);
                        }
                    }
                    if (tesoreria2 == 3)
                    {
                        if (nick == nickname1)
                        {
                            dinero1 = dinero1 + 100;
                            return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Tesoreria" + "-" + 3);
                        }
                        if (nick == nickname2)
                        {
                            dinero2 = dinero2 + 100;
                            return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Tesoreria" + "-" + 3);
                        }
                        if (nick == nickname3)
                        {
                            dinero3 = dinero3 + 100;
                            return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Tesoreria" + "-" + 3);
                        }
                        if (nick == nickname4)
                        {
                            dinero4 = dinero4 + 100;
                            return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Tesoreria" + "-" + 3);
                        }
                    }
                    if (tesoreria2 == 4)
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 100)
                            {
                                dinero1 = dinero1 - 100;
                                return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Tesoreria" + "-" + 4);
                            }
                        }
                        if (nick == nickname2)
                        {
                            if (dinero2 >= 100)
                            {
                                dinero2 = dinero2 - 100;
                                return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Tesoreria" + "-" + 4);
                            }
                        }
                        if (nick == nickname3)
                        {
                            if (dinero3 >= 100)
                            {
                                dinero3 = dinero3 - 100;
                                return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Tesoreria" + "-" + 4);
                            }
                        }
                        if (nick == nickname4)
                        {
                            if (dinero4 >= 100)
                            {
                                dinero4 = dinero4 - 100;
                                return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Tesoreria" + "-" + 4);
                            }
                        }
                    }
                    if (tesoreria2 == 5)
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 100)
                            {
                                dinero1 = dinero1 - 100;
                                return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Tesoreria" + "-" + 5);
                            }
                        }
                        if (nick == nickname2)
                        {
                            if (dinero2 >= 100)
                            {
                                dinero2 = dinero2 - 100;
                                return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Tesoreria" + "-" + 5);
                            }
                        }
                        if (nick == nickname3)
                        {
                            if (dinero3 >= 100)
                            {
                                dinero3 = dinero3 - 100;
                                return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Tesoreria" + "-" + 5);
                            }
                        }
                        if (nick == nickname4)
                        {
                            if (dinero4 >= 100)
                            {
                                dinero4 = dinero4 - 100;
                                return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Tesoreria" + "-" + 5);
                            }
                        }
                    }
                    if (tesoreria2 == 6)
                    {
                        if (nick == nickname1)
                        {
                            dinero1 = dinero1 + 150;
                            return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Tesoreria" + "-" + 6);
                        }
                        if (nick == nickname2)
                        {
                            dinero2 = dinero2 + 150;
                            return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Tesoreria" + "-" + 6);
                        }
                        if (nick == nickname3)
                        {
                            dinero3 = dinero3 + 150;
                            return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Tesoreria" + "-" + 6);
                        }
                        if (nick == nickname4)
                        {
                            dinero4 = dinero4 + 150;
                            return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Tesoreria" + "-" + 6);
                        }
                    }
                    if (tesoreria2 == 7)
                    {
                        if (nick == nickname1)
                        {
                            dinero1 = dinero1 + 10;
                            return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Tesoreria" + "-" + 7);
                        }
                        if (nick == nickname2)
                        {
                            dinero2 = dinero2 + 10;
                            return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Tesoreria" + "-" + 7);
                        }
                        if (nick == nickname3)
                        {
                            dinero3 = dinero3 + 10;
                            return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Tesoreria" + "-" + 7);
                        }
                        if (nick == nickname4)
                        {
                            dinero4 = dinero4 + 10;
                            return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Tesoreria" + "-" + 7);
                        }
                    }
                    if (tesoreria2 == 8)
                    {
                        if (nick == nickname1)
                        {
                            dinero1 = dinero1 + 200;
                            return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Tesoreria" + "-" + 8);
                        }
                        if (nick == nickname2)
                        {
                            dinero2 = dinero2 + 200;
                            return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Tesoreria" + "-" + 8);
                        }
                        if (nick == nickname3)
                        {
                            dinero3 = dinero3 + 200;
                            return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Tesoreria" + "-" + 8);
                        }
                        if (nick == nickname4)
                        {
                            dinero4 = dinero4 + 200;
                            return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Tesoreria" + "-" + 8);
                        }
                    }
                    break;
                case 35: //SOLAR---AVENIDA OESTE
                    if (nickdueño35 == "")
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 320)
                            {
                                dinero1 = dinero1 - 320;
                                nickdueño35 = nickname1;
                                return ("2" + "-" + dinero1 + "-" + nickname1 + "-" + 35);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname2)
                        {
                            if (dinero2 >= 320)
                            {
                                dinero2 = dinero2 - 320;
                                nickdueño35 = nickname2;
                                return ("2" + "-" + dinero2 + "-" + nickname2 + "-" + 35);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname3)
                        {
                            if (dinero3 >= 320)
                            {
                                dinero3 = dinero3 - 320;
                                nickdueño35 = nickname3;
                                return ("2" + "-" + dinero3 + "-" + nickname3 + "-" + 35);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname4)
                        {
                            if (dinero4 >= 320)
                            {
                                dinero4 = dinero4 - 320;
                                nickdueño35 = nickname4;
                                return ("2" + "-" + dinero4 + "-" + nickname4 + "-" + 35);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                    }
                    else
                    {
                        if (nick == nickdueño35)
                        {
                            if (nick == nickname1)
                            {
                                if(nick == nickdueño32 && nick == nickdueño33 && nick == nickdueño35)
                            	{
                                	if(casadueño35 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero1 >= 150)
                                    	{ 
                                        	dinero1 = dinero1 - 150; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño35 = nickname1;
                                        	return ("6" + "-" + dinero1 + "-" + nickname1 + "-" + 35);
                                    	}
                                	}
                                	else if (casadueño35 == nickname1 && hoteldueño35 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero1 >= 1400)
                                    	{
                                        	dinero1 = dinero1 - 1400; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño35 = nickname1;
                                        	return ("7" + "-" + dinero1 + "-" + nickname1 + "-" + 35);
                                    	}
                                	}
                                	else if (casadueño35 == nickname1 && hoteldueño35 == nickname1) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero1 = dinero1;
                                    	return ("1" + "-" + dinero1 + "-" + nickname1);
                                	}
                                }
                            }
                            if (nick == nickname2)
                            {
                                if(nick == nickdueño32 && nick == nickdueño33 && nick == nickdueño35)
                            	{
                                	if(casadueño35 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero2 >= 150)
                                    	{ 
                                        	dinero2 = dinero2 - 150; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño35 = nickname2;
                                        	return ("6" + "-" + dinero2 + "-" + nickname2 + "-" + 35);
                                    	}
                                	}
                                	else if (casadueño35 == nickname2 && hoteldueño35 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero2 >= 1400)
                                    	{
                                        	dinero2 = dinero2 - 1400; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño35 = nickname2;
                                        	return ("7" + "-" + dinero2 + "-" + nickname2 + "-" + 35);
                                    	}
                                	}
                                	else if (casadueño35 == nickname2 && hoteldueño35 == nickname2) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero2 = dinero2;
                                    	return ("1" + "-" + dinero2 + "-" + nickname2);
                                	}
                                }
                            }
                            if (nick == nickname3)
                            {
                                if(nick == nickdueño32 && nick == nickdueño33 && nick == nickdueño35)
                            	{
                                	if(casadueño35 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero3 >= 150)
                                    	{ 
                                        	dinero3 = dinero3 - 150; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño35 = nickname3;
                                        	return ("6" + "-" + dinero3 + "-" + nickname3 + "-" + 35);
                                    	}
                                	}
                                	else if (casadueño35 == nickname3 && hoteldueño35 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero3 >= 1400)
                                    	{
                                        	dinero3 = dinero3 - 1400; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño35 = nickname3;
                                        	return ("7" + "-" + dinero3 + "-" + nickname3 + "-" + 35);
                                    	}
                                	}
                                	else if (casadueño35 == nickname3 && hoteldueño35 == nickname3) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero3 = dinero3;
                                    	return ("1" + "-" + dinero3 + "-" + nickname3);
                                	}
                                }
                            }
                            if (nick == nickname4)
                            {
                                if(nick == nickdueño32 && nick == nickdueño33 && nick == nickdueño35)
                            	{
                                	if(casadueño35 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero4 >= 150)
                                    	{ 
                                        	dinero4 = dinero4 - 150; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño35 = nickname4;
                                        	return ("6" + "-" + dinero4 + "-" + nickname4 + "-" + 35);
                                    	}
                                	}
                                	else if (casadueño35 == nickname4 && hoteldueño35 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero4 >= 1400)
                                    	{
                                        	dinero4 = dinero4 - 1400; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño35 = nickname4;
                                        	return ("7" + "-" + dinero4 + "-" + nickname4 + "-" + 35);
                                    	}
                                	}
                                	else if (casadueño35 == nickname4 && hoteldueño35 == nickname4) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero4 = dinero4;
                                    	return ("1" + "-" + dinero4 + "-" + nickname4);
                                	}
                                }
                            }
                        }
                        else
                        {
                            if (nickdueño35 == nickname1)
                            {
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 28;
                                    dinero1 = dinero1 + 28;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 28 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 28;
                                    dinero1 = dinero1 + 28;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 28 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 28;
                                    dinero1 = dinero1 + 28;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 28 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño35 == nickname2)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 28;
                                    dinero2 = dinero2 + 28;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 28 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 28;
                                    dinero2 = dinero2 + 28;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 28 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 28;
                                    dinero2 = dinero2 + 28;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 28 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño35 == nickname3)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 28;
                                    dinero3 = dinero3 + 28;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 28 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 28;
                                    dinero3 = dinero3 + 28;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 28 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 28;
                                    dinero3 = dinero3 + 28;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 28 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño35 == nickname4)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 28;
                                    dinero4 = dinero4 + 28;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 28 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 28;
                                    dinero4 = dinero4 + 28;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 28 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 28;
                                    dinero4 = dinero4 + 28;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 28 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                            }
                        }
                    }

                    break;
                case 36: //TAXI UCAB ESTE
                    if (nickdueño36 == "")
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 200)
                            {
                                dinero1 = dinero1 - 200;
                                nickdueño36 = nickname1;
                                return ("2" + "-" + dinero1 + "-" + nickname1 + "-" + 36);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname2)
                        {
                            if (dinero2 >= 200)
                            {
                                dinero2 = dinero2 - 200;
                                nickdueño36 = nickname2;
                                return ("2" + "-" + dinero2 + "-" + nickname2 + "-" + 36);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname3)
                        {
                            if (dinero3 >= 200)
                            {
                                dinero3 = dinero3 - 200;
                                nickdueño36 = nickname3;
                                return ("2" + "-" + dinero3 + "-" + nickname3 + "-" + 36);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname4)
                        {
                            if (dinero4 >= 200)
                            {
                                dinero4 = dinero4 - 200;
                                nickdueño36 = nickname4;
                                return ("2" + "-" + dinero4 + "-" + nickname4 + "-" + 36);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                    }
                    else
                    {
                        if (nick == nickdueño36)
                        {
                            if (nick == nickname1)
                            {
                                dinero1 = dinero1;
                                return ("1" + "-" + dinero1 + "-" + nickname1);
                            }
                            if (nick == nickname2)
                            {
                                dinero2 = dinero2;
                                return ("1" + "-" + dinero2 + "-" + nickname2);
                            }
                            if (nick == nickname3)
                            {
                                dinero3 = dinero3;
                                return ("1" + "-" + dinero3 + "-" + nickname3);
                            }
                            if (nick == nickname4)
                            {
                                dinero4 = dinero4;
                                return ("1" + "-" + dinero4 + "-" + nickname4);
                            }
                        }
                        else
                        {
                            if (nickdueño36 == nickname1)
                            {
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 25;
                                    dinero1 = dinero1 + 25;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 25 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 25;
                                    dinero1 = dinero1 + 25;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 25 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 25;
                                    dinero1 = dinero1 + 25;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 25 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño36 == nickname2)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 25;
                                    dinero2 = dinero2 + 25;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 25 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 25;
                                    dinero2 = dinero2 + 25;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 25 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 25;
                                    dinero2 = dinero2 + 25;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 25 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño36 == nickname3)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 25;
                                    dinero3 = dinero3 + 25;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 25 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 25;
                                    dinero3 = dinero3 + 25;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 25 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 25;
                                    dinero3 = dinero3 + 25;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 25 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño36 == nickname4)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 25;
                                    dinero4 = dinero4 + 25;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 25 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 25;
                                    dinero4 = dinero4 + 25;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 25 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 25;
                                    dinero4 = dinero4 + 25;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 25 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                            }
                        }
                    }

                    break;
                case 37: //CASUALIDAD
                    int casualidad2 = random.Next(1, 6);
                    if (casualidad2 == 1)
                    {
                        if (nick == nickname1)
                        {
                            dinero1 = dinero1 + 200;
                            return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Casualidad" + "-" + 1);
                        }
                        if (nick == nickname2)
                        {
                            dinero2 = dinero2 + 200;
                            return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Casualidad" + "-" + 1);
                        }
                        if (nick == nickname3)
                        {
                            dinero3 = dinero3 + 200;
                            return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Casualidad" + "-" + 1);
                        }
                        if (nick == nickname4)
                        {
                            dinero4 = dinero4 + 200;
                            return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Casualidad" + "-" + 1);
                        }
                    }
                    if (casualidad2 == 2)
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 15)
                            {
                                dinero1 = dinero1 - 15;
                                return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Casualidad" + "-" + 2);
                            }
                        }
                        if (nick == nickname2)
                        {
                            if (dinero2 >= 15)
                            {
                                dinero2 = dinero2 - 15;
                                return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Casualidad" + "-" + 2);
                            }
                        }
                        if (nick == nickname3)
                        {
                            if (dinero3 >= 15)
                            {
                                dinero3 = dinero3 - 15;
                                return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Casualidad" + "-" + 2);
                            }
                        }
                        if (nick == nickname4)
                        {
                            if (dinero4 >= 15)
                            {
                                dinero4 = dinero4 - 15;
                                return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Casualidad" + "-" + 2);
                            }
                        }
                    }
                    if (casualidad2 == 3)
                    {
                        if (nick == nickname1)
                        {
                            dinero1 = dinero1 + 50;
                            return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Casualidad" + "-" + 3);
                        }
                        if (nick == nickname2)
                        {
                            dinero2 = dinero2 + 50;
                            return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Casualidad" + "-" + 3);
                        }
                        if (nick == nickname3)
                        {
                            dinero3 = dinero3 + 50;
                            return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Casualidad" + "-" + 3);
                        }
                        if (nick == nickname4)
                        {
                            dinero4 = dinero4 + 50;
                            return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Casualidad" + "-" + 3);
                        }
                    }
                    if (casualidad2 == 4)
                    {
                        if (nick == nickname1)
                        {
                            dinero1 = dinero1 + 150;
                            return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Casualidad" + "-" + 4);
                        }
                        if (nick == nickname2)
                        {
                            dinero2 = dinero2 + 150;
                            return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Casualidad" + "-" + 4);
                        }
                        if (nick == nickname3)
                        {
                            dinero3 = dinero3 + 150;
                            return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Casualidad" + "-" + 4);
                        }
                        if (nick == nickname4)
                        {
                            dinero4 = dinero4 + 150;
                            return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Casualidad" + "-" + 4);
                        }
                    }
                    if (casualidad2 == 5)
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 15)
                            {
                                dinero1 = dinero1 - 100;
                                return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Casualidad" + "-" + 5);
                            }
                        }
                        if (nick == nickname2)
                        {
                            if (dinero2 >= 15)
                            {
                                dinero2 = dinero2 - 100;
                                return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Casualidad" + "-" + 5);
                            }
                        }
                        if (nick == nickname3)
                        {
                            if (dinero3 >= 15)
                            {
                                dinero3 = dinero3 - 100;
                                return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Casualidad" + "-" + 5);
                            }
                        }
                        if (nick == nickname4)
                        {
                            if (dinero4 >= 15)
                            {
                                dinero4 = dinero4 - 100;
                                return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Casualidad" + "-" + 5);
                            }
                        }
                    }
                    if (casualidad2 == 6)
                    {
                        if (nick == nickname1)
                        {
                            tarjeta1 = "Si";
                            return ("4" + "-" + dinero1 + "-" + nickname1 + "-" + "Casualidad" + "-" + 6);
                        }
                        if (nick == nickname2)
                        {
                            tarjeta2 = "Si";
                            return ("4" + "-" + dinero2 + "-" + nickname2 + "-" + "Casualidad" + "-" + 6);
                        }
                        if (nick == nickname3)
                        {
                            tarjeta3 = "Si";
                            return ("4" + "-" + dinero3 + "-" + nickname3 + "-" + "Casualidad" + "-" + 6);
                        }
                        if (nick == nickname4)
                        {
                            tarjeta4 = "Si";
                            return ("4" + "-" + dinero4 + "-" + nickname4 + "-" + "Casualidad" + "-" + 6);
                        }
                    }
                    break;
                case 38: //SOLAR---AVENIDA SUR
                    if (nickdueño38 == "")
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 350)
                            {
                                dinero1 = dinero1 - 350;
                                nickdueño38 = nickname1;
                                return ("2" + "-" + dinero1 + "-" + nickname1 + "-" + 38);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname2)
                        {
                            if (dinero2 >= 350)
                            {
                                dinero2 = dinero2 - 350;
                                nickdueño38 = nickname2;
                                return ("2" + "-" + dinero2 + "-" + nickname2 + "-" + 38);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname3)
                        {
                            if (dinero3 >= 350)
                            {
                                dinero3 = dinero3 - 350;
                                nickdueño38 = nickname3;
                                return ("2" + "-" + dinero3 + "-" + nickname3 + "-" + 38);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname4)
                        {
                            if (dinero4 >= 350)
                            {
                                dinero4 = dinero4 - 350;
                                nickdueño38 = nickname4;
                                return ("2" + "-" + dinero4 + "-" + nickname4 + "-" + 38);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                    }
                    else
                    {
                        if (nick == nickdueño38)
                        {
                            if (nick == nickname1)
                            {
                                if(nick == nickdueño38 && nick == nickdueño40)
                            	{
                                	if(casadueño38 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero1 >= 175)
                                    	{ 
                                        	dinero1 = dinero1 - 175; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño38 = nickname1;
                                        	return ("6" + "-" + dinero1 + "-" + nickname1 + "-" + 38);
                                    	}
                                	}
                                	else if (casadueño38 == nickname1 && hoteldueño38 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero1 >= 1500)
                                    	{
                                        	dinero1 = dinero1 - 1500; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño38 = nickname1;
                                        	return ("7" + "-" + dinero1 + "-" + nickname1 + "-" + 38);
                                    	}
                                	}
                                	else if (casadueño38 == nickname1 && hoteldueño38 == nickname1) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero1 = dinero1;
                                    	return ("1" + "-" + dinero1 + "-" + nickname1);
                                	}
                                }
                            }
                            if (nick == nickname2)
                            {
                                if(nick == nickdueño38 && nick == nickdueño40)
                            	{
                                	if(casadueño38 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero2 >= 175)
                                    	{ 
                                        	dinero2 = dinero2 - 175; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño38 = nickname2;
                                        	return ("6" + "-" + dinero2 + "-" + nickname2 + "-" + 38);
                                    	}
                                	}
                                	else if (casadueño38 == nickname2 && hoteldueño38 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero2 >= 1500)
                                    	{
                                        	dinero2 = dinero2 - 1500; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño38 = nickname2;
                                        	return ("7" + "-" + dinero2 + "-" + nickname2 + "-" + 38);
                                    	}
                                	}
                                	else if (casadueño38 == nickname2 && hoteldueño38 == nickname2) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero2 = dinero2;
                                    	return ("1" + "-" + dinero2 + "-" + nickname2);
                                	}
                                }
                            }
                            if (nick == nickname3)
                            {
                                if(nick == nickdueño38 && nick == nickdueño40)
                            	{
                                	if(casadueño38 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero3 >= 175)
                                    	{ 
                                        	dinero3 = dinero3 - 175; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño38 = nickname3;
                                        	return ("6" + "-" + dinero3 + "-" + nickname3 + "-" + 38);
                                    	}
                                	}
                                	else if (casadueño38 == nickname3 && hoteldueño38 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero3 >= 1500)
                                    	{
                                        	dinero3 = dinero3 - 1500; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño38 = nickname3;
                                        	return ("7" + "-" + dinero3 + "-" + nickname3 + "-" + 38);
                                    	}
                                	}
                                	else if (casadueño38 == nickname3 && hoteldueño38 == nickname3) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero3 = dinero3;
                                    	return ("1" + "-" + dinero3 + "-" + nickname3);
                                	}
                                }
                            }
                            if (nick == nickname4)
                            {
                                if(nick == nickdueño38 && nick == nickdueño40)
                            	{
                                	if(casadueño38 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero4 >= 175)
                                    	{ 
                                        	dinero4 = dinero4 - 175; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño38 = nickname4;
                                        	return ("6" + "-" + dinero4 + "-" + nickname4 + "-" + 38);
                                    	}
                                	}
                                	else if (casadueño38 == nickname4 && hoteldueño38 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero4 >= 1500)
                                    	{
                                        	dinero4 = dinero4 - 1500; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño38 = nickname4;
                                        	return ("7" + "-" + dinero4 + "-" + nickname4 + "-" + 38);
                                    	}
                                	}
                                	else if (casadueño38 == nickname4 && hoteldueño38 == nickname4) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero4 = dinero4;
                                    	return ("1" + "-" + dinero4 + "-" + nickname4);
                                	}
                                }
                            }
                        }
                        else
                        {
                            if (nickdueño38 == nickname1)
                            {
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 35;
                                    dinero1 = dinero1 + 35;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 35 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 35;
                                    dinero1 = dinero1 + 35;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 35 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 35;
                                    dinero1 = dinero1 + 35;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 35 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño38 == nickname2)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 35;
                                    dinero2 = dinero2 + 35;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 35 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 35;
                                    dinero2 = dinero2 + 35;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 35 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 35;
                                    dinero2 = dinero2 + 35;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 35 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño38 == nickname3)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 35;
                                    dinero3 = dinero3 + 35;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 35 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 35;
                                    dinero3 = dinero3 + 35;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 35 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 35;
                                    dinero3 = dinero3 + 35;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 35 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño38 == nickname4)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 35;
                                    dinero4 = dinero4 + 35;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 35 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 35;
                                    dinero4 = dinero4 + 35;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 35 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 35;
                                    dinero4 = dinero4 + 35;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 35 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                            }
                        }
                    }

                    break;
                case 39: //CHUCHERÍAS
                    if (nick == nickname1)
                    {
                        if (dinero1 >= 100)
                        {
                            dinero1 = dinero1 - 100;
                            return ("1" + "-" + dinero1 + "-" + nickname1);
                        }
                        else
                        {
                            return "0";
                        }
                    }
                    if (nick == nickname2)
                    {
                        if (dinero2 >= 100)
                        {
                            dinero2 = dinero2 - 100;
                            return ("1" + "-" + dinero2 + "-" + nickname2);
                        }
                        else
                        {
                            return "0";
                        }
                    }
                    if (nick == nickname3)
                    {
                        if (dinero3 >= 100)
                        {
                            dinero3 = dinero3 - 100;
                            return ("1" + "-" + dinero3 + "-" + nickname3);
                        }
                        else
                        {
                            return "0";
                        }
                    }
                    if (nick == nickname4)
                    {
                        if (dinero4 >= 100)
                        {
                            dinero4 = dinero4 - 100;
                            return ("1" + "-" + dinero4 + "-" + nickname4);
                        }
                        else
                        {
                            return "0";
                        }
                    }
                    break;
                case 40: //SOLAR---AVENIDA ESTE
                    if (nickdueño40 == "")
                    {
                        if (nick == nickname1)
                        {
                            if (dinero1 >= 400)
                            {
                                dinero1 = dinero1 - 400;
                                nickdueño40 = nickname1;
                                return ("2" + "-" + dinero1 + "-" + nickname1 + "-" + 40);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname2)
                        {
                            if (dinero2 >= 400)
                            {
                                dinero2 = dinero2 - 400;
                                nickdueño40 = nickname2;
                                return ("2" + "-" + dinero2 + "-" + nickname2 + "-" + 40);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname3)
                        {
                            if (dinero3 >= 400)
                            {
                                dinero3 = dinero3 - 400;
                                nickdueño40 = nickname3;
                                return ("2" + "-" + dinero3 + "-" + nickname3 + "-" + 40);
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else if (nick == nickname4)
                        {
                            if (dinero4 >= 400)
                            {
                                dinero4 = dinero4 - 400;
                                nickdueño40 = nickname4;
                                return ("2" + "-" + dinero4 + "-" + nickname4 + "-" + 40);
                            }
                            else
                            {
                                return "0";
                            }
                        }

                    }
                    else
                    {
                        if (nick == nickdueño40)
                        {
                            if (nick == nickname1)
                            {
                                if(nick == nickdueño38 && nick == nickdueño40)
                            	{
                                	if(casadueño40 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero1 >= 200)
                                    	{ 
                                        	dinero1 = dinero1 - 200; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño40 = nickname1;
                                        	return ("6" + "-" + dinero1 + "-" + nickname1 + "-" + 40);
                                    	}
                                	}
                                	else if (casadueño40 == nickname1 && hoteldueño40 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero1 >= 2000)
                                    	{
                                        	dinero1 = dinero1 - 2000; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño40 = nickname1;
                                        	return ("7" + "-" + dinero1 + "-" + nickname1 + "-" + 40);
                                    	}
                                	}
                                	else if (casadueño40 == nickname1 && hoteldueño40 == nickname1) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero1 = dinero1;
                                    	return ("1" + "-" + dinero1 + "-" + nickname1);
                                	}
                                }
                            }
                            if (nick == nickname2)
                            {
                                if(nick == nickdueño38 && nick == nickdueño40)
                            	{
                                	if(casadueño40 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero2 >= 200)
                                    	{ 
                                        	dinero2 = dinero2 - 200; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño40 = nickname2;
                                        	return ("6" + "-" + dinero2 + "-" + nickname2 + "-" + 40);
                                    	}
                                	}
                                	else if (casadueño40 == nickname2 && hoteldueño40 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero2 >= 2000)
                                    	{
                                        	dinero2 = dinero2 - 2000; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño40 = nickname2;
                                        	return ("7" + "-" + dinero2 + "-" + nickname2 + "-" + 40);
                                    	}
                                	}
                                	else if (casadueño40 == nickname2 && hoteldueño40 == nickname2) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero2 = dinero2;
                                    	return ("1" + "-" + dinero2 + "-" + nickname2);
                                	}
                                }
                            }
                            if (nick == nickname3)
                            {
                                if(nick == nickdueño38 && nick == nickdueño40)
                            	{
                                	if(casadueño40 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero3 >= 200)
                                    	{ 
                                        	dinero3 = dinero3 - 200; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño40 = nickname3;
                                        	return ("6" + "-" + dinero3 + "-" + nickname3 + "-" + 40);
                                    	}
                                	}
                                	else if (casadueño40 == nickname3 && hoteldueño40 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero3 >= 2000)
                                    	{
                                        	dinero3 = dinero3 - 2000; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño40 = nickname3;
                                        	return ("7" + "-" + dinero3 + "-" + nickname3 + "-" + 40);
                                    	}
                                	}
                                	else if (casadueño40 == nickname3 && hoteldueño40 == nickname3) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero3 = dinero3;
                                    	return ("1" + "-" + dinero3 + "-" + nickname3);
                                	}
                                }
                            }
                            if (nick == nickname4)
                            {
                                if(nick == nickdueño38 && nick == nickdueño40)
                            	{
                                	if(casadueño40 == "") //PARA COMPRAR CASA
                                	{
                                    	if(dinero4 >= 200)
                                    	{ 
                                        	dinero4 = dinero4 - 200; // Se resta lo que cuesta la casa en la casilla 2
                                        	casadueño40 = nickname4;
                                        	return ("6" + "-" + dinero4 + "-" + nickname4 + "-" + 40);
                                    	}
                                	}
                                	else if (casadueño40 == nickname4 && hoteldueño40 == "") //PARA COMPRAR HOTEL
                                	{
                                    	if(dinero4 >= 2000)
                                    	{
                                        	dinero4 = dinero4 - 2000; //Se resta lo que cuesta el hotel en la casilla2
                                        	hoteldueño40 = nickname4;
                                        	return ("7" + "-" + dinero4 + "-" + nickname4 + "-" + 40);
                                    	}
                                	}
                                	else if (casadueño40 == nickname4 && hoteldueño40 == nickname4) //YA TIENE CASA Y HOTEL
                                	{
                                    	dinero4 = dinero4;
                                    	return ("1" + "-" + dinero4 + "-" + nickname4);
                                	}
                                }
                            }
                        }
                        else
                        {
                            if (nickdueño40 == nickname1)
                            {
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 50;
                                    dinero1 = dinero1 + 50;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 50 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 50;
                                    dinero1 = dinero1 + 50;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 50 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 50;
                                    dinero1 = dinero1 + 50;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 50 + "-" + dinero1 + "-" + nickname1);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño40 == nickname2)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 50;
                                    dinero2 = dinero2 + 50;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 50 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 50;
                                    dinero2 = dinero2 + 50;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 50 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 50;
                                    dinero2 = dinero2 + 50;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 50 + "-" + dinero2 + "-" + nickname2);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño40 == nickname3)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 50;
                                    dinero3 = dinero3 + 50;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 50 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 50;
                                    dinero3 = dinero3 + 50;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 50 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                                if (nick == nickname4)
                                {
                                    dinero4 = dinero4 - 50;
                                    dinero3 = dinero3 + 50;
                                    return ("3" + "-" + dinero4 + "-" + nickname4 + "-" + 50 + "-" + dinero3 + "-" + nickname3);
                                    //Hacer cuentas
                                }
                            }
                            if (nickdueño40 == nickname4)
                            {
                                if (nick == nickname1)
                                {
                                    dinero1 = dinero1 - 50;
                                    dinero4 = dinero4 + 50;
                                    return ("3" + "-" + dinero1 + "-" + nickname1 + "-" + 50 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname2)
                                {
                                    dinero2 = dinero2 - 50;
                                    dinero4 = dinero4 + 50;
                                    return ("3" + "-" + dinero2 + "-" + nickname2 + "-" + 50 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                                if (nick == nickname3)
                                {
                                    dinero3 = dinero3 - 50;
                                    dinero4 = dinero4 + 50;
                                    return ("3" + "-" + dinero3 + "-" + nickname3 + "-" + 50 + "-" + dinero4 + "-" + nickname4);
                                    //Hacer cuentas
                                }
                            }
                        }
                    }

                    break;
            }
            return "0";
        }
    }
}
