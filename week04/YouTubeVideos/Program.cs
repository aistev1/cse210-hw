using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        var video1 = new Video("Learning C#", "Stephen Isiko", 300);
        var video2 = new Video("Intro to OOP", "Mukisa Derick", 450);
        var video3 = new Video("Advanced C# Techniques", "Lydia Isiko", 600);

        video1.AddComment(new Comment("Mike", "Great video!"));
        video1.AddComment(new Comment("Sara", "Very helpful, thanks!"));
        video1.AddComment(new Comment("Tom", "I learned a lot!"));

        video2.AddComment(new Comment("Anna", "Clear explanation."));
        video2.AddComment(new Comment("Ben", "Nice examples."));
        video2.AddComment(new Comment("Lucy", "Thanks for sharing."));

        video3.AddComment(new Comment("Sam", "Excellent content."));
        video3.AddComment(new Comment("Kate", "Well structured!"));
        video3.AddComment(new Comment("Leo", "Learned some new tips."));

        List<Video> videos = new List<Video> { video1, video2, video3 };

        foreach (var video in videos)
        {
            video.DisplayVideoInfo();
        }
    }
}
