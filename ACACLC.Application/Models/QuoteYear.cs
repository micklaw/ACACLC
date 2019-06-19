using System;
using System.Collections.Generic;
using System.Text;

namespace ACACLC.Application.Models
{
    public class QuoteYear
    {
        public IList<QuoteMonth> Months { get; set; } = new List<QuoteMonth>();
    }
}
