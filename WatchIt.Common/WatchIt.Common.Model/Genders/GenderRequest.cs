namespace WatchIt.Common.Model.Genders;

public class GenderRequest : Gender
{
    #region PUBLIC METHODS

    public Database.Model.Common.Gender CreateGender() => new Database.Model.Common.Gender()
    {
        Name = Name
    };

    #endregion
}