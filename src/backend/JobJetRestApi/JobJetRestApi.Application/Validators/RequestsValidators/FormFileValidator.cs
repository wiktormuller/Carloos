using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentValidation;
using JobJetRestApi.Application.Validators.Common;
using Microsoft.AspNetCore.Http;

namespace JobJetRestApi.Application.Validators.RequestsValidators;

public class FormFileValidator : AbstractValidator<IFormFile>
{
    private PermittedExtensionsAbstract PermittedExtensions { get; }
    
    public FormFileValidator(int minSizeInBits, int maxSizeInBits, PermittedExtensionsAbstract permittedExtensions)
    {
        PermittedExtensions = permittedExtensions;
        
        RuleFor(formFile => formFile.Length)
            .ExclusiveBetween(minSizeInBits,  maxSizeInBits);

        RuleFor(formFile => formFile.FileName)
            .Must(IsExtensionAllowed)
            .WithMessage($"File must be in one of those extensions: {permittedExtensions.GetFormatFilesAsReadableString()}.");
    }

    private bool IsExtensionAllowed(string fileName)
    {
        var uploadedExtension = Path.GetExtension(fileName).ToLowerInvariant();

        return !string.IsNullOrEmpty(uploadedExtension) && 
               PermittedExtensions.FormatFiles
                   .Select(x => x.Value)
                   .Contains(uploadedExtension);
    }
}
