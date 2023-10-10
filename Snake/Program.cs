using System.ComponentModel;
using System.Xml.Serialization;

namespace Snake
{
    internal class Program
    {
        //Так вроде делать неправильно, но по другому я еще не умею -_-
        static int foodX;
        static int foodY;
        static int snakeLen = 10;
        static int[] body_x = new int[100];
        static int[] body_y = new int[100];

        static void SpamFood()
        {
            bool isOnSnake;
            do
            {
                isOnSnake = false;
                Random rndFood = new Random();

                foodX = rndFood.Next(4, 116);
                if (foodX % 2 != 0) foodX += 1;

                foodY = rndFood.Next(5, 37);

                //Проверка на спавн еды внутри тела змейки
                for (int i = 0; i < snakeLen; i++)
                {
                    if (foodX == body_x[i] && foodY == body_y[i])
                    {
                        isOnSnake = true;
                        break;
                    }
                }
            }
            while (isOnSnake);
        }

        //Отрисовка игрового поля
        static void DrawField()
        {
            for (int x = 2; x <= 118; x += 116)
            {
                for (int y = 4; y <= 38; y++)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.SetCursorPosition(x, y);
                    Console.Write("██");
                }
            }

            for (int y = 4; y <= 38; y += 34)
            {
                for (int x = 2; x <= 118; x++)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.SetCursorPosition(x, y);
                    Console.Write("█");
                }
            }
        }

        static void Main(string[] args)
        {
            Console.SetWindowSize(120, 40);
            Console.SetBufferSize(120, 40);
            Console.CursorVisible = false;

            //Рандомизация спавна змейки при запуске
            Random rndHead = new Random();

            int head_x = rndHead.Next(4, 116);
            if (head_x % 2 != 0) head_x += 1;
            int head_y = rndHead.Next(5, 37);
            int dir = 0;
            int score = snakeLen - 10;
            int scoreRecord = score;
            bool isGame = true;

            for (int i = 0; i < snakeLen; i++)
            {
                body_x[i] = head_x - (i * 2);
                body_y[i] = head_y;
            }

            SpamFood();

            while(isGame == true)
            {
                Console.SetCursorPosition(head_x, head_y);
                Console.Write("  ");

                Console.SetCursorPosition(foodX, foodY);
                Console.Write("  ");

                for (int i = 0; i < snakeLen; i++)
                {
                    Console.SetCursorPosition(body_x[i], body_y[i]);
                    Console.Write("  ");
                }

                if (Console.KeyAvailable == true)
                {
                    ConsoleKeyInfo key;
                    Console.SetCursorPosition(0, 0);
                    key = Console.ReadKey();
                    Console.SetCursorPosition(0, 0);
                    Console.Write("  ");

                    if (key.Key == ConsoleKey.D && dir != 2) dir = 0;
                    if (key.Key == ConsoleKey.S && dir != 3) dir = 1;
                    if (key.Key == ConsoleKey.A && dir != 0) dir = 2;
                    if (key.Key == ConsoleKey.W && dir != 1) dir = 3;
                }

                if (dir == 0) head_x += 2;
                if (dir == 1) head_y += 1;
                if (dir == 2) head_x -= 2;
                if (dir == 3) head_y -= 1;

                if (head_x < 4) head_x = 116;
                if (head_x > 116) head_x = 4;
                if (head_y < 5) head_y = 37;
                if (head_y > 37) head_y = 5;

                for (int i = snakeLen; i > 0; i--)
                {
                    body_x[i] = body_x[i - 1];
                    body_y[i] = body_y[i - 1];
                }

                body_x[0] = head_x;
                body_y[0] = head_y;

                for (int i = 1; i < snakeLen; i++)
                {
                    if (body_x[i] == head_x && body_y[i] == head_y)
                    {
                        isGame = false;
                    }
                }

                if (head_x == foodX && head_y == foodY)
                {
                    SpamFood();
                    snakeLen++;
                    score++;
                }

                

                for (int i = 0; i < snakeLen; i++)
                {
                    Console.SetCursorPosition(body_x[i], body_y[i]);
                    Console.Write("██");
                }
                
                //Перезапись рекорда
                if (scoreRecord > score)
                {
                    scoreRecord = scoreRecord;
                }
                else
                {
                    scoreRecord = score;
                }

                DrawField();

                //Отображение счета
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(60, 2);
                Console.Write("Счет : {0}", score);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(foodX, foodY);
                Console.Write("██");

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.SetCursorPosition(head_x, head_y);
                Console.Write("██");

                System.Threading.Thread.Sleep(50);

                //Перезапуск игры
                if (!isGame)
                {
                    Console.SetCursorPosition(24, 20);
                    Console.Write("Игра окончена. Ваш рекорд на текущий сеанс: {0}. Нажмите R для перезапуска.", scoreRecord) ;
                    while (true)
                    {
                        if (Console.KeyAvailable)
                        {
                            ConsoleKeyInfo key = Console.ReadKey(true);
                            if (key.Key == ConsoleKey.R)
                            {
                                isGame = true;
                                head_x = rndHead.Next(4, 116);
                                if (head_x % 2 != 0) head_x += 1;
                                head_y = rndHead.Next(5, 37);
                                snakeLen = 10;
                                score = snakeLen - 10;

                                for (int i = 0; i < body_x.Length; i++)
                                {
                                    body_x[i] = 0;
                                    body_y[i] = 0;
                                }

                                Console.Clear();

                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}