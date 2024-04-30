using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace TD.Models
{
    public class UnitShip
    {
        public float velocity { get; set; }
        private int HP;
        private List<Vector2> Path;
        public Vector2 currentCoords;
        private int index = 0;
        public UnitShip(float velocity, List<Vector2> path, Vector2 startCoords)
        {
            this.velocity = velocity;
            Path = path;
            HP = 10;
            currentCoords = startCoords;
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
            var newCoords = new Vector2(currentCoords.X + 0.5f, currentCoords.Y);
            currentCoords = newCoords;
        }
    }
}
