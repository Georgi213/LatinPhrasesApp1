﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LatinPhrasesApp.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> LatinPhrases { get; set; }
        public List<string> TranslatedPhrases { get; set; }

        public Author()
        {
            LatinPhrases = new List<string>();
            TranslatedPhrases = new List<string>();
        }
    }
}
