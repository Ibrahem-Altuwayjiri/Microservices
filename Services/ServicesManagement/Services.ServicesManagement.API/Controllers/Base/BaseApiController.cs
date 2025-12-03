using Microsoft.AspNetCore.Mvc;
using Services.ServicesManagement.Application.IService;
using Services.ServicesManagement.Application.Models.Abstract;
using System.Collections.Generic;

namespace Services.ServicesManagement.API.Controllers.Base
{
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        protected ResponseDto _response;

        public BaseApiController()
        {
            _response = new ResponseDto();
        }

        protected bool ValidateModel()
        {
            if (ModelState.IsValid)
                return true;

            _response.IsSuccess = false;
            var errorMessages = new List<string>();
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    errorMessages.Add(error.ErrorMessage);
                }
            }
            _response.Message = string.Join(", ", errorMessages);
            return false;
        }
    }
}
