using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Validation;
using FluentAssertions;
using System;
using Xunit;

namespace CleanArchMvc.Domain.Tests
{
    public class ProductUnitTest1
    {
        [Fact]
        public void CreateProduct_WithValidparameters_ResultObjectValidState()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, "product image");
            action.Should().NotThrow<DomainExceptionValidation>();
        }

        [Fact]
        public void CreateProduct_NegativeIdValue_DomainExceptionValidation()
        {
            Action action = () => new Product(-1, "Product Name", "Product Description", 9.99m, 99, "product image");
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid Id value.");
        }

        [Fact]
        public void CreateProduct_ShortNameValue_DomainExceptionValidation()
        {
            Action action = () => new Product(1, "Pr", "Product Description", 9.99m, 99, "product image");
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid name. too short, minimum 3 characters");
        }

        [Fact]
        public void CreateProduct_LongImageName_DomainExceptionValidation()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, "product image tooooooooooooooooooooooooooooooooooooooooooooooo loooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooongggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggg");
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid image name, too long, maximum 250 characters");
        }

        [Fact]
        public void CreateProduct_WithNullImageName_NoDomainExceptionValidation()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, null);
            action.Should().NotThrow<DomainExceptionValidation>();
        }

        [Fact]
        public void CreateProduct_WithNullImageName_NoNullReferenceException()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, null);
            action.Should().NotThrow<NullReferenceException>();
        }

        [Fact]
        public void CreateProduct_WithEmptyImageName_NoDomainExceptionValidation()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, "");
            action.Should().NotThrow<DomainExceptionValidation>();
        }

        [Fact]
        public void CreateProduct_InvalidPriceValue_DomainExceptionValidation()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", -9.99m, 99, "Product image");
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid price value");
        }

        [Theory]
        [InlineData(-5)]
        public void CreateProduct_InvalidStockValue_DomainExceptionValidation(int value)
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, value, "Product image");
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid stock value");
        }
    }
}
