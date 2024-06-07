using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace TD.View
{
    public static class ConstantsView
    {
        public const int ShipPictureSize = 64;
        public const int BlockSize = 256;
        public static Rectangle shipAsset = new Rectangle(256* 4, 0, 256, 256);
        public static Rectangle towerAsset = new Rectangle(256 * 2, 0, 256, 256);
        public static Rectangle fireAsset = new Rectangle(256 * 6, 0, 256, 256);
    }
}
