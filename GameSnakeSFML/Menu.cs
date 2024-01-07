using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
using System.Xml.Linq;
using System.Numerics;

namespace GameSnakeSFML
{
    internal class Menu
    {
        public static void Start(bool game_status)
        {
            RenderWindow window = new RenderWindow(new VideoMode(800, 600), "SFML Menu Example");
            Texture backgroundTexture;
            Sprite backgroundSprite;
            backgroundTexture = new Texture("C:\\Games\\2.jpg");
            backgroundSprite = new Sprite(backgroundTexture);
            backgroundSprite.Scale = new Vector2f(0.5f,0.58f);
            backgroundSprite.Position = new Vector2f(0,0);
            Font font = new Font("C:\\Windows\\Fonts\\impact.ttf"); // Replace with the path to your font file
            Text title = new Text("Main Menu", font, 40);
            title.Position = new Vector2f(300, 50);

            Text playOption = new Text("Play", font, 30);
            playOption.Position = new Vector2f(350, 200);

            Text exitOption = new Text("Exit", font, 30);
            exitOption.Position = new Vector2f(350, 300);

            int selectedOption = 0; // Index of the currently selected option
            while (window.IsOpen)
            {
                window.DispatchEvents();

                window.Clear(Color.White);

                // Draw the menu items
                window.Draw(backgroundSprite);
                window.Draw(title);
                window.Draw(playOption);
                window.Draw(exitOption);
                

                // Highlight the selected option
                if (selectedOption == 0)
                    playOption.FillColor = Color.Red;
                else
                    playOption.FillColor = Color.Black;

                if (selectedOption == 1)
                    exitOption.FillColor = Color.Red;
                else
                    exitOption.FillColor = Color.Black;

                window.Display();

                // Check for keyboard input
                if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
                {
                    selectedOption = 0;
                }
                else if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
                {
                    selectedOption = 1;
                }
                else if (Keyboard.IsKeyPressed(Keyboard.Key.Return))
                {
                    // Perform action based on the selected option
                    if (selectedOption == 0)
                    {
                        //game_status = true;
                        break;
                        // Add your game logic here
                    }
                    else if (selectedOption == 1)
                    {
                        //game_status = false;
                        window.Close();
                    }
                }
            }
        }
    }
}
