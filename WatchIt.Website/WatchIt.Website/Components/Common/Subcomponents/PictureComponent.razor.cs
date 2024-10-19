using Microsoft.AspNetCore.Components;
using WatchIt.Common.Model;

namespace WatchIt.Website.Components.Common.Subcomponents;

public partial class PictureComponent : ComponentBase
{
    #region PARAMETERS
    
    [Parameter] public Picture? Picture { get; set; }
    [Parameter] public required string Placeholder { get; set; }
    [Parameter] public PictureComponentAspectRatio AspectRatio { get; set; } = PictureComponentAspectRatio.Default;
    [Parameter] public string AlternativeText { get; set; } = "picture";
    [Parameter] public string Class { get; set; } = string.Empty;

    #endregion



    #region STRUCTS

    public struct PictureComponentAspectRatio
    {
        #region Properties
        
        public int Vertical { get; set; }
        public int Horizontal { get; set; }
        
        #endregion
        
        
        
        #region Constructors

        public PictureComponentAspectRatio() : this(3, 5) {}
        
        public PictureComponentAspectRatio(int horizontal, int vertical)
        {
            Horizontal = horizontal;
            Vertical = vertical;
        }
        
        public static PictureComponentAspectRatio Default = new PictureComponentAspectRatio();
        
        #endregion
        
        
        
        #region Public methods

        public override string ToString() => $"{Horizontal}/{Vertical}";

        #endregion
    }
    
    #endregion
}