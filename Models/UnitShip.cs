using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using TD.View;

namespace TD.Models
{
    public class UnitShip
    {
        public float velocity { get; set; }
        private int HP;
        public Vector2 DestinationPosition { get; protected set; }
        private List<Vector2> Path;
        public bool MoveDone { get; protected set; } = true;
        private Vector2 currentCoords;
        private int index = 1;
        private bool isHit = false;
        private double hitTimer = 0;
        private const double hitDelay = 1;

        public UnitShip(float velocity, List<Vector2> path, Vector2 startCoords)
        {
            this.velocity = velocity;
            Path = path;
            HP = 3;
            currentCoords = startCoords;
            MoveDone = false;
        }
        public Vector2 GetCoords()
        {
            return currentCoords;
        }

        private Rectangle GetSprite()
        {
            return new Rectangle(256 * 4, 0, 256, 256);
        }

        public bool IsShipLoose()
        {
            for (int y = 0; y < 7; y++)
            {
                if (Vector2.Distance(currentCoords, Globals.TranslateTileToCoords(12, y)) < velocity)
                    return true;
            }
            return false;
        }
        public Vector2 GetNextCoords()
        {
            index++;
            if (index >= Path.Count) return Vector2.Zero;
            return Path[index];
        }

        public bool IsDrawned() {
            if (!isHit)
            {
                HP--;
                isHit = true;
                return HP == 0;
            }
            else
            {
                return false;
            }
        }
        

        public void Update(GameTime gameTime)
        {
            if (isHit)
            {
                hitTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if (hitTimer >= hitDelay)
                {
                    isHit = false;
                    hitTimer = 0;
                }
            }
            if (index < Path.Count)
            {
                var target = Globals.TranslateTileToCoords((int)Path[index].X, (int)Path[index].Y);
                var direction = Vector2.Normalize(target - currentCoords);
                currentCoords += direction * velocity;
                if (Vector2.Distance(currentCoords, target) < velocity && index < Path.Count)
                {
                    currentCoords = target;
                    index++;
                }
            }
        }
    }
}
