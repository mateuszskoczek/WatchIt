using System.ComponentModel;
using System.Reflection;
using Microsoft.AspNetCore.Components;

namespace WatchIt.Website.Components.Subcomponents.Common;

public partial class Loading : Component
{
    #region PARAMETERS

    [Parameter] public Colors Color { get; set; } = Colors.Dark;

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

    public enum Colors
    {
        [Description("dark")]
        Dark,
        
        [Description("light")]
        Light,
    }
    
    #endregion
}