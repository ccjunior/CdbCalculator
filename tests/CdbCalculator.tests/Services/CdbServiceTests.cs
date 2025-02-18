using CdbCalculator.API.Dtos;
using CdbCalculator.API.Services;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CdbCalculator.tests.Services
{
    public class CdbServiceTests
    {
        private readonly Mock<ICdbService> _cdbServiceMock;

        public CdbServiceTests()
        {
            _cdbServiceMock = new Mock<ICdbService>();
        }
        [Fact]
        public void InvestCalculate_WithValidInput_ReturnsCorrectValues()
        {
            var request = new CdbRequestDto(1000m, 12);

            var expectedResponse = new CdbResponseDto
            {
                GrossValue = 1200m,
                NetValue = 1150m
            };

            _cdbServiceMock
                .Setup(x => x.InvestCalculate(It.Is<CdbRequestDto>(r =>
                    r.InitialValue == request.InitialValue &&
                    r.DeadlineMonths == request.DeadlineMonths)))
                .Returns(expectedResponse);

            var result = _cdbServiceMock.Object.InvestCalculate(request);

            result.Should().NotBeNull();
            result.GrossValue.Should().Be(expectedResponse.GrossValue);
            result.NetValue.Should().Be(expectedResponse.NetValue);
            _cdbServiceMock.Verify(x => x.InvestCalculate(It.IsAny<CdbRequestDto>()), Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-1000)]
        public void InvestCalculate_WithInvalidInitialValue_ThrowsArgumentException(decimal initialValue)
        {
            var request = new CdbRequestDto(initialValue, 12);

            _cdbServiceMock
                .Setup(x => x.InvestCalculate(It.Is<CdbRequestDto>(r => r.InitialValue <= 0)))
                .Throws<ArgumentException>();

            var action = () => _cdbServiceMock.Object.InvestCalculate(request);
            action.Should().Throw<ArgumentException>();
            _cdbServiceMock.Verify(x => x.InvestCalculate(It.IsAny<CdbRequestDto>()), Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-12)]
        public void InvestCalculate_WithInvalidDeadlineMonths_ThrowsArgumentException(int months)
        {
            var request = new CdbRequestDto(1000m, months);

            _cdbServiceMock
                .Setup(x => x.InvestCalculate(It.Is<CdbRequestDto>(r => r.DeadlineMonths <= 0)))
                .Throws<ArgumentException>();

            var action = () => _cdbServiceMock.Object.InvestCalculate(request);
            action.Should().Throw<ArgumentException>();
            _cdbServiceMock.Verify(x => x.InvestCalculate(It.IsAny<CdbRequestDto>()), Times.Once);
        }

        [Fact]
        public void InvestCalculate_WithNullRequest_ThrowsArgumentNullException()
        {
            _cdbServiceMock
                .Setup(x => x.InvestCalculate(null))
                .Throws<ArgumentNullException>();

            var action = () => _cdbServiceMock.Object.InvestCalculate(null);
            action.Should().Throw<ArgumentNullException>();
            _cdbServiceMock.Verify(x => x.InvestCalculate(null), Times.Once);
        }

        [Theory]
        [InlineData(1000, 5)] // Até 6 meses
        [InlineData(1000, 12)] // Até 12 meses
        [InlineData(1000, 24)] // Até 24 meses
        [InlineData(1000, 36)] // Acima de 24 meses
        public void InvestCalculate_WithDifferentDeadlines_ReturnsExpectedValues(decimal initialValue, int months)
        {
            var request = new CdbRequestDto(initialValue, months);

            var expectedResponse = new CdbResponseDto
            {
                GrossValue = initialValue * 1.1m, // Valor exemplo
                NetValue = initialValue * 1.08m   // Valor exemplo
            };

            _cdbServiceMock
                .Setup(x => x.InvestCalculate(It.Is<CdbRequestDto>(r =>
                    r.InitialValue == initialValue &&
                    r.DeadlineMonths == months)))
                .Returns(expectedResponse);

            var result = _cdbServiceMock.Object.InvestCalculate(request);

            result.Should().NotBeNull();
            result.GrossValue.Should().Be(expectedResponse.GrossValue);
            result.NetValue.Should().Be(expectedResponse.NetValue);
            _cdbServiceMock.Verify(x => x.InvestCalculate(It.IsAny<CdbRequestDto>()), Times.Once);
        }

        [Fact]
        public void InvestCalculate_VerifyPrecision_ReturnsValuesWithTwoDecimalPlaces()
        {
            var request = new CdbRequestDto(1000.999m, 12);

            var expectedResponse = new CdbResponseDto
            {
                GrossValue = 1200.45m,
                NetValue = 1150.32m
            };

            _cdbServiceMock
                .Setup(x => x.InvestCalculate(It.IsAny<CdbRequestDto>()))
                .Returns(expectedResponse);

            var result = _cdbServiceMock.Object.InvestCalculate(request);

            result.GrossValue.Should().Be(Math.Round(expectedResponse.GrossValue, 2));
            result.NetValue.Should().Be(Math.Round(expectedResponse.NetValue, 2));
            _cdbServiceMock.Verify(x => x.InvestCalculate(It.IsAny<CdbRequestDto>()), Times.Once);
        }
    }
}
