using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace zadanie4
{
    class Slownik
    {
        private Wyraz korzen;
        class Wyraz
        {
            public string wyraz;
            public Wyraz ojciec;
            public Wyraz lewy;
            public Wyraz prawy;
            public Wyraz(string wyraz)
            {
                this.wyraz = wyraz;
            }
        }
        public Slownik(bool a)
        {

        }
        public Slownik()
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
            //System.GC.Collect();
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
                        //Console.WriteLine("to jest juz w slowniku :(");
                        break;
                    }
                    if (aktualny.prawy == null && String.Compare(wstawiany.wyraz, aktualny.wyraz) > 0)
                    {
                        aktualny.prawy = wstawiany;
                        wstawiany.ojciec = aktualny;
                        break;
                    }
                    else if (aktualny.lewy == null && String.Compare(wstawiany.wyraz, aktualny.wyraz) < 0)
                    {
                        aktualny.lewy = wstawiany;
                        wstawiany.ojciec = aktualny;
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
        public void Usun(String s)
        {
            Wyraz doUsuniecia = Szukaj(s);
            if (doUsuniecia != null)
            {
                if (doUsuniecia.prawy == null && doUsuniecia.lewy == null) //jesli brak synow
                {
                    if (doUsuniecia != korzen)
                    {
                        if (doUsuniecia.ojciec.lewy == doUsuniecia) doUsuniecia.ojciec.lewy = null;
                        else doUsuniecia.ojciec.prawy = null;
                    }
                    else korzen = null;
                }
                else if (doUsuniecia.prawy == null) // jesli ma tylko jednego syna (lewego)
                {
                    doUsuniecia.lewy.ojciec = doUsuniecia.ojciec;
                    if (doUsuniecia != korzen)
                    {
                        if (doUsuniecia.ojciec.lewy == doUsuniecia) doUsuniecia.ojciec.lewy = doUsuniecia.lewy;
                        else doUsuniecia.ojciec.prawy = doUsuniecia.lewy;
                    }
                    else korzen = doUsuniecia.lewy;
                }
                else if (doUsuniecia.lewy == null)//jesli ma syna prawego
                {
                    doUsuniecia.prawy.ojciec = doUsuniecia.ojciec;
                    if (doUsuniecia != korzen)
                    {
                        if (doUsuniecia.ojciec.lewy == doUsuniecia) doUsuniecia.ojciec.lewy = doUsuniecia.prawy;
                        else doUsuniecia.ojciec.prawy = doUsuniecia.prawy;
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
                    if (nastepnik.prawy != null && nastepnik.ojciec != doUsuniecia) //jesli nastepnik ma prawego syna 
                    {
                        nastepnik.prawy.ojciec = nastepnik.ojciec;
                        nastepnik.ojciec.lewy = nastepnik.prawy;
                    }
                    else if (nastepnik.ojciec != doUsuniecia)//jesli go nie ma
                    {
                        nastepnik.ojciec.lewy = null;
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
                    else nastepnik.lewy = null;

                }
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
        public void WyswietlDrzewo()
        {
            int COUNT = 20;
            void print2DUtil(Wyraz root, int space)
            {
                if (root == null)
                    return;
                space += COUNT;
                print2DUtil(root.prawy, space);
                Console.Write("\n");
                for (int i = COUNT; i < space; i++)
                    Console.Write(" ");
                Console.Write(root.wyraz + "\n");

                print2DUtil(root.lewy, space);
            }
            print2DUtil(korzen, 0);
        }
    } }
