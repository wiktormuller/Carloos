using System.Collections.Generic;
using System.Linq;

namespace JobJetRestApi.Application.Validators.Common;

public class PermittedExtensionsForOfferApplication : PermittedExtensionsAbstract
{
    public override List<Extension> FormatFiles { get; }

    public PermittedExtensionsForOfferApplication()
    {
        FormatFiles = new List<Extension>
        {
            new(".pdf"),
            new(".doc"),
            new(".docx"),
            new(".png"),
            new(".jpg"),
            new(".jpeg")
        };
    }
    
    public override string GetFormatFilesAsReadableString()
    {
        return string.Join(", ", FormatFiles.Select(formatFile => formatFile.Value));
    }
}