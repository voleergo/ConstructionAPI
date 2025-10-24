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
    [EnableCors("ProductionPolicy")]
    public class PaymentScheduleController : ControllerBase
    {
        private readonly IPaymentScheduleService _paymentScheduleService;

        public PaymentScheduleController(IPaymentScheduleService paymentScheduleService)
        {
            _paymentScheduleService = paymentScheduleService;
        }

        [HttpGet]
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

        [HttpGet]
        [ActionName("Reminders")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public IActionResult GetUpcomingPaymentReminders(int fkUser)
        {
            try
            {
                var result = _paymentScheduleService.GetUpcomingPaymentReminders(fkUser);

                return Ok(new
                {
                    ResponseCode = 1,
                    ResponseMessage = result.Count > 0
                        ? "Upcoming payment reminders fetched successfully."
                        : "No upcoming payment reminders found.",
                    ResponseStatus = true,
                    Result = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    ResponseCode = 0,
                    ResponseMessage = "An error occurred while fetching payment reminders.",
                    ResponseStatus = false,
                    Error = ex.Message
                });
            }
        }
        [HttpPost]
        [ActionName("Payment")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public IActionResult UpdatePayment([FromBody] PaymentModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new { ResponseCode = 0, ResponseMessage = "Invalid input data", ResponseStatus = false });
                }

                var response = _paymentScheduleService.UpdatePayment(model);

                return Ok(new
                {
                    ResponseCode = response.ResponseCode,
                    ResponseMessage = response.ResponseMessage,
                    ResponseStatus = response.ResponseStatus,
                    PaymentId = response.ResponseID
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    ResponseCode = "500",
                    ResponseMessage = "An error occurred while updating payment.",
                    Error = ex.Message
                });
            }
        }

        [HttpGet]
        [ActionName("Payment")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public IActionResult GetPayments(int projectId = 0, int paymentScheduleId = 0)
        {
            try
            {
                var result = _paymentScheduleService.GetPayments(projectId, paymentScheduleId);
                if (result == null || result.Count == 0)
                    return Ok(new { ResponseCode = 0, ResponseMessage = "No payment records found.", Result = result });

                return Ok(new
                {
                    ResponseCode = 1,
                    ResponseMessage = "Payment details retrieved successfully.",
                    Result = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    ResponseCode = "500",
                    ResponseMessage = "An error occurred while retrieving payments.",
                    Error = ex.Message
                });
            }
        }

        [HttpGet]
        [ActionName("Invoice")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public IActionResult GetInvoices([FromQuery] int invoiceId = 0, [FromQuery] int projectId = 0, [FromQuery] int customerId = 0)
        {
            try
            {
                var invoices = _paymentScheduleService.GetInvoices(invoiceId, projectId, customerId);
                return Ok(new { Success = true, Data = invoices, Message = "Invoices retrieved successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = $"Error retrieving invoices: {ex.Message}" });
            }
        }
    }
}


