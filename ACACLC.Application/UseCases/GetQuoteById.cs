using ACACLC.Application.Constants;
using ACACLC.Application.Interfaces;
using ACACLC.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACACLC.Application.UseCases
{
    public class GetQuoteById
    {
        private readonly IStorage storage;

        public GetQuoteById(IStorage storage)
        {
            this.storage = storage;
        }

        public Quote Handle(Guid id) => (this.storage.Get<List<Quote>>(Keys.Quotes) ?? new List<Quote>()).FirstOrDefault(i => i.Id == id);
    }
}
