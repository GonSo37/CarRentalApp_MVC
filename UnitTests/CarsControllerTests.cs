using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CarRentalApp_MVC.Controllers;
using CarRentalApp_MVC.Models;
using CarRentalApp_MVC.Services;
using CarRentalApp_MVC.ViewModels;
using FluentValidation;
using MapsterMapper;
using CarRentalApp_MVC.Validators;

public class CarsControllerTests
{
    private readonly Mock<ICarService> _carServiceMock;
    private readonly Mock<CarViewModelValidator> _validatorMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CarsController _controller;

    public CarsControllerTests()
    {
        _carServiceMock = new Mock<ICarService>();
        _validatorMock = new Mock<CarViewModelValidator>(null);
        _mapperMock = new Mock<IMapper>();

        _controller = new CarsController(_carServiceMock.Object, _validatorMock.Object, _mapperMock.Object);
    }

    [Fact]
    public void Index_ReturnsViewWithCars()
    {
        var cars = new List<Car> { new Car { CarId = 1, Brand = "Toyota" } }.AsQueryable();
        var carViewModels = new List<CarViewModel> { new CarViewModel { Brand = "Toyota" } };

        _carServiceMock.Setup(s => s.GetAllCars()).Returns(cars);
        _mapperMock.Setup(m => m.Map<List<CarViewModel>>(cars)).Returns(carViewModels);

        var result = _controller.Index();

        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<List<CarViewModel>>(viewResult.Model);
        Assert.Single(model);
        Assert.Equal("Toyota", model[0].Brand);
    }

    [Fact]
    public void AddCar_GET_ReturnsView()
    {
        var result = _controller.AddCar();

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.IsType<CarViewModel>(viewResult.Model);
    }

    [Fact]
    public void EditCar_GET_ExistingCar_ReturnsViewWithModel()
    {
        int carId = 1;
        var car = new Car { CarId = carId, Brand = "BMW" };
        var viewModel = new CarViewModel { Brand = "BMW" };

        _carServiceMock.Setup(s => s.GetCarById(carId)).Returns(car);
        _mapperMock.Setup(m => m.Map<CarViewModel>(car)).Returns(viewModel);

        var result = _controller.EditCar(carId);

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal(viewModel, viewResult.Model);
    }

    [Fact]
    public void EditCar_GET_NonExistentCar_ReturnsNotFound()
    {
        _carServiceMock.Setup(s => s.GetCarById(It.IsAny<int>())).Returns((Car)null);

        var result = _controller.EditCar(999);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void DeleteCar_GET_ExistingCar_ReturnsView()
    {
        int carId = 1;
        var car = new Car { CarId = carId, Brand = "Audi" };
        var viewModel = new CarViewModel { Brand = "Audi" };

        _carServiceMock.Setup(s => s.GetCarById(carId)).Returns(car);
        _mapperMock.Setup(m => m.Map<CarViewModel>(car)).Returns(viewModel);

        var result = _controller.DeleteCar(carId);

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal(viewModel, viewResult.Model);
    }

    [Fact]
    public void DeleteCar_GET_InvalidId_RedirectsToIndex()
    {
        var result = _controller.DeleteCar(0);

        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
    }

    [Fact]
    public void DeleteCar_GET_NonExistentCar_ReturnsNotFound()
    {
 
        _carServiceMock.Setup(s => s.GetCarById(It.IsAny<int>())).Returns((Car)null);

        var result = _controller.DeleteCar(999);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Delete_POST_ExistingCar_DeletesAndRedirects()
    {
        int carId = 1;
        var car = new Car { CarId = carId };
        _carServiceMock.Setup(s => s.GetCarById(carId)).Returns(car);

        var result = _controller.Delete(carId);

        _carServiceMock.Verify(s => s.DeleteCar(carId), Times.Once);
        _carServiceMock.Verify(s => s.Save(), Times.Once);
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
    }

    [Fact]
    public void Delete_POST_NonExistentCar_ReturnsNotFound()
    {
        _carServiceMock.Setup(s => s.GetCarById(It.IsAny<int>())).Returns((Car)null);

        var result = _controller.Delete(999);

        Assert.IsType<NotFoundResult>(result);
        _carServiceMock.Verify(s => s.DeleteCar(It.IsAny<int>()), Times.Never);
        _carServiceMock.Verify(s => s.Save(), Times.Never);
    }
}
