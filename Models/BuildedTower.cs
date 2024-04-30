using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.Models
{
    public class BuildedTower
    {
        public Vector2 Placement { get; }
        public readonly static int ShootRadius = 128; //если в будущем добавится бонусная башня - изменить!


        public BuildedTower(Vector2 placement)
        {
            Placement = placement;
        }

        public bool IsHit(Vector2 shipCoords)
        {
            var x = shipCoords.X;
            var y = shipCoords.Y;
            var circle = (x - Placement.X) * (x - Placement.X) + (y - Placement.Y) * (y - Placement.Y) <= ShootRadius;
            return circle;
        }
    }
}
