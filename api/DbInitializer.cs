using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

public static class DbInitializer
{
    public static void Initialize(BookDb context)
    {
        // Look for any books.
        if (context.Books.Any())
        {
            return;   // DB has been seeded
        }

        var books = new Book[]
        {
            new Book { Id = 1, Title = "20000 Lines of C", Author = "John Doe", Image = "20000-lines-of-c.png", Published = 2020, Synopsis = "A deep dive into the world of C programming, exploring 20000 lines of code.", Price = 29.99 },
            new Book { Id = 2, Title = "Arrays in the Sun", Author = "Jane Smith", Image = "arrays-in-the-sun.png", Published = 2019, Synopsis = "A thrilling adventure of data structures basking in the sun.", Price = 24.99 },
            new Book { Id = 3, Title = "Binary New World", Author = "Alice Johnson", Image = "binary-new-world.png", Published = 2018, Synopsis = "Discover a new world where everything is binary.", Price = 19.99 },
            new Book { Id = 4, Title = "Bit and the Endian", Author = "Bob Brown", Image = "bit-and-the-endian.png", Published = 2017, Synopsis = "Follow Bit on a journey to understand endianness in computing.", Price = 22.99 },
            new Book { Id = 5, Title = "Brief History of Datetime", Author = "Charlie Davis", Image = "brief-history-of-datetime.png", Published = 2016, Synopsis = "A historical account of the evolution of datetime in programming.", Price = 18.99 },
            new Book { Id = 6, Title = "Catch 10110", Author = "David Evans", Image = "catch-10110.png", Published = 2015, Synopsis = "A satirical novel about the absurdities of binary logic.", Price = 21.99 },
            new Book { Id = 7, Title = "Do Androids Dream of Google", Author = "Eve Foster", Image = "do-androids-dream-of-google.png", Published = 2014, Synopsis = "Explore the dreams of androids in a world dominated by Google.", Price = 25.99 },
            new Book { Id = 8, Title = "Fear and Debugging in Las Vegas", Author = "Frank Green", Image = "fear-and-debugging-in-las-vegas.png", Published = 2013, Synopsis = "A wild ride through the debugging process in the city of Las Vegas.", Price = 23.99 },
            new Book { Id = 9, Title = "Infinite Loop", Author = "Grace Harris", Image = "infinite-loop.png", Published = 2012, Synopsis = "A programmer's nightmare of being stuck in an infinite loop.", Price = 17.99 },
            new Book { Id = 10, Title = "Jane Error", Author = "Hank Irving", Image = "jane-error.png", Published = 2011, Synopsis = "The story of Jane and her quest to fix a mysterious error.", Price = 20.99 },
            new Book { Id = 11, Title = "Charlie and the Object Factory", Author = "Ivy Jackson", Image = "kids-charlie-and-the-object-factory.png", Published = 2010, Synopsis = "Charlie explores the magical world of object-oriented programming.", Price = 16.99 },
            new Book { Id = 12, Title = "Charlotte's Website", Author = "Jack King", Image = "kids-charlottes-website.png", Published = 2009, Synopsis = "Charlotte weaves a beautiful website to save her friend.", Price = 15.99 },
            new Book { Id = 13, Title = "Green Eggs and Haml", Author = "Karen Lee", Image = "kids-green-eggs-and-haml.png", Published = 2008, Synopsis = "A fun tale about learning the HAML templating language.", Price = 14.99 },
            new Book { Id = 14, Title = "James and the Giant Breach", Author = "Larry Miller", Image = "kids-james-and-the-giant-breach.png", Published = 2007, Synopsis = "James discovers a giant security breach and works to fix it.", Price = 19.99 },
            new Book { Id = 15, Title = "One Fish Two Fish", Author = "Mona Nelson", Image = "kids-one-fish-two-fish.png", Published = 2006, Synopsis = "A counting adventure with fish in a programming world.", Price = 13.99 },
            new Book { Id = 16, Title = "Wrinkle in Datetime", Author = "Nina Owens", Image = "kids-wrinkle-in-datetime.png", Published = 2005, Synopsis = "A journey through time and space with datetime functions.", Price = 18.99 },
            new Book { Id = 17, Title = "Lord of the Files", Author = "Oscar Perez", Image = "lord-of-the-files.png", Published = 2004, Synopsis = "A gripping tale of survival in a world of corrupted files.", Price = 22.99 },
            new Book { Id = 18, Title = "Me Code Pretty One Day", Author = "Paula Quinn", Image = "me-code-pretty-one-day.png", Published = 2003, Synopsis = "A humorous memoir of a programmer's journey to write beautiful code.", Price = 21.99 },
            new Book { Id = 19, Title = "Pride and Polymorphism", Author = "Quincy Roberts", Image = "pride-and-polymorphism.png", Published = 2002, Synopsis = "A classic love story set in the world of object-oriented programming.", Price = 24.99 },
            new Book { Id = 20, Title = "Site Runner", Author = "Rachel Smith", Image = "site-runner.png", Published = 2001, Synopsis = "A fast-paced thriller about running a high-traffic website.", Price = 23.99 },
            new Book { Id = 21, Title = "Source Code 5", Author = "Sam Taylor", Image = "source-code-5.png", Published = 2000, Synopsis = "The fifth installment in the epic saga of source code adventures.", Price = 20.99 },
            new Book { Id = 22, Title = "Switch Expr Jekyl Hyde", Author = "Tina Underwood", Image = "switch-expr-jekyl-hyde.png", Published = 1999, Synopsis = "A tale of duality in programming with switch expressions.", Price = 19.99 },
            new Book { Id = 23, Title = "Tale of Two Servers", Author = "Ulysses Vaughn", Image = "tale-of-two-servers.png", Published = 1998, Synopsis = "A story of two servers and their intertwined fates.", Price = 22.99 },
            new Book { Id = 24, Title = "The Bug Jar", Author = "Vera White", Image = "the-bug-jar.png", Published = 1997, Synopsis = "A collection of stories about the most notorious bugs in history.", Price = 18.99 },
            new Book { Id = 25, Title = "The Hex Files", Author = "Walter Xander", Image = "the-hex-files.png", Published = 1996, Synopsis = "Investigating mysterious hexadecimal codes and their origins.", Price = 21.99 },
            new Book { Id = 26, Title = "War && Peace", Author = "Yvonne Young", Image = "war-andand-peace.png", Published = 1995, Synopsis = "An epic tale of conflict and resolution in the world of programming.", Price = 25.99 }
        };

        context.Books.AddRange(books);
        context.SaveChanges();
    }
}