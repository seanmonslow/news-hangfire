﻿using System;
using System.Collections.Generic;

namespace newshangfire.Models
{
    public class NewsApiResponse
    {
        public string status { get; set; }
        public int totalResults { get; set; }
        public List<Article> Articles { get; set; }
    }
}
