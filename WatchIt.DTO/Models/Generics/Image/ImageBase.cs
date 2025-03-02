namespace WatchIt.DTO.Models.Generics.Image;

public abstract class ImageBase
{
    #region PROPERTIES

    public byte[] Image { get; set; } = default!;
    public string MimeType { get; set; } = default!;
    
    #endregion



    #region PUBLIC METHODS

    public override string ToString() => $"data:{MimeType};base64,{Convert.ToBase64String(Image)}";

    #endregion
}