﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace JobJetRestApi.Infrastructure.Dtos.EmailService;

public class EmailRequest
{
    public string ToEmail { get; set; }
    
    public string Subject { get; set; }
    
    public string Body { get; set; }
    
    public List<IFormFile> Attachments { get; set; }
}