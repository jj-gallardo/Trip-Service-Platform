using Api.Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Cards
{
    class CreateCardCommandHandler : IRequestHandler<CreateCardCommand, CardDto>
    {
        private IMongoDBContext _context;
        private IMapper _mapper;
        public CreateCardCommandHandler(IMongoDBContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<CardDto> Handle(CreateCardCommand request, CancellationToken cancellationToken)
        {
            var card = new Trip.Domain.Entities.Card()
            {
                Title = request.Title,
                Description = request.Description,
            };
            
            await this._context.Cards.InsertOneAsync(card);             
            return _mapper.Map<CardDto>(card);
        }
    }
}
