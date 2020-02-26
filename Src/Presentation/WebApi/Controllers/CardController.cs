using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Cards;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Trip.WebApi.Controllers
{
    public class CardController : BaseController
    {
        public CardController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet()]
        [ProducesResponseType(typeof(List<CardDto>), StatusCodes.Status200OK)]        
        public async Task<ActionResult<List<CardDto>>> Get()
        {
            var result = await _mediator.Send(new GetAllCardsQuery());
            return Ok(result);
        }

        [HttpPost()]
        [ProducesResponseType(typeof(CardDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<CardDto>> Create([FromBody]CreateCardCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
