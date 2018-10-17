﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM.Entity
{
    class Song
    {
        private string _name;
        private string _description;
        private string _singer;
        private string _author;
        private string _thumbnail;
        private string _link;

        public Song()
        {
        }

        public Song(string name, string description, string singer, string author, string thumbnail, string link)
        {
            this.name = name;
            this.description = description;
            this.singer = singer;
            this.author = author;
            this.thumbnail = thumbnail;
            this.link = link;
        }

        public string name { get => _name; set => _name = value; }
        public string description { get => _description; set => _description = value; }
        public string singer { get => _singer; set => _singer = value; }
        public string author { get => _author; set => _author = value; }
        public string thumbnail { get => _thumbnail; set => _thumbnail = value; }
        public string link { get => _link; set => _link = value; }
    }
}
