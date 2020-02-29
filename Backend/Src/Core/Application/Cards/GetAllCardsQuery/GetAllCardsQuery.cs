using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Cards
{
    public class GetAllCardsQuery : IRequest<List<CardDto>>   { }
}
