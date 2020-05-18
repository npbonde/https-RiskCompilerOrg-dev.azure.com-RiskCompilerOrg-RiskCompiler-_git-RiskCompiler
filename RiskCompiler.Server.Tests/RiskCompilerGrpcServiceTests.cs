using FluentAssertions;
using RiskCompiler.Server.GrpcServices;
using RiskCompiler.ServiceLayer.Misc;
using RiskCompiler.Shared.DTO;
using System;
using System.Threading.Tasks;
using Xunit;

namespace RiskCompiler.Server.Tests
{
    public class RiskCompilerGrpcServiceTests
    {
        [Fact]
        public void ServiceRunner_succeeds_and_returns_VoidDto()
        {
            // Arrange

            // Act

            var result = RiskCompilerGrpcService.ServiceRunner(() => 
            {
                return new VoidDto(); 
            });

            // Assert

            result.Should().BeOfType(typeof(ValueTask<ReturnValueDto>));
            result.Result.Should().BeOfType(typeof(ReturnValueDto));
            result.Result.StatusCode.Should().Be(StatusCodeDto.OK);
            result.Result.StatusMessage.Should().BeNull();
            result.Result.Dto.Should().BeOfType(typeof(VoidDto));
        }

        [Fact]
        public void ServiceRunner_fails_and_throws_BizLogicException()
        {
            // Arrange

            // Act

            var result = RiskCompilerGrpcService.ServiceRunner(() =>
            {
                throw new BizLogicException(StatusCodeDto.InvalidArgument);
            });

            // Assert

            result.Should().BeOfType(typeof(ValueTask<ReturnValueDto>));
            result.Result.Should().BeOfType(typeof(ReturnValueDto));
            result.Result.StatusCode.Should().Be(StatusCodeDto.InvalidArgument);
            result.Result.StatusMessage.Should().BeNull();
            result.Result.Dto.Should().BeNull();
        }

        [Fact]
        public void ServiceRunner_fails_and_throws_NullReferenceException()
        {
            // Arrange

            // Act

            var result = RiskCompilerGrpcService.ServiceRunner(() =>
            {
                throw new NullReferenceException();
            });

            // Assert

            result.Should().BeOfType(typeof(ValueTask<ReturnValueDto>));
            result.Result.Should().BeOfType(typeof(ReturnValueDto));
            result.Result.StatusCode.Should().Be(StatusCodeDto.UnknownError);
            result.Result.StatusMessage.Should().BeNull();
            result.Result.Dto.Should().BeNull();
        }
    }
}
