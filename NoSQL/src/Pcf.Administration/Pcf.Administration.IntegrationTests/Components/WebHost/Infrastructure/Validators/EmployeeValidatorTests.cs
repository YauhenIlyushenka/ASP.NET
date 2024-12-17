using AutoFixture;
using FluentValidation.TestHelper;
using Pcf.Administration.Core.Domain.Enums;
using Pcf.Administration.WebHost.Infrastructure.Validators;
using Pcf.Administration.WebHost.Models.Request;
using System;
using System.Linq;
using Xunit;

namespace Pcf.Administration.IntegrationTests.Components.WebHost.Infrastructure.Validators
{
	public class EmployeeValidatorTests
	{
		private readonly IFixture _fixture;
		private readonly EmployeeValidator _validator;

		public EmployeeValidatorTests()
		{
			_fixture = new Fixture();
			_validator = new EmployeeValidator();
		}

		[Fact]
		public void Should_HaveError_When_FirstName_Is_Empty()
		{
			// Arrange
			_fixture.Customize<Role>(c => c.FromFactory(() =>
			{
				var values = Enum.GetValues(typeof(Role)).Cast<Role>().ToList();
				values.Remove(Role.None);

				return values[_fixture.Create<int>() % values.Count];  // Random Role, except None
			}));

			var model = _fixture
				.Build<EmployeeRequest>()
				.With(x => x.FirstName, string.Empty)
				.Create();

			// Act
			var result = _validator.TestValidate(model);

			// Assert
			result.ShouldHaveValidationErrorFor(x => x.FirstName);
		}

		[Fact]
		public void Should_HaveError_When_LastName_Is_Empty()
		{
			// Arrange
			_fixture.Customize<Role>(c => c.FromFactory(() =>
			{
				var values = Enum.GetValues(typeof(Role)).Cast<Role>().ToList();
				values.Remove(Role.None);

				return values[_fixture.Create<int>() % values.Count];  // Random Role, except None
			}));

			var model = _fixture
				.Build<EmployeeRequest>()
				.With(x => x.LastName, string.Empty)
				.Create();

			// Act
			var result = _validator.TestValidate(model);

			// Assert
			result.ShouldHaveValidationErrorFor(x => x.LastName);
		}

		[Fact]
		public void Should_HaveError_When_Email_Is_Empty()
		{
			// Arrange
			_fixture.Customize<Role>(c => c.FromFactory(() =>
			{
				var values = Enum.GetValues(typeof(Role)).Cast<Role>().ToList();
				values.Remove(Role.None);

				return values[_fixture.Create<int>() % values.Count];  // Random Role, except None
			}));

			var model = _fixture
				.Build<EmployeeRequest>()
				.With(x => x.Email, string.Empty)
				.Create();

			// Act
			var result = _validator.TestValidate(model);

			// Assert
			result.ShouldHaveValidationErrorFor(x => x.Email);
		}

		[Fact]
		public void Should_HaveError_When_AppliedPromocodesCount_Is_LessThan_Or_Equal_To_Zero()
		{
			// Arrange
			_fixture.Customize<Role>(c => c.FromFactory(() =>
			{
				var values = Enum.GetValues(typeof(Role)).Cast<Role>().ToList();
				values.Remove(Role.None);

				return values[_fixture.Create<int>() % values.Count];  // Random Role, except None
			}));

			var model = _fixture
				.Build<EmployeeRequest>()
				.With(x => x.AppliedPromocodesCount, 0)
				.Create();

			// Act
			var result = _validator.TestValidate(model);

			// Assert
			result.ShouldHaveValidationErrorFor(x => x.AppliedPromocodesCount);
		}

		[Fact]
		public void Should_Not_HaveError_When_Role_Is_Valid()
		{
			// Arrange
			var validRole = Enum.GetValues(typeof(Role))
				.Cast<Role>()
				.FirstOrDefault(r => r != Role.None);

			var model = _fixture
				.Build<EmployeeRequest>()
				.With(x => x.Role, validRole)
				.Create();

			// Act
			var result = _validator.TestValidate(model);

			// Assert
			result.ShouldNotHaveValidationErrorFor(x => x.Role);
		}

		[Fact]
		public void Should_HaveError_When_Role_Is_Invalid_And_Throws_ArgumentException()
		{
			// Arrange
			var model = _fixture
				.Build<EmployeeRequest>()
				.With(x => x.Role, Role.None)
				.Create();

			// Act & Assert
			var exception = Assert.Throws<ArgumentException>(() => _validator.Validate(model));
		}
	}
}
