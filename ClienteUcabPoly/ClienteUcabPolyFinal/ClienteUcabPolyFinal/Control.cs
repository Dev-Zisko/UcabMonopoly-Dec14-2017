using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlMonopoly
{

    public class Control
    {
        public int conocerCasilla(int posx, int posy)
        {
            int id = 0;
            if ((posx == 669 && posy == 444) || (posx == 696 && posy == 444) || (posx == 669 && posy == 472) || (posx == 696 && posy == 472))
            {
                id = 1;
            }
            if ((posx == 585 && posy == 444) || (posx == 612 && posy == 444) || (posx == 585 && posy == 472) || (posx == 612 && posy == 472))
            {
                id = 2;
            }
            if ((posx == 526 && posy == 444) || (posx == 553 && posy == 444) || (posx == 526 && posy == 472) || (posx == 553 && posy == 472))
            {
                id = 3;
            }
            if ((posx == 467 && posy == 444) || (posx == 494 && posy == 444) || (posx == 467 && posy == 472) || (posx == 494 && posy == 472))
            {
                id = 4;
            }
            if ((posx == 408 && posy == 444) || (posx == 435 && posy == 444) || (posx == 408 && posy == 472) || (posx == 435 && posy == 472))
            {
                id = 5;
            }
            if ((posx == 348 && posy == 444) || (posx == 375 && posy == 444) || (posx == 348 && posy == 472) || (posx == 375 && posy == 472))
            {
                id = 6;
            }
            if ((posx == 290 && posy == 444) || (posx == 317 && posy == 444) || (posx == 290 && posy == 472) || (posx == 317 && posy == 472))
            {
                id = 7;
            }
            if ((posx == 231 && posy == 444) || (posx == 258 && posy == 444) || (posx == 231 && posy == 472) || (posx == 258 && posy == 472))
            {
                id = 8;
            }
            if ((posx == 170 && posy == 444) || (posx == 197 && posy == 444) || (posx == 170 && posy == 472) || (posx == 197 && posy == 472))
            {
                id = 9;
            }
            if ((posx == 112 && posy == 444) || (posx == 139 && posy == 444) || (posx == 112 && posy == 472) || (posx == 139 && posy == 472))
            {
                id = 10;
            }
            if ((posx == 3 && posy == 444) || (posx == 30 && posy == 444) || (posx == 3 && posy == 472) || (posx == 30 && posy == 472))
            {
                id = 11;
            }
            if ((posx == 3 && posy == 400) || (posx == 30 && posy == 400) || (posx == 57 && posy == 400) || (posx == 83 && posy == 400))
            {
                id = 12;
            }
            if ((posx == 3 && posy == 355) || (posx == 30 && posy == 355) || (posx == 57 && posy == 355) || (posx == 83 && posy == 355))
            {
                id = 13;
            }
            if ((posx == 3 && posy == 322) || (posx == 30 && posy == 322) || (posx == 57 && posy == 322) || (posx == 83 && posy == 322))
            {
                id = 14;
            }
            if ((posx == 3 && posy == 285) || (posx == 30 && posy == 285) || (posx == 57 && posy == 285) || (posx == 83 && posy == 285))
            {
                id = 15;
            }
            if ((posx == 3 && posy == 237) || (posx == 30 && posy == 237) || (posx == 57 && posy == 237) || (posx == 83 && posy == 237))
            {
                id = 16;
            }
            if ((posx == 3 && posy == 201) || (posx == 30 && posy == 201) || (posx == 57 && posy == 201) || (posx == 83 && posy == 201))
            {
                id = 17;
            }
            if ((posx == 669 && posy == 444) || (posx == 696 && posy == 444) || (posx == 669 && posy == 472) || (posx == 696 && posy == 472))
            {
                id = 1;
            }
            if ((posx == 585 && posy == 444) || (posx == 612 && posy == 444) || (posx == 585 && posy == 472) || (posx == 612 && posy == 472))
            {
                id = 2;
            }
            if ((posx == 526 && posy == 444) || (posx == 553 && posy == 444) || (posx == 526 && posy == 472) || (posx == 553 && posy == 472))
            {
                id = 3;
            }
            if ((posx == 467 && posy == 444) || (posx == 494 && posy == 444) || (posx == 467 && posy == 472) || (posx == 494 && posy == 472))
            {
                id = 4;
            }
            if ((posx == 408 && posy == 444) || (posx == 435 && posy == 444) || (posx == 408 && posy == 472) || (posx == 435 && posy == 472))
            {
                id = 5;
            }
            if ((posx == 348 && posy == 444) || (posx == 375 && posy == 444) || (posx == 348 && posy == 472) || (posx == 375 && posy == 472))
            {
                id = 6;
            }
            if ((posx == 290 && posy == 444) || (posx == 317 && posy == 444) || (posx == 290 && posy == 472) || (posx == 317 && posy == 472))
            {
                id = 7;
            }
            if ((posx == 231 && posy == 444) || (posx == 258 && posy == 444) || (posx == 231 && posy == 472) || (posx == 258 && posy == 472))
            {
                id = 8;
            }
            if ((posx == 170 && posy == 444) || (posx == 197 && posy == 444) || (posx == 170 && posy == 472) || (posx == 197 && posy == 472))
            {
                id = 9;
            }
            if ((posx == 112 && posy == 444) || (posx == 139 && posy == 444) || (posx == 112 && posy == 472) || (posx == 139 && posy == 472))
            {
                id = 10;
            }
            if ((posx == 3 && posy == 444) || (posx == 30 && posy == 444) || (posx == 3 && posy == 472) || (posx == 30 && posy == 472))
            {
                id = 11;
            }
            if ((posx == 3 && posy == 400) || (posx == 30 && posy == 400) || (posx == 57 && posy == 400) || (posx == 83 && posy == 400))
            {
                id = 12;
            }
            if ((posx == 3 && posy == 355) || (posx == 30 && posy == 355) || (posx == 57 && posy == 355) || (posx == 83 && posy == 355))
            {
                id = 13;
            }
            if ((posx == 3 && posy == 322) || (posx == 30 && posy == 322) || (posx == 57 && posy == 322) || (posx == 83 && posy == 322))
            {
                id = 14;
            }
            if ((posx == 3 && posy == 285) || (posx == 30 && posy == 285) || (posx == 57 && posy == 285) || (posx == 83 && posy == 285))
            {
                id = 15;
            }
            if ((posx == 3 && posy == 237) || (posx == 30 && posy == 237) || (posx == 57 && posy == 237) || (posx == 83 && posy == 237))
            {
                id = 16;
            }
            if ((posx == 3 && posy == 201) || (posx == 30 && posy == 201) || (posx == 57 && posy == 201) || (posx == 83 && posy == 201))
            {
                id = 17;
            }
            if ((posx == 3 && posy == 154) || (posx == 30 && posy == 154) || (posx == 57 && posy == 154) || (posx == 83 && posy == 154))
            {
                id = 18;
            }
            if ((posx == 3 && posy == 121) || (posx == 30 && posy == 121) || (posx == 57 && posy == 121) || (posx == 83 && posy == 121))
            {
                id = 19;
            }
            if ((posx == 3 && posy == 84) || (posx == 30 && posy == 84) || (posx == 57 && posy == 84) || (posx == 83 && posy == 84))
            {
                id = 20;
            }
            if ((posx == 3 && posy == 25) || (posx == 30 && posy == 25) || (posx == 57 && posy == 25) || (posx == 83 && posy == 25))
            {
                id = 21;
            }
            if ((posx == 112 && posy == 15) || (posx == 139 && posy == 15) || (posx == 112 && posy == 42) || (posx == 139 && posy == 42))
            {
                id = 22;
            }
            if ((posx == 171 && posy == 15) || (posx == 198 && posy == 15) || (posx == 171 && posy == 42) || (posx == 198 && posy == 42))
            {
                id = 23;
            }
            if ((posx == 231 && posy == 15) || (posx == 258 && posy == 15) || (posx == 231 && posy == 42) || (posx == 258 && posy == 42))
            {
                id = 24;
            }
            if ((posx == 290 && posy == 15) || (posx == 317 && posy == 15) || (posx == 290 && posy == 42) || (posx == 317 && posy == 42))
            {
                id = 25;
            }
            if ((posx == 349 && posy == 15) || (posx == 376 && posy == 15) || (posx == 349 && posy == 42) || (posx == 376 && posy == 42))
            {
                id = 26;
            }
            if ((posx == 407 && posy == 15) || (posx == 434 && posy == 15) || (posx == 407 && posy == 42) || (posx == 434 && posy == 42))
            {
                id = 27;
            }
            if ((posx == 466 && posy == 15) || (posx == 493 && posy == 15) || (posx == 466 && posy == 42) || (posx == 493 && posy == 42))
            {
                id = 28;
            }
            if ((posx == 525 && posy == 15) || (posx == 552 && posy == 15) || (posx == 525 && posy == 42) || (posx == 552 && posy == 42))
            {
                id = 29;
            }
            if ((posx == 584 && posy == 15) || (posx == 611 && posy == 15) || (posx == 584 && posy == 42) || (posx == 611 && posy == 42))
            {
                id = 30;
            }
            if ((posx == 669 && posy == 15) || (posx == 696 && posy == 15) || (posx == 669 && posy == 42) || (posx == 696 && posy == 42))
            {
                id = 31;
            }
            if ((posx == 643 && posy == 85) || (posx == 669 && posy == 85) || (posx == 696 && posy == 85) || (posx == 724 && posy == 85))
            {
                id = 32;
            }
            if ((posx == 643 && posy == 123) || (posx == 669 && posy == 123) || (posx == 696 && posy == 123) || (posx == 724 && posy == 123))
            {
                id = 33;
            }
            if ((posx == 643 && posy == 157) || (posx == 669 && posy == 157) || (posx == 696 && posy == 157) || (posx == 724 && posy == 157))
            {
                id = 34;
            }
            if ((posx == 643 && posy == 201) || (posx == 669 && posy == 201) || (posx == 696 && posy == 201) || (posx == 724 && posy == 201))
            {
                id = 35;
            }
            if ((posx == 643 && posy == 236) || (posx == 669 && posy == 236) || (posx == 696 && posy == 236) || (posx == 724 && posy == 236))
            {
                id = 36;
            }
            if ((posx == 643 && posy == 275) || (posx == 669 && posy == 275) || (posx == 696 && posy == 275) || (posx == 724 && posy == 275))
            {
                id = 37;
            }
            if ((posx == 643 && posy == 320) || (posx == 669 && posy == 320) || (posx == 696 && posy == 320) || (posx == 724 && posy == 320))
            {
                id = 38;
            }
            if ((posx == 643 && posy == 357) || (posx == 669 && posy == 357) || (posx == 696 && posy == 357) || (posx == 724 && posy == 357))
            {
                id = 39;
            }
            if ((posx == 643 && posy == 400) || (posx == 669 && posy == 400) || (posx == 696 && posy == 400) || (posx == 724 && posy == 400))
            {
                id = 40;
            }
            return id;
        }
        public string moverse(int casilla, string nick)
        {
            if (casilla == 1)
            {
                if (nick == "Azul")
                    return ("669-444");
                if (nick == "Morado")
                    return ("696-444");
                if (nick == "Verde")
                    return ("669-472");
                if (nick == "Rojo")
                    return ("696-444");
            }
            if (casilla == 2)
            {
                if (nick == "Azul")
                    return ("585-444");
                if (nick == "Morado")
                    return ("612-444");
                if (nick == "Verde")
                    return ("585-472");
                if (nick == "Rojo")
                    return ("612-472");
            }
            if (casilla == 3)
            {
                if (nick == "Azul")
                    return ("526-444");
                if (nick == "Morado")
                    return ("553-444");
                if (nick == "Verde")
                    return ("526-472");
                if (nick == "Rojo")
                    return ("553-472");
            }
            if (casilla == 4)
            {
                if (nick == "Azul")
                    return ("467-444");
                if (nick == "Morado")
                    return ("494-444");
                if (nick == "Verde")
                    return ("467-472");
                if (nick == "Rojo")
                    return ("494-472");
            }
            if (casilla == 5)
            {
                if (nick == "Azul")
                    return ("408-444");
                if (nick == "Morado")
                    return ("435-444");
                if (nick == "Verde")
                    return ("408-472");
                if (nick == "Rojo")
                    return ("435-472");
            }
            if (casilla == 6)
            {
                if (nick == "Azul")
                    return ("348-444");
                if (nick == "Morado")
                    return ("375-444");
                if (nick == "Verde")
                    return ("348-472");
                if (nick == "Rojo")
                    return ("375-472");
            }
            if (casilla == 7)
            {
                if (nick == "Azul")
                    return ("290-444");
                if (nick == "Morado")
                    return ("317-444");
                if (nick == "Verde")
                    return ("290-472");
                if (nick == "Rojo")
                    return ("317-472");
            }
            if (casilla == 8)
            {
                if (nick == "Azul")
                    return ("231-444");
                if (nick == "Morado")
                    return ("258-444");
                if (nick == "Verde")
                    return ("231-472");
                if (nick == "Rojo")
                    return ("258-472");
            }
            if (casilla == 9)
            {
                if (nick == "Azul")
                    return ("170-444");
                if (nick == "Morado")
                    return ("197-444");
                if (nick == "Verde")
                    return ("170-472");
                if (nick == "Rojo")
                    return ("197-472");
            }
            if (casilla == 10)
            {
                if (nick == "Azul")
                    return ("112-444");
                if (nick == "Morado")
                    return ("139-444");
                if (nick == "Verde")
                    return ("112-472");
                if (nick == "Rojo")
                    return ("139-472");
            }
            if (casilla == 11)
            {
                if (nick == "Azul")
                    return ("3-444");
                if (nick == "Morado")
                    return ("30-444");
                if (nick == "Verde")
                    return ("3-472");
                if (nick == "Rojo")
                    return ("30-472");
            }
            if (casilla == 12)
            {
                if (nick == "Azul")
                    return ("3-400");
                if (nick == "Morado")
                    return ("30-400");
                if (nick == "Verde")
                    return ("57-400");
                if (nick == "Rojo")
                    return ("83-400");
            }
            if (casilla == 13)
            {
                if (nick == "Azul")
                    return ("3-355");
                if (nick == "Morado")
                    return ("30-355");
                if (nick == "Verde")
                    return ("57-35");
                if (nick == "Rojo")
                    return ("83-355");
            }
            if (casilla == 14)
            {
                if (nick == "Azul")
                    return ("3-322");
                if (nick == "Morado")
                    return ("30-322");
                if (nick == "Verde")
                    return ("57-322");
                if (nick == "Rojo")
                    return ("83-322");
            }
            if (casilla == 15)
            {
                if (nick == "Azul")
                    return ("3-285");
                if (nick == "Morado")
                    return ("30-285");
                if (nick == "Verde")
                    return ("57-285");
                if (nick == "Rojo")
                    return ("83-285");
            }
            if (casilla == 16)
            {
                if (nick == "Azul")
                    return ("3-237");
                if (nick == "Morado")
                    return ("30-237");
                if (nick == "Verde")
                    return ("57-237");
                if (nick == "Rojo")
                    return ("83-237");
            }
            if (casilla == 17)
            {
                if (nick == "Azul")
                    return ("3-201");
                if (nick == "Morado")
                    return ("30-201");
                if (nick == "Verde")
                    return ("57-201");
                if (nick == "Rojo")
                    return ("83-201");
            }
            if (casilla == 18)
            {
                if (nick == "Azul")
                    return ("3-154");
                if (nick == "Morado")
                    return ("30-154");
                if (nick == "Verde")
                    return ("57-154");
                if (nick == "Rojo")
                    return ("83-154");
            }
            if (casilla == 19)
            {
                if (nick == "Azul")
                    return ("3-121");
                if (nick == "Morado")
                    return ("30-121");
                if (nick == "Verde")
                    return ("57-121");
                if (nick == "Rojo")
                    return ("83-121");
            }
            if (casilla == 20)
            {
                if (nick == "Azul")
                    return ("3-84");
                if (nick == "Morado")
                    return ("30-84");
                if (nick == "Verde")
                    return ("57-84");
                if (nick == "Rojo")
                    return ("83-84");
            }
            if (casilla == 21)
            {
                if (nick == "Azul")
                    return ("3-25");
                if (nick == "Morado")
                    return ("30-25");
                if (nick == "Verde")
                    return ("57-25");
                if (nick == "Rojo")
                    return ("83-25");
            }
            if (casilla == 22)
            {
                if (nick == "Azul")
                    return ("112-15");
                if (nick == "Morado")
                    return ("139-15");
                if (nick == "Verde")
                    return ("112-42");
                if (nick == "Rojo")
                    return ("139-42");
            }
            if (casilla == 23)
            {
                if (nick == "Azul")
                    return ("171-15");
                if (nick == "Morado")
                    return ("198-15");
                if (nick == "Verde")
                    return ("171-42");
                if (nick == "Rojo")
                    return ("198-42");
            }
            if (casilla == 24)
            {
                if (nick == "Azul")
                    return ("231-15");
                if (nick == "Morado")
                    return ("258-15");
                if (nick == "Verde")
                    return ("231-42");
                if (nick == "Rojo")
                    return ("258-42");
            }
            if (casilla == 25)
            {
                if (nick == "Azul")
                    return ("290-15");
                if (nick == "Morado")
                    return ("317-15");
                if (nick == "Verde")
                    return ("290-42");
                if (nick == "Rojo")
                    return ("317-42");
            }
            if (casilla == 26)
            {
                if (nick == "Azul")
                    return ("349-15");
                if (nick == "Morado")
                    return ("376-15");
                if (nick == "Verde")
                    return ("349-42");
                if (nick == "Rojo")
                    return ("376-42");
            }
            if (casilla == 27)
            {
                if (nick == "Azul")
                    return ("407-15");
                if (nick == "Morado")
                    return ("434-15");
                if (nick == "Verde")
                    return ("407-42");
                if (nick == "Rojo")
                    return ("434-42");
            }
            if (casilla == 28)
            {
                if (nick == "Azul")
                    return ("466-15");
                if (nick == "Morado")
                    return ("493-15");
                if (nick == "Verde")
                    return ("466-42");
                if (nick == "Rojo")
                    return ("493-42");
            }
            if (casilla == 29)
            {
                if (nick == "Azul")
                    return ("525-15");
                if (nick == "Morado")
                    return ("552-15");
                if (nick == "Verde")
                    return ("525-42");
                if (nick == "Rojo")
                    return ("552-42");
            }
            if (casilla == 30)
            {
                if (nick == "Azul")
                    return ("584-15");
                if (nick == "Morado")
                    return ("611-15");
                if (nick == "Verde")
                    return ("584-42");
                if (nick == "Rojo")
                    return ("611-42");
            }
            if (casilla == 31)
            {
                if (nick == "Azul")
                    return ("669-15");
                if (nick == "Morado")
                    return ("696-15");
                if (nick == "Verde")
                    return ("669-42");
                if (nick == "Rojo")
                    return ("696-42");
            }
            if (casilla == 32)
            {
                if (nick == "Azul")
                    return ("643-85");
                if (nick == "Morado")
                    return ("669-85");
                if (nick == "Verde")
                    return ("696-85");
                if (nick == "Rojo")
                    return ("724-85");
            }
            if (casilla == 33)
            {
                if (nick == "Azul")
                    return ("643-123");
                if (nick == "Morado")
                    return ("669-123");
                if (nick == "Verde")
                    return ("696-123");
                if (nick == "Rojo")
                    return ("724-123");
            }
            if (casilla == 34)
            {
                if (nick == "Azul")
                    return ("643-157");
                if (nick == "Morado")
                    return ("669-157");
                if (nick == "Verde")
                    return ("696-157");
                if (nick == "Rojo")
                    return ("724-157");
            }
            if (casilla == 35)
            {
                if (nick == "Azul")
                    return ("643-201");
                if (nick == "Morado")
                    return ("669-201");
                if (nick == "Verde")
                    return ("696-201");
                if (nick == "Rojo")
                    return ("724-201");
            }
            if (casilla == 36)
            {
                if (nick == "Azul")
                    return ("643-236");
                if (nick == "Morado")
                    return ("669-236");
                if (nick == "Verde")
                    return ("696-236");
                if (nick == "Rojo")
                    return ("724-236");
            }
            if (casilla == 37)
            {
                if (nick == "Azul")
                    return ("643-275");
                if (nick == "Morado")
                    return ("669-275");
                if (nick == "Verde")
                    return ("696-275");
                if (nick == "Rojo")
                    return ("724-275");
            }
            if (casilla == 38)
            {
                if (nick == "Azul")
                    return ("643-320");
                if (nick == "Morado")
                    return ("669-320");
                if (nick == "Verde")
                    return ("696-320");
                if (nick == "Rojo")
                    return ("724-320");
            }
            if (casilla == 39)
            {
                if (nick == "Azul")
                    return ("643-357");
                if (nick == "Morado")
                    return ("669-357");
                if (nick == "Verde")
                    return ("696-357");
                if (nick == "Rojo")
                    return ("724-357");
            }
            if (casilla == 40)
            {
                if (nick == "Azul")
                    return ("643-400");
                if (nick == "Morado")
                    return ("669-400");
                if (nick == "Verde")
                    return ("696-400");
                if (nick == "Rojo")
                    return ("724-400");
            }
            return ("0");
        }

    }
}
