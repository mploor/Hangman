using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hangman
{
    public static class Words
    {
        public static string PickRandom()
        {
            string[] words = new string[] {
            "potato", "lipstick", "ostrich", "valium", "intestine", "river", "skyscraper", "spaghetti",
            "tomato", "spaceship", "velocity", "abstract", "pasture", "galloping", "dinner"
            };
            var rnd = new Random();
            return words[rnd.Next(words.Length)];
        }

        public static void DisplayWord(char?[] word)
        {
            foreach (char? letter in word)
            {
                if (letter == null)
                {
                    Console.Write("_ ");
                } else
                {
                    Console.Write(letter.ToString() + " ");
                }
            }
            Console.WriteLine();
            Console.WriteLine();
        }
        
        public static void DisplayGuesses(List<char> letters)
        {
            Console.Write("Bad guesses: ");
            foreach (char letter in letters)
            {
                Console.Write(letter.ToString() + " ");
            }
            Console.WriteLine();
        }

        public static void DrawScaffold(int count)
        {
            string line1 = " -----";
            string line2 = " |   |";
            string line3;
            string line4;
            string line5;
            string line6;
            string line7 = "_|___";
            if (count > 0)
            {
                line3 = " |   O ";
            } else
            {
                line3 = " |";
            }
            if (count == 2)
            {
                line4 = " |   |";
                line5 = " |   | ";
            } else if (count == 3)
            {
                line4 = " |  \\|";
                line5 = " |   | ";
            } else if (count >= 4)
            {
                line4 = " |  \\|/";
                line5 = " |   | ";
            } else
            {
                line4 = " |";
                line5 = " |";
            }
            if (count == 5)
            {
                line6 = " |  /";
            } else if (count == 6)
            {
                line6 = " |  / \\";
            } else
            {
                line6 = " |";
            }
            Console.Clear();
            Console.WriteLine(line1);
            Console.WriteLine(line2);
            Console.WriteLine(line3);
            Console.WriteLine(line4);
            Console.WriteLine(line5);
            Console.WriteLine(line6);
            Console.WriteLine(line7);
            Console.WriteLine();
        }
    }


    public class Program
    {
        public static void Main(string[] args)
        {
            var newGame = true;

            while (newGame)
            {
                // Initialize everything
                string gameWord = Words.PickRandom().ToUpper();
                char?[] guessedWord = new char?[gameWord.Length];
                for (int i = 0; i < guessedWord.Length; i++)
                {
                    guessedWord[i] = null;
                }
                int misses = 0;
                int guessedLetterCount = 0;
                List<char> guessedLetters = new List<char>();
                bool gameOver = false;
                bool validChoice = false;
                string guess;
                string anotherGame;

                Words.DrawScaffold(misses);
                Words.DisplayWord(guessedWord);

                while (!gameOver)
                {
                    //Get player's guess
                    do
                    {
                        Console.WriteLine("Pick a letter");
                        guess = Console.ReadLine();
                        if (guess.Length == 1 || guess == " ")
                        {
                            validChoice = true;
                        }
                        else
                        {
                            validChoice = false;
                            Console.WriteLine("Invalid choice! Enter only one single letter");
                        }
                    } while (!validChoice);

                    // Check if letter is in word, react appropriately
                    if (gameWord.Contains(guess.ToUpper()))
                    {
                        for (int i = 0; i < gameWord.Length; i++)
                        {
                            if (gameWord[i] == guess.ToUpper().ToCharArray()[0])
                            {
                                guessedWord[i] = guess.ToUpper().ToCharArray()[0];
                                guessedLetterCount++;
                            }
                        }
                    }
                    else
                    {
                        guessedLetters.Add(guess.ToUpper().ToCharArray()[0]);
                        misses++;
                    }

                    // Update console with latest move
                    Words.DrawScaffold(misses);
                    Words.DisplayWord(guessedWord);
                    Words.DisplayGuesses(guessedLetters);

                    // Check for win or game over
                    if (guessedLetterCount == gameWord.Length)
                    {
                        gameOver = true;
                        Console.WriteLine("You Win!");
                    }
                    if (misses == 6)
                    {
                        gameOver = true;
                        Console.WriteLine("You Lose!");
                    }
                }
                Console.WriteLine("Play again? (y/n)");
                anotherGame = Console.ReadLine();
                if (anotherGame == "y")
                {
                    newGame = true;
                }
                else
                {
                    newGame = false;
                }
            }
        }
    }
}
