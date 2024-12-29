﻿using System.ComponentModel.DataAnnotations;

namespace MataAtlantica.API.Models;

public record LoginRequest(
    [Required(AllowEmptyStrings = false)][EmailAddress] string Login,
    [Required(AllowEmptyStrings = false)] string Senha);
