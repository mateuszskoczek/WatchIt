namespace WatchIt.DTO.Models.Generics.Rating;

public interface IRatingUserResponse : IRatingResponse
{
    DateTimeOffset? Date { get; }
}