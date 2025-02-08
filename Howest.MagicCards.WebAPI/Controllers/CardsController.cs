using AutoMapper;
using AutoMapper.QueryableExtensions;
using Howest.MagicCards.DAL.Models;
using Howest.MagicCards.DAL.Repositories;
using Howest.MagicCards.Shared.DTO;
using Howest.MagicCards.Shared.Extensions;
using Howest.MagicCards.Shared.Filters;
using Howest.MagicCards.WebAPI.Configuration;
using Howest.MagicCards.WebAPI.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Howest.MagicCards.WebAPI.Controllers
{
    [ApiVersion("1.1")]
    [ApiVersion("1.5")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ResponseCache(Duration = 120, Location = ResponseCacheLocation.Any)]
    public class CardsController : ControllerBase
    {

        private readonly ICardRepository _cardRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<CardsController> _logger;
        private const string _internalErrorMessage = "There seems to be an internal error"; //constants do not seem to have the all uppercase naming convention
        private readonly ApiBehaviourConf _options;

        public CardsController(ICardRepository cardRepository, IMapper mapper, ILogger<CardsController> logger, IOptionsSnapshot<ApiBehaviourConf> options)
        {
            _cardRepo = cardRepository;
            _mapper = mapper;
            _logger = logger;
            _options = options.Value;
        }

        [MapToApiVersion("1.1")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CardReadBasicDTO>), 200)]
        [ProducesResponseType(typeof(Response<CardReadBasicDTO>), 500)]
        public async Task<ActionResult<PagedResponse<IEnumerable<CardReadBasicDTO>>>> GetAllCards([FromQuery] PaginationFilter filter)
        {
            filter.PageSize = filter.PageSize > _options.MaxPageSize ? _options.MaxPageSize : filter.PageSize;

            try
            {
                return Ok(new PagedResponse<IEnumerable<CardReadBasicDTO>>(
                            await _cardRepo.GetAllCards()
                                    .ToPagedList(filter.PageNumber, filter.PageSize) 
                                    .ProjectTo<CardReadBasicDTO>(_mapper.ConfigurationProvider)
                                    .ToListAsync(),
                            filter.PageNumber,
                            filter.PageSize)
                { Message = _options.MessageToClient }
                        );
            }
            catch (Exception error)
            {
                _logger.LogError(error, "An error occurred while fetching all cards");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response<CardReadBasicDTO>()
                    {
                        Succeeded = false,
                        Errors = [$"Status code: {StatusCodes.Status500InternalServerError}"],
                        Message = _internalErrorMessage
                    });
            }
        }



        [MapToApiVersion("1.5")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CardReadBasicDTO>), 200)]
        [ProducesResponseType(typeof(Response<CardReadBasicDTO>), 500)]
        public async Task<ActionResult<PagedResponse<IEnumerable<CardReadBasicDTO>>>> GetAllCards([FromQuery] CardFilter filter)
        {
            filter.PageSize = filter.PageSize > _options.MaxPageSize ? _options.MaxPageSize : filter.PageSize;

            try
            {
                return Ok(new PagedResponse<IEnumerable<CardReadBasicDTO>>(
                            await _cardRepo.GetAllCards()
                                    .ToFilteredList(filter.SetCode, filter.ArtistName, filter.Rarity, filter.Name, filter.Type, filter.Text)
                                    .SortByNameAscDesc(filter.Ascending)
                                    .ToPagedList(filter.PageNumber, filter.PageSize)
                                    .ProjectTo<CardReadBasicDTO>(_mapper.ConfigurationProvider)
                                    .ToListAsync(), // async version ToListAsync exists here but if it just waits for the full SQL query then async adds them to a small list might not have noticable performance gains
                                                    // query is not executed until ToList() is called (apparently, van horen zeggen) so make it async anyway, can't hurt anyway
                            filter.PageNumber,
                            filter.PageSize)
                { Message = _options.MessageToClient }
                        );
            }
            catch (Exception error)
            {
                _logger.LogError(error, "An error occurred while fetching cards");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response<CardReadBasicDTO>()
                    {
                        Succeeded = false,
                        Errors = [$"Status code: {StatusCodes.Status500InternalServerError}"],
                        Message = _internalErrorMessage
                    });
            }
        }

        [MapToApiVersion("1.5")]
        [HttpGet("{id}", Name = "GetCardById")]
        [ProducesResponseType(typeof(Response<CardReadDetailDTO>), 200)]
        [ProducesResponseType(typeof(Response<CardReadDetailDTO>), 404)]
        [ProducesResponseType(typeof(Response<CardReadDetailDTO>), 500)]
        public async Task<ActionResult<CardReadDetailDTO>> GetCardById(int id)
        {
            try
            {
                return (await _cardRepo.GetCardbyId(id) is Card foundCard)
                ? Ok(_mapper.Map<CardReadDetailDTO>(foundCard))
                : NotFound($"This card (id:{id}) doesn't exist, why are you asking for it");
            }
            catch (Exception error)
            {
                _logger.LogError(error, $"An error occurred while fetching card {id}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response<CardReadDetailDTO>()
                    {
                        Succeeded = false,
                        Errors = [$"Status code: {StatusCodes.Status500InternalServerError}"],
                        Message = _internalErrorMessage
                    });
            }
        }
    }
}

