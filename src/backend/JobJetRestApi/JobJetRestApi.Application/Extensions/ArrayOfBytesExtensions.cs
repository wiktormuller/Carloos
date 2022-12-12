using System;

namespace JobJetRestApi.Application.Extensions;

public static class ArrayOfBytesExtensions
{
    public static string GetBase64String(this byte[] bytes)
    {
        return Convert.ToBase64String(bytes);
    }
}