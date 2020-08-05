using System;

namespace zadanie4
{
    class Program
    {
        static void Main(string[] args)
        {
            SlownikAVL d = new SlownikAVL();
           
            
            while (true)
            {


                PiszMenu();
                switch (Convert.ToInt32(Console.ReadLine()))
                {
                    case 1:
                        d.Wstaw(Console.ReadLine());
                        break;
                    case 2:
                        d.Usun(Console.ReadLine());
                        break;
                    case 3:
                        if (d.Szukajj(Console.ReadLine()) == true)
                            Console.WriteLine("TAK");
                        else Console.WriteLine("NIE");
                        break;
                    case 4:
                        Console.WriteLine("Odpowiedz: " + d.Ile(Console.ReadLine())+ "\n");
                        break;
                    case 5:
                        d.WyswietlDrzewo();
                        break;
                    case 0:
                        d = new SlownikAVL(false);
                        break;
                    default:
                        break;

                }
                
                void PiszMenu()
                {
                    Console.WriteLine("1. Dodaj\n2.Usun\n3.Szukaj\n4.Ile\n5.Pisz\n0. Nowe Drzewo");
                }
            }
         
        }
    }
}
