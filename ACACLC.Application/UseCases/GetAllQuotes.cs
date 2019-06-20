using ACACLC.Application.Constants;
using ACACLC.Application.Interfaces;
using ACACLC.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACACLC.Application.UseCases
{
    public class GetAllQuotes
    {
        private readonly IStorage storage;

        public GetAllQuotes(IStorage storage)
        {
            this.storage = storage;
        }

        public IList<Quote> Handle() => this.storage.Get<List<Quote>>(Keys.Quotes);
    }
}
