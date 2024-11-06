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
    [Parameter] public int? Height { get; set; }
    [Parameter] public int? Width { get; set; }
    [Parameter] public bool Circle { get; set; }
    [Parameter] public bool Shadow { get; set; } = true;

    #endregion
    
    
    
    #region FIELDS
    
    private Dictionary<string, object> _attributes = [];
    
    #endregion 
    
    
    
    #region PRIVATE METHODS

    protected override void OnParametersSet()
    {
        _attributes.Clear();
        if (Height.HasValue)
        {
            _attributes.Add("height", Height.Value);
        }
        else if (Width.HasValue)
        {
            _attributes.Add("width", Width.Value);
        }
        
        if (Circle)
        {
            AspectRatio = PictureComponentAspectRatio.Square;
        }
    }

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
        
        public static readonly PictureComponentAspectRatio Default = new PictureComponentAspectRatio();
        public static readonly PictureComponentAspectRatio Photo = new PictureComponentAspectRatio(16, 9);
        public static readonly PictureComponentAspectRatio Square = new PictureComponentAspectRatio(1, 1);
        
        #endregion
        
        
        
        #region Public methods

        public override string ToString() => $"{Horizontal}/{Vertical}";

        #endregion
    }
    
    #endregion
}