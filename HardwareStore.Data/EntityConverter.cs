using HardwareStore.Data.Models;
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

    public static MainCategory ConvertMainCategory(MainCategoryDb mainCategory)
    {
        return new MainCategory
        {
            Id = mainCategory.Id,
            Name = mainCategory.Name
        };
    }
    
    public static MainCategoryDb ConvertMainCategory(MainCategory mainCategory)
    {
        return new MainCategoryDb
        {
            Name = mainCategory.Name
        };
    }
    
    public static CategoryDb ConvertCategory(Category category)
    {
        return new CategoryDb
        {
            Name = category.Name,
            MainCategoryId = category.MainCategory.Id
        };
    }
    
    public static Category ConvertCategory(CategoryDb category)
    {
        return new Category
        {
            Id = category.Id,
            Name = category.Name,
        };
    }
    
    public static CategoryTitle ConvertCategoryTitle(CategoryTitleDb categoryTitle)
    {
        return new CategoryTitle
        {
            Id = categoryTitle.Id,
            Name = categoryTitle.Name,
        };
    }
}