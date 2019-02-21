using ID3tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID3Tags
{
    class Program
    {
        static void Main(string[] args)
        {
            TagReader tags = new TagReader(@"E:\Music\KORN - DISCOGRAPHY (1994-13) [CHANNEL NEO]\[1998] Follow The Leader (2 CD)\[CD 1] Follow The Leader\02 - Freak On A Leash.mp3");

            foreach(String tag in tags.tagData.Values)
            {
                Console.WriteLine(tag);
            }

            Console.ReadLine();
        }
    }
}
