using AutoMapper;
using BBSK_Psycho.BusinessLayer;
using BBSK_Psycho.BusinessLayer.Services.Interfaces;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;
using BBSK_Psycho.Extensions;
using BBSK_Psycho.Models;
using BBSK_Psycho.Models.Requests;
using BBSK_Psycho.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBSK_Psycho.Controllers;

[ApiController]
[Authorize]
[Produces("application/json")]
[Route("[controller]")]
public class ApplicationForPsychologistSearchController : ControllerBase
{
    private readonly IApplicationForPsychologistSearchServices _applicationForPsychologistSearchServices;
    private readonly IMapper _mapper;

    public ClaimModel Claims;

    public ApplicationForPsychologistSearchController(IApplicationForPsychologistSearchServices applicationForPsychologistSearchServices, IMapper mapper)
    {
        _applicationForPsychologistSearchServices = applicationForPsychologistSearchServices;
        _mapper = mapper;

    }


    [AuthorizeByRole(Role.Client)]
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public ActionResult<int> AddApplicationForPsychologist([FromBody] ApplicationForPsychologistSearchRequest request)
    {
        var claims = this.GetClaims();
        var id = _applicationForPsychologistSearchServices.AddApplicationForPsychologist(_mapper.Map<ApplicationForPsychologistSearch>(request), claims);

        return Created($"{this.GetRequestPath()}/{id}", id);
    }

    [AuthorizeByRole(Role.Client)]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApplicationForPsychologistSearchResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public ActionResult<ApplicationForPsychologistSearchResponse> GetApplicationForPsychologistById([FromRoute] int id)
    {
        var claims = this.GetClaims();

        var rquest = _applicationForPsychologistSearchServices.GetApplicationForPsychologistById(id, claims);
        return Ok(_mapper.Map<ApplicationForPsychologistSearchResponse>(rquest));
    }


    [Authorize(Roles = nameof(Role.Manager))]
    [HttpGet]
    [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public ActionResult<List<ClientResponse>> GetAllApplicationsForPsychologist()
    {
        var request = _applicationForPsychologistSearchServices.GetAllApplicationsForPsychologist();
        return Ok(_mapper.Map<List<ApplicationForPsychologistSearchResponse>>(request));


    }

    


    

    [AuthorizeByRole(Role.Client)]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public ActionResult DeleteApplicationForPsychologist([FromRoute] int id)
    {
        var claimsUser = this.GetClaims();

        _applicationForPsychologistSearchServices.DeleteApplicationForPsychologist(id, claimsUser);
        return NoContent();
    }


    [AuthorizeByRole(Role.Client)]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public ActionResult UpdateClientById([FromBody] ApplicationForPsychologistSearchUpdateRequest newModel, [FromRoute] int id)
    {
        var claims = this.GetClaims();

       var request = _mapper.Map<ApplicationForPsychologistSearch>(newModel);

        _applicationForPsychologistSearchServices.UpdateApplicationForPsychologist(request, id, claims);

        return NoContent();
    }

}
