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

        private int hastighetinc = 10; // Hur ofta hastigheten ökar 

        public Ball(Texture2D texture)
          : base(texture)
        {
            speed = 5f;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            if (startposition == null)
            {
                startposition = position;
                hastighet = speed;

                Restart();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space)) //startar spelet 
                spelar = true;

            if (!spelar)
                return;

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer > hastighetinc)
            {
                speed++;
                timer = 0;
            }

            foreach (var sprite in sprites) //gör så att bollen studsar ifall den träffar bats, tak eller golv
            {
                if (sprite == this)
                    continue;

                if (this.velocity.X > 0 && this.IsTouchingLeft(sprite))
                    this.velocity.X = -this.velocity.X;
                if (this.velocity.X < 0 && this.IsTouchingRight(sprite))
                    this.velocity.X = -this.velocity.X;
                if (this.velocity.Y > 0 && this.IsTouchingTop(sprite))
                    this.velocity.Y = -this.velocity.Y;
                if (this.velocity.Y < 0 && this.IsTouchingBottom(sprite))
                    this.velocity.Y = -this.velocity.Y;
            }

            if (position.Y <= 0 || position.Y + _texture.Height >= Game1.hojd)
                velocity.Y = -velocity.Y;

            if (position.X <= 0) //ifall bollen går utanför, starta om
            {
                Restart();
            }

            if (position.X + _texture.Width >= Game1.bredd)
            {
                Restart();
            }

            position += velocity * speed;
        }

        public void Restart()
        {
            var direction = Game1.Random.Next(0, 4); //vilket håll den ska gå åt först vid start och riktning

            switch (direction)
            {
                case 0:
                    velocity = new Vector2(1, 1);
                    break;
                case 1:
                    velocity = new Vector2(1, -1);
                    break;
                case 2:
                    velocity = new Vector2(-1, -1);
                    break;
                case 3:
                    velocity = new Vector2(-1, 1);
                    break;
            }

            position = (Vector2)startposition; //restartar spelet med samma inställningar
            speed = (float)hastighet;
            timer = 0;
            spelar = false;
        }
    }
}
