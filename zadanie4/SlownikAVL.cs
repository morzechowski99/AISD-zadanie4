using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace zadanie4
{
    class SlownikAVL 
    {
        private Wyraz korzen;
        class Wyraz
        {
            public string wyraz;
            public Wyraz lewy;
            public Wyraz prawy;
            public Wyraz ojciec;
            public int waga;
            public Wyraz(string wyraz)
            {
                this.wyraz = wyraz;
                waga = 0;
            }
        }
        public SlownikAVL(bool a)
        {

        }
        public SlownikAVL()
            {
                StreamReader s = new StreamReader("C:/Users/marci/Desktop/in.txt");
                s.ReadLine();
                String value;
                while ((value = s.ReadLine()) != null)
                {
                    String[] p = value.Split(null);
                    Menu(Convert.ToChar(p[0]), p[1]);
                }
                s.Close();
            }
            private void Menu(char a, String b)
            {
                switch (a)
                {
                    case 'W':
                        Wstaw(b);
                        break;
                    case 'U':
                        Usun(b);
                        break;
                    case 'S':
                        if (Szukajj(b) == true)
                            Console.WriteLine("TAK");
                        else Console.WriteLine("NIE");
                        break;
                    case 'L':
                        Console.WriteLine(Ile(b));
                        break;
                }
            }
        public void Wstaw(String s)
        {
            if (korzen == null)
            {
                korzen = new Wyraz(s);
            }
            else
            {
                Wyraz wstawiany = new Wyraz(s);
                Wyraz aktualny = korzen;
                while (true)
                {
                    if (wstawiany.wyraz == aktualny.wyraz)
                    {
                       
                        break;
                    }
                    if (aktualny.prawy == null && String.Compare(wstawiany.wyraz, aktualny.wyraz) > 0)
                    {
                        aktualny.prawy = wstawiany;
                        wstawiany.ojciec = aktualny;
                        aktualny.waga--;
                        wstawiany.waga = 0;
                        Balansuj(aktualny);
                        break;
                    }
                    else if (aktualny.lewy == null && String.Compare(wstawiany.wyraz, aktualny.wyraz) < 0)
                    {
                        aktualny.lewy = wstawiany;
                        wstawiany.ojciec = aktualny;
                        aktualny.waga++;
                        wstawiany.waga = 0;
                        Balansuj(aktualny);
                        break;
                    }
                    else if (String.Compare(wstawiany.wyraz, aktualny.wyraz) < 0)
                    {
                        aktualny = aktualny.lewy;
                    }
                    else if (String.Compare(wstawiany.wyraz, aktualny.wyraz) > 0)
                    {
                        aktualny = aktualny.prawy;
                    }
                }
                
            }
        }
        private void Balansuj(Wyraz pom)
        {
            if (pom == null) return;
            else if (pom.waga == 0) return;
            if (pom.waga == 1 || pom.waga == -1)
            {
                if (pom.ojciec != null)
                {
                    if (pom.ojciec.lewy == pom) pom.ojciec.waga++;
                    else pom.ojciec.waga--;
                    Balansuj(pom.ojciec);

                }

            }
            else if (pom.waga == 2 && pom.lewy.waga == 1)
            {
                
                RR(pom);
            }
            else if (pom.waga == -2 && pom.prawy.waga == -1)
            {
                
                LL(pom);
                
            }
            else if (pom.waga == 2 && pom.lewy.waga == -1) LR(pom);
            else if (pom.waga == -2 && pom.prawy.waga == 1) RL(pom);
          

        }
        private void RR(Wyraz rotowany)
        {
            if(rotowany.lewy.waga == 1)
            {
                rotowany.waga = 0;
                rotowany.lewy.waga = 0;
            }
            else if(rotowany.lewy.waga == 0)
            {
                rotowany.waga = 1;
                rotowany.lewy.waga = -1;
            }
            if (rotowany == korzen)
            {
                korzen = rotowany.lewy;
                rotowany.lewy.ojciec = null;
            }
            else
            {
                rotowany.lewy.ojciec = rotowany.ojciec;
                if (rotowany.ojciec?.prawy == rotowany)
                {
                    
                    rotowany.ojciec.prawy = rotowany.lewy;
                }else rotowany.ojciec.lewy = rotowany.lewy;
            }
            
            rotowany.ojciec = rotowany.lewy;
            Wyraz pom = rotowany.lewy;
            if (rotowany.lewy.prawy != null)
            {
                rotowany.lewy.prawy.ojciec = rotowany;
                rotowany.lewy = rotowany.lewy.prawy;
            }
            else rotowany.lewy = null;
            pom.prawy = rotowany;
         
                
            
            
        }
        private void LL(Wyraz rotowany)
        {

            if (rotowany.prawy.waga == -1)
            {
                rotowany.waga = 0;
                rotowany.prawy.waga = 0;
            }
            else if (rotowany.prawy.waga == 0)
            {
                rotowany.waga = -1;
                rotowany.prawy.waga = 1;
            }
            if (rotowany == korzen)
            {
                korzen = rotowany.prawy;
                rotowany.prawy.ojciec = null;
            }
            else
            {
                
                rotowany.prawy.ojciec = rotowany.ojciec;
                if (rotowany.ojciec?.prawy == rotowany)
                {
                    rotowany.ojciec.prawy = rotowany.prawy;
                }
                else rotowany.ojciec.lewy = rotowany.prawy;
            }

            rotowany.ojciec = rotowany.prawy;
            Wyraz pom = rotowany.prawy;
            if (rotowany.prawy.lewy != null)
            {
                rotowany.prawy.lewy.ojciec = rotowany;
                rotowany.prawy = rotowany.prawy.lewy;
            }
            else rotowany.prawy = null;
            pom.lewy = rotowany;
        }
        private void LR(Wyraz rotowany)
        {
            int pom;
            if (rotowany.lewy.prawy == null) pom = 0;
            else pom = rotowany.lewy.prawy.waga;
            LL(rotowany.lewy);
            RR(rotowany);
            if(pom == -1 )
            {
                rotowany.ojciec.waga = 0; //c
                rotowany.waga = 0; //a
                rotowany.ojciec.lewy.waga = 1; //b
            }
            else if (pom == 0)
            {
                rotowany.ojciec.waga = 0;
                rotowany.waga = 0;
                rotowany.ojciec.lewy.waga = 0;
            }
            else if (pom == 1)
            {
                rotowany.ojciec.waga = 0;
                rotowany.waga = -1;
                rotowany.ojciec.lewy.waga = 0;
            }
        }
        private void RL(Wyraz rotowany)
        {
            
                int pom;
            if (rotowany.prawy.lewy == null) pom = 0;
                else pom = rotowany.prawy.lewy.waga;
                RR(rotowany.prawy);
                LL(rotowany);
                if (pom == 1)
                {
                    rotowany.ojciec.waga = 0;
                    rotowany.waga = 0;
                    rotowany.ojciec.prawy.waga = -1;
                }
                else if (pom == 0)
                {
                    rotowany.ojciec.waga = 0;
                    rotowany.waga = 0;
                    rotowany.ojciec.prawy.waga = 0;
                }
                if (pom == -1)
                {
                    rotowany.ojciec.waga = 0;
                    rotowany.waga = 1;
                    rotowany.ojciec.prawy.waga = 0;
                }
            
        }
        private Wyraz Szukaj(String s)
        {
            Wyraz aktualny = korzen;
            while (true)
            {
                if (aktualny.wyraz == s)
                {

                    return aktualny;
                }
                else if (String.Compare(s, aktualny.wyraz) > 0)
                {
                    if (aktualny.prawy == null)
                    {

                        return null;
                    }
                    else
                    {
                        aktualny = aktualny.prawy;
                    }
                }
                else if (String.Compare(s, aktualny.wyraz) < 0)
                {
                    if (aktualny.lewy == null)
                    {

                        return null;
                    }
                    else
                    {
                        aktualny = aktualny.lewy;
                    }
                }
            }
        }
        public void Usun(String s)
        {
            Wyraz doUsuniecia = Szukaj(s);
            if(doUsuniecia!= null)
            {
                if (doUsuniecia.prawy == null && doUsuniecia.lewy == null) //jesli brak synow
                {
                    if (doUsuniecia != korzen)
                    {
                        if (doUsuniecia.ojciec.lewy == doUsuniecia)
                        {
                            doUsuniecia.ojciec.lewy = null;
                            doUsuniecia.ojciec.waga--;
                        }
                        else
                        {
                            doUsuniecia.ojciec.prawy = null;
                            doUsuniecia.ojciec.waga++;
                        }
                        Balansuj2(doUsuniecia.ojciec);
                    }
                    else korzen = null;
                }
                else if (doUsuniecia.prawy == null) // jesli ma tylko jednego syna (lewego)
                {
                    doUsuniecia.lewy.ojciec = doUsuniecia.ojciec;
                    if (doUsuniecia != korzen)
                    {
                        if (doUsuniecia.ojciec.lewy == doUsuniecia)
                        {
                            doUsuniecia.ojciec.lewy = doUsuniecia.lewy;
                            doUsuniecia.ojciec.waga--;
                        }
                        else
                        {
                            doUsuniecia.ojciec.prawy = doUsuniecia.lewy;
                            doUsuniecia.ojciec.waga++;
                        }
                       
                        Balansuj2(doUsuniecia.ojciec);
                    }
                    else korzen = doUsuniecia.lewy;
                }
                else if (doUsuniecia.lewy == null)//jesli ma syna prawego
                {
                    doUsuniecia.prawy.ojciec = doUsuniecia.ojciec;
                    if (doUsuniecia != korzen)
                    {
                        if (doUsuniecia.ojciec.lewy == doUsuniecia)
                        {
                            doUsuniecia.ojciec.lewy = doUsuniecia.prawy;
                            doUsuniecia.ojciec.waga--;
                        }
                        else
                        {
                            doUsuniecia.ojciec.prawy = doUsuniecia.prawy;
                            doUsuniecia.ojciec.waga++;
                        }
                        Balansuj2(doUsuniecia.ojciec);
                    }
                    else korzen = doUsuniecia.prawy;
                }
                else //jesli jest dwoch synow
                {
                    Wyraz nastepnik = doUsuniecia.prawy;
                    
                    while (true) //szukanie nastepnika
                    {
                        if (nastepnik.lewy == null) break;
                        nastepnik = nastepnik.lewy;
                    }
                    Wyraz pom;
                    nastepnik.waga = doUsuniecia.waga;
                    if (nastepnik.ojciec != doUsuniecia)
                    {
                        pom = nastepnik.ojciec;
                    }
                    else
                    {
                        pom = nastepnik;
                        nastepnik.waga++;
                    }
                    if (nastepnik.prawy != null && nastepnik.ojciec != doUsuniecia) //jesli nastepnik ma prawego syna 
                    {
                        nastepnik.prawy.ojciec = nastepnik.ojciec;
                        nastepnik.ojciec.lewy = nastepnik.prawy;
                        nastepnik.ojciec.waga--;
                    }
                    else if (nastepnik.ojciec != doUsuniecia)//jesli go nie ma
                    {
                        nastepnik.ojciec.lewy = null;
                        nastepnik.ojciec.waga--;
                    }
                    
                    if (doUsuniecia == korzen) //jesli usuwamy korzen
                    {
                        korzen = nastepnik;
                        nastepnik.ojciec = null;
                    }
                    else
                    {
                        nastepnik.ojciec = doUsuniecia.ojciec;

                        if (doUsuniecia.ojciec.lewy == doUsuniecia) doUsuniecia.ojciec.lewy = nastepnik;
                        else doUsuniecia.ojciec.prawy = nastepnik;
                    }
                    
                    if (doUsuniecia.prawy != nastepnik)
                    {
                        nastepnik.prawy = doUsuniecia.prawy;
                        nastepnik.prawy.ojciec = nastepnik;
                    }
                  
                    if (doUsuniecia.lewy != null)
                    {
                        nastepnik.lewy = doUsuniecia.lewy;
                        doUsuniecia.lewy.ojciec = nastepnik;
                    }
                    else
                    {
                        nastepnik.lewy = null;
                        
                    }
                    Balansuj2(pom);
                }
                
            }
            
        }
        private void Balansuj2(Wyraz balansowany)
        {
            if (balansowany.waga == 1 || balansowany.waga == -1) return;
            if (balansowany.waga == 0)
            {
                if (balansowany != korzen)
                {
                    if (balansowany.ojciec.lewy == balansowany) balansowany.ojciec.waga--;
                    else balansowany.ojciec.waga++;
                    Balansuj2(balansowany.ojciec);
                }
            }
            if(balansowany.waga == -2 && balansowany.prawy.waga == 0)
            {
                LL(balansowany);
               
            }
            else if (balansowany.waga == 2 && balansowany.lewy.waga == 0)
            {
                RR(balansowany);
               
            }
            else if(balansowany.waga == -2 && balansowany.prawy.waga == -1)
            {
                Wyraz pom = balansowany.prawy;
                
                LL(balansowany);
                if (pom != korzen)
                {
                    if (pom.ojciec.lewy == balansowany) pom.ojciec.waga--;
                    else pom.ojciec.waga++;
                    Balansuj2(pom.ojciec);
                }
            }
            else if (balansowany.waga == 2 && balansowany.lewy.waga == 1)
            {
                Wyraz pom = balansowany.lewy;
                
                RR(balansowany);
               
                if (pom != korzen)
                {
                    if (pom.ojciec.lewy == pom) pom.ojciec.waga--;
                    else pom.ojciec.waga++;
                   
                    Balansuj2(pom.ojciec);
                }
            }
            else if (balansowany.waga == -2 && balansowany.prawy.waga == 1)
            {
                Wyraz pom = balansowany.prawy.lewy;
                RL(balansowany);
                if (pom != korzen)
                {
                    if (pom.ojciec.lewy == pom) pom.ojciec.waga--;
                    else pom.ojciec.waga++;
                    Balansuj2(pom.ojciec);
                }
            }
            else if (balansowany.waga == 2 && balansowany.lewy.waga == -1)
            {
                Wyraz pom = balansowany.lewy.prawy;
                LR(balansowany);
                if (pom != korzen)
                {
                    if (pom.ojciec.lewy == pom) pom.ojciec.waga--;
                    else pom.ojciec.waga++;
                    Balansuj2(pom.ojciec);
                }
            }
        }
        public void WyswietlDrzewo()
        {
            int spacje = 15;
            void rysuj(Wyraz korzen, int odstep)
            {
                if (korzen == null)
                    return;
                odstep += spacje;
                rysuj(korzen.prawy, odstep);
                Console.Write("\n");
                for (int i = spacje; i < odstep; i++)
                    Console.Write(" ");
                Console.Write(korzen.wyraz + " "+ korzen.waga+"\n");

                rysuj(korzen.lewy, odstep);
            }
            rysuj(korzen, 0);
        }
        public bool Szukajj(String s)
        {
            if (Szukaj(s) == null) return false;
            else return true;
        }
        public int Ile(String s)
        {
            int ile = 0;
            void Sprawdz(Wyraz sprawdzany)
            {
                if (sprawdzany == null) return;
                string pom = sprawdzany.wyraz.Substring(0, s.Length);
                if (pom == s)
                {
                    ile++;
                    Sprawdz(sprawdzany.lewy);
                    Sprawdz(sprawdzany.prawy);
                }
                else if (String.Compare(s, pom) > 0)
                {
                    Sprawdz(sprawdzany.prawy);
                }
                else if (String.Compare(s, pom) < 0)
                {
                    Sprawdz(sprawdzany.lewy);
                }

            }
            Sprawdz(korzen);

            return ile;
        }
    }
    
}

