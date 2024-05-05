using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;

namespace TD.Models
{
    public class UnitShip
    {
        public float velocity { get; set; }
        private int HP;
        public Vector2 DestinationPosition { get; protected set; }
        private List<Vector2> Path;
        public bool MoveDone { get; protected set; } = true;
        public Vector2 currentCoords;
        private int index = 0;
        private int iterations = 0;
        public UnitShip(float velocity, List<Vector2> path, Vector2 startCoords)
        {
            this.velocity = velocity;
            Path = path;
            HP = 10;
            currentCoords = startCoords;
            DestinationPosition = Path[1];
            MoveDone = false;
        }
        public Rectangle GetSprite()
        {
            return new Rectangle(256 * 4, 0, 256, 256);
        }

        public Vector2 GetNextCoords()
        {
            index++;
            if (index >= Path.Count) return Vector2.Zero;
            return Path[index];
        }
        public bool IsDrawned()
        {
            HP--;
            return HP == 0;
        }
        

        public void Draw(GameTime gameTime, UnitShip ship)
        {
            var destination = new Rectangle((int)ship.currentCoords.X, (int)ship.currentCoords.Y, 64, 64);
            Globals.SpriteBatch.Draw(Globals.TextureAtlas, destination, new Rectangle(256 * 4, 0, 256, 256), Color.White);

        }

        public void Update(GameTime gameTime)
        {
            /*var newCoords = new Vector2(currentCoords.X + 0.5f, currentCoords.Y);
            currentCoords = newCoords;
*/          if (MoveDone) return;
            var direction = DestinationPosition - Globals.TranslateCoordsToTile((int)currentCoords.X, (int)currentCoords.Y);
             if (direction != Vector2.Zero) direction.Normalize();

             currentCoords.X += direction.X * 0.5f;
             currentCoords.Y += direction.Y * 0.5f;
            /*var distance = Globals.Time * velocity;
            int iterations = (int)Math.Ceiling(distance / 5);
            distance /= iterations;
            for (int i = 0; i < iterations; i++)
            {
                var newVector = distance * direction;
                currentCoords += newVector;
                if (NearDestination()) return;
            }*/
            if (isTimeToChange())
            {
                index += 1;
                DestinationPosition = Path[index];
            }
            iterations++;
        }
        private bool isTimeToChange()
        {
           return iterations%100==0;
        }
        private bool NearDestination()
        {
            if ((DestinationPosition - currentCoords).Length() < 5)
            {
                currentCoords = DestinationPosition;

                if (index < Path.Count - 1)
                {
                    index++;
                    DestinationPosition = Path[index];
                }
                else
                {
                    MoveDone = true;
                }
                return true;
            }
            return false;
        }
    }
}
