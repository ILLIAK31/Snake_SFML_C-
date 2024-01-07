//using System;
//using System.Collections.Generic;
//using SFML.Window;
//using SFML.Graphics;
//using SFML.System;
//using System.Xml.Linq;
//using System.Linq;
//using System.Numerics;

//namespace GameSnakeSFML
//{
//    class Program
//    {
//        static RenderWindow window;
//        static Clock clock;
//        static Snake snake;
//        static Food food;
//        static int score;
//        static Font font = new Font("C:\\Windows\\Fonts\\impact.ttf");
//        static Texture backgroundTexture;
//        static Sprite backgroundSprite;
//        static bool game_status = false;
//        public static void Start()
//        {
//            RenderWindow window = new RenderWindow(new VideoMode(800, 600), "Menu");
//            Texture backgroundTexture;
//            Sprite backgroundSprite;
//            backgroundTexture = new Texture("C:\\Games\\2.jpg");
//            backgroundSprite = new Sprite(backgroundTexture);
//            backgroundSprite.Scale = new Vector2f(0.5f, 0.58f);
//            backgroundSprite.Position = new Vector2f(0, 0);
//            Font font = new Font("C:\\Windows\\Fonts\\impact.ttf"); // Replace with the path to your font file
//            Text title = new Text("Menu", font, 40);
//            title.Position = new Vector2f(300, 50);

//            Text playOption = new Text("Play", font, 30);
//            playOption.Position = new Vector2f(350, 200);

//            Text exitOption = new Text("Exit", font, 30);
//            exitOption.Position = new Vector2f(350, 300);

//            int selectedOption = 0; // Index of the currently selected option
//            while (window.IsOpen)
//            {
//                window.DispatchEvents();

//                window.Clear(Color.White);

//                Draw the menu items
//                window.Draw(backgroundSprite);
//                window.Draw(title);
//                window.Draw(playOption);
//                window.Draw(exitOption);


//                Highlight the selected option
//                if (selectedOption == 0)
//                    playOption.FillColor = Color.Red;
//                else
//                    playOption.FillColor = Color.Black;

//                if (selectedOption == 1)
//                    exitOption.FillColor = Color.Red;
//                else
//                    exitOption.FillColor = Color.Black;

//                window.Display();

//                Check for keyboard input
//                if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
//                    {
//                        selectedOption = 0;
//                    }
//                    else if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
//                    {
//                        selectedOption = 1;
//                    }
//                    else if (Keyboard.IsKeyPressed(Keyboard.Key.Return))
//                    {
//                        Perform action based on the selected option
//                        if (selectedOption == 0)
//                        {
//                            game_status = true;
//                            break;
//                            Add your game logic here
//                    }
//                        else if (selectedOption == 1)
//                        {
//                            game_status = false;
//                            window.Close();
//                        }
//                    }
//            }
//        }
//        static void Main()
//        {
//            Start();
//            if (game_status)
//            {
//                window = new RenderWindow(new VideoMode(800, 600), "Snake");
//                window.Closed += (_, __) => window.Close();
//                backgroundTexture = new Texture("C:\\Games\\1.jpg");
//                backgroundSprite = new Sprite(backgroundTexture);
//                backgroundSprite.Scale = new Vector2f((float)window.Size.X / backgroundTexture.Size.X, 0.9f);
//                backgroundSprite.Position = new Vector2f(0, 50);
//                clock = new Clock();
//                snake = new Snake();
//                food = new Food(); // Using the modified Food class
//                score = 0;

//                while (window.IsOpen)
//                {
//                    window.DispatchEvents();
//                    Update();
//                    Render();
//                }
//            }
//        }

//        static void Update()
//        {
//            float deltaTime = clock.Restart().AsSeconds();
//            snake.Update(deltaTime);

//            if (snake.CollidesWithItself() || snake.CollidesWithWall())
//            {
//                window.Close();
//            }

//            if (snake.CollidesWithFood(food))
//            {
//                snake.Eat();
//                food.Respawn();
//                score++;
//            }
//        }

//        static void Render()
//        {
//            window.Clear();
//            window.Draw(backgroundSprite);
//            snake.Render(window);
//            food.Render(window);

//            Text scoreText = new Text($"Score: {score}", font, 20);
//            scoreText.Position = new Vector2f(10, 10);
//            scoreText.CharacterSize = 20;
//            window.Draw(scoreText);

//            window.Display();
//        }
//    }

//    class Snake
//    {
//        private const float SnakeSpeed = 200.0f;
//        private const float SnakeSize = 20.0f;

//        private readonly CircleShape snakeBody;
//        private Vector2f direction;
//        private Vector2f position;
//        private List<Vector2f> bodyParts;

//        public Snake()
//        {
//            snakeBody = new CircleShape(SnakeSize)
//            {
//                FillColor = Color.Red
//            };

//            direction = new Vector2f(1.0f, 0.0f);
//            position = new Vector2f(400, 300); // Initial position

//            bodyParts = new List<Vector2f>();
//            bodyParts.Add(position);
//        }

//        public void Update(float deltaTime)
//        {
//            if (Keyboard.IsKeyPressed(Keyboard.Key.Up) && direction.Y == 0.0f)
//                direction = new Vector2f(0.0f, -1.0f);
//            else if (Keyboard.IsKeyPressed(Keyboard.Key.Down) && direction.Y == 0.0f)
//                direction = new Vector2f(0.0f, 1.0f);
//            else if (Keyboard.IsKeyPressed(Keyboard.Key.Left) && direction.X == 0.0f)
//                direction = new Vector2f(-1.0f, 0.0f);
//            else if (Keyboard.IsKeyPressed(Keyboard.Key.Right) && direction.X == 0.0f)
//                direction = new Vector2f(1.0f, 0.0f);

//            position += direction * SnakeSpeed * deltaTime;

//            for (int i = bodyParts.Count - 1; i > 0; i--)
//            {
//                bodyParts[i] = bodyParts[i - 1];
//            }

//            bodyParts[0] = position;
//            snakeBody.Position = position;
//        }

//        public void Render(RenderWindow window)
//        {
//            window.Draw(snakeBody);

//            foreach (var bodyPart in bodyParts)
//            {
//                CircleShape bodyPartShape = new CircleShape(SnakeSize)
//                {
//                    FillColor = Color.Red
//                    Position = bodyPart
//                };
//                window.Draw(bodyPartShape);
//            }
//        }

//        public bool CollidesWithItself()
//        {
//            for (int i = 1; i < bodyParts.Count; i++)
//            {
//                if (bodyParts[0] == bodyParts[i])
//                    return true;
//            }
//            return false;
//        }

//        public bool CollidesWithWall()
//        {
//            return (position.X < 0 || position.X >= 760 || position.Y < 50 || position.Y >= 560);
//        }

//        public bool CollidesWithFood(Food food)
//        {
//            return snakeBody.GetGlobalBounds().Intersects(food.GetBounds());
//        }

//        public void Eat()
//        {
//            Vector2f tailPosition = bodyParts[bodyParts.Count - 1];
//            Vector2f newBodyPart = new Vector2f(tailPosition.X, tailPosition.Y);
//            bodyParts.Add(newBodyPart);
//        }

//        public void Reset()
//        {
//            position = new Vector2f(400, 300);
//            direction = new Vector2f(1.0f, 0.0f);
//            bodyParts.Clear();
//            bodyParts.Add(position);
//        }
//    }

//    class Food
//    {
//        private const float FoodSize = 20.0f;
//        private Sprite foodSprite;
//        private Texture foodTexture;
//        private Vector2f position;

//        public Food()
//        {
//            foodTexture = new Texture(@"C:\Games\3.png");
//            foodSprite = new Sprite(foodTexture)
//            {
//                Scale = new Vector2f(0.15f, 0.15f),
//                Origin = new Vector2f(foodTexture.Size.X / 2, foodTexture.Size.Y / 2),
//            };

//            Respawn();
//        }

//        public void Respawn()
//        {
//            Random rand = new Random();
//            position = new Vector2f(rand.Next(0, 39) * FoodSize, rand.Next(5, 29) * FoodSize);
//            foodSprite.Position = position;
//        }

//        public FloatRect GetBounds()
//        {
//            return foodSprite.GetGlobalBounds();
//        }

//        public void Render(RenderWindow window)
//        {
//            window.Draw(foodSprite);
//        }
//    }
//}

using System;
using System.Collections.Generic;
using System.IO;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using static SFML.Window.Mouse;

class Snake
{
    static public int B_Score = 0;
    static public bool Win_status = false;
    static public bool Lose_status = false;
    private RenderWindow window;
    private Texture Background_Texture = new Texture(@"C:\Games\1.jpg");
    private Sprite Background_Sprite = new Sprite();
    private Texture Fruit_Texture = new Texture(@"C:\Games\3.png");
    private Sprite Fruit_Sprite = new Sprite();
    private int X, Y, Score;
    private List<Vector2i> snake = new List<Vector2i>();
    private Vector2i food = new Vector2i();
    static Font font = new Font("C:\\Windows\\Fonts\\impact.ttf");
    private RectangleShape progressBar;
    private float progressBarWidth;
    public Snake()
    {
        window = new RenderWindow(new VideoMode(800, 600), "Snake");
        Background_Sprite.Texture = Background_Texture;
        Background_Sprite.Scale = new Vector2f((float)window.Size.X / Background_Texture.Size.X, 0.9f);
        Background_Sprite.Position = new Vector2f(0, 60);
        Fruit_Sprite.Texture = Fruit_Texture;
        float Fruit_Scale = 0.09f;
        Fruit_Sprite.Scale = new Vector2f(Fruit_Scale, Fruit_Scale);
        window.SetFramerateLimit(9);
        Vector2i head = new Vector2i(10, 10);
        snake.Add(head);
        Spawn_Food();
        progressBar = new RectangleShape(new Vector2f(0, 20));
        progressBar.FillColor = Color.Red;
        progressBar.Position = new Vector2f(400, 20);
        progressBarWidth = 0;
    }
    public void Run()
    {
        Clock clock = new Clock();
        while (window.IsOpen)
        {
            if (Snake.Win_status)
            {
                while (true)
                {
                    window.Clear();
                    Text winText = new Text($"WIN", font, 100)
                    {
                        Position = new Vector2f(300, 200)
                    };
                    winText.FillColor = Color.Yellow;
                    window.Draw(winText);
                    if (Score > B_Score)
                        B_Score = Score;
                    Text winText2 = new Text($"Best Score {B_Score}\n Press Enter or Esc", font, 30)
                    {
                        Position = new Vector2f(300, 370)
                    };
                    winText2.FillColor = Color.White;
                    window.Draw(winText2);
                    window.Display();
                    window.DispatchEvents();
                    if (Keyboard.IsKeyPressed(Keyboard.Key.Enter))
                    {
                        Score = 0;
                        progressBarWidth = 0;
                        progressBar.FillColor = Color.Red;
                        Win_status = false;
                        snake.Clear();
                        Vector2i head = new Vector2i(10, 10);
                        snake.Add(head);
                        break;
                    }
                    else if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                    {
                        Program.game_status = false;
                        window.Close();
                    }
                }
            }
            float deltaTime = clock.Restart().AsSeconds();
            Process();
            Update(deltaTime);
            if (Snake.Lose_status)
            {
                while (true)
                {
                    window.Clear();
                    Text winText = new Text($"Lose", font, 100)
                    {
                        Position = new Vector2f(300, 200)
                    };
                    winText.FillColor = Color.Red;
                    window.Draw(winText);
                    if (Score > B_Score)
                        B_Score = Score;
                    Text winText2 = new Text($"Best Score {B_Score}\n Press Enter or Esc", font, 30)
                    {
                        Position = new Vector2f(300, 370)
                    };
                    winText2.FillColor = Color.White;
                    window.Draw(winText2);
                    window.Display();
                    window.DispatchEvents();
                    if (Keyboard.IsKeyPressed(Keyboard.Key.Enter))
                    {
                        Score = 0;
                        progressBarWidth = 0;
                        progressBar.FillColor = Color.Red;
                        Lose_status = false;
                        snake.Clear();
                        Vector2i head = new Vector2i(10, 10);
                        snake.Add(head);
                        break;
                    }
                    else if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                    {
                        Program.game_status = false;
                        window.Close();
                    }
                }
            }
            Render();
        }
    }
    private void Spawn_Food()
    {
        Random mt_engine = new Random();
        do
        {
            food.X = mt_engine.Next(0, 20);
            food.Y = mt_engine.Next(5, 20);
        } while (Food_Position(food.X, food.Y));
        Fruit_Sprite.Position = new Vector2f(food.X * 20, food.Y * 20);
    }
    private void Process()
    {
        window.DispatchEvents();
        if (Keyboard.IsKeyPressed(Keyboard.Key.Left) && X != 1)
        {
            X = -1;
            Y = 0;
        }
        else if (Keyboard.IsKeyPressed(Keyboard.Key.Right) && X != -1)
        {
            X = 1;
            Y = 0;
        }
        else if (Keyboard.IsKeyPressed(Keyboard.Key.Up) && Y != 1)
        {
            X = 0;
            Y = -1;
        }
        else if (Keyboard.IsKeyPressed(Keyboard.Key.Down) && Y != -1)
        {
            X = 0;
            Y = 1;
        }
    }
    private void Update(float deltaTime)
    {
        int newX = snake[0].X + X;
        int newY = snake[0].Y + Y;
        if (newX < 0 || newX >= 40 || newY < 3 || newY >= 30 || Check_Collision())
        {
            Lose_status = true;
        }
        else
        {
            Vector2i newHead = new Vector2i(newX, newY);
            snake.Insert(0, newHead);
            if (newX == food.X && newY == food.Y)
            {
                Score += 10;
                progressBarWidth += 10;
                // Check if the score reaches 200
                if (Score >= 50)
                {
                    Score = 50;
                    progressBarWidth = 200;
                    progressBar.FillColor = Color.Green;
                    Win_status = true;
                }
                else
                {
                    Spawn_Food();
                }
            }
            else
            {
                snake.RemoveAt(snake.Count - 1);
            }
        }
    }
    private void Render()
    {
        window.Clear();
        window.Draw(Background_Sprite);
        window.Draw(Fruit_Sprite);
        CircleShape Body_Segment = new CircleShape(10)
        {
            FillColor = Color.Red
        };
        foreach (var segment in snake)
        {
            Body_Segment.Position = new Vector2f(segment.X * 20, segment.Y * 20);
            window.Draw(Body_Segment);
        }
        progressBar.Size = new Vector2f(progressBarWidth, 20);
        window.Draw(progressBar);
        Text scoreText = new Text($"Score: {Score}", font, 20);
        scoreText.Position = new Vector2f(10, 20);
        scoreText.CharacterSize = 20;
        window.Draw(scoreText);
        Text scoreText2 = new Text($"{Score}/200", font, 20);
        scoreText2.Position = new Vector2f(300, 20);
        scoreText2.CharacterSize = 20;
        window.Draw(scoreText2);
        window.Display();
    }
    private bool Check_Collision()
    {
        for (int i = 1; i < snake.Count; i++)
        {
            if (snake[i].X == snake[0].X && snake[i].Y == snake[0].Y)
                return true;
        }
        return false;
    }
    private bool Food_Position(int x, int y)
    {
        foreach (var segment in snake)
        {
            if (segment.X == x && segment.Y == y)
                return true;
        }
        return false;
    }
    private void Restart()
    {
        Score = 0;
        progressBarWidth = 0;
        Win_status = false;
        snake.Clear();
    }
}

class Program
{
    static RenderWindow window;
    static Clock clock;
    static int score;
    static Font font = new Font("C:\\Windows\\Fonts\\impact.ttf");
    static Texture backgroundTexture;
    static Sprite backgroundSprite;
    static public bool game_status = false;
    public static void Start()
    {
        RenderWindow window = new RenderWindow(new VideoMode(800, 600), "Menu");
        Texture backgroundTexture;
        Sprite backgroundSprite;
        backgroundTexture = new Texture("C:\\Games\\2.jpg");
        backgroundSprite = new Sprite(backgroundTexture);
        backgroundSprite.Scale = new Vector2f(0.5f, 0.58f);
        backgroundSprite.Position = new Vector2f(0, 0);
        Font font = new Font("C:\\Windows\\Fonts\\impact.ttf");
        Text title = new Text("Menu", font, 40);
        title.Position = new Vector2f(300, 50);
        Text playOption = new Text("Play", font, 30);
        playOption.Position = new Vector2f(350, 200);
        Text exitOption = new Text("Exit", font, 30);
        exitOption.Position = new Vector2f(350, 300);
        int selectedOption = 0; 
        while (window.IsOpen)
        {
            window.DispatchEvents();
            window.Clear(Color.White);
            window.Draw(backgroundSprite);
            window.Draw(title);
            window.Draw(playOption);
            window.Draw(exitOption);
            if (selectedOption == 0)
                playOption.FillColor = Color.Red;
            else
                playOption.FillColor = Color.Black;

            if (selectedOption == 1)
                exitOption.FillColor = Color.Red;
            else
                exitOption.FillColor = Color.Black;
            window.Display();
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
                if (selectedOption == 0)
                {
                    game_status = true;
                    window.Close();
                    break;
                }
                else if (selectedOption == 1)
                {
                    game_status = false;
                    window.Close();
                }
            }
        }
    }
    static void Main()
    {
        Start();
        if (game_status)
        {
            Snake snakeGame = new Snake();
            snakeGame.Run();
        }
    }
}