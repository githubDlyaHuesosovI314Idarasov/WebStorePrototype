using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public class ReviewNotify
    {
        public String Title { get; set; } = null!;
        public String ThumbnailUrl { get; set; } = null!;
        public String Text { get; set; } = null!;
    }
}
