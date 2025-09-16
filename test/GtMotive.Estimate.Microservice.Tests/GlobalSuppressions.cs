// <copyright file="GlobalSuppressions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using GtMotive.Estimate.Microservice.Tests;
using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Usage", "CA2234:Pass system uri objects instead of strings", Justification = "Test", Scope = "member", Target = "~M:GtMotive.Estimate.Microservice.Tests.Infrastructure.VehiclesControllerInfrastructureTests.CreateVehicleWithInvalidContentTypeShouldValidateRequest~System.Threading.Tasks.Task")]
[assembly: SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "Test", Scope = "member", Target = "~M:GtMotive.Estimate.Microservice.Tests.Infrastructure.VehiclesControllerInfrastructureTests.CreateVehicleWithInvalidContentTypeShouldValidateRequest~System.Threading.Tasks.Task")]
[assembly: SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1507:Code should not contain multiple blank lines in a row", Justification = "Test", Scope = "member", Target = "~M:GtMotive.Estimate.Microservice.Tests.Infrastructure.TestWebApplicationFactory`1.#ctor(System.Action{Microsoft.Extensions.DependencyInjection.IServiceCollection})")]
[assembly: SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1517:Code should not contain blank lines at start of file", Justification = "test")]
[assembly: SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1200:Using directives should be placed correctly", Justification = "test")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1633:File should have header", Justification = "test")]
