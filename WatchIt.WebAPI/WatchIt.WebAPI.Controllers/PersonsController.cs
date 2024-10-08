using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WatchIt.Common.Model.Persons;
using WatchIt.Common.Model.Roles;
using WatchIt.WebAPI.Services.Controllers.Persons;

namespace WatchIt.WebAPI.Controllers;

[ApiController]
[Route("persons")]
public class PersonsController : ControllerBase
{
    #region SERVICES

    private readonly IPersonsControllerService _personsControllerService;
    
    #endregion
    
    
    
    #region CONSTRUCTORS

    public PersonsController(IPersonsControllerService personsControllerService)
    {
        _personsControllerService = personsControllerService;
    }
    
    #endregion
    
    
    
    #region METHODS
    
    #region Main
    
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<PersonResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllMovies(PersonQueryParameters query) => await _personsControllerService.GetAllPersons(query);
    
    [HttpGet("{id}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PersonResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetMovie([FromRoute] long id) => await _personsControllerService.GetPerson(id);
    
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(PersonResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> PostMovie([FromBody] PersonRequest body) => await _personsControllerService.PostPerson(body);
    
    [HttpPut("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> PutMovie([FromRoute] long id, [FromBody]PersonRequest body) => await _personsControllerService.PutPerson(id, body);

    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DeleteMovie([FromRoute] long id) => await _personsControllerService.DeletePerson(id);
    
    #endregion
    
    #region View count
    
    [HttpGet("view")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<PersonResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetMoviesViewRank([FromQuery] int first = 5, [FromQuery] int days = 7) => await _personsControllerService.GetPersonsViewRank(first, days);
    
    #endregion
    
    #region Photo
    
    [HttpGet("{id}/photo")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PersonPhotoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetPersonPhoto([FromRoute] long id) => await _personsControllerService.GetPersonPhoto(id);
    
    [HttpPut("{id}/photo")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(PersonPhotoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> PutPersonPhoto([FromRoute]long id, [FromBody]PersonPhotoRequest body) => await _personsControllerService.PutPersonPhoto(id, body);
    
    [HttpDelete("{id}/photo")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DeletePersonPhoto([FromRoute]long id) => await _personsControllerService.DeletePersonPhoto(id);
    
    #endregion

    #region Roles

    [HttpGet("{id}/roles/actor")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<ActorRoleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetPersonAllActorRoles([FromRoute]long id, ActorRolePersonQueryParameters query) => await _personsControllerService.GetPersonAllActorRoles(id, query);
    
    [HttpPost("{id}/roles/actor")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(ActorRoleResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> PostPersonActorRole([FromRoute]long id, [FromBody]ActorRolePersonRequest body) => await _personsControllerService.PostPersonActorRole(id, body);
    
    [HttpGet("{id}/roles/creator")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<CreatorRoleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetPersonAllCreatorRoles([FromRoute]long id, CreatorRolePersonQueryParameters query) => await _personsControllerService.GetPersonAllCreatorRoles(id, query);
    
    [HttpPost("{id}/roles/creator")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(CreatorRoleResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> PostPersonCreatorRole([FromRoute]long id, [FromBody]CreatorRolePersonRequest body) => await _personsControllerService.PostPersonCreatorRole(id, body);

    #endregion
    
    #endregion
}