using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.Extensions;
using BBSK_Psycho.Models;
using BBSK_Psycho.Models.Requests;
using BBSK_Psycho.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BBSK_Psycho.DataLayer.Repositories;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using AutoMapper;
using BBSK_Psycho.BusinessLayer;

namespace BBSK_Psycho.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class PsychologistsController : ControllerBase
{

    private readonly IPsychologistsRepository _psychologistsRepository;
    private readonly IPsychologistService _psychologistServices;
    private readonly IMapper _mapper;
    public ClaimModel Claims;
    public PsychologistsController(IPsychologistService psychologistServices, IMapper mapper)
    {
        _psychologistServices = psychologistServices;
        _mapper = mapper;
    }

    [AuthorizeByRole]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PsychologistResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task <ActionResult<PsychologistResponse>> GetPsychologist(int id)
    {
        var claims = this.GetClaims();

        var result = await _psychologistServices.GetPsychologist(id, claims);
        if (result == null)
            return NotFound();
        else
            return Ok(_mapper.Map<PsychologistResponse>(result));
    }

    [AuthorizeByRole(Role.Client)]
    [HttpGet()]
    [ProducesResponseType(typeof(GetAllPsychologistsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task <ActionResult<List<GetAllPsychologistsResponse>>> GetAllPsychologists()
    {
        var claims = this.GetClaims();
        var result = await _psychologistServices.GetAllPsychologists(claims);

        return Ok(_mapper.Map<List<GetAllPsychologistsResponse>>(result));
    }

    [HttpGet("avg-price")]
    [Authorize(Roles = nameof(Role.Psychologist))]
    [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task <ActionResult<decimal>> GetAveragePsychologistPrice()
    {
        return 0.20m;
    }

    [AllowAnonymous]
    [HttpPost()]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task <ActionResult<int>> AddPsychologist([FromBody] AddPsychologistRequest psychologistRequest)
    {
        var result = await _psychologistServices.AddPsychologist(_mapper.Map<Psychologist>(psychologistRequest));
        return Created("", result);
    }

    [AuthorizeByRole(Role.Psychologist)]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task <ActionResult> UpdatePsychologist([FromBody] UpdatePsychologistRequest psychologistRequest, int id)
    {
        var claims = this.GetClaims();
        await _psychologistServices.UpdatePsychologist(_mapper.Map<Psychologist>(psychologistRequest), id, claims);
        return NoContent();
    }

    [AuthorizeByRole(Role.Psychologist)]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task <ActionResult> DeletePsychologist(int id)
    {
        var claims = this.GetClaims();
        await _psychologistServices.DeletePsychologist(id, claims);
        return NoContent();
    }

    [AuthorizeByRole(Role.Psychologist, Role.Client)]
    [HttpGet("{psychologistId}/comments")]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    public async Task <ActionResult<List<GetCommentsByPsychologistIdResponse>>> GetCommentsByPsychologistId(int psychologistId)
    {
        var claims = this.GetClaims();
        var result = await _psychologistServices.GetCommentsByPsychologistId(psychologistId, claims);
        return Ok(_mapper.Map<List<GetCommentsByPsychologistIdResponse>>(result));
    }

    [AuthorizeByRole(Role.Psychologist, Role.Client)]
    [HttpGet("{psychologistId}/orders")]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    public async Task <ActionResult <List<OrderResponse>>> GetOrdersByPsychologistId(int id)
    {
        var claims = this.GetClaims();
        var result = await _psychologistServices.GetOrdersByPsychologistId(id, claims);
        return Ok(_mapper.Map<List<OrderResponse>>(result));
    }

    [AuthorizeByRole(Role.Client)]
    [HttpPost("{psychologistId}/comments")]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
    public async Task <ActionResult<int>> AddCommentToPsyhologist([FromBody] CommentRequest commentRequest, int psychologistId)
    {
        var claims = this.GetClaims();
        var result = _psychologistServices.AddCommentToPsyhologist(_mapper.Map<Comment>(commentRequest), psychologistId, claims);
        return Created("", result);
    }

    [AllowAnonymous]
    [HttpGet("search-by-parametrs")]
    [ProducesResponseType(typeof(GetAllPsychologistsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<List<GetAllPsychologistsResponse>>> GetPsychologistsByParametrs([FromQuery]FilterOptionRequest filterOptionRequest)
    {
        var result = await _psychologistServices.GetPsychologistsByFilter(filterOptionRequest.Price, filterOptionRequest.Problems, filterOptionRequest.Gender);

        return Ok(_mapper.Map<List<GetAllPsychologistsResponse>>(result));
    }
}
