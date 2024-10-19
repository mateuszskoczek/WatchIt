using System.ComponentModel;
using System.Reflection;
using Microsoft.AspNetCore.Components;

namespace WatchIt.Website.Components.Common.Subcomponents;

public partial class LoadingComponent : ComponentBase
{
    #region PARAMETERS

    [Parameter] public LoadingComponentColors Color { get; set; } = LoadingComponentColors.Dark;

    #endregion
    
    
    
    #region PRIVATE METHODS

    private string GetColor()
    {
        DescriptionAttribute? attribute = Color.GetType()
                                               .GetTypeInfo()
                                               .GetMember(Color.ToString())
                                               .FirstOrDefault(member => member.MemberType == MemberTypes.Field)!
                                               .GetCustomAttributes(typeof(DescriptionAttribute), false)
                                               .SingleOrDefault()
                                               as DescriptionAttribute;
        return attribute!.Description;
    }
    
    #endregion
    
    
    
    #region ENUMS

    public enum LoadingComponentColors
    {
        [Description("dark")]
        Dark,
        
        [Description("light")]
        Light,
    }
    
    #endregion
}