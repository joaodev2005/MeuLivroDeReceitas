using System.Net;
using MyRecipeBook.Communication.Requests;
using Shouldly;

namespace WebApi.Tests.User.Profile;

public class GetUserProfileInvalidTokenTests : BaseIntegrationTest
{
    private const string REQUEST_URI = "/users/profile";

    private readonly string _tokenUserNotExistDatabase;

    public GetUserProfileInvalidTokenTests(MyRecipeBookApplicationFactory factory) : base(factory)
    {
        _tokenUserNotExistDatabase = factory.TOKEN_USER_NOT_FOUND_IN_DATABASE;
    }

    [Fact]
    public async Task Error_Token_Invalid()
    {
        var request = new RequestChangePasswordJson();

        var response = await Put(REQUEST_URI, request, accessToken: "tokenInvalid");

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Error_Withou_Invalid()
    {
        var request = new RequestChangePasswordJson();

        var response = await Put(REQUEST_URI, request, accessToken: string.Empty);

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);

    }

    [Fact]
    public async Task Error_Token_With_User_NotFound()
    {
        var request = new RequestChangePasswordJson();

        var response = await Put(REQUEST_URI, request, _tokenUserNotExistDatabase);

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
}
