using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchIt.DTO.Models.Controllers.People.Person;
using WatchIt.DTO.Models.Controllers.People.Person.Query;
using WatchIt.DTO.Models.Generics.Image;
using WatchIt.DTO.Models.Generics.Rating;
using WatchIt.DTO.Query;
using WatchIt.WebAPI.BusinessLogic.People;
using WatchIt.WebAPI.Constants;

namespace WatchIt.WebAPI.Controllers;

[ApiController]
[Route("people")]
public class PeopleController(IPeopleBusinessLogic peopleBusinessLogic) : ControllerBase
{
    #region Main

    [HttpGet]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<IEnumerable<PersonResponse>>> GetPeople([FromQuery] PersonFilterQuery filterQuery, [FromQuery] OrderQuery orderQuery, [FromQuery] PagingQuery pagingQuery, [FromQuery(Name = "include_pictures")]bool includePictures = false) =>
        await peopleBusinessLogic.GetPeople(filterQuery, orderQuery, pagingQuery, includePictures);

    [HttpGet("{id:long}")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<PersonResponse>> GetPerson([FromRoute(Name = "id")] long id, [FromQuery(Name = "include_pictures")]bool includePictures = false) =>
        await peopleBusinessLogic.GetPerson(id, includePictures);

    [HttpPost]
    [Authorize(Policy = Policies.Admin)]
    [TranslateResultToActionResult]
    public async Task<Result<PersonResponse>> PostPerson([FromBody] PersonRequest body) =>
        await peopleBusinessLogic.PostPerson(body);

    [HttpPut("{id:long}")]
    [Authorize(Policy = Policies.Admin)]
    [TranslateResultToActionResult]
    public async Task<Result<PersonResponse>> PutPerson([FromRoute(Name = "id")] long id, [FromBody] PersonRequest body) =>
        await peopleBusinessLogic.PutPerson(id, body);

    [HttpDelete("{id:long}")]
    [Authorize(Policy = Policies.Admin)]
    [TranslateResultToActionResult]
    public async Task<Result> DeletePerson([FromRoute(Name = "id")] long id) =>
        await peopleBusinessLogic.DeletePerson(id);

    #endregion

    #region Rating

    [HttpGet("{id:long}/rating")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<RatingOverallResponse>> GetPersonRating([FromRoute(Name = "id")] long id) =>
        await peopleBusinessLogic.GetPersonRating(id);

    [HttpGet("{id:long}/rating/{account_id:long}")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<RatingUserOverallResponse>> GetPersonUserRating([FromRoute(Name = "id")] long id, [FromRoute(Name = "account_id")] long accountId) =>
        await peopleBusinessLogic.GetPersonUserRating(id, accountId);

    #endregion

    #region View Count

    [HttpPut("{id:long}/view_count")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result> PutPeopleViewCount([FromRoute(Name = "id")] long id) =>
        await peopleBusinessLogic.PutPeopleViewCount(id);

    #endregion

    #region Picture

    [HttpGet("{id:long}/picture")]
    [AllowAnonymous]
    [TranslateResultToActionResult]
    public async Task<Result<ImageResponse>> GetPersonPicture([FromRoute(Name = "id")] long id) =>
        await peopleBusinessLogic.GetPersonPicture(id);

    [HttpPut("{id:long}/picture")]
    [Authorize(Policy = Policies.Admin)]
    [TranslateResultToActionResult]
    public async Task<Result<ImageResponse>> PutPersonPicture([FromRoute(Name = "id")] long id, [FromBody] ImageRequest body) =>
        await peopleBusinessLogic.PutPersonPicture(id, body);

    [HttpDelete("{id:long}/picture")]
    [Authorize(Policy = Policies.Admin)]
    [TranslateResultToActionResult]
    public async Task<Result> DeletePersonPicture([FromRoute(Name = "id")] long id) =>
        await peopleBusinessLogic.DeletePersonPicture(id);

    #endregion
}
