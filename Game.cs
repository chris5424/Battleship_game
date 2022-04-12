using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship
{
    public class Game
    {
        private static readonly string[] Fields = new[]
        {
            "A1", "A2", "A3", "A4", "A5", "A6", "A7", "A8", "A9", "A10",
            "B1", "B2", "B3", "B4", "B5", "B6", "B7", "B8", "B9", "B10",
            "C1", "C2", "C3", "C4", "C5", "C6", "C7", "C8", "C9", "C10",
            "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9", "D10",
            "E1", "E2", "E3", "E4", "E5", "E6", "E7", "E8", "E9", "E10",
            "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10",
            "G1", "G2", "G3", "G4", "G5", "G6", "G7", "G8", "G9", "G10",
            "H1", "H2", "H3", "H4", "H5", "H6", "H7", "H8", "H9", "H10",
            "I1", "I2", "I3", "I4", "I5", "I6", "I7", "I8", "I9", "I10",
            "J1", "J2", "J3", "J4", "J5", "J6", "J7", "J8", "J9", "J10",
        };
        //private readonly List<Coordinates> _fields = new();
        //private readonly List<Ship> _shipsToPlace = new();

        public void InitGame()
        {
            Player playerOne = new();
            Player playerTwo = new();

            //playerOneBoards.PlaceShipsRandomOnBoard(_shipsToPlace);
            Console.WriteLine("");
            //playerTwoBoards.PlaceShipsRandomOnBoard(_shipsToPlace);

            Random rnd = new();
            int whoseTurn = rnd.Next(1, 3);
            Coordinates shot;
            int shipIndex;

            //play till one player win
            while (!(playerOne.Lost || playerTwo.Lost))
            {
                if (whoseTurn == 1)
                {
                    shot = playerOne.GetShot();
                    playerOne.PlayerBoards.SaveShot(shot);
                    shipIndex = playerTwo.PlayerBoards.CheckFire(shot);
                    if (shipIndex>0)
                    {
                        playerTwo.PlayerShips[shipIndex - 1].ReceivedHits++;
                        //Console.WriteLine("Trafiony");
                        if (playerTwo.PlayerShips[shipIndex - 1].ReceivedHits == playerTwo.PlayerShips[shipIndex - 1].Size)
                        {
                            //Console.WriteLine("Zatopiony\n");
                            playerTwo.PlayerShips[shipIndex - 1].IsSunk = true; ;
                            //Console.WriteLine("Tura gracza 2");
                            whoseTurn = 2;
                        }
                    }
                    else
                    {
                        //Console.WriteLine("Pudło");
                        //Console.WriteLine("\nTura gracza 2");
                        whoseTurn = 2;
                    }
                                           
                    playerTwo.CheckLost();
                }
                else
                {
                    shot = playerTwo.GetShot();
                    playerTwo.PlayerBoards.SaveShot(shot);
                    shipIndex = playerOne.PlayerBoards.CheckFire(shot);
                    if (shipIndex > 0)
                    {
                        //Console.WriteLine(playerTwo.PlayerShips.Count);
                        playerOne.PlayerShips[shipIndex - 1].ReceivedHits++;
                        //Console.WriteLine("Trafiony");
                        if (playerOne.PlayerShips[shipIndex - 1].ReceivedHits == playerOne.PlayerShips[shipIndex - 1].Size)
                        {
                            //Console.WriteLine("Zatopiony\n");
                            playerOne.PlayerShips[shipIndex -1].IsSunk = true;
                            //Console.WriteLine("Tura gracza 1");
                            whoseTurn = 1;
                        }
                    }
                    else
                    {
                        //    Console.WriteLine("Pudło");
                        //    Console.WriteLine("\nTura gracza 1");
                        whoseTurn = 1;
                    }
                    playerOne.CheckLost();
                }
                
            }
            //Console.WriteLine("");
            playerOne.PlayerBoards.PrintBoard();
            
            if (playerOne.Lost)
            {
                Console.WriteLine("Gra zakończona. Wygrał gracz drugi!");
            }
            else
            {
                Console.WriteLine("Gra zakończona. Wygrał gracz pierwszy!");
            }

            
        }
        
    }
}
