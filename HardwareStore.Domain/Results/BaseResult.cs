﻿namespace HardwareStore.Domain.Results;

public class BaseResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = null!;
}