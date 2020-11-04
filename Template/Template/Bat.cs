using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Tutorial010.Models;

namespace Template
{
    public class Bat : Sprite
    {
        public Bat(Texture2D texture) 
          : base(texture)
        {
            Speed = 10f; //hastighet på bats
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites) //röra på bats
        {
            if (Keyboard.GetState().IsKeyDown(Input.Up))
                Velocity.Y = -Speed;
            else if (Keyboard.GetState().IsKeyDown(Input.Down))
                Velocity.Y = Speed;

            Position += Velocity;

            Position.Y = MathHelper.Clamp(Position.Y, 0, Game1.hojd - _texture.Height); //så dem stannar vid kanterna på skärmen

            Velocity = Vector2.Zero;
        }
    }
}