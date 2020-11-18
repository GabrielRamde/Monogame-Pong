using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Template
{
    public class Bat : Sprite
    {
        public Bat(Texture2D texture) 
          : base(texture)
        {
            speed = 10f; //hastighet på bats
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites) //röra på bats
        {
            if (Keyboard.GetState().IsKeyDown(Input.Up))
                velocity.Y = -speed;
            else if (Keyboard.GetState().IsKeyDown(Input.Down))
                velocity.Y = speed;

            position += velocity;

            position.Y = MathHelper.Clamp(position.Y, 0, Game1.hojd - _texture.Height); //så dem stannar vid kanterna på skärmen

            velocity = Vector2.Zero;
        }
    }
}