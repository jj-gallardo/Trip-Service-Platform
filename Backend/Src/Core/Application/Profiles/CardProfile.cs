using Application.Cards;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Trip.Domain.Entities;

namespace Application.Profiles
{
    class CardProfile : Profile
    {
        public CardProfile()
        {
            CreateMap<Card, CardDto>().ReverseMap();
        }
    }
}
