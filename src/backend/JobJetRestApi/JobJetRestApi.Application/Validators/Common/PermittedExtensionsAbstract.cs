using System.Collections.Generic;
using JobJetRestApi.Application.Validators.RequestsValidators;

namespace JobJetRestApi.Application.Validators.Common;

public abstract class PermittedExtensionsAbstract
{
    public abstract List<Extension> FormatFiles { get; }
    
    public abstract string GetFormatFilesAsReadableString();
}

public class Extension
{
    public string Value { get; }

    public Extension(string extension)
    {
        Value = extension;
    }
}