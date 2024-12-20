using Pcf.GivingToCustomer.WebHost.Infrastructure.Validators;
using Pcf.GivingToCustomer.WebHost.Models;
using System.Collections.Generic;
using System;
using Xunit;
using AutoFixture;
using FluentValidation.TestHelper;
using Pcf.GivingToCustomer.Core.Domain.Enums;
using System.Linq;

namespace Pcf.GivingToCustomer.IntegrationTests.Api.Infrastructure.Validators
{
	public class CustomerValidatorTests
	{
		private readonly CustomerValidator _validator;
		private readonly Fixture _fixture;

		public CustomerValidatorTests()
		{
			_validator = new CustomerValidator();
			_fixture = new Fixture();
		}

		[Fact]
		public void Should_HaveError_When_FirstNameIsEmpty()
		{
			// Arrange
			var request = _fixture.Build<CreateOrEditCustomerRequest>()
				.With(x => x.FirstName, "")
				.Create();

			// Act
			var result = _validator.TestValidate(request);

			// Assert
			result.ShouldHaveValidationErrorFor(x => x.FirstName);
		}

		[Fact]
		public void Should_HaveError_When_FirstNameExceedsMaxLength()
		{
			// Arrange
			var request = _fixture.Build<CreateOrEditCustomerRequest>()
				.With(x => x.FirstName, new string('A', 33))
				.Create();

			// Act
			var result = _validator.TestValidate(request);

			// Assert
			result.ShouldHaveValidationErrorFor(x => x.FirstName);
		}

		[Fact]
		public void Should_HaveError_When_EmailIsInvalid()
		{
			// Arrange
			var request = _fixture.Build<CreateOrEditCustomerRequest>()
				.With(x => x.Email, "invalid-email")
				.Create();

			// Act
			var result = _validator.TestValidate(request);

			// Assert
			result.ShouldHaveValidationErrorFor(x => x.Email);
		}

		[Fact]
		public void Should_HaveError_When_EmailIsEmpty()
		{
			// Arrange
			var request = _fixture.Build<CreateOrEditCustomerRequest>()
				.With(x => x.Email, "")
				.Create();

			// Act
			var result = _validator.TestValidate(request);

			// Assert
			result.ShouldHaveValidationErrorFor(x => x.Email);
		}

		[Fact]
		public void Should_HaveError_When_PreferenceIsInvalid()
		{
			// Arrange
			var request = _fixture.Build<CreateOrEditCustomerRequest>()
				.With(x => x.Preferences, new List<Preference> { Preference.None })
				.Create();

			// Act
			var result = _validator.TestValidate(request);

			// Assert
			result.ShouldHaveValidationErrorFor(x => x.Preferences);
		}

		[Fact]
		public void Should_NotHaveError_When_PreferencesAreValid()
		{
			// Arrange
			var validPreferences = new List<Preference> { Preference.Family, Preference.Children };

			var request = _fixture.Build<CreateOrEditCustomerRequest>()
				.With(x => x.Preferences, validPreferences)
				.Create();

			// Act
			var result = _validator.TestValidate(request);

			// Assert
			result.ShouldNotHaveValidationErrorFor(x => x.Preferences);
		}

		[Fact]
		public void Should_HaveCustomErrorMessage_When_InvalidPreferenceDetected()
		{
			// Arrange
			var invalidPreferences = new List<Preference> { Preference.None };
			var request = _fixture.Build<CreateOrEditCustomerRequest>()
				.With(x => x.Preferences, invalidPreferences)
				.Create();

			// Act
			var result = _validator.TestValidate(request);

			// Assert
			var errorMessage = $"Invalid Preference has been detected. You should choose any preferences from the pull: {string.Join(", ", Enum.GetValues(typeof(Preference)).Cast<Preference>().Where(preference => preference != Preference.None))}";
			result.ShouldHaveValidationErrorFor(x => x.Preferences)
				  .WithErrorMessage(errorMessage);
		}
	}
}
