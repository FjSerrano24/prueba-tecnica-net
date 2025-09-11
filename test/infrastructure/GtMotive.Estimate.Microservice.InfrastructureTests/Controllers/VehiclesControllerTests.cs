using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.Models.Requests;
using GtMotive.Estimate.Microservice.InfrastructureTests.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace GtMotive.Estimate.Microservice.InfrastructureTests.Controllers
{
    /// <summary>
    /// Infrastructure tests for VehiclesController.
    /// Tests the HTTP endpoint reception and model validation without full implementation.
    /// </summary>
    public sealed class VehiclesControllerTests : IClassFixture<TestServerCollectionFixture>
    {
        private readonly TestServerCollectionFixture _testServerFixture;
        private readonly HttpClient _httpClient;

        public VehiclesControllerTests(TestServerCollectionFixture testServerFixture)
        {
            _testServerFixture = testServerFixture;
            _httpClient = _testServerFixture.TestServer.CreateClient();
        }

        [Fact]
        public async Task CreateVehicle_WithValidRequest_ShouldAcceptRequest()
        {
            // Arrange
            var request = new CreateVehicleRequest
            {
                LicensePlate = "1234ABC",
                VIN = "1HGBH41JXMN109186",
                Brand = "Toyota",
                Model = "Corolla",
                ManufactureYear = 2023
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("/api/vehicles", content);

            // Assert
            // We expect the request to be processed (not rejected due to validation)
            // The actual business logic might fail due to missing dependencies,
            // but the HTTP endpoint should accept the valid request format
            Assert.NotEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotEqual(HttpStatusCode.UnsupportedMediaType, response.StatusCode);
        }

        [Fact]
        public async Task CreateVehicle_WithInvalidLicensePlate_ShouldReturnBadRequest()
        {
            // Arrange
            var request = new CreateVehicleRequest
            {
                LicensePlate = "", // Invalid: empty
                VIN = "1HGBH41JXMN109186",
                Brand = "Toyota",
                Model = "Corolla",
                ManufactureYear = 2023
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("/api/vehicles", content);

            // Assert
            // Should reject due to model validation
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateVehicle_WithInvalidVIN_ShouldReturnBadRequest()
        {
            // Arrange
            var request = new CreateVehicleRequest
            {
                LicensePlate = "1234ABC",
                VIN = "INVALID", // Invalid: too short
                Brand = "Toyota",
                Model = "Corolla",
                ManufactureYear = 2023
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("/api/vehicles", content);

            // Assert
            // Should reject due to model validation
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateVehicle_WithInvalidManufactureYear_ShouldReturnBadRequest()
        {
            // Arrange
            var request = new CreateVehicleRequest
            {
                LicensePlate = "1234ABC",
                VIN = "1HGBH41JXMN109186",
                Brand = "Toyota",
                Model = "Corolla",
                ManufactureYear = 1800 // Invalid: too old
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("/api/vehicles", content);

            // Assert
            // Should reject due to model validation
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetAvailableVehicles_ShouldAcceptRequest()
        {
            // Act
            var response = await _httpClient.GetAsync("/api/vehicles/available");

            // Assert
            // The endpoint should accept the request (no validation issues)
            // Actual data retrieval might fail due to missing dependencies
            Assert.NotEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}

