﻿namespace JobJetRestApi.Application.Contracts.V1.Requests;

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}