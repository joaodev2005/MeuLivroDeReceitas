using System.Diagnostics.CodeAnalysis;
using CommonTestUtilities.Requests;
using MyRecipeBook.Application.UseCases.Recipe;
using MyRecipeBook.Communication.Enums;
using MyRecipeBook.Exception;
using Shouldly;

namespace Validators.Tests.Recipe;

public class RecipeValidatorTests
{
    [Fact]
    public void Success()
    {
        var validator = new RecipeValidator();

        var request = RequestRecipeJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.ShouldBeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("         ")]
    [SuppressMessage("Usage", "xUnit1026:Theory methods should use all of their parameters", Justification = "The parameter is used in the test case.")]
    public void Validate_ShouldHaveError_WhenTitleIsEmpty(string title)
    {
        var validator = new RecipeValidator();

        var request = RequestRecipeJsonBuilder.Build();
        request.Title = title;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceMessagesException.VALIDATION_TITLE_REQUIRED));
        });
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenTitleIsTooLong()
    {
        var validator = new RecipeValidator();

        var request = RequestRecipeJsonBuilder.Build();
        request.Title = new string('a', 251); 

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceMessagesException.VALIDATION_TITLE_MAX_LENGTH));
        });
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenCookTimeIsInvalid()
    {
        var validator = new RecipeValidator();

        var request = RequestRecipeJsonBuilder.Build();
        request.CookTime = (CookTime)1000;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceMessagesException.VALIDATION_COOK_TIME_INVALID));
        });
    }

    [Fact]
    public void Validate_shouldHaveError_WhenThereIsNotDishType()
    {
        var validator = new RecipeValidator();

        var request = RequestRecipeJsonBuilder.Build();
        request.DishTypes = [];

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceMessagesException.VALIDATION_AT_LEAST_ONE_DISH_TYPE));
        });
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenDishTypeIsInvalid()
    {
        var validator = new RecipeValidator();

        var request = RequestRecipeJsonBuilder.Build();
        request.DishTypes = [(MyRecipeBook.Communication.Enums.DishType)1000];

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceMessagesException.VALIDATION_DISH_TYPE_INVALID));
        });
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenThereIsNotIngredient()
    {
        var validator = new RecipeValidator();

        var request = RequestRecipeJsonBuilder.Build();
        request.Ingredients = [];

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceMessagesException.VALIDATION_AT_LEAST_ONE_INGREDIENT));
        });
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("         ")]
    [SuppressMessage("Usage", "xUnit1026:Theory methods should use all of their parameters", Justification = "The parameter is used in the test case.")]
    public void Validate_ShouldHaveError_WhenIngredientIsEmpty(string ingredient)
    {
        var validator = new RecipeValidator();

        var request = RequestRecipeJsonBuilder.Build();
        request.Ingredients = [ingredient];

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceMessagesException.VALIDATION_INGREDIENT_EMPTY));
        });
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenIngredientIsTooLong()
    {
        var validator = new RecipeValidator();

        var request = RequestRecipeJsonBuilder.Build();
        request.Ingredients = [new string('a', 251)];

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceMessagesException.VALIDATION_INGREDIENT_MAX_LENGTH));
        });
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-5)]
    public void Validate_ShouldHaveError_WhenInstructionOrderIsInvalid(int order)
    {
        var validator = new RecipeValidator();

        var request = RequestRecipeJsonBuilder.Build();
        request.Instructions = [new MyRecipeBook.Communication.Requests.RequestRecipeInstructionJson { Order = order, Description = "Valid description" }];

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceMessagesException.VALIDATION_INSTRUCTION_ORDER_INVALID));
        });
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("         ")]
    [SuppressMessage("Usage", "xUnit1026:Theory methods should use all of their parameters", Justification = "The parameter is used in the test case.")]
    public void Validate_ShouldHaveError_WhenInstructionDescriptionIsEmpty(string description)
    {
        var validator = new RecipeValidator();

        var request = RequestRecipeJsonBuilder.Build();
        request.Instructions = [new MyRecipeBook.Communication.Requests.RequestRecipeInstructionJson { Order = 1, Description = description }];

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceMessagesException.VALIDATION_INSTRUCTION_DESCRIPTION_REQUIRED));
        });
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenInstructionDescriptionIsTooLong()
    {
        var validator = new RecipeValidator();

        var request = RequestRecipeJsonBuilder.Build();
        request.Instructions = [new MyRecipeBook.Communication.Requests.RequestRecipeInstructionJson { Order = 1, Description = new string('a', 2001) }];

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceMessagesException.VALIDATION_INSTRUCTION_DESCRIPTION_MAX_LENGTH));
        });
    }

    [Fact]
    public void Validate_shouldHaveError_WhenInstructionsHaveDuplicateOrders()
    {
        var validator = new RecipeValidator();

        var request = RequestRecipeJsonBuilder.Build();
        request.Instructions = [
            new MyRecipeBook.Communication.Requests.RequestRecipeInstructionJson { Order = 1, Description = "Valid description" },
            new MyRecipeBook.Communication.Requests.RequestRecipeInstructionJson { Order = 1, Description = "Another valid description" }
        ];

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceMessagesException.VALIDATION_INSTRUCTION_DUPLICATE_ORDER));
        });
    }
}
