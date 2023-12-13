namespace sacral09112023
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public decimal AnnualIncome { get; set; }
        public int CreditScore { get; set; }
        public decimal DisbursedAmount { get; set; }
        public decimal VehicleAssessmentValue { get; set; }
        public decimal PaymentAmount { get; set; }
        public string VendorName { get; set; }
        public bool FundsAvailability { get; set; }
        public bool PaymentApproval { get; set; }
    }
}