using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TD.Models;

namespace TD.Factories
{
    public class ShipFactory
    {
        public List<UnitShip> ShipList { get; }
        private int TotalAmountOfShips = 2;
        private int CurrentAmountOfShips = 1;
        private double SpawnTimer = 0;
        private const double SpawnInterval = 4; 
        private List<UnitShip> KilledBoats = new List<UnitShip>();
        Random Random = new Random();
        public List<List<Vector2>> GetPath()
        {
            List<Vector2> path = new List<Vector2>
            {
                new Vector2(0, 6),
                new Vector2(6, 6),
                new Vector2(6, 1),
                new Vector2(9, 1),
                new Vector2(9, 5),
                new Vector2(12, 5)
            };
            List<Vector2> path2 = new List<Vector2>
            {
                new Vector2(0, 1),
                new Vector2(9, 1),
                new Vector2(9, 6),
                new Vector2(12, 6),
            };
            List<Vector2> path3 = new List<Vector2>
            {
                new Vector2(0, 3),
                new Vector2(3, 3),
                new Vector2(3, 1),
                new Vector2(12, 1),
            };
            List<List<Vector2>> randomPaths = new List<List<Vector2>>
            {
                path,
                path2,
                path3
            };
            return randomPaths;
        }
        public void BlowShip(UnitShip ship)
        {
            KilledBoats.Add(ship);
        }

        public void Clear()
        {
            KilledBoats.Clear();
        }
        public void BlowCheck(int radius, Vector2 shootPos, SoundEffect bomb, ref bool flag, ref int points)
        {
            foreach (var ship in ShipList)
            {
                if ((ship.GetCoords().X - shootPos.X) * (ship.GetCoords().X - shootPos.X) + (ship.GetCoords().Y - shootPos.Y) * (ship.GetCoords().Y - shootPos.Y) <= radius * radius)
                {
                    if (ship.IsDrawned())
                    {
                        bomb.Play();
                        KilledBoats.Add(ship);
                        flag = true;
                        points += 2;
                        break;
                    }
                }
            }
        }
        public List<UnitShip> GetKilled()
        {
            return KilledBoats;
        }
        public ShipFactory()
        {
            var paths = GetPath();
            var path = paths[Random.Next(0, paths.Count)];
            ShipList = new List<UnitShip>
            {
                new UnitShip(1f, path, Globals.TranslateTileToCoords((int)path[0].X, (int)path[0].Y))
            };
        }
        public bool IsLost()
        {
            foreach (var ship in ShipList)
            {
                if (ship.IsShipLoose()) return true;
            }
            return false;
        }
        public bool IsWin()
        {
            return CurrentAmountOfShips == TotalAmountOfShips && ShipList.Count == 0;
        }

        
        public void RemoveUnexistedShips()
        {
            foreach (var ship in KilledBoats)
            {
                ShipList.Remove(ship);
            }
        }

        public void Update(GameTime gameTime)
        {
            Spawn(gameTime);
            
            foreach (UnitShip ship in ShipList)
            {
                ship.Update(gameTime);
            }
        }
        public void Spawn(GameTime gameTime)
        {
            SpawnTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (SpawnTimer > SpawnInterval && CurrentAmountOfShips < TotalAmountOfShips)
            {
                var randVelocity = Random.Next(1, 6);
                var paths = GetPath();
                var path = paths[Random.Next(1, paths.Count)];
                SpawnTimer = 0;
                ShipList.Add(new UnitShip(randVelocity, path, Globals.TranslateTileToCoords((int)path[0].X, (int)path[0].Y)));
                CurrentAmountOfShips++;
            }
        }
    }
}
