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
    public class Ball : Sprite
    {
        private float timer = 0f; // Ökar hastigheten under en tid
        private Vector2? startposition = null;
        private float? hastighet;
        private bool spelar;

        public int hastighetinc = 10; // Hur ofta hastigheten ökar 

        public Ball(Texture2D texture)
          : base(texture)
        {
            Speed = 5f;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            if (startposition == null)
            {
                startposition = Position;
                hastighet = Speed;

                Restart();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space)) //startar spelet 
                spelar = true;

            if (!spelar)
                return;

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer > hastighetinc)
            {
                Speed++;
                timer = 0;
            }

            foreach (var sprite in sprites) //gör så att bollen studsar ifall den träffar bats, tak eller golv
            {
                if (sprite == this)
                    continue;

                if (this.Velocity.X > 0 && this.IsTouchingLeft(sprite))
                    this.Velocity.X = -this.Velocity.X;
                if (this.Velocity.X < 0 && this.IsTouchingRight(sprite))
                    this.Velocity.X = -this.Velocity.X;
                if (this.Velocity.Y > 0 && this.IsTouchingTop(sprite))
                    this.Velocity.Y = -this.Velocity.Y;
                if (this.Velocity.Y < 0 && this.IsTouchingBottom(sprite))
                    this.Velocity.Y = -this.Velocity.Y;
            }

            if (Position.Y <= 0 || Position.Y + _texture.Height >= Game1.hojd)
                Velocity.Y = -Velocity.Y;

            if (Position.X <= 0) //ifall bollen går utanför, starta om
            {
                Restart();
            }

            if (Position.X + _texture.Width >= Game1.bredd)
            {
                Restart();
            }

            Position += Velocity * Speed;
        }

        public void Restart()
        {
            var direction = Game1.Random.Next(0, 4); //vilket håll den ska gå åt först vid start

            switch (direction)
            {
                case 0:
                    Velocity = new Vector2(1, 1);
                    break;
                case 1:
                    Velocity = new Vector2(1, -1);
                    break;
                case 2:
                    Velocity = new Vector2(-1, -1);
                    break;
                case 3:
                    Velocity = new Vector2(-1, 1);
                    break;
            }

            Position = (Vector2)startposition; //restartar spelet med samma inställningar
            Speed = (float)hastighet;
            timer = 0;
            spelar = false;
        }
    }
}
