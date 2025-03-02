using Microsoft.AspNetCore.Mvc;
using WatchIt.Database.Model.Media;
using WatchIt.DTO.Models.Controllers.Media.Medium.Filters;
using WatchIt.DTO.Query;

namespace WatchIt.DTO.Models.Controllers.Media.Medium.Query;

public class MediumFilterQuery : BaseMediumFilterQuery<Database.Model.Media.Medium>
{
    #region PROPERTIES

    [FromQuery(Name = "type")]
    public MediumType? Type { get; set; }

    #endregion
    
    
    
    #region PUBLIC METHODS

    public override IEnumerable<Filter<Database.Model.Media.Medium>> GetFilters() => base.GetFilters()
                                                                                           .Append(new MediumTypeFilter(Type));

    #endregion
}