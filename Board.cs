﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship
{
    public class Board
    {
        //private string[,] ShipsBoard { get; set; }
        private int[,] ShipsBoard { get; set; }
        private int[,] FiresBoard { get; set; }
        private int[,] OccupancyBoard { get; set; }

        public Board(int boardSize)
        {
            //ShipsBoard = new string[boardSize, boardSize];
            ShipsBoard = new int[boardSize, boardSize];
            OccupancyBoard = new int[boardSize, boardSize];
            FiresBoard = new int[boardSize, boardSize];
            //for (int i = 0; i < 10; i++)
            //{
            //    for (int j = 0; j < 10; j++)
            //    {
            //        ShipsBoard[i, j] = "0";
            //    }
            //}
        }

        private bool CheckOccupancy(int startRow, int startColumn, bool direction, int shipSize)
        {
            //check occupancy
            if (direction)
            {
                for (int i = 0; i < shipSize; i++)
                {
                    if (OccupancyBoard[startRow, startColumn + i] > 0)
                    {
                        //Console.WriteLine("Statek na kolizyjnej.");
                        return true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < shipSize; i++)
                {
                    if (OccupancyBoard[startRow + i, startColumn] > 0)
                    {
                        //Console.WriteLine("Statek na kolizyjnej.");
                        return true;
                    }
                }
            }
            return false;
        }

        public void PlaceShipsRandomOnBoard(List<Ship> ShipsToPlace)
        {
            Random rnd = new();
            int startColumn, startRow;
            int endColumn, endRow;
            bool direction; // 0 - horizontal, 1 - vertical

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
                    endRow = startRow + shipSize;
                    endColumn = startColumn + shipSize;

                    //check boundaries
                    if (direction && (endColumn) > 10)
                    {
                        //Console.WriteLine("Statek wyjdzie poza mapę.");
                        continue;
                    }
                    else if (!direction && endRow > 10)
                    {
                        //Console.WriteLine("Statek wyjdzie poza mapę.");
                        continue;
                    }

                    if (CheckOccupancy(startRow, startColumn, direction, shipSize))
                        continue;

                    //place ship and occupy fields around it
                    //Console.WriteLine("Wszystko git, umieść statek.");
                    isPlaced = true;
                    if (direction)
                    {
                        for (int i = 0; i < shipSize; i++)
                        {
                            ShipsBoard[startRow, startColumn + i] = ShipsToPlace.IndexOf(ship)+1;
                            OccupancyBoard[startRow, startColumn + i] = 1;

                            //make occupied border around ship - horizontal ships
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
                                if (endColumn < 10)
                                {
                                    OccupancyBoard[startRow, endColumn] = 2;
                                }
                                if (endColumn < 10 && startRow > 0)
                                {
                                    OccupancyBoard[startRow - 1, endColumn] = 2;
                                }
                                if (endColumn < 10 && startRow < 9)
                                {
                                    OccupancyBoard[startRow + 1, endColumn] = 2;
                                }

                                if (startRow > 0 && endColumn - 1 < 10)
                                {
                                    OccupancyBoard[startRow - 1, endColumn - 1] = 2;
                                }
                                if (startRow < 9 && endColumn - 1 < 10)
                                {
                                    OccupancyBoard[startRow + 1, endColumn - 1] = 2;
                                }

                                if (startRow < 9 && endColumn - 1 < 10)
                                {
                                    OccupancyBoard[startRow + 1, endColumn - 1] = 2;
                                }
                                if (startRow > 0 && endColumn - 1 < 10)
                                {
                                    OccupancyBoard[startRow - 1, endColumn - 1] = 2;
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
                        for (int i = 0; i < shipSize; i++)
                        {
                            ShipsBoard[startRow + i, startColumn] = ShipsToPlace.IndexOf(ship)+1;
                            OccupancyBoard[startRow + i, startColumn] = 1;

                            //make occupied border around ship - vertical ships
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
                                if (endRow < 10)
                                {
                                    OccupancyBoard[endRow, startColumn] = 2;
                                }
                                if (startColumn > 0 && endRow < 10)
                                {
                                    OccupancyBoard[endRow, startColumn - 1] = 2;
                                }
                                if (startColumn < 9 && endRow < 10)
                                {
                                    OccupancyBoard[endRow, startColumn + 1] = 2;
                                }

                                if (endRow - 1 < 10 && startColumn > 0)
                                {
                                    OccupancyBoard[endRow - 1, startColumn - 1] = 2;
                                }
                                if (endRow - 1 < 10 && startColumn < 9)
                                {
                                    OccupancyBoard[endRow - 1, startColumn + 1] = 2;
                                }

                                if (endRow - 1 < 10 && startColumn < 9)
                                {
                                    OccupancyBoard[endRow - 1, startColumn + 1] = 2;
                                }
                                if (endRow - 1 < 10 && startColumn > 0)
                                {
                                    OccupancyBoard[endRow - 1, startColumn - 1] = 2;
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
            }
            OccupancyBoard = null;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(ShipsBoard[i, j] + " ");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("");
        }

        public int CheckFire(Coordinates shot)
        {
            if (ShipsBoard[shot.Row, shot.Column] > 0)
            {                
                return ShipsBoard[shot.Row, shot.Column];
            }                
            else
                return 0;
        }

        public void SaveShot(Coordinates shot)
        {
            //if()
            FiresBoard[shot.Row, shot.Column] = 1;
        }

        public void PrintBoard()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(FiresBoard[i, j] + " ");
                }
                Console.WriteLine("");
            }
        }

    }
}
