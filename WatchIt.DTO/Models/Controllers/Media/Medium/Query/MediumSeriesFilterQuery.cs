using Microsoft.AspNetCore.Mvc;
using Refit;
using WatchIt.Database.Model.Media;
using WatchIt.DTO.Models.Controllers.Media.Medium.Filters;
using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Media.Medium.Query;

public class MediumSeriesFilterQuery : BaseMediumFilterQuery<MediumSeries>
{
    #region PROPERTIES

    [FromQuery(Name = "has_ended")]
    [AliasAs("has_ended")]
    public bool? HasEnded { get; set; }

    #endregion
    
    
    
    #region PUBLIC METHODS

    public override IEnumerable<Filter<MediumSeries>> GetFilters() => base.GetFilters()
                                                                          .Append(new MediumSeriesHasEndedFilter(HasEnded));

    #endregion
}