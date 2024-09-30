using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using PromoCodeFactory.BusinessLogic.Models.Partner;
using PromoCodeFactory.BusinessLogic.Services.Implementation;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.Core.Exceptions;
using PromoCodeFactory.Core.Helpers;
using System.Linq.Expressions;

namespace PromoCodeFactory.UnitTests.BusinessLogic
{
	public class PartnerServiceTests
	{
		private readonly Mock<IRepository<Partner, Guid>> _partnerRepositoryMock;
		private readonly PartnerService _partnerService;

		public PartnerServiceTests()
		{
			var fixture = new Fixture().Customize(new AutoMoqCustomization());
			_partnerRepositoryMock = fixture.Freeze<Mock<IRepository<Partner, Guid>>>();
			_partnerService = new PartnerService(_partnerRepositoryMock.Object);
		}

		[Fact]
		public async Task SetPartnerPromoCodeLimitAsync_WhenSetNewLimit_ShouldReturnPartnerPromoCodeLimitDto()
		{
			// Arrange
			var autoFixture = new Fixture();
			var partnerId = Guid.Parse("def47943-7aaf-44a1-ae21-05aa4948b165");
			var endDateTime = DateTime.Now.AddYears(5);
			var limit = 5;
			var partner = autoFixture
				.Build<Partner>()
				.With(
					x => x.PartnerLimits,
					new List<PartnerPromoCodeLimit>
					{
						new PartnerPromoCodeLimit
						{
							CancelDate = null,
							EndDate = endDateTime
						}
					})
				.Create();

			var requestDto = autoFixture
				.Build<PartnerPromoCodeLimitRequestDto>()
				.With(x => x.EndDate, endDateTime.ToDateString())
				.With(x => x.Limit, limit)
				.Create();

			var expectedResult = autoFixture
				.Build<PartnerPromoCodeLimitDto>()
				.Without(x => x.Partner)
				.With(x => x.CancelDate, (string)null)
				.With(x => x.CreateDate, DateTime.Now.ToDateString())
				.With(x => x.EndDate, endDateTime.ToDateString())
				.With(x => x.Limit, limit)
				.Create();

			_partnerRepositoryMock
				.Setup(x => x.GetByIdAsync(It.IsAny<Expression<Func<Partner, bool>>>(), nameof(Partner.PartnerLimits), false))
				.ReturnsAsync(partner);

			// Act
			var actionResult = await _partnerService.SetPartnerPromoCodeLimitAsync(partnerId, requestDto);

			// Assert
			Assert.Equal(expectedResult.Limit, actionResult.Limit);
			Assert.Equal(expectedResult.CreateDate, actionResult.CreateDate);
			Assert.Equal(expectedResult.EndDate, actionResult.EndDate);
			Assert.Null(actionResult.CancelDate);
		}

		[Fact]
		public async Task SetPartnerPromoCodeLimitAsync_WhenSetNewLimitWithInActivePreviousLimit_CancelDateForPreviousLimitShouldNotBeSetup()
		{
			// Arrange
			var autoFixture = new Fixture();
			var partnerId = Guid.Parse("def47943-7aaf-44a1-ae21-05aa4948b165");
			var endDateTime = DateTime.Now.AddYears(-1);
			var partner = autoFixture
				.Build<Partner>()
				.With(x => x.Id, partnerId)
				.With(
					x => x.PartnerLimits,
					new List<PartnerPromoCodeLimit>
					{
						new PartnerPromoCodeLimit
						{
							CancelDate = null,
							EndDate = endDateTime
						}
					})
				.Create();

			var requestDto = autoFixture
				.Build<PartnerPromoCodeLimitRequestDto>()
				.With(x => x.EndDate, DateTime.Now.AddYears(1).ToDateString())
				.Create();

			var expectedResult = autoFixture
				.Build<PartnerPromoCodeLimitDto>()
				.Without(x => x.Partner)
				.With(x => x.CancelDate, (string)null)
				.With(x => x.EndDate, endDateTime.ToDateString())
				.Create();

			_partnerRepositoryMock
				.Setup(x => x.GetByIdAsync(It.IsAny<Expression<Func<Partner, bool>>>(), nameof(Partner.PartnerLimits), false))
				.ReturnsAsync(partner);

			// Act
			var actionResult = await _partnerService.SetPartnerPromoCodeLimitAsync(partnerId, requestDto);

			// Assert
			Assert.Contains(actionResult.Partner.PartnerLimits,
				patnerLimit => patnerLimit.CancelDate == expectedResult.CancelDate && patnerLimit.EndDate.Equals(expectedResult.EndDate));
		}

		[Fact]
		public async Task SetPartnerPromoCodeLimitAsync_WhenSetNewLimitWithActivePreviousLimit_CancelDateForPreviousLimitShouldBeSetup()
		{
			// Arrange
			var autoFixture = new Fixture();
			var partnerId = Guid.Parse("def47943-7aaf-44a1-ae21-05aa4948b165");
			var endDateTime = DateTime.Now.AddYears(5);
			var partner = autoFixture
				.Build<Partner>()
				.With(x => x.Id, partnerId)
				.With(
					x => x.PartnerLimits,
					new List<PartnerPromoCodeLimit>
					{
						new PartnerPromoCodeLimit
						{
							CancelDate = null,
							EndDate = endDateTime
						}
					})
				.Create();

			var requestDto = autoFixture
				.Build<PartnerPromoCodeLimitRequestDto>()
				.With(x => x.EndDate, endDateTime.ToDateString())
				.Create();

			var expectedResult = autoFixture
				.Build<PartnerPromoCodeLimitDto>()
				.Without(x => x.Partner)
				.With(x => x.CancelDate, DateTime.Now.ToDateString())
				.With(x => x.EndDate, endDateTime.ToDateString())
				.Create();

			_partnerRepositoryMock
				.Setup(x => x.GetByIdAsync(It.IsAny<Expression<Func<Partner, bool>>>(), nameof(Partner.PartnerLimits), false))
				.ReturnsAsync(partner);

			// Act
			var actionResult = await _partnerService.SetPartnerPromoCodeLimitAsync(partnerId, requestDto);

			// Assert
			Assert.Contains(actionResult.Partner.PartnerLimits,
				patnerLimit => patnerLimit.CancelDate != null
					&& patnerLimit.CancelDate.Equals(expectedResult.CancelDate)
					&& patnerLimit.EndDate.Equals(expectedResult.EndDate));
		}

		[Fact]
		public async Task SetPartnerPromoCodeLimitAsync_WhenSetNewLimitWithInactivePreviousLimit_NumberIssuedPromoCodesShouldNotBeReset()
		{
			// Arrange
			var autoFixture = new Fixture();
			var partnerId = Guid.Parse("def47943-7aaf-44a1-ae21-05aa4948b165");
			var numberIssuedPromoCodes = 5;
			var partner = autoFixture
				.Build<Partner>()
				.With(x => x.Id, partnerId)
				.With(x => x.NumberIssuedPromoCodes, numberIssuedPromoCodes)
				.With(
					x => x.PartnerLimits,
					new List<PartnerPromoCodeLimit>
					{
						new PartnerPromoCodeLimit
						{
							CancelDate = null,
							EndDate = DateTime.Now.AddYears(-1)
						}
					})
				.Create();

			var requestDto = autoFixture
				.Build<PartnerPromoCodeLimitRequestDto>()
				.With(x => x.EndDate, DateTime.Now.AddYears(1).ToDateString())
				.Create();

			var expectedNumberIssuedPromoCodes = 5;

			_partnerRepositoryMock
				.Setup(x => x.GetByIdAsync(It.IsAny<Expression<Func<Partner, bool>>>(), nameof(Partner.PartnerLimits), false))
				.ReturnsAsync(partner);

			// Act
			var actionResult = await _partnerService.SetPartnerPromoCodeLimitAsync(partnerId, requestDto);

			// Assert
			Assert.Equal(expectedNumberIssuedPromoCodes, actionResult.Partner.NumberIssuedPromoCodes);
		}

		[Fact]
		public async Task SetPartnerPromoCodeLimitAsync_WhenSetNewLimitWithActivePreviousLimit_NumberIssuedPromoCodesShouldBeReset()
		{
			// Arrange
			var autoFixture = new Fixture();
			var partnerId = Guid.Parse("def47943-7aaf-44a1-ae21-05aa4948b165");
			var numberIssuedPromoCodes = 5;
			var partner = autoFixture
				.Build<Partner>()
				.With(x => x.Id, partnerId)
				.With(x => x.NumberIssuedPromoCodes, numberIssuedPromoCodes)
				.With(
					x => x.PartnerLimits,
					new List<PartnerPromoCodeLimit>
					{
						new PartnerPromoCodeLimit
						{
							CancelDate = null,
							EndDate = DateTime.Now.AddYears(5)
						}
					})
				.Create();

			var requestDto = autoFixture
				.Build<PartnerPromoCodeLimitRequestDto>()
				.With(x => x.EndDate, DateTime.Now.AddYears(1).ToDateString())
				.Create();

			var expectedNumberIssuedPromoCodes = 0;

			_partnerRepositoryMock
				.Setup(x => x.GetByIdAsync(It.IsAny<Expression<Func<Partner, bool>>>(), nameof(Partner.PartnerLimits), false))
				.ReturnsAsync(partner);

			// Act
			var actionResult = await _partnerService.SetPartnerPromoCodeLimitAsync(partnerId, requestDto);

			// Assert
			Assert.Equal(expectedNumberIssuedPromoCodes, actionResult.Partner.NumberIssuedPromoCodes);
		}

		[Fact]
		public async Task SetPartnerPromoCodeLimitAsync_VerifySaveChangesRepositoryCall_SaveChangesCalled()
		{
			// Arrange
			var autoFixture = new Fixture();
			var partnerId = Guid.Parse("def47943-7aaf-44a1-ae21-05aa4948b165");
			var partner = autoFixture
				.Build<Partner>()
				.With(x => x.Id, partnerId)
				.With(
					x => x.PartnerLimits,
					new List<PartnerPromoCodeLimit>
					{
						new PartnerPromoCodeLimit
						{
							CancelDate = null,
							EndDate = DateTime.Now.AddYears(5)
						}
					})
				.Create();

			var requestDto = autoFixture
				.Build<PartnerPromoCodeLimitRequestDto>()
				.With(x => x.EndDate, DateTime.Now.AddYears(1).ToDateString())
				.Create();

			_partnerRepositoryMock
				.Setup(x => x.GetByIdAsync(It.IsAny<Expression<Func<Partner, bool>>>(), nameof(Partner.PartnerLimits), false))
				.ReturnsAsync(partner);

			// Act
			await _partnerService.SetPartnerPromoCodeLimitAsync(partnerId, requestDto);

			// Assert
			_partnerRepositoryMock.Verify(s => s.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
		}

		[Theory]
		[MemberData(nameof(PartnerExceptionDataForSetMethod))]
		public async Task SetPartnerPromoCodeLimitAsync_ReturnsException(
			Guid partnerId,
			Partner partner,
			Exception expectedException,
			string exceptionMessage,
			PartnerPromoCodeLimitRequestDto dto)
		{
			// Arrange
			_partnerRepositoryMock
				.Setup(x => x.GetByIdAsync(It.IsAny<Expression<Func<Partner, bool>>>(), nameof(Partner.PartnerLimits), false))
				.ReturnsAsync(partner);

			// Act
			Func<Task> act = () => _partnerService.SetPartnerPromoCodeLimitAsync(partnerId, dto);

			// Assert
			var exception = expectedException switch
			{
				NotFoundException => await Assert.ThrowsAsync<NotFoundException>(act),
				BadRequestException => await Assert.ThrowsAsync<BadRequestException>(act),
				_ => await Assert.ThrowsAsync<Exception>(act),
			};

			Assert.Equal(exceptionMessage, exception.Message);
		}

		[Fact]
		public async Task CancelPartnerPromoCodeLimitAsync_VerifySaveChangesRepositoryCall_SaveChangesCalled()
		{
			// Arrange
			var partnerId = Guid.Parse("def47943-7aaf-44a1-ae21-05aa4948b165");
			var partner = new Fixture()
				.Build<Partner>()
				.With(x => x.Id, partnerId)
				.With(
					x => x.PartnerLimits,
					new List<PartnerPromoCodeLimit>
					{
						new PartnerPromoCodeLimit
						{
							CancelDate = null,
							EndDate = DateTime.Now.AddYears(5)
						}
					})
				.Create();

			_partnerRepositoryMock
				.Setup(x => x.GetByIdAsync(It.IsAny<Expression<Func<Partner, bool>>>(), nameof(Partner.PartnerLimits), false))
				.ReturnsAsync(partner);

			// Act
			await _partnerService.CancelPartnerPromoCodeLimitAsync(partnerId);

			// Assert
			_partnerRepositoryMock.Verify(s => s.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
		}

		[Theory]
		[MemberData(nameof(PartnerExceptionDataForCancelMethod))]
		public async Task CancelPartnerPromoCodeLimitAsync_ReturnsException(
			Guid partnerId,
			Partner partner,
			Exception expectedException,
			string exceptionMessage)
		{
			// Arrange
			_partnerRepositoryMock
				.Setup(x => x.GetByIdAsync(It.IsAny<Expression<Func<Partner, bool>>>(), nameof(Partner.PartnerLimits), false))
				.ReturnsAsync(partner);

			// Act
			Func<Task> act = () => _partnerService.CancelPartnerPromoCodeLimitAsync(partnerId);

			// Assert
			var exception = expectedException switch
			{
				NotFoundException => await Assert.ThrowsAsync<NotFoundException>(act),
				BadRequestException => await Assert.ThrowsAsync<BadRequestException>(act),
				_ => await Assert.ThrowsAsync<Exception>(act),
			};

			Assert.Equal(exceptionMessage, exception.Message);
		}

		public static IEnumerable<object[]> PartnerExceptionDataForCancelMethod()
		{
			var partnerId = Guid.Parse("def47943-7aaf-44a1-ae21-05aa4948b165");

			return new List<object[]>
			{
					new object[]
					{
						partnerId,
						null,
						new NotFoundException(),
						$"The {nameof(Partner)} with Id {partnerId} has not been found.",
					},
					new object[]
					{
						partnerId,
						new Fixture()
							.Build<Partner>()
							.With(x => x.Id, partnerId)
							.With(x => x.IsActive, false)
							.Without(x => x.PartnerLimits)
							.Create(),
						new BadRequestException(),
						$"The {nameof(Partner)} with id: {partnerId} is not active."
					},
					new object[]
					{
						partnerId,
						new Fixture()
						.Build<Partner>()
						.With(x => x.Id, partnerId)
						.With(
							x => x.PartnerLimits,
							new List<PartnerPromoCodeLimit>
							{
								new PartnerPromoCodeLimit
								{
									CancelDate = null,
									EndDate = DateTime.Now.AddYears(-1)
								}
							})
						.Create(),
						new BadRequestException(),
						$"No active {nameof(Partner.PartnerLimits)} found for {nameof(Partner)} with id: {partnerId}."
					}
			};
		}

		public static IEnumerable<object[]> PartnerExceptionDataForSetMethod()
		{
			var partnerId = Guid.Parse("def47943-7aaf-44a1-ae21-05aa4948b165");

			return new List<object[]>
			{
					new object[]
					{
						partnerId,
						null,
						new NotFoundException(),
						$"The {nameof(Partner)} with Id {partnerId} has not been found.",
						new Fixture().Create<PartnerPromoCodeLimitRequestDto>(),
					},
					new object[]
					{
						partnerId,
						new Fixture()
							.Build<Partner>()
							.With(x => x.Id, partnerId)
							.With(x => x.IsActive, false)
							.Without(x => x.PartnerLimits)
							.Create(),
						new BadRequestException(),
						$"The {nameof(Partner)} with id: {partnerId} is not active.",
						new Fixture().Create<PartnerPromoCodeLimitRequestDto>(),
					}
			};
		}
	}
}
