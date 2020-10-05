using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using ControlMonopoly;

namespace ClienteUcabPolyFinal
{
    public partial class Interfaz : Form
    {
		static private NetworkStream stream;
        static private StreamWriter streamw;
        static private StreamReader streamr;
        static private TcpClient client = new TcpClient();
        static private string nick = "unknown";
		private delegate void DAddItem(String s);
        int turno, dado, posx, posy, casilla;
        string bancarota = "No";
        string nickcasilla="";
        string tarjeta = "No";
        static ControlMonopoly.Control control = new ControlMonopoly.Control();
		
        
        public Interfaz()
        {
            InitializeComponent();
            
        }

        private void pbEntrar_Click(object sender, EventArgs e)
        {
            try
            {
                nick = txtUsuario.Text;
                Conectar();
                streamw.WriteLine("000" + txtUsuario.Text + "-" + txtContraseña.Text);
                streamw.Flush();
               
            }
            catch(Exception ex)
            {
                MessageBox.Show("No se pudo conectar con el servidor, intente de nuevo (login)");
                Console.WriteLine("EXCEPCION: " + ex);
            }
        }

		void Escuchar()
		{
			while (client.Connected)
			{
				try
				{
					string mensaje = streamr.ReadLine();
                    if(mensaje!=null)
                        switch (mensaje.Substring(0, 4))
                        {

                            case "000Y":
                                if (InvokeRequired)
                                    Invoke(new Action(() => tabVistas.SelectTab(1)));
                                break;
                            case "000N":
                                MessageBox.Show("Datos Incorrectos.");
                                break;
                            case "001Y":
                                if (InvokeRequired)
                                    Invoke(new Action(() => tabVistas.SelectTab(2)));
                                if (InvokeRequired)
                                    Invoke(new Action(() => laNickR.Text = nick));
                                if (InvokeRequired)
                                    Invoke(new Action(() => laDineroR.Text = "1500"));
                                if(InvokeRequired)
                                    Invoke(new Action(() => this.pbJugador1.Location = new System.Drawing.Point(669, 444)));
                                if (InvokeRequired)
                                    Invoke(new Action(() => this.pbJugador2.Location = new System.Drawing.Point(696, 444)));
                                if (InvokeRequired)
                                    Invoke(new Action(() => this.pbJugador3.Location = new System.Drawing.Point(669, 472)));
                                if (InvokeRequired)
                                    Invoke(new Action(() => this.pbJugador4.Location = new System.Drawing.Point(696, 472)));
                                streamw.WriteLine("002" + nick + ": Quiero saber que turno me asignas?.");
                                streamw.Flush();
                                casilla = 1;
                                break;
                            case "002Y":

                                mensaje = mensaje.Substring(4); // usuario-contraseña
                                string[] separadas = mensaje.Split('-'); //[usuario][turno]
                                string usuario = separadas[0];
                                if (usuario == nick)
                                {

                                    turno = int.Parse(separadas[1]);
                                    MessageBox.Show(nick + " el turno asignado para ti es: " + turno);
                                    if (InvokeRequired)
                                        Invoke(new Action(() => laTurnoR.Text = turno.ToString()));
                                }

                                break;
                            case "003Y": //SI es mi turno
                                mensaje = mensaje.Substring(4);
                                if (int.Parse(mensaje) == turno)
                                {
                                    if(int.Parse(mensaje) != turno)
                                    {
                                        MessageBox.Show("Le toca al jugador del turno: " + turno);
                                    }
                                    MessageBox.Show("Es tu turno.");
                                    if (InvokeRequired)
                                    {
                                        if (bancarota == "Si")
                                        {
                                            Invoke(new Action(() => btVender.Visible = false));
                                            Invoke(new Action(() => btPasarTurno.Visible = true));
                                            Invoke(new Action(() => btTirarDado.Visible = false));
                                        }
                                        if (bancarota == "No")
                                        {
                                            Invoke(new Action(() => btVender.Visible = false));
                                            Invoke(new Action(() => btPasarTurno.Visible = true));
                                            Invoke(new Action(() => btTirarDado.Visible = true));
                                        }

                                    }
                                }
                                else
                                {
                                    if (InvokeRequired)
                                    {
                                        Invoke(new Action(() => btVender.Visible = false));
                                        Invoke(new Action(() => btPasarTurno.Visible = false));
                                        Invoke(new Action(() => btTirarDado.Visible = false));
                                    }
                                }
                                break;
                            case "004Y":
                                mensaje = mensaje.Substring(4);
                                dado = int.Parse(mensaje);
                                if (dado == 1)
                                {
                                    this.pbDado.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.dado1;
                                }
                                if (dado == 2)
                                {
                                    this.pbDado.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.dado2;
                                }
                                if (dado == 3)
                                {
                                    this.pbDado.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.dado3;
                                }
                                if (dado == 4)
                                {
                                    this.pbDado.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.dado4;
                                }
                                if (dado == 5)
                                {
                                    this.pbDado.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.dado5;
                                }
                                if (dado == 6)
                                {
                                    this.pbDado.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.dado6;
                                }
                                 streamw.WriteLine("005" + dado + "-" + casilla + "-" + nick + "-" +": Gracias ya tire el dado, quiero moverme.");
                                 streamw.Flush();
                                break;
                        case "005Y":
                                string msjmoverse = mensaje.Substring(4);
                                string[] sepmoverse = msjmoverse.Split('-');//[nickname][mover][codigo][dinero]
                                string[] sepmoverse1 = msjmoverse.Split('-');
                                string nicknamemovimiento = sepmoverse[0];
                                casilla = int.Parse(sepmoverse[1]);
                                string posiciones = control.moverse(casilla,nicknamemovimiento);
                                sepmoverse = posiciones.Split('-');
                                int posxmovimiento = int.Parse(sepmoverse[0]);
                                int posymovimiento = int.Parse(sepmoverse[1]);
                                posx = posxmovimiento;
                                posy = posymovimiento;
                                nickcasilla = nicknamemovimiento;
                                if (nicknamemovimiento == "Azul") { 
                                if (InvokeRequired)
                                    Invoke(new Action(() => this.pbJugador1.Location = new System.Drawing.Point(posxmovimiento, posymovimiento)));
                                }
                                if (nicknamemovimiento == "Morado")
                                {
                                    if (InvokeRequired)
                                        Invoke(new Action(() => this.pbJugador2.Location = new System.Drawing.Point(posxmovimiento, posymovimiento)));
                                }
                                if (nicknamemovimiento == "Verde")
                                {
                                    if (InvokeRequired)
                                        Invoke(new Action(() => this.pbJugador3.Location = new System.Drawing.Point(posxmovimiento, posymovimiento)));
                                }
                                if (nicknamemovimiento == "Rojo")
                                {
                                    if (InvokeRequired)
                                        Invoke(new Action(() => this.pbJugador4.Location = new System.Drawing.Point(posxmovimiento, posymovimiento)));
                                }
                                if(sepmoverse1[2] == "2")
                                {
                                    if(nicknamemovimiento == nick)
                                    {
                                        string dinero = sepmoverse1[3];
                                        if (InvokeRequired)
                                            Invoke(new Action(() => laDineroR.Text = dinero));
                                        MessageBox.Show("Haz pasado por salida obtuviste 200$.");
                                    }
                                }
                                 streamw.WriteLine("009" + nickcasilla +"-"+ posxmovimiento + "-" + posymovimiento + "-" + ": Gracias ya me movi, refresca.");
                                 streamw.Flush();
                                break;
                        case "006Y":
                                string msjdinero = mensaje.Substring(4);
                                string[] sepdinero = msjdinero.Split('-');//[codigo][dinero][nick][propiedad][dineropagado][nickpagado]
                                int codigo = int.Parse(sepdinero[0]);
                                if (codigo == 1)
                                {
                                    string dinero = sepdinero[1];
                                    string nickdinero = sepdinero[2];
                                    if (nickdinero == nick)
                                    {
                                        int comprobardinero = int.Parse(dinero);
                                        if(comprobardinero <= 200)
                                            MessageBox.Show("Deberias vender algunas propiedades, si tu dinero baja de 0 quedarás en banca rota.");
                                        if (InvokeRequired)
                                            Invoke(new Action(() => laDineroR.Text = dinero));
                                    }
                                }
                                if (codigo == 2)
                                {
                                    string dinero = sepdinero[1];
                                    string nickdinero = sepdinero[2];
                                    int idcasilla = int.Parse(sepdinero[3]);
                                    if (nickdinero == nick)
                                    {
                                        if (InvokeRequired)
                                            Invoke(new Action(() => laDineroR.Text = dinero));
                                        if (idcasilla == 2)
                                        {
                                            this.pbPropiedad.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.PropiedadPostgrado;
                                            MessageBox.Show("Haz comprado Postgrado, por la cantidad de: 60$.");
                                        }
                                        if (idcasilla == 4)
                                        {
                                            this.pbPropiedad.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.PropiedadFeria;
                                            MessageBox.Show("Haz comprado Feria, por la cantidad de: 60$.");
                                        }
                                        if (idcasilla == 6)
                                        {
                                            this.pbPropiedad.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.PropiedadTaxiSur;
                                            MessageBox.Show("Haz comprado el Taxi del Sur, por la cantidad de: 200$.");
                                        }
                                        if (idcasilla == 7)
                                        {
                                            this.pbPropiedad.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.PropiedadModulo6;
                                            MessageBox.Show("Haz comprado Modulo 6, por la cantidad de: 100$.");

                                        }
                                        if (idcasilla == 9)
                                        {
                                            this.pbPropiedad.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.PropiedadModulo5;
                                            MessageBox.Show("Haz comprado Modulo 5, por la cantidad de: 100$.");
                                        }
                                        if (idcasilla == 10)
                                        {
                                            this.pbPropiedad.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.PropiedadModulo4;
                                            MessageBox.Show("Haz comprado Modulo 4, por la cantidad de: 120$.");
                                        }
                                        if (idcasilla == 12)
                                        {
                                            this.pbPropiedad.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.PropiedadBiblioteca;
                                            MessageBox.Show("Haz comprado Biblioteca, por la cantidad de: 140$.");
                                        }
                                        if (idcasilla == 13)
                                        {
                                            this.pbPropiedad.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.PropiedadElectricidad;
                                            MessageBox.Show("Haz comprado el servicio de Electricidad, por la cantidad de: 150$.");

                                        }
                                        if (idcasilla == 14)
                                        {
                                            this.pbPropiedad.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.PropiedadModulo3;
                                            MessageBox.Show("Haz comprado Modulo 3, por la cantidad de: 140$.");
                                        } 
                                        if (idcasilla == 15)
                                        {
                                            this.pbPropiedad.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.PropiedadCafetin;
                                            MessageBox.Show("Haz comprado Cafetin, , por la cantidad de: 160$.");

                                        }
                                        if (idcasilla == 16)
                                        {
                                            this.pbPropiedad.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.PropiedadTaxiOeste;
                                            MessageBox.Show("Haz comprado el Taxi del Oeste, por la cantidad de: 200$.");
                                        }  
                                        if (idcasilla == 17)
                                        {
                                            this.pbPropiedad.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.PropiedadModulo2;
                                            MessageBox.Show("Haz comprado Modulo 2, por la cantidad de: 180$.");
                                        }
                                        if (idcasilla == 19)
                                        {
                                            this.pbPropiedad.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.PropiedadModulo1;
                                            MessageBox.Show("Haz comprado Modulo 1, por la cantidad de: 180$.");
                                        } 
                                        if (idcasilla == 20)
                                        {
                                            this.pbPropiedad.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.PropiedadSolarium;
                                            MessageBox.Show("Haz comprado Solarium, por la cantidad de: 200$.");
                                        }
                                        if (idcasilla == 22)
                                        {
                                            this.pbPropiedad.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.PropiedadLaboratorios;
                                            MessageBox.Show("Haz comprado Laboratorios, por la cantidad de: 220$.");
                                        }
                                        if (idcasilla == 24)
                                        {
                                            this.pbPropiedad.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.PropiedadFacultades;
                                            MessageBox.Show("Haz comprado Facultades, por la cantidad de: 220$.");
                                        }
                                        if (idcasilla == 25)
                                        {
                                            this.pbPropiedad.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.PropiedadPlaya;
                                            MessageBox.Show("Haz comprado Playa, por la cantidad de: 240$.");
                                        }
                                        if (idcasilla == 26)
                                        {
                                            this.pbPropiedad.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.PropiedadTaxiNorte;
                                            MessageBox.Show("Haz comprado el Taxi del Norte, por la cantidad de: 200$.");
                                        }
                                        if (idcasilla == 27)
                                        {
                                            this.pbPropiedad.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.PropiedadSambilito;
                                            MessageBox.Show("Haz comprado Sambilito, por la cantidad de: 260$.");
                                        }   
                                        if (idcasilla == 28)
                                        {
                                            this.pbPropiedad.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.PropiedadCanchas;
                                            MessageBox.Show("Haz comprado Canchas, por la cantidad de: 260$.");
                                        }
                                        if (idcasilla == 29)
                                        {
                                            this.pbPropiedad.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.PropiedadAgua;
                                            MessageBox.Show("Haz comprado el servicio de Agua, por la cantidad de: 150$.");
                                        }
                                        if (idcasilla == 30)
                                        {
                                            this.pbPropiedad.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.PropiedadGimnasio;
                                            MessageBox.Show("Haz comprado Gimnasio, por la cantidad de: 280$.");
                                        }
                                        if (idcasilla == 32)
                                        {
                                            this.pbPropiedad.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.PropiedadCincuentenario;
                                            MessageBox.Show("Haz comprado Cincuentenario, por la cantidad de: 300$.");
                                        } 
                                        if (idcasilla == 33)
                                        {
                                            this.pbPropiedad.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.PropiedadAvNorte;
                                            MessageBox.Show("Haz comprado la Av. Norte, por la cantidad de: 300$.");
                                        }
                                        if (idcasilla == 35)
                                        {
                                            this.pbPropiedad.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.PropiedadAvOeste;
                                            MessageBox.Show("Haz comprado la Av. Oeste, por la cantidad de: 320$.");
                                        }
                                        if (idcasilla == 36)
                                        {
                                            this.pbPropiedad.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.PropiedadTaxiEste;
                                            MessageBox.Show("Haz comprado el Taxi del Este, por la cantidad de: 200$.");
                                        }
                                        if (idcasilla == 38)
                                        {
                                            this.pbPropiedad.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.PropiedadAvSur;
                                            MessageBox.Show("Haz comprado la Av. Sur, por la cantidad de: 350$.");
                                        }
                                        if (idcasilla == 40)
                                        {
                                            this.pbPropiedad.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.PropiedadAvEste;
                                            MessageBox.Show("Haz comprado la Av. Este, por la cantidad de: 400$.");
                                        }
                                        int comprobardinero = int.Parse(dinero);
                                        if (comprobardinero <= 200)
                                            MessageBox.Show("Deberias vender algunas propiedades, si tu dinero baja de 0 quedarás en banca rota.");
                                    }
                                }
                                if (codigo == 3)//[codigo][dinero][nick][propiedad][dineropagado][nickpagado]
                                {
                                    string dinero = sepdinero[1];
                                    string nickdinero = sepdinero[2];
                                    string dineropagado = sepdinero[4];
                                    string nickpagado = sepdinero[5];
                                    if (nickdinero == nick)
                                    {
                                        int comprobardinero = int.Parse(dinero);
                                        if(comprobardinero >= 0)
                                        {
                                            MessageBox.Show("Tuviste que pagar al dueño de la propiedad, te quedo la cantidad de: " + dinero);
                                            if (InvokeRequired)
                                                Invoke(new Action(() => laDineroR.Text = dinero));
                                            if (comprobardinero <= 200)
                                                MessageBox.Show("Deberias vender algunas propiedades, si tu dinero baja de 0 quedarás en banca rota.");
                                        }
                                        else
                                        {
                                            bancarota = "Si";
                                            MessageBox.Show("Haz quedado en BANCA ROTA.");
                                            streamw.WriteLine("007" + nick + "-" + bancarota);
                                            streamw.Flush();
                                        }

                                    }
                                    if (nickpagado == nick)
                                    {
                                        MessageBox.Show("Haz recibido dinero de tus propiedades, ahora tienes la cantidad de: " + dineropagado);
                                        if (InvokeRequired)
                                            Invoke(new Action(() => laDineroR.Text = dineropagado));
                                    }
                                }
                                if (codigo == 4)//[codigo][dinero][nick][tipo][propiedad]
                                {
                                    string dinero = sepdinero[1];
                                    string nickdinero = sepdinero[2];
                                    string tipo = sepdinero[3];
                                    int idtipo = int.Parse(sepdinero[4]);
                                    if(nickdinero == nick)
                                    {
                                        if (tipo == "Tesoreria")
                                        {
                                            if (idtipo == 1)
                                            {
                                                this.pbTipo.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.Tesoreria1;
                                                Thread.Sleep(5000);
                                                this.pbTipo.BackgroundImage = null;
                                            }
                                            if (idtipo == 2)
                                            {
                                                this.pbTipo.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.Tesoreria2;
                                                Thread.Sleep(5000);
                                                this.pbTipo.BackgroundImage = null;
                                            }
                                            if (idtipo == 3)
                                            {
                                                this.pbTipo.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.Tesoreria3;
                                                Thread.Sleep(5000);
                                                this.pbTipo.BackgroundImage = null;
                                            }
                                            if (idtipo == 4)
                                            {
                                                this.pbTipo.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.Tesoreria4;
                                                Thread.Sleep(5000);
                                                this.pbTipo.BackgroundImage = null;
                                            }
                                            if (idtipo == 5)
                                            {
                                                this.pbTipo.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.Tesoreria5;
                                                Thread.Sleep(5000);
                                                this.pbTipo.BackgroundImage = null;
                                            }
                                            if (idtipo == 6)
                                            {
                                                this.pbTipo.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.Tesoreria6;
                                                Thread.Sleep(5000);
                                                this.pbTipo.BackgroundImage = null;
                                            }
                                            if (idtipo == 7)
                                            {
                                                this.pbTipo.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.Tesoreria7;
                                                Thread.Sleep(5000);
                                                this.pbTipo.BackgroundImage = null;
                                            }
                                            if (idtipo == 8)
                                            {
                                                this.pbTipo.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.Tesoreria8;
                                                Thread.Sleep(5000);
                                                this.pbTipo.BackgroundImage = null;
                                            }                                          
                                        }
                                        if (tipo == "Casualidad")
                                        {
                                            if (idtipo == 1)
                                            {
                                                this.pbTipo.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.Casualidad1;
                                                Thread.Sleep(5000);
                                                this.pbTipo.BackgroundImage = null;
                                            }
                                            if (idtipo == 2)
                                            {
                                                this.pbTipo.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.Casualidad2;
                                                Thread.Sleep(5000);
                                                this.pbTipo.BackgroundImage = null;
                                            }
                                            if (idtipo == 3)
                                            {
                                                this.pbTipo.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.Casualidad3;
                                                Thread.Sleep(5000);
                                                this.pbTipo.BackgroundImage = null;
                                            }
                                            if (idtipo == 4)
                                            {
                                                this.pbTipo.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.Casualidad4;
                                                Thread.Sleep(5000);
                                                this.pbTipo.BackgroundImage = null;
                                            }
                                            if (idtipo == 5)
                                            {
                                                this.pbTipo.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.Casualidad5;
                                                Thread.Sleep(5000);
                                                this.pbTipo.BackgroundImage = null;
                                            }
                                            if (idtipo == 6)
                                            {
                                                this.pbTipo.BackgroundImage = global::ClienteUcabPolyFinal.Properties.Resources.Casualidad6;
                                                Thread.Sleep(5000);
                                                this.pbTipo.BackgroundImage = null;
                                                tarjeta = "Si";
                                                if (InvokeRequired)
                                                    Invoke(new Action(() => laEstado.Text = tarjeta));
                                            }
                                            if (InvokeRequired)
                                                Invoke(new Action(() => laDineroR.Text = dinero));
                                            int comprobardinero = int.Parse(dinero);
                                            if (comprobardinero <= 50)
                                                MessageBox.Show("Deberias vender algunas propiedades, si tu dinero baja de 0 quedarás en banca rota.");
                                        }
                                    }

                                }
                                if (codigo == 5)//[codigo][dinero][nick][tarjeta]
                                {
                                    string dinero = sepdinero[1];
                                    string nickdinero = sepdinero[2];
                                    tarjeta = sepdinero[3];
                                    if (nickdinero == nick)
                                    {
                                        if (InvokeRequired)
                                            Invoke(new Action(() => laDineroR.Text = dinero));
                                    }
                                    if(tarjeta == "No")
                                    {
                                        if (InvokeRequired)
                                            Invoke(new Action(() => laEstado.Text = tarjeta));
                                    }
                                    if (nickdinero == "Azul")
                                    {
                                        if (InvokeRequired)
                                            Invoke(new Action(() => this.pbJugador1.Location = new System.Drawing.Point(3, 444)));
                                    }
                                    if (nickdinero == "Morado")
                                    {
                                        if (InvokeRequired)
                                            Invoke(new Action(() => this.pbJugador2.Location = new System.Drawing.Point(30, 444)));
                                    }
                                    if (nickdinero == "Verde")
                                    {
                                        if (InvokeRequired)
                                            Invoke(new Action(() => this.pbJugador3.Location = new System.Drawing.Point(3, 472)));
                                    }
                                    if (nickdinero == "Rojo")
                                    {
                                        if (InvokeRequired)
                                            Invoke(new Action(() => this.pbJugador4.Location = new System.Drawing.Point(30, 472)));
                                    }
                                    int comprobardinero = int.Parse(dinero);
                                    if (comprobardinero <= 50)
                                        MessageBox.Show("Deberias vender algunas propiedades, si tu dinero baja de 0 quedarás en banca rota.");
                                }
                                    break;
                        case "007Y":
                                string msjganador = mensaje.Substring(4);
                                if (msjganador == nick)
                                {
                                    MessageBox.Show("FELICIDADES HAZ GANADO EL JUEGO!!!");
                                }
                                break;
                        case "008Y":
                            break;
                        case "009Y":
                                string msjrefrescar = mensaje.Substring(4); 
                                string[] seprefrescar = msjrefrescar.Split('-');//[nick][posx][posy]
                                string nicknamerefresh = seprefrescar[0];
                                int posxrefresh = int.Parse(seprefrescar[1]);
                                int posyrefresh = int.Parse(seprefrescar[2]);
                                if (nicknamerefresh == "Azul")
                                {
                                    if (InvokeRequired)
                                        Invoke(new Action(() => this.pbJugador1.Location = new System.Drawing.Point(posxrefresh, posyrefresh)));
                                }
                                if (nicknamerefresh == "Morado")
                                {
                                    if (InvokeRequired)
                                        Invoke(new Action(() => this.pbJugador2.Location = new System.Drawing.Point(posxrefresh, posyrefresh)));
                                }
                                if (nicknamerefresh == "Verde")
                                {
                                    if (InvokeRequired)
                                        Invoke(new Action(() => this.pbJugador3.Location = new System.Drawing.Point(posxrefresh, posyrefresh)));
                                }
                                if (nicknamerefresh == "Rojo")
                                {
                                    if (InvokeRequired)
                                        Invoke(new Action(() => this.pbJugador4.Location = new System.Drawing.Point(posxrefresh, posyrefresh)));
                                }
                                if (nicknamerefresh == nick)
                                {
                                    int idcasilla = control.conocerCasilla(posx, posy);
                                    streamw.WriteLine("006" + nick + "-" + idcasilla + "-" + "que sucede en mi casilla?");
                                    streamw.Flush();
                                }
                                break;
                        case "010Y":
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
                            Console.WriteLine("Usuario incorrecto");
                            break;
                        default:
                            MessageBox.Show("EL hilo escucha no entro en switch");
                            client.Close();
                            break;
                    }
					
				}
				catch(Exception ex)
                {
                    MessageBox.Show("No se ha podido conectar al servidor (Thread: Escuchar)");
                    Console.WriteLine("EXCEPCION: " + ex);
                    client.Close();
                    break;
                }
			}
		}
		
		void Conectar()
		{
            try
            {
                client.Connect("192.168.43.129", 8000);
               
                if (client.Connected)
                {
                    

                    stream = client.GetStream();
                    streamw = new StreamWriter(stream);
                    streamr = new StreamReader(stream);
                    Thread hiloescucha = new Thread(Escuchar);
                    hiloescucha.Start();
                }
                else
                {
                    MessageBox.Show("Servidor no Disponible (Conectar1)");


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Servidor no Disponible (Conectar2)");
                Console.WriteLine("EXCEPCION: "+ex);
               
            }
        }

        private void pbJugar_Click(object sender, EventArgs e)
        {
            streamw.WriteLine("001" + nick + ": le ha dado a play.");
            streamw.Flush();
        }

        private void laDinero_Click(object sender, EventArgs e)
        {

        }

        private void btPasarTurno_Click(object sender, EventArgs e)
        {
            //PASA EL TURNO 4 VECES FUMATE UNA PS
            streamw.WriteLine("010" + turno + "-" + "Confirmado, pasar turnos.");
            streamw.Flush();
        }

        private void Interfaz_FormClosing(object sender, FormClosingEventArgs e)
        {

            Environment.Exit(1);
                
        }

        private void btUsar_Click(object sender, EventArgs e)
        {

        }

        private void btTirarDado_Click(object sender, EventArgs e)
        {
            btTirarDado.Visible = false;
            streamw.WriteLine("004" + nick + ": tiro el dado.");
            streamw.Flush();
        }
    }
}
