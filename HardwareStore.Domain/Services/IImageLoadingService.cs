﻿using HardwareStore.Domain.Results;

namespace HardwareStore.Domain.Services;

public interface IImageLoadingService
{
    public Task<BaseResult> UploadImageAsync(string filename, Stream file);
    public Task<Stream> GetImageForExamQuestion(string? filename);
}