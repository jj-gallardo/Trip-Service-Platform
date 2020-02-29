using Api.Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Trip.Domain.Entities;


namespace Application.Cards
{
    class GetAllCardsQueryHandler : IRequestHandler<GetAllCardsQuery, List<CardDto>>
    {
        private IMongoDBContext _context;
        private IMapper _mapper;
        public GetAllCardsQueryHandler(IMongoDBContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<List<CardDto>> Handle(GetAllCardsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<CardDto>>(await this._context.Cards.Find(_ => true).ToListAsync(cancellationToken));
        }
    }
}
