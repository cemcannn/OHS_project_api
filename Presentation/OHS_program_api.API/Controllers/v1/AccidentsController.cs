using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OHS_program_api.API.Attributes;
using OHS_program_api.Application.Consts;
using OHS_program_api.Application.CustomAttributes;
using OHS_program_api.Application.Enums;
using OHS_program_api.Application.Features.Commands.Safety.Accident.CreateAccident;
using OHS_program_api.Application.Features.Commands.Safety.Accident.DeleteAccident;
using OHS_program_api.Application.Features.Commands.Safety.Accident.UpdateAccident;
using OHS_program_api.Application.Features.Queries.Safety.Accident.GetAccidentById;
using OHS_program_api.Application.Features.Queries.Safety.Accident.GetAccidents;

namespace OHS_program_api.API.Controllers.v1
{
    /// <summary>
    /// Kazalar API Controller - API v1.0
    /// Kaza oluşturma, güncelleme, silme ve sorgulama işlemleri
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    [Produces("application/json")]
    public class AccidentsController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly ILogger<AccidentsController> _logger;

        public AccidentsController(IMediator mediator, ILogger<AccidentsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Kaza ID'sine göre detay bilgilerini getirir
        /// </summary>
        /// <param name="getAccidentByIdQueryRequest">Kaza ID'si</param>
        /// <returns>Kaza detay bilgileri</returns>
        /// <response code="200">Başarıyla döndürüldü</response>
        /// <response code="404">Kaza bulunamadı</response>
        /// <response code="401">Yetkilendirme başarısız</response>
        [HttpGet("{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Accident By Id", Menu = AuthorizeDefinitionConstants.Accidents)]
        [CacheResponse(durationSeconds: 300)] // 5 dakika cache
        public async Task<IActionResult> GetAccident([FromRoute] GetAccidentByIdQueryRequest getAccidentByIdQueryRequest)
        {
            GetAccidentByIdQueryResponse response = await _mediator.Send(getAccidentByIdQueryRequest);
            return Ok(response);
        }

        /// <summary>
        /// Sayfalama ile kazaların listesini getirir
        /// </summary>
        /// <param name="GetAccidentsQueryRequest">Sayfalama parametreleri</param>
        /// <returns>Kaza listesi</returns>
        /// <response code="200">Başarıyla döndürüldü</response>
        /// <response code="401">Yetkilendirme başarısız</response>
        [HttpGet]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get All Accidents", Menu = AuthorizeDefinitionConstants.Accidents)]
        [CacheResponse(durationSeconds: 600)] // 10 dakika cache
        public async Task<IActionResult> GetAccidents([FromQuery] GetAccidentsQueryRequest GetAccidentsQueryRequest)
        {
            GetAccidentsQueryResponse response = await _mediator.Send(GetAccidentsQueryRequest);
            return Ok(response);
        }

        /// <summary>
        /// Yeni kaza kaydı oluşturur
        /// </summary>
        /// <param name="createAccidentCommandRequest">Kaza bilgileri</param>
        /// <returns>Oluşturulan kaza ID'si</returns>
        /// <response code="200">Başarıyla oluşturuldu</response>
        /// <response code="400">Geçersiz veri</response>
        /// <response code="401">Yetkilendirme başarısız</response>
        [HttpPost]
        [AuthorizeDefinition(ActionType = ActionType.Writing, Definition = "Create Accident", Menu = AuthorizeDefinitionConstants.Accidents)]
        public async Task<IActionResult> CreateAccident([FromBody] CreateAccidentCommandRequest createAccidentCommandRequest)
        {
            CreateAccidentCommandResponse response = await _mediator.Send(createAccidentCommandRequest);
            return Ok(response);
        }

        /// <summary>
        /// Var olan kaza kaydını günceller
        /// </summary>
        /// <param name="updateAccidentCommandRequest">Güncellenmiş kaza bilgileri</param>
        /// <returns>Güncellenmiş kaza ID'si</returns>
        /// <response code="200">Başarıyla güncellendi</response>
        /// <response code="400">Geçersiz veri</response>
        /// <response code="401">Yetkilendirme başarısız</response>
        /// <response code="404">Kaza bulunamadı</response>
        [HttpPut]
        [AuthorizeDefinition(ActionType = ActionType.Updating, Definition = "Update Accident", Menu = AuthorizeDefinitionConstants.Accidents)]
        public async Task<IActionResult> UpdateAccident([FromBody] UpdateAccidentCommandRequest updateAccidentCommandRequest)
        {
            UpdateAccidentCommandResponse response = await _mediator.Send(updateAccidentCommandRequest);
            return Ok(response);
        }

        /// <summary>
        /// Kaza kaydını siler
        /// </summary>
        /// <param name="deleteAccidentCommandRequest">Silinecek kaza ID'si</param>
        /// <returns>Silinen kaza ID'si</returns>
        /// <response code="200">Başarıyla silindi</response>
        /// <response code="401">Yetkilendirme başarısız</response>
        /// <response code="404">Kaza bulunamadı</response>
        [HttpDelete("{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Deleting, Definition = "Delete Accident", Menu = AuthorizeDefinitionConstants.Accidents)]
        public async Task<IActionResult> DeleteAccident([FromRoute] DeleteAccidentCommandRequest deleteAccidentCommandRequest)
        {
            DeleteAccidentCommandResponse response = await _mediator.Send(deleteAccidentCommandRequest);
            return Ok(response);
        }
    }
}
