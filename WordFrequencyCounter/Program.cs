using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        
        //exception handling
        try
        {
            // Getting Text File Path
            Console.WriteLine("Please enter the path of your TEXT file ");
            string filePath = Console.ReadLine();

            // Call Getting Word Frequency
            Dictionary<string, int> wordFrequencies = GetWordFrequency(filePath);

            //Getting Top N Words
            Console.WriteLine("Please enter the number N:");
            int n;

            //Checking if the N is an integer greater than 0
            while (!int.TryParse(Console.ReadLine(), out n) || n <= 0)
            {
                Console.WriteLine("Invalid input. N should be an integer greater than 0. Please try again:");
            }

            //Printing the result
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Word     Frequency");
            foreach (var entry in GetTopNWords(wordFrequencies, n))
            {
                Console.WriteLine($"{entry.Key,-8} {entry.Value}");
            }
        }
        catch (Exception ex)
        {
            //Showing the error message
            Console.WriteLine("Error : Please check the file path again !   : " + ex.Message);
        }

        Console.ReadLine();
    }

    // Getting Word Frequency
    static Dictionary<string, int> GetWordFrequency(string filePath)
    {
        // Reading the file
        string text = File.ReadAllText(filePath);

        // Splitting the text into words
        string[] words = text.Split(new[] { ' ', '\t', '\n', '\r', ',', '.', ';', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

        Dictionary<string, int> wordFrequencies = new Dictionary<string, int>();

        foreach (string word in words)
        {
            // Converting the word to lowercase
            string lowercaseWord = word.ToLower();
            if (wordFrequencies.ContainsKey(lowercaseWord))
            {
                wordFrequencies[lowercaseWord]++;
            }
            else
            {
                wordFrequencies[lowercaseWord] = 1;
            }
        }

        return wordFrequencies;
    }

    //Getting Top N Words
    static IEnumerable<KeyValuePair<string, int>> GetTopNWords(Dictionary<string, int> wordFrequencies, int n)
    {
        return wordFrequencies
            .OrderByDescending(pair => pair.Value)
            .ThenBy(pair => pair.Key)
            .Take(n);
    }
}
