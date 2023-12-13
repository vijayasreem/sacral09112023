
using Microsoft.AspNetCore.Mvc;
using sacral09112023.DTO;
using sacral09112023.Service;

namespace sacral09112023.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanApplicationController : ControllerBase
    {
        private readonly ILoanApplicationService _loanApplicationService;

        public LoanApplicationController(ILoanApplicationService loanApplicationService)
        {
            _loanApplicationService = loanApplicationService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Welcome to the loan application API.");
        }

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyDocument([FromBody] DocumentVerificationRequest request)
        {
            try
            {
                var verificationResult = await _loanApplicationService.VerifyDocument(request);
                if (verificationResult.IdentityVerified && verificationResult.AddressVerified)
                {
                    return Ok("Document verification successful. You are eligible for banking services.");
                }
                else
                {
                    return BadRequest("Document verification incomplete. You are not eligible for banking services.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred during document verification: {ex.Message}");
            }
        }

        [HttpPost("evaluate-credit")]
        public async Task<IActionResult> EvaluateCredit([FromBody] CreditEvaluationRequest request)
        {
            try
            {
                var evaluationResult = await _loanApplicationService.EvaluateCredit(request);
                if (evaluationResult.Income >= 30000 && evaluationResult.CreditScore >= 700)
                {
                    return Ok("Congratulations! You are eligible for a high credit limit.");
                }
                else if (evaluationResult.Income >= 20000 && evaluationResult.CreditScore >= 600)
                {
                    return Ok("You are eligible for a moderate credit limit.");
                }
                else
                {
                    return Ok("You are not eligible for a loan.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred during credit evaluation: {ex.Message}");
            }
        }

        [HttpPost("disburse")]
        public async Task<IActionResult> DisburseLoan([FromBody] LoanDisbursementRequest request)
        {
            try
            {
                var disbursementResult = await _loanApplicationService.DisburseLoan(request);
                if (disbursementResult.Amount <= disbursementResult.VehicleAssessmentValue)
                {
                    return Ok($"Vehicle assessment passed. Disbursed amount: {disbursementResult.Amount}");
                }
                else
                {
                    return BadRequest("Vehicle assessment failed. Loan amount cannot exceed the vehicle value.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred during loan disbursement: {ex.Message}");
            }
        }

        [HttpPost("approve-payment")]
        public async Task<IActionResult> ApprovePayment([FromBody] PaymentApprovalRequest request)
        {
            try
            {
                var approvalResult = await _loanApplicationService.ApprovePayment(request);
                if (approvalResult.PaymentAmount <= 1000.0)
                {
                    return Ok("Payment approved.");
                }
                else
                {
                    return Ok("Payment approval required. Please provide approval.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred during payment approval: {ex.Message}");
            }
        }
    }
}
