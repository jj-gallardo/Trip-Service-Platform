using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Cards
{
    public class CreateCardCommand : IRequest<CardDto>
    {        
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
