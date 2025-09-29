using Construction.DomainModel;
using Construction.DomainModel.PaymentSchedule;
using Construction.Interface;
using Construction.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Construction.API.Controllers
{
    [Route("v1/[action]")]
    [ApiController]
    public class PaymentScheduleController : ControllerBase
    {
        private readonly IPaymentScheduleService _paymentScheduleService;

        public PaymentScheduleController(IPaymentScheduleService paymentScheduleService)
        {
            _paymentScheduleService = paymentScheduleService;
        }

        [HttpGet]
        [EnableCors("AllowOrigin")]
        [ActionName("PaymentSchedule")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public IActionResult GetPaymentSchedules(int projectId, int paymentScheduleId)
        {
            try
            {
                var result = _paymentScheduleService.GetPaymentSchedules(projectId, paymentScheduleId);
                return Ok(new
                {
                    Result = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    ResponseCode = "500",
                    ResponseMessage = "An error occurred while retrieving payment schedules.",
                    Error = ex.Message
                });
            }
        }



        [HttpPost]
        [EnableCors("AllowOrigin")]
        [ActionName("PaymentSchedule")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public IActionResult UpdatePaymentSchedule([FromBody] PaymentScheduleUpdateModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new
                    {
                        ResponseCode = 0,
                        ResponseMessage = "Invalid input data",
                        ResponseStatus = false
                    });
                }

                var response = _paymentScheduleService.UpdatePaymentSchedule(model);

                return Ok(new
                {
                    ResponseCode = response.ResponseCode,
                    ResponseMessage = response.ResponseMessage,
                    ResponseStatus = response.ResponseStatus,
                    PaymentScheduleId = response.ResponseID
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    ResponseCode = 0,
                    ResponseMessage = ex.Message,
                    ResponseStatus = false
                });
            }
        }
        [HttpDelete]
        [ActionName("PaymentSchedule")]
        [EnableCors]
        [ApiExplorerSettings(IgnoreApi = false)]

        public IActionResult DeletePaymentSchedule(int id_paymentSchedule)
        {
            try
            {
                var result = _paymentScheduleService.DeletePaymentSchedule(id_paymentSchedule);
                return Ok(new
                {
                    ResponseMessage = result.ResponseMessage,
                    Result = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    ResponseCode = "500",
                    ResponseMessage = "An error occurred while deleting the project.",
                    Error = ex.Message
                });
            }
        }

    }
}


