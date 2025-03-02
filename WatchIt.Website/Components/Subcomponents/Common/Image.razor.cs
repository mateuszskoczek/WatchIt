using Microsoft.AspNetCore.Components;
using WatchIt.DTO.Models.Generics.Image;

namespace WatchIt.Website.Components.Subcomponents.Common;

public partial class Image : Component
{
    #region PARAMETERS
    
    [Parameter] public ImageBase? Content { get; set; }
    [Parameter] public required string Placeholder { get; set; }
    [Parameter] public ImageComponentAspectRatio AspectRatio { get; set; } = ImageComponentAspectRatio.Default;
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
            AspectRatio = ImageComponentAspectRatio.Square;
        }
    }

    #endregion



    #region STRUCTS

    public struct ImageComponentAspectRatio
    {
        #region Properties
        
        public int Vertical { get; set; }
        public int Horizontal { get; set; }
        
        #endregion
        
        
        
        #region Constructors

        public ImageComponentAspectRatio() : this(3, 5) {}
        
        public ImageComponentAspectRatio(int horizontal, int vertical)
        {
            Horizontal = horizontal;
            Vertical = vertical;
        }
        
        public static readonly ImageComponentAspectRatio Default = new ImageComponentAspectRatio();
        public static readonly ImageComponentAspectRatio Photo = new ImageComponentAspectRatio(16, 9);
        public static readonly ImageComponentAspectRatio Square = new ImageComponentAspectRatio(1, 1);
        
        #endregion
        
        
        
        #region Public methods

        public override string ToString() => $"{Horizontal}/{Vertical}";

        #endregion
    }
    
    #endregion
}