using Xunit;
using System.Linq;
using SampleProject.Utils.Models;
using SampleProject.Utils.Tests.Unit.Mocks;

namespace SampleProject.Utils.Tests.Unit.Tests;

[Trait("Category", "Unit")]
public class ResultTests
{
    [Fact]
    public void Success_CreateSuccessResult_ReturnsResult()
    {
        // Act
        var result = Result.Success();

        // Assert
        Assert.False(result.IsError);
        Assert.Empty(result.ErrorCodes);
    }

    [Fact]
    public void SuccessOfT_CreateSuccessResult_ReturnsResult()
    {
        // Arrange
        var model = new MockModel();

        // Act
        var result = Result<MockModel>.Success(model);

        // Assert
        Assert.NotNull(result.Data);
        Assert.Equal(model, result.Data);

        Assert.False(result.IsError);
        Assert.Empty(result.ErrorCodes);
    }

    [Theory]
    [InlineData(CommonErrorCodes.Conflict)]
    [InlineData(CommonErrorCodes.NotFound)]
    [InlineData(CommonErrorCodes.Forbidden)]
    [InlineData("ERROR_CODE")]
    [InlineData("ERROR_CODE", "ANOTHER_ERROR_CODE")]
    public void Fail_CreateFailResultWithErrorCode_ReturnsFailResult(params string[] errorCodes)
    {
        // Act
        var result = Result.Fail(errorCodes);

        // Assert
        Assert.True(result.IsError);
        Assert.NotEmpty(result.ErrorCodes);
        Assert.Equal(errorCodes, result.ErrorCodes);
    }

    [Theory]
    [InlineData(CommonErrorCodes.Conflict)]
    [InlineData(CommonErrorCodes.NotFound)]
    [InlineData(CommonErrorCodes.Forbidden)]
    [InlineData("ERROR_CODE")]
    [InlineData("ERROR_CODE", "ANOTHER_ERROR_CODE")]
    public void FailOfT_CreateFailResultWithErrorCode_ReturnsFailResult(params string[] errorCodes)
    {
        // Act
        var result = Result<MockModel>.Fail(errorCodes);

        // Assert
        Assert.Null(result.Data);

        Assert.True(result.IsError);
        Assert.NotEmpty(result.ErrorCodes);
        Assert.Equal(errorCodes, result.ErrorCodes);
    }

    [Theory]
    [InlineData(CommonErrorCodes.Conflict)]
    [InlineData(CommonErrorCodes.NotFound)]
    [InlineData(CommonErrorCodes.Forbidden)]
    [InlineData("ERROR_CODE")]
    [InlineData("ERROR_CODE", "ANOTHER_ERROR_CODE")]
    public void FailOfT_CreateFailResultWithDataAndErrorCode_ReturnsFailResult(params string[] errorCodes)
    {
        // Arrange
        var model = new MockModel();

        // Act
        var result = Result<MockModel>.Fail(model, errorCodes);

        // Assert
        Assert.NotNull(result.Data);
        Assert.Equal(model, result.Data);

        Assert.True(result.IsError);
        Assert.NotEmpty(result.ErrorCodes);
        Assert.Equal(errorCodes, result.ErrorCodes);
    }

    [Fact]
    public void Fail_CreateFailResultWithNoErrorCode_ReturnsFailResult()
    {
        // Act
        var result = Result.Fail();

        // Assert
        Assert.True(result.IsError);
        Assert.Empty(result.ErrorCodes);
    }

    [Fact]
    public void FailOfT_CreateFailResultWithNoDataAndNoErrorCode_ReturnsFailResult()
    {
        // Act
        var result = Result<MockModel>.Fail();

        // Assert
        Assert.Null(result.Data);

        Assert.True(result.IsError);
        Assert.Empty(result.ErrorCodes);
    }

    [Fact]
    public void FailOfT_CreateFailResultWithDataAndNoErrorCode_ReturnsFailResult()
    {
        // Arrange
        var model = new MockModel();

        // Act
        var result = Result<MockModel>.Fail(model);

        // Assert
        Assert.NotNull(result.Data);
        Assert.Equal(model, result.Data);

        Assert.True(result.IsError);
        Assert.Empty(result.ErrorCodes);
    }

    [Fact]
    public void Fail_CreateFailResultAndAddError_ReturnsFailResult()
    {
        // Act
        var errorCode = "ERROR_CODE";
        var result = Result.Fail();
        result.AddErrorCode(errorCode);

        // Assert
        Assert.True(result.IsError);
        Assert.NotEmpty(result.ErrorCodes);
        Assert.Equal(errorCode, result.ErrorCodes.First());
        Assert.Single(result.ErrorCodes);
    }

    [Fact]
    public void FailOfT_CreateFailResultWithNoDataAndAddError_ReturnsFailResult()
    {
        // Act
        var errorCode = "ERROR_CODE";
        var result = Result<MockModel>.Fail();
        result.AddErrorCode(errorCode);

        // Assert
        Assert.Null(result.Data);

        Assert.True(result.IsError);
        Assert.NotEmpty(result.ErrorCodes);
        Assert.Equal(errorCode, result.ErrorCodes.First());
        Assert.Single(result.ErrorCodes);
    }

    [Fact]
    public void FailOfT_CreateFailResultWithDataAndAddError_ReturnsFailResult()
    {
        // Arrange
        var model = new MockModel();

        // Act
        var errorCode = "ERROR_CODE";
        var result = Result<MockModel>.Fail(model);
        result.AddErrorCode(errorCode);

        // Assert
        Assert.NotNull(result.Data);
        Assert.Equal(model, result.Data);

        Assert.True(result.IsError);
        Assert.NotEmpty(result.ErrorCodes);
        Assert.Equal(errorCode, result.ErrorCodes.First());
        Assert.Single(result.ErrorCodes);
    }

    [Theory]
    [InlineData(CommonErrorCodes.Conflict)]
    [InlineData(CommonErrorCodes.NotFound)]
    [InlineData(CommonErrorCodes.Forbidden)]
    [InlineData("ERROR_CODE")]
    [InlineData("ERROR_CODE", "ANOTHER_ERROR_CODE")]
    public void Fail_CreateFailResultWithErrorAndAddError_ReturnsFailResult(params string[] errorCodes)
    {
        // Act
        var errorCode = "YET_ANOTHER_ERROR_CODE";
        var result = Result.Fail(errorCodes);
        result.AddErrorCode(errorCode);

        // Assert
        Assert.True(result.IsError);
        Assert.NotEmpty(result.ErrorCodes);
        Assert.Contains(errorCode, result.ErrorCodes);
    }

    [Theory]
    [InlineData(CommonErrorCodes.Conflict)]
    [InlineData(CommonErrorCodes.NotFound)]
    [InlineData(CommonErrorCodes.Forbidden)]
    [InlineData("ERROR_CODE")]
    [InlineData("ERROR_CODE", "ANOTHER_ERROR_CODE")]
    public void FailOfT_CreateFailResultWithNoDataAndErrorCodeAndAddError_ReturnsFailResult(params string[] errorCodes)
    {
        // Act
        var errorCode = "YET_ANOTHER_ERROR_CODE";
        var result = Result<MockModel>.Fail(errorCodes);
        result.AddErrorCode(errorCode);

        // Assert
        Assert.Null(result.Data);

        Assert.True(result.IsError);
        Assert.NotEmpty(result.ErrorCodes);
        Assert.Contains(errorCode, result.ErrorCodes);
    }

    [Theory]
    [InlineData(CommonErrorCodes.Conflict)]
    [InlineData(CommonErrorCodes.NotFound)]
    [InlineData(CommonErrorCodes.Forbidden)]
    [InlineData("ERROR_CODE")]
    [InlineData("ERROR_CODE", "ANOTHER_ERROR_CODE")]
    public void FailOfT_CreateFailResultWithDataAndErrorCodeAndAddError_ReturnsFailResult(params string[] errorCodes)
    {
        // Arrange
        var model = new MockModel();

        // Act
        var errorCode = "YET_ANOTHER_ERROR_CODE";
        var result = Result<MockModel>.Fail(model, errorCodes);
        result.AddErrorCode(errorCode);

        // Assert
        Assert.NotNull(result.Data);
        Assert.Equal(model, result.Data);

        Assert.True(result.IsError);
        Assert.NotEmpty(result.ErrorCodes);
        Assert.Contains(errorCode, result.ErrorCodes);
    }
}