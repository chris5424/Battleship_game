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

        private List<Ship> ShipsToPlace = new List<Ship>();

        private int[,] Board { get; set; }
        private int[,] OccupancyBoard { get; set; }

        public void InitGame()
        {
            Board = new int[10, 10];
            OccupancyBoard = new int[10, 10];
            ShipsToPlace.Add(new Ship { Size = 5, Name = "Carrier" });
            ShipsToPlace.Add(new Ship { Size = 4, Name = "Battleship" });
            ShipsToPlace.Add(new Ship { Size = 3, Name = "Cruiser" });
            ShipsToPlace.Add(new Ship { Size = 3, Name = "Submarine" });
            ShipsToPlace.Add(new Ship { Size = 2, Name = "Destroyer" });
        }

        private bool CheckOccupancy(int startRow, int startColumn, bool direction, int shipSize)
        {

            //check occupancy
            if (direction)
            {
                //endColumn = startColumn + shipSize;
                for (int i = 0; i < shipSize; i++)
                {
                    if (OccupancyBoard[startRow, startColumn + i] > 0)
                    {
                        Console.WriteLine("Statek na kolizyjnej.");
                        return true;
                    }
                }
            }
            else
            {
                //endRow = startRow + shipSize;
                for (int i = 0; i < shipSize; i++)
                {
                    if (OccupancyBoard[startRow + i, startColumn] > 0)
                    {
                        Console.WriteLine("Statek na kolizyjnej.");
                        return true;
                    }
                }
            }
            return false;
        }

        public void PlaceShipsRandomOnBoard()
        {
            Random rnd = new Random();
            var occupiedFields = new int[10, 10];
            int startColumn = -1;
            int endColumn = -1;
            int startRow = -1;
            int endRow = -1;
            bool direction = false; // 0 - horizontal, 1 - vertical

            foreach (var ship in ShipsToPlace)
            {
                int shipSize = ship.Size;
                bool isPlaced = false;

                while (!isPlaced)
                {
                    //draw starting coordinates
                    startColumn = rnd.Next(0, 10);
                    startRow = rnd.Next(0, 10);
                    direction = Convert.ToBoolean(rnd.Next(0, 2));


                    //check boundaries
                    if (direction && (startColumn + shipSize) > 10)
                    {
                        Console.WriteLine("Statek wyjdzie poza mapę.");
                        continue;
                    }
                    else if (!direction && startRow + shipSize > 10)
                    {
                        Console.WriteLine("Statek wyjdzie poza mapę.");
                        continue;
                    }

                    if (CheckOccupancy(startRow, startColumn, direction, shipSize))
                        continue;

                    //place ship and occupy fields around it
                    Console.WriteLine("Wszystko git, umieść statek.");
                    isPlaced = true;
                    if (direction)
                    {
                        endColumn = startColumn + shipSize;
                        for (int i = 0; i < shipSize; i++)
                        {
                            Board[startRow, startColumn + i] = shipSize;
                            OccupancyBoard[startRow, startColumn + i] = 1;

                            //make occupied border around ship
                            if (i == 0)
                            {
                                if (startRow > 0)
                                {
                                    OccupancyBoard[startRow - 1, startColumn] = 2;
                                }
                                if (startColumn > 0)
                                {
                                    OccupancyBoard[startRow, startColumn - 1] = 2;
                                }
                                if (startColumn > 0 && startRow > 0)
                                {
                                    OccupancyBoard[startRow - 1, startColumn - 1] = 2;
                                }
                                if (startRow < 9 && startColumn > 0)
                                {
                                    OccupancyBoard[startRow + 1, startColumn - 1] = 2;
                                }
                                if (startRow < 9)
                                {
                                    OccupancyBoard[startRow + 1, startColumn] = 2;
                                }
                            }
                            else if (i == shipSize - 1)
                            {
                                if (startColumn + shipSize < 10)
                                {
                                    OccupancyBoard[startRow, startColumn + shipSize] = 2;
                                }

                                if (startRow > 0 && startColumn + shipSize - 1 < 10)
                                {
                                    OccupancyBoard[startRow - 1, startColumn + shipSize - 1] = 2;
                                }
                                if (startRow < 9 && startColumn + shipSize - 1 < 10)
                                {
                                    OccupancyBoard[startRow + 1, startColumn + shipSize - 1] = 2;
                                }

                                if (startRow < 9 && startColumn + shipSize - 1 < 10)
                                {
                                    OccupancyBoard[startRow + 1, startColumn + shipSize - 1] = 2;
                                }
                                if (startRow > 0 && startColumn + shipSize - 1< 10)
                                {
                                    OccupancyBoard[startRow - 1, startColumn + shipSize - 1] = 2;
                                }
                            }
                            else
                            {
                                if (startRow > 0)
                                {
                                    OccupancyBoard[startRow - 1, startColumn + i] = 2;
                                }
                                if (startRow < 9)
                                {
                                    OccupancyBoard[startRow + 1, startColumn + i] = 2;
                                }
                            }
                        }
                    }
                    else
                    {
                        endRow = startRow + shipSize;
                        for (int i = 0; i < shipSize; i++)
                        {
                            Board[startRow + i, startColumn] = shipSize;
                            OccupancyBoard[startRow + i, startColumn] = 1;

                            //make occupied border around ship
                            if (i == 0)
                            {
                                if (startColumn > 0)
                                {
                                    OccupancyBoard[startRow, startColumn - 1] = 2;
                                }
                                if (startRow > 0)
                                {
                                    OccupancyBoard[startRow - 1, startColumn] = 2;
                                }
                                if (startColumn > 0 && startRow > 0)
                                {
                                    OccupancyBoard[startRow - 1, startColumn - 1] = 2;
                                }
                                if (startColumn < 9 && startRow > 0)
                                {
                                    OccupancyBoard[startRow - 1, startColumn + 1] = 2;
                                }
                                if (startColumn < 9)
                                {
                                    OccupancyBoard[startRow, startColumn + 1] = 2;
                                }
                            }
                            else if (i == shipSize - 1)
                            {
                                if (startRow + shipSize < 10)
                                {
                                    OccupancyBoard[startRow + shipSize, startColumn] = 2;
                                }
                                

                                if (startRow + shipSize - 1 < 10 && startColumn > 0)
                                {
                                    OccupancyBoard[startRow + shipSize - 1, startColumn - 1] = 2;
                                }
                                if (startRow + shipSize - 1 < 10 && startColumn < 9)
                                {
                                    OccupancyBoard[startRow + shipSize - 1, startColumn + 1] = 2;
                                }

                                if (startRow + shipSize - 1 < 10 && startColumn < 9)
                                {
                                    OccupancyBoard[startRow + shipSize - 1, startColumn + 1] = 2;
                                }
                                if (startRow + shipSize - 1 < 10 && startColumn > 0)
                                {
                                    OccupancyBoard[startRow + shipSize - 1, startColumn - 1] = 2;
                                }
                            }
                            else
                            {
                                if (startColumn > 0)
                                {
                                    OccupancyBoard[startRow + i, startColumn - 1] = 2;
                                }

                                if (startColumn < 9)
                                {
                                    OccupancyBoard[startRow + i, startColumn + 1] = 2;
                                }
                            }
                        }
                    }

                    //show boards
                    //for (int i = 0; i < 10; i++)
                    //{
                    //    for (int j = 0; j < 10; j++)
                    //    {
                    //        Console.Write(Board[i, j] + " ");
                    //    }
                    //    Console.WriteLine("");
                    //}
                    //Console.WriteLine("Occupancy");
                    //for (int i = 0; i < 10; i++)
                    //{
                    //    for (int j = 0; j < 10; j++)
                    //    {
                    //        Console.Write(OccupancyBoard[i, j] + " ");
                    //    }
                    //    Console.WriteLine("");
                    //}
                }
                
            }for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        Console.Write(Board[i, j] + " ");
                    }
                    Console.WriteLine("");
                }
        }
    }
}
