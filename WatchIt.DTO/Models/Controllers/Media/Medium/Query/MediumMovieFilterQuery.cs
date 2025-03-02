using Microsoft.AspNetCore.Mvc;
using Refit;
using WatchIt.Database.Model.Media;
using WatchIt.DTO.Models.Controllers.Media.Medium.Filters;
using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Media.Medium.Query;

public class MediumMovieFilterQuery : BaseMediumFilterQuery<MediumMovie>
{
    #region PROPERTIES

    [FromQuery(Name = "budget_from")]
    [AliasAs("budget_from")]
    public decimal? BudgetFrom { get; set; }

    [FromQuery(Name = "budget_to")]
    [AliasAs("budget_to")]
    public decimal? BudgetTo { get; set; }

    #endregion
    
    
    
    #region PUBLIC METHODS

    public override IEnumerable<Filter<MediumMovie>> GetFilters() => base.GetFilters()
                                                                         .Append(new MediumMovieBudgetFromFilter(BudgetFrom))
                                                                         .Append(new MediumMovieBudgetToFilter(BudgetTo));

    #endregion
}