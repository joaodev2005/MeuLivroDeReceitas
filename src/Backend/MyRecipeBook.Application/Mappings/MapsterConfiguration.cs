using System.Runtime.CompilerServices;
using Mapster;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Entities;

[assembly: InternalsVisibleTo("UseCase.Tests")]
namespace MyRecipeBook.Application.Mappings;

internal static class MapsterConfiguration
{
    internal static void Configure()
    {
        TypeAdapterConfig<RequestRegisterUserAccountJson, User>
            .NewConfig()
            .Ignore(destination => destination.Password);

        TypeAdapterConfig<RequestRecipeJson, Recipe>
            .NewConfig()
            .Map(destination => destination.Ingredients, request => request.Ingredients.Select(ingredient => new RecipeIngredient
            {
                Item = ingredient
            }))
            .Map(destination => destination.DishTypes, request => request.DishTypes.Select(dishType => new RecipeDishType
            {
                Type = (Domain.Enums.DishType)dishType
            }))
            .Map(destination => destination.Instructions, request => request.Instructions.Select(instruction => new RecipeInstruction
            {
                Order = instruction.Order,
                Description = instruction.Description
            }));
    }
}
