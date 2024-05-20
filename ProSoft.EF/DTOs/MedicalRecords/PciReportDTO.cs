using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.MedicalRecords
{
    public class PciReportDTO
    {
        public string? PatName { get; set; }
        public int SerialHistory { get; set; }
        public string? PatSsn { get; set; }
        public DateTime? EntryDate { get; set; }
        public string? FirstOperator { get; set; }
        public string? SecondOperator { get; set; }
        public string? TypeOfAdmission { get; set; }
        public string? RepurtusionStrategy { get; set; }
        public string? AmountOfContrastPciMl { get; set; }
        public string? PciAccess { get; set; }
        public double? IntraAorticBalloon { get; set; }
        public double? AdenosineLc { get; set; }
        public double? GpInhibitosLc { get; set; }
        public double? AdrenalineLc { get; set; }
        public double? HeparinLc { get; set; }
        public double? TridilLc { get; set; }
        public double? CcbLc { get; set; }
        public double? HematomaPci { get; set; }
        public double? CinPci { get; set; }
        public double? PerforationPci { get; set; }
        public double? DisectionPci { get; set; }
        public double? NoReflow { get; set; }
        public string? OutcomeOfNoReflow { get; set; }
        public string? MethodOfNoReflowManagement { get; set; }
        public double? DeathPci { get; set; }
        public string? StepOneOfPci { get; set; }
        public string? Step2Pci { get; set; }
        public string? Step3Pci { get; set; }
        public string? Step4Pci { get; set; }
        public string? Step5Pci { get; set; }
        public string? Step6Pci { get; set; }
        public string? Step7Pci { get; set; }
        public string? Step8Pci { get; set; }
        public string? DiagnosisOfPci { get; set; }
        public string? RecomendationPci { get; set; }
        public string? NoOfDiseasedVesselS50 { get; set; }
        public string? VesselOfRevascularization { get; set; }
        public string? Culprit { get; set; }
        public double? TreatmentOfNonculprit { get; set; }
        public string? TimiFlowPre { get; set; }
        public string? LesionConfiguration { get; set; }
        public double? BifurcationalLesion { get; set; }
        public double? TrifurcationalLesion { get; set; }
        public double? CalcificLesion { get; set; }
        public double? Cto { get; set; }
        public double? TortiousVessel { get; set; }
        public string? TimiFlowPost { get; set; }
        public string? TypeOfWire { get; set; }
        public string? HeavnessOfWire { get; set; }
        public double? BuddyWire { get; set; }
        public double? FailedWiring { get; set; }
        public double? ThrombusAspiration { get; set; }
        public double? AnchorBalloon { get; set; }
        public string? Predilatation { get; set; }
        public string? MaxPressureOfBalloon { get; set; }
        public double? Postdilatation { get; set; }
        public string? MaxPressureOfPostBalloon { get; set; }
        public string? DiameterOfPredilatation { get; set; }
        public string? DiameterOfPostdilatationBal { get; set; }
        public string? TypeStent { get; set; }
        public string? DiameterOfStent { get; set; }
        public string? LenghtOfStent { get; set; }
        public string? DiameterOf2ndStent { get; set; }
        public string? LenghtOf2ndStent { get; set; }
        public double? OverlappInMainVessel { get; set; }
        public string? BifurcationalTechnique { get; set; }
        public string? Type2ndStent { get; set; }
        public string? Type3rdStent { get; set; }
        public string? DiameterOf3rdStent { get; set; }
        public string? LenghtOf3rdStent { get; set; }
        public string? Type4thStent { get; set; }
        public string? DiameterOf4thStent { get; set; }
        public string? LenghtOf4thStent { get; set; }
        public string? Type5thStent { get; set; }
        public string? DiameterOf5thStent { get; set; }
        public string? LenghtOf5thStent { get; set; }
        public string? Type6thStent { get; set; }
        public string? DiameterOf6thStent { get; set; }
        public string? LenghtOf6thStent { get; set; }
        public int? PatId { get; set; }
        public int? Serial { get; set; }
    }
}
