﻿using AutoFixture;
using FluentValidation.TestHelper;
using PromoCodeFactory.Core.Helpers;
using PromoCodeFactory.WebHost.Infrastructure.Validators;
using PromoCodeFactory.WebHost.Models.Request.Partner;

namespace PromoCodeFactory.UnitTests.WebHost.Infrastructure.Validators
{
	public class PartnerPromocodeLimitValidatorTests
	{
		private readonly PartnerPromocodeLimitValidator _partnerPromocodeLimitValidator;

		public PartnerPromocodeLimitValidatorTests()
		{
			_partnerPromocodeLimitValidator = new PartnerPromocodeLimitValidator();
		}

		[Fact]
		public void ValidateSetPartnerPromoCodeLimitRequest_WhenRequestIsCorrect_ShouldBeValidatedProperly()
		{
			// Arrange
			var autoFixture = new Fixture();
			var requestDto = autoFixture
				.Build<SetPartnerPromoCodeLimitRequest>()
				.With(x => x.Limit, 5)
				.With(x => x.EndDate, DateTime.Now.AddYears(5).ToDateString())
				.Create();

			// Act
			var response = _partnerPromocodeLimitValidator.TestValidate(requestDto);

			// Assert
			response.ShouldNotHaveValidationErrorFor(x => x.Limit);
			response.ShouldNotHaveValidationErrorFor(x => x.EndDate);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(-5)]
		public void ValidateSetPartnerPromoCodeLimitRequest_WhenLimitHasUnsuitableValue_ShouldBeFailedWithLimitValidation(int limit)
		{
			// Arrange
			var autoFixture = new Fixture();
			var requestDto = autoFixture
				.Build<SetPartnerPromoCodeLimitRequest>()
				.With(x => x.Limit, limit)
				.With(x => x.EndDate, DateTime.Now.AddYears(5).ToDateString())
				.Create();

			// Act
			var response = _partnerPromocodeLimitValidator.TestValidate(requestDto);

			// Assert
			response.ShouldHaveValidationErrorFor(x => x.Limit).Only();
			response.ShouldNotHaveValidationErrorFor(x => x.EndDate);
		}
	}
}
