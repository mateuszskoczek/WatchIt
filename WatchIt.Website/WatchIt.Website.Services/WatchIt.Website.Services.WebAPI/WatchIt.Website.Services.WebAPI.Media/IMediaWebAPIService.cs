﻿using WatchIt.Common.Model.Genres;
using WatchIt.Common.Model.Media;
using WatchIt.Common.Model.Photos;
using WatchIt.Common.Model.Rating;

namespace WatchIt.Website.Services.WebAPI.Media;

public interface IMediaWebAPIService
{
    Task GetMedia(long mediaId, Action<MediaResponse>? successAction = null, Action? notFoundAction = null);
    
    Task GetMediaGenres(long mediaId, Action<IEnumerable<GenreResponse>>? successAction = null, Action? notFoundAction = null);
    Task PostMediaGenre(long mediaId, long genreId, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null, Action? notFoundAction = null);
    Task DeleteMediaGenre(long mediaId, long genreId, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null, Action? notFoundAction = null);

    Task GetMediaRating(long mediaId, Action<RatingResponse>? successAction = null, Action? notFoundAction = null);
    Task GetMediaRatingByUser(long mediaId, long userId, Action<short>? successAction = null, Action? notFoundAction = null);
    Task PutMediaRating(long mediaId, RatingRequest body, Action? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? notFoundAction = null);
    Task DeleteMediaRating(long mediaId, Action? successAction = null, Action? unauthorizedAction = null);

    Task PostMediaView(long mediaId, Action? successAction = null, Action? notFoundAction = null);
    
    Task GetMediaPoster(long mediaId, Action<MediaPosterResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? notFoundAction = null);
    Task PutMediaPoster(long mediaId, MediaPosterRequest data, Action<MediaPosterResponse>? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    Task DeleteMediaPoster(long mediaId, Action? successAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null);
    
    Task GetMediaPhotos(long mediaId, PhotoQueryParameters? query = null, Action<IEnumerable<PhotoResponse>>? successAction = null, Action? notFoundAction = null);
    Task GetMediaPhotoRandomBackground(long mediaId, Action<PhotoResponse>? successAction = null, Action? notFoundAction = null);
    Task PostMediaPhoto(long mediaId, MediaPhotoRequest data, Action? successAction = null, Action<IDictionary<string, string[]>>? badRequestAction = null, Action? unauthorizedAction = null, Action? forbiddenAction = null, Action? notFoundAction = null);
}