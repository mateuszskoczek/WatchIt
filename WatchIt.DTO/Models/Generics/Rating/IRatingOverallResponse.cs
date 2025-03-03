namespace WatchIt.DTO.Models.Generics.Rating;

public interface IRatingOverallResponse : IRatingResponse
{
    long Count { get; set; }
}