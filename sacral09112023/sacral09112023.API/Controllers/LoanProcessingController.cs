
using Microsoft.AspNetCore.Mvc;
using sacral09112023.DTO;
using sacral09112023.Service;

namespace sacral09112023.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanProcessingController : ControllerBase
    {
        private readonly ILoanProcessingService _loanProcessingService;

        public LoanProcessingController(ILoanProcessingService loanProcessingService)
        {
            _loanProcessingService = loanProcessingService;
        }

        [HttpGet]
        public IActionResult GetWelcomeMessage()
        {
            return Ok("Welcome to the Loan Processing API!");
        }

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyDocuments([FromBody] DocumentVerificationRequest request)
        {
            var result = await _loanProcessingService.VerifyDocumentsAsync(request);

            if (result.IdentityVerified && result.AddressVerified)
            {
                return Ok("Document verification successful. You are eligible for banking services.");
            }

            return BadRequest("Document verification incomplete. Not eligible for banking services.");
        }

        [HttpPost("evaluate-credit")]
        public async Task<IActionResult> EvaluateCredit([FromBody] CreditEvaluationRequest request)
        {
            var result = await _loanProcessingService.EvaluateCreditAsync(request);

            if (result.Income >= 30000 && result.CreditScore >= 700)
            {
                return Ok("Congratulations! You are eligible for a high credit limit.");
            }
            else if (result.Income >= 20000 && result.CreditScore >= 600)
            {
                return Ok("You are eligible for a moderate credit limit.");
            }

            return BadRequest("You are not eligible for a loan at this time.");
        }

        [HttpPost("disburse")]
        public async Task<IActionResult> DisburseLoan([FromBody] DisbursementRequest request)
        {
            var result = await _loanProcessingService.DisburseLoanAsync(request);

            if (result.DisbursedAmount <= result.VehicleAssessmentValue)
            {
                return Ok($"Vehicle assessment passed. Disbursed amount: {result.DisbursedAmount}");
            }

            return BadRequest("Vehicle assessment failed. Loan amount cannot exceed vehicle value.");
        }

        [HttpPost("approve-payment")]
        public async Task<IActionResult> ApprovePayment([FromBody] PaymentApprovalRequest request)
        {
            var result = await _loanProcessingService.ApprovePaymentAsync(request);

            if (result.VendorVerificationStatus == VendorVerificationStatus.Valid &&
                result.FundsAvailabilityStatus == FundsAvailabilityStatus.Available &&
                result.PaymentApprovalStatus == PaymentApprovalStatus.Approved)
            {
                return Ok($"Payment disbursement successful. Vendor: {result.VendorName}, Amount: {result.PaymentAmount}");
            }
            else if (result.VendorVerificationStatus == VendorVerificationStatus.Invalid)
            {
                return BadRequest("Vendor information is invalid.");
            }
            else if (result.FundsAvailabilityStatus == FundsAvailabilityStatus.InsufficientFunds)
            {
                return BadRequest("Insufficient funds for disbursement.");
            }
            else if (result.PaymentApprovalStatus == PaymentApprovalStatus.RequiresApproval)
            {
                return BadRequest("Payment approval required.");
            }

            return BadRequest("Payment disbursement failed.");
        }
    }
}
