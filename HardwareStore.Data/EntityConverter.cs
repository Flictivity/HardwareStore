﻿using HardwareStore.Data.Models;
using HardwareStore.Domain.Enums;
using HardwareStore.Domain.Models;

namespace HardwareStore.Data;

public static class EntityConverter
{
    public static User ConvertUser(UserDb user)
    {
        return new User
        {
            Id = user.Id,
            Email = user.Email,
            Password = user.Password,
            LastName = user.LastName,
            FirstName = user.FirstName,
            Role = (Roles) user.Role
        };
    }
    public static UserDb ConvertUser(User user)
    {
        return new UserDb
        {
            Email = user.Email,
            Password = user.Password,
            LastName = user.LastName,
            FirstName = user.FirstName,
            Role = (int) user.Role
        };
    }
}