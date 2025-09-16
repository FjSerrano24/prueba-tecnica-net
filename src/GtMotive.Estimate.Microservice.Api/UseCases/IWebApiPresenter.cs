using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases
{
    /// <summary>
    /// Web API Presenter interface.
    /// As described in README.md: "from the Web Api perspective the Controller only see the ViewModel property."
    /// This interface allows the controller to access the ActionResult after the presenter processes the output.
    /// </summary>
    public interface IWebApiPresenter
    {
        /// <summary>
        /// Gets the Action Result after the presenter processes the use case output.
        /// The controller returns this result to complete the HTTP response.
        /// </summary>
        IActionResult ActionResult { get; }
    }
}
