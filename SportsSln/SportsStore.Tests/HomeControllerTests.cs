using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Xunit;
using SportsStore.Models;
using SportsStore.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace SportsStore.Tests;
public class HomeControllerTests
{
    [Fact]
    public void Can_Use_Repository()
    {
        //Arrange
        Mock<IStoreRepository> mock = new Mock<IStoreRepository>();

        mock
            .Setup(x => x.Products)
            .Returns((new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"}
            }).AsQueryable<Product>());

        var sut = new HomeController(mock.Object);

        //Act
        IEnumerable<Product>? result = (sut.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;


        //Assert
        Product[] prodArray = result?.ToArray() ?? Array.Empty<Product>();

        Assert.True(prodArray.Length == 2);
        Assert.Equal("P1", prodArray[0].Name);
        Assert.Equal("P2", prodArray[1].Name);
    }
}
