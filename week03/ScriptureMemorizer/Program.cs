using System;
using System.Collections.Generic;


/*
                                  Exceeding Requirements
  - Added a library of multiple scriptures. Program selects one at random at the start.
  - Added difficulty levels (Easy = 1 word, Medium = 3 words, Hard = 5 words per round).
  - Used Proverbs 3:5–6 as one scripture, in addition to another (John 14:6).
 */
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Scripture Memorizer!");
        Console.WriteLine("Choose a difficulty level: (1) Easy (2) Medium (3) Hard");
        Console.Write("Enter your choice: ");
        int choice = int.Parse(Console.ReadLine() ?? "2");

        int wordsToHide = choice switch
        {
            1 => 1,
            2 => 3,
            3 => 5,
            _ => 3
        };

        List<Scripture> scriptures = new List<Scripture>
        {
            new Scripture(new Reference("Proverbs", 3, 5, 6),
                "Trust in the Lord with all thine heart; and lean not unto thine own understanding. In all thy ways acknowledge him, and he shall direct thy paths."),

            new Scripture(new Reference("John", 14, 6),
                "Jesus saith unto him, I am the way, the truth, and the life: no man cometh unto the Father, but by me.")
        };

        Random rand = new Random();
        Scripture scripture = scriptures[rand.Next(scriptures.Count)];

        while (!scripture.AllHidden())
        {
            Console.Clear();
            scripture.Display();
            Console.WriteLine("\nPress Enter to hide words or type 'quit' to exit.");
            string input = Console.ReadLine();

            if (input?.ToLower() == "quit")
                break;

            scripture.HideRandomWords(wordsToHide);
        }

        Console.Clear();
        scripture.Display();
        Console.WriteLine("\nAll words are hidden. Great job!");
    }
}



class Reference
{
    private string book;
    private int chapter;
    private int verseStart;
    private int verseEnd;

    public Reference(string book, int chapter, int verse)
    {
        this.book = book;
        this.chapter = chapter;
        this.verseStart = verse;
        this.verseEnd = verse;
    }

    public Reference(string book, int chapter, int verseStart, int verseEnd)
    {
        this.book = book;
        this.chapter = chapter;
        this.verseStart = verseStart;
        this.verseEnd = verseEnd;
    }

    public string GetDisplayText()
    {
        if (verseStart == verseEnd)
            return $"{book} {chapter}:{verseStart}";
        else
            return $"{book} {chapter}:{verseStart}-{verseEnd}";
    }
}

class Word
{
    private string text;
    private bool hidden;

    public Word(string text)
    {
        this.text = text;
        this.hidden = false;
    }

    public void Hide()
    {
        hidden = true;
    }

    public bool IsHidden()
    {
        return hidden;
    }

    public string GetDisplayText()
    {
        return hidden ? new string('_', text.Length) : text;
    }
}

class Scripture
{
    private Reference reference;
    private List<Word> words;

    public Scripture(Reference reference, string text)
    {
        this.reference = reference;
        words = new List<Word>();
        foreach (string word in text.Split(' '))
        {
            words.Add(new Word(word));
        }
    }

    public void Display()
    {
        Console.WriteLine(reference.GetDisplayText());
        foreach (Word word in words)
        {
            Console.Write(word.GetDisplayText() + " ");
        }
        Console.WriteLine();
    }

    public void HideRandomWords(int count)
    {
        Random rand = new Random();
        int hidden = 0;
        while (hidden < count && !AllHidden())
        {
            int index = rand.Next(words.Count);
            if (!words[index].IsHidden())
            {
                words[index].Hide();
                hidden++;
            }
        }
    }

    public bool AllHidden()
    {
        foreach (Word word in words)
        {
            if (!word.IsHidden())
                return false;
        }
        return true;
    }
}
